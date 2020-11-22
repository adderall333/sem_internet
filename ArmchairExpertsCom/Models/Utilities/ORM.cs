using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArmchairExpertsCom.Models.Interfaces;
using Npgsql;

namespace ArmchairExpertsCom.Models.Utilities
{
    public static class ORM
    { 
        private const string ConnectionString =
            "Host=localhost;Username=postgres;Password=qweasd123;Database=armchair_experts";
        
        public static void Insert(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var properties = IModel.GetBasicProperties(model); 
            var names = properties
                .Skip(1)
                .Select(p => p.Name)
                .ToArray()
                .PlaceCommas();
            var values = properties
                .Skip(1)
                .Select(p => p.GetValue(model).AddQuotationMarks())
                .ToArray()
                .PlaceCommas();

            var query = $"insert into {GetTableName(model)} ({names}) values ({values}) returning id";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();
            if (reader.Read())
                model.Id = reader.GetInt32(0);
            connection.Close();
        }

        public static void Update(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var changes = IModel
                .GetBasicProperties(model)
                .Skip(1)
                .Select(p => p.Name + " = " + p.GetValue(model).AddQuotationMarks())
                .ToArray()
                .PlaceCommas();
            
            var query = $"update {GetTableName(model)} set {changes} where id = {model.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
            connection.Close();
        }
        
        public static void Delete(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"delete from {GetTableName(model)} where id = {model.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
            connection.Close();
        }

        public static void AddRelation(IModel model1, IModel model2)
        {
            var connection = GetConnection();
            connection.Open();

            var query = "";
            if (model1.GetType() == typeof(User) && model2.GetType() == typeof(User))
                query = $"insert into {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                        $"(subscriber_id, subscribe_id) " +
                        $"values ({model1.Id}, {model2.Id})";
            else
                query = $"insert into {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                        $"({model1.GetType().Name}_id, {model2.GetType().Name}_id) " +
                        $"values ({model1.Id}, {model2.Id})";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteRelation(IModel model1, IModel model2)
        {
            var connection = GetConnection();
            connection.Open();

            var query = "";
            if (model1.GetType() == typeof(User) && model2.GetType() == typeof(User))
                query = $"delete from {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                        $"subscriber_id = {model1.Id} and " +
                        $"subscribe_id = {model2.Id}";
            else
                query = $"delete from {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                        $"where {model1.GetType().Name}_id = {model1.Id} and " +
                        $"{model2.GetType().Name}_id = {model2.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
            connection.Close();
        }

        public static IEnumerable<IModel> Select(Type type)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select * from {GetTableName(type)}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();

            while (reader.Read())
            {
                yield return FillIn(type, reader);
            }
            connection.Close();
        }
        
        public static IEnumerable<int> GetRelations(IModel model, Type type)
        {
            var relations = new List<int>();
            var reader = model.GetForeignReader(type);

            var i = reader.GetName(1).StartsWith(type.Name.ToLower()) || 
                    model.GetType() == typeof(User) && type == typeof(User) ? 1 : 0;
            while (reader.Read())
            {
                relations.Add(reader.GetInt32(i));
            }

            return relations;
        }

        private static string GetTableName(IModel model)
        {
            return model is User ? "\"user\"" : model.GetType().Name;
        }
        
        private static string GetTableName(Type type)
        {
            return type == typeof(User) ? "\"user\"" : type.Name;
        }
        
        private static IModel FillIn(Type type, NpgsqlDataReader reader)
        {
            var instance = (IModel)Activator.CreateInstance(type);
            
            var i = 0;
            while (i < reader.FieldCount)
            {
                type.GetProperty(reader.GetName(i), BindingFlags.IgnoreCase |  
                                                    BindingFlags.Public | 
                                                    BindingFlags.Instance)?
                    .SetValue(instance, reader.GetValue(i));
                i++;
            }

            return instance;
        }
        
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        private static string PlaceCommas(this string[] parts)
        {
            var stringBuilder = new StringBuilder();
            var i = 0;
            foreach (var part in parts)
            {
                i++;
                stringBuilder.Append(part);
                if (i < parts.Length)
                    stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }
        
        private static string AddQuotationMarks(this object obj)
        {
            if (obj is string str)
                return "'" + str + "'";
            return obj.ToString();
        }
        
        private static NpgsqlDataReader GetForeignReader(this IModel model, Type type)
        {
            var connection = GetConnection();
            connection.Open();

            var query = "";
            if (model.GetType() == typeof(User) && type == typeof(User))
                query = $"select * from {GetStagingTableName(model.GetType(), type)} " +
                        $"where subscriber_id = {model.Id}";
            else
                 query = $"select * from {GetStagingTableName(model.GetType(), type)} " +
                         $"where {model.GetType().Name}_id = {model.Id}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();
            return reader;
        }

        private static string GetStagingTableName(Type type1, Type type2)
        {
            return type1.Name.IsEarlierThan(type2.Name)
                ? $"{type2.Name}_{type1.Name}"
                : $"{type1.Name}_{type2.Name}";
        }
        
        private static bool IsEarlierThan(this string str1, string str2)
        {
            return String.Compare(str1, str2, StringComparison.Ordinal) > 0;
        }
    }
}