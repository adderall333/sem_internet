using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArmchairExpertsCom.Pages.Models.Interfaces;
using Npgsql;

namespace ArmchairExpertsCom.Pages.Models.Utilities
{
    public static class ORM
    {
        public static void Insert(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var properties = model.GetBasicProperties(); 
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

            var query = $"insert into {model.GetType().Name} ({names}) values ({values})";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void Update(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var changes = model
                .GetBasicProperties()
                .Skip(1)
                .Select(p => p.Name + " = " + p.GetValue(model).AddQuotationMarks())
                .ToArray()
                .PlaceCommas();
            
            var query = $"update {model.GetType().Name} set {changes} where id = {model.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }
        
        public static void Delete(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"delete from {model.GetType().Name} where id = {model.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void AddRelation(IModel model1, IModel model2)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"insert into {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                        $"({model1.GetType().Name}_id, {model2.GetType().Name}_id) " +
                        $"values ({model1.Id}, {model2.Id})";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void DeleteRelation(IModel model1, IModel model2)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"delete from {GetStagingTableName(model1.GetType(), model2.GetType())} " +
                              $"where {model1.GetType().Name}_id = {model1.Id} and " +
                              $"{model2.GetType().Name}_id = {model2.Id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static IEnumerable<IModel> Select(Type type)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select * from {type.Name}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();

            while (reader.Read())
            {
                yield return FillIn(type, reader);
            }
        }
        
        public static IEnumerable<int> GetRelations(IModel model, Type type)
        {
            var relations = new List<int>();
            var reader = model.GetForeignReader(type);

            var i = reader.GetName(0).StartsWith(type.Name.ToLower()) ? 0 : 1;
            while (reader.Read())
            {
                relations.Add(reader.GetInt32(i));
            }

            return relations;
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
            var cs = "Host=localhost;Username=postgres;Password=qweasd123;Database=armchair_experts";
            return new NpgsqlConnection(cs);
        }

        private static PropertyInfo[] GetBasicProperties(this IModel model)
        {
            var type = model.GetType();
            var properties = type
                .GetProperties()
                .Where(p => !p.GetCustomAttributes(typeof(MetaDataAttribute), false).Any())
                .Where(p => !p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any())
                .ToArray();
            return properties;
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
        
        private static NpgsqlDataReader GetForeignReader(this IModel model, Type type2)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select * from {GetStagingTableName(model.GetType(), type2)} " +
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