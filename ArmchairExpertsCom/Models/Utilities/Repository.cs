using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ArmchairExpertsCom.Models.Interfaces;

namespace ArmchairExpertsCom.Models.Utilities
{
    public static class Repository
    {
        private static readonly Type[] UserCreatedTypes = 
        {
            typeof(Comment),
            typeof(BookEvaluation),
            typeof(FilmEvaluation),
            typeof(SerialEvaluation),
            typeof(BookReview),
            typeof(FilmReview),
            typeof(SerialReview),
            typeof(Selection)
        };
        
        private static readonly Type[] AdminCreatedTypes = 
        {
            typeof(Book),
            typeof(Film),
            typeof(BookGenre),
            typeof(FilmGenre),
            typeof(SerialGenre),
            typeof(Image),
            typeof(Serial),
            typeof(User)
        };

        private static Type[] Types => UserCreatedTypes.Concat(AdminCreatedTypes).ToArray();
        
        private static Dictionary<Type, List<IModel>> Data { get; set; } 
            = new Dictionary<Type, List<IModel>>();

        public static bool IsLoaded { get; private set; }

        public static Type GetType(string typeName)
        {
            return Types.First(t => t.Name.ToLower() == typeName);
        }
        
        public static bool Contains<T>(T model)
        {
            if (Data.ContainsKey(typeof(T))) return Data[typeof(T)].Contains((IModel) model);
            Data[typeof(T)] = new List<IModel>();
            return false;
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
            where T : class
        {
            var satisfying = Data[typeof(T)].Select(e => (T) e).Where(condition).ToList();
            if (satisfying.Count > 1)
                throw new ArgumentException();
            return satisfying.Count < 1 ? null : satisfying.First();
        }

        public static void SaveChanges()
        {
            var models = Data.Values.SelectMany(e => e).ToList();
            
            foreach (var model in models)
            {
                if (model.IsNew)
                {
                    ORM.Insert(model);
                    model.IsNew = false;
                }

                if (model.IsChanged)
                {
                    ORM.Update(model);
                    model.IsChanged = false;
                }

                if (model.IsDeleted)
                {
                    ORM.Delete(model);
                }
            }

            var dbSets = models.SelectMany(m => m.GetType()
                .GetRelationProperties()
                .Select(p => (DbSet) p.GetValue(m)));
            
            foreach (var dbSet in dbSets)
            {
                if (dbSet == null)
                    continue;
                
                foreach (var model in dbSet.NewModels)
                    ORM.AddRelation(dbSet.Parent, model);

                foreach (var model in dbSet.RemovedModels)
                    ORM.DeleteRelation(dbSet.Parent, model);
            }
        }

        public static void LoadDataAndRelations()
        {
            if (IsLoaded)
                return;
            LoadData();
            LoadRelations();
            IsLoaded = true;
        }

        public static void Refresh()
        {
            LoadData();
            LoadRelations();
            IsLoaded = true;
        }

        public static Dictionary<Type, List<IModel>> GetAdminCreatedData()
        {
            LoadDataAndRelations();
            var data = new Dictionary<Type, List<IModel>>();
            foreach (var type in AdminCreatedTypes)
            {
                data[type] = Data[type];
            }
            return data;
        }

        public static List<IModel> GetModelsByTypeName(string typeName, bool isAdminCreated)
        {
            var type = isAdminCreated 
                ? AdminCreatedTypes.First(t => t.Name.ToLower() == typeName)
                : Types.First(t => t.Name.ToLower() == typeName);
            return Data[type];
        }
        
        public static List<IModel> GetModelsByType(Type currentType, bool isAdminCreated)
        {
            var type = isAdminCreated 
                ? AdminCreatedTypes.First(t => t == currentType)
                : Types.First(t => t == currentType);
            return Data[type];
        }
        
        private static void LoadData()
        {
            foreach (var type in Types)
            {
                Data[type] = ORM.Select(type).ToList();
            }
        }

        private static void LoadRelations()
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