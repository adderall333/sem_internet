using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public static class ObjectsGetter
    {
        private static NpgsqlConnection GetConnection()
        {
            var cs = "Host=localhost;Username=postgres;Password={{pass}};Database={{dbname}}";
            return new NpgsqlConnection(cs);
        }

        private static IModel FillIn<T>(NpgsqlDataReader reader)
            where T : IModel
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var i = 0;
            while (reader.Read())
            {
                properties[i].SetValue(instance, reader.GetValue(i));
            }
            return instance;
        }

        public static int LastId(string type)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select max(id) from {type}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
        
        public static IEnumerable<IModel> All<T>()
            where T : IModel
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select * from {typeof(T)}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();

            while (reader.Read())
            {
                yield return FillIn<T>(reader);
            }
        }
        
        public static IEnumerable<IModel> Filter<T>(params string[] conditions)
            where T : IModel
        {
            var connection = GetConnection();
            connection.Open();

            var query = new StringBuilder($"select * from {typeof(T)} where {conditions.First()} ");
            foreach (var condition in conditions.Skip(1))
            {
                query.Append($"and {condition} ");
            }
            var reader = new NpgsqlCommand(query.ToString(), connection).ExecuteReader();

            while (reader.Read())
            {
                yield return FillIn<T>(reader);;
            }
        }

        public static void Update<T>(string queryPart, int id)
            where T : IModel
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"update {typeof(T)} set {queryPart} where id = {id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void Insert<T>(string names, string values)
            where T : IModel
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"insert into {typeof(T)} ({names}) values ({values})";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void Delete<T>(int id)
            where T : IModel
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"delete from {typeof(T)} where id = {id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }
    }
}