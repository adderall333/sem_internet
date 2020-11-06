using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public static class ORM
    {
        public static void Insert(IModel model)
        {
            var connection = GetConnection();
            connection.Open();

            var properties = model.GetProperties(); 
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
                .GetProperties()
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
            var cs = "Host=localhost;Username=postgres;Password={{pass}};Database={{dbname}}";
            return new NpgsqlConnection(cs);
        }

        private static PropertyInfo[] GetProperties(this IModel model)
        {
            var type = model.GetType();
            var properties = type
                .GetProperties()
                .Where(p => !p.Name.StartsWith("_"))
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
    }
}