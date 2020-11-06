using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArmchairExpertsCom.Pages
{
    public static class Repository
    {
        private static readonly Type[] Types = 
        {
            typeof(Book),
            //todo
        };
        private static Dictionary<Type, List<IModel>> Data { get; set; } 
            = new Dictionary<Type, List<IModel>>();

        public static bool Contains<T>(T model)
        {
            return Data[typeof(T)].Contains((IModel)model);
        }
        
        public static void Add<T>(T model)
            where T : IModel
        {
            Data[typeof(T)].Add(model);
        }
        
        public static IEnumerable<T> All<T>()
        {
            return Data[typeof(T)].Select(e => (T)e);
        }
        
        public static IEnumerable<T> Filter<T>(Func<T, bool> condition)
        {
            return Data[typeof(T)].Select(e => (T) e).Where(condition);
        }

        public static T Get<T>(Func<T, bool> condition)
        {
            var satisfying = Data[typeof(T)].Select(e => (T) e).Where(condition).ToList();
            if (satisfying.Count != 1)
                throw new ArgumentException();
            return satisfying.First();
        }

        public static void SaveChanges()
        {
            foreach (var model in Data.SelectMany(kvp => kvp.Value))
            {
                if (model._isNew)
                    ORM.Insert(model);
                
                if (model._isChanged)
                    ORM.Update(model);
                
                if (model._isDeleted)
                    ORM.Delete(model);
            }
        }

        private static void LoadData()
        {
            foreach (var type in Types)
            {
                Data[type] = ORM.Select(type).ToList();
            }
        }
    }
}