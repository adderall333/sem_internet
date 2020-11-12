using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ArmchairExpertsCom.Pages.Models.Interfaces;

namespace ArmchairExpertsCom.Pages.Models.Utilities
{
    public static class Repository
    {
        private static readonly Type[] Types = 
        {
            typeof(Book),
            /*typeof(Comment),
            typeof(Evaluation),
            typeof(Film),*/
            typeof(BookGenre),
            typeof(Image),
            /*typeof(Recommendation),
            typeof(Review),
            typeof(Selection),
            typeof(Serial),
            typeof(User)*/
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
            var models = Data.Values.SelectMany(e => e).ToList();
            
            foreach (var model in models)
            {
                if (model.IsNew)
                    ORM.Insert(model);
                
                if (model.IsChanged)
                    ORM.Update(model);
                
                if (model.IsDeleted)
                    ORM.Delete(model);
            }

            var dbSets = models.SelectMany(m => m.GetType()
                .GetRelationProperties()
                .Select(p => (DbSet) p.GetValue(m)));
            
            foreach (var dbSet in dbSets)
            {
                foreach (var model in dbSet.NewModels)
                    ORM.AddRelation(dbSet.Parent, model);

                foreach (var model in dbSet.RemovedModels)
                {
                    ORM.DeleteRelation(dbSet.Parent, model);
                }
            }
        }

        public static void LoadData()
        {
            foreach (var type in Types)
            {
                Data[type] = ORM.Select(type).ToList();
            }
        }

        public static void LoadRelations()
        {
            foreach (var model in Data.Values.SelectMany(e => e))
            {
                var properties = model.GetType().GetRelationProperties();
                foreach (var property in properties)
                {
                    model.FillRelations(property);
                }
            }
        }

        private static void FillRelations(this IModel model, PropertyInfo property)
        {
            var attribute = (ForeignKeyAttribute) property
                .GetCustomAttributes(typeof(ForeignKeyAttribute))
                .First();
            var relations = ORM.GetRelations(model, attribute.Type);
            property.SetValue(model, new DbSet(model, Data[attribute.Type]
                    .Where(m => relations.Contains(m.Id))));
        }

        private static IEnumerable<PropertyInfo> GetRelationProperties(this Type type)
        {
            return type
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any());
        }
    }
}