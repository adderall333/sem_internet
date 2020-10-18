using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace ArmchairExpertsCom.Models
{
    public static class ObjectsGetter
    {
        public static NpgsqlConnection GetConnection()
        {
            var cs = "Host=localhost;Username=postgres;Password={{pass}};Database={{dbname}}";
            return new NpgsqlConnection(cs);
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
        
        public static IEnumerable<IModel> All(string type)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"select * from {type}";
            var reader = new NpgsqlCommand(query, connection).ExecuteReader();

            while (reader.Read())
            {
                var instance = (IModel)new object();
                instance.FillIn(reader);
                yield return instance;
            }
        }
        
        public static IEnumerable<IModel> Filter(string type, params string[] conditions)
        {
            var connection = GetConnection();
            connection.Open();

            var query = new StringBuilder($"select * from {type} where {conditions.First()} ");
            foreach (var condition in conditions.Skip(1))
            {
                query.Append($"and {condition} ");
            }
            var reader = new NpgsqlCommand(query.ToString(), connection).ExecuteReader();

            while (reader.Read())
            {
                var instance = (IModel)new object();
                instance.FillIn(reader);
                yield return instance;
            }
        }

        public static void Update(string type, string queryPart, int id)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"update {type} set {queryPart} where id = {id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void Insert(string type, string names, string values)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"insert into {type} ({names}) values ({values})";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }

        public static void Delete(string type, int id)
        {
            var connection = GetConnection();
            connection.Open();

            var query = $"delete from {type} where id = {id}";
            new NpgsqlCommand(query, connection).ExecuteNonQuery();
        }
    }
}