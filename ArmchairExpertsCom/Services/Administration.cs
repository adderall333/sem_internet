using System;
using System.Linq;
using ArmchairExpertsCom.Models;
using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace ArmchairExpertsCom.Services
{
    public static class Administration
    {
        public static bool IsAdmin(string authKey)
        {
            return Repository.Get<User>(user => user.PasswordKey == authKey).Role == "admin";
        }

        public static void EditModel(string typeName, int id, IFormCollection changes)
        {
            var model = Repository
                .GetModelsByTypeName(typeName, true)
                .First(m => m.Id == id);

            var isChanged = false;
            foreach (var property in IModel.GetBasicProperties(model).Where(p => p.Name != "Id"))
            {
                if (!changes.ContainsKey(property.Name)) continue;
                isChanged = true;
                property.SetValue(model, property.PropertyType == typeof(int) 
                    ? (object) int.Parse(changes[property.Name].ToString())
                    : changes[property.Name].ToString());
            }

            if (!isChanged) return;
            
            model.Save();
            Repository.SaveChanges();
        }

        public static IModel CreateModel(string typeName, IFormCollection values)
        {
            var model = (IModel) Activator.CreateInstance(Repository.GetType(typeName));
            
            foreach (var property in IModel.GetBasicProperties(model).Where(p => p.Name != "Id"))
            {
                if (!values.ContainsKey(property.Name)) 
                    return null;
                property.SetValue(model, property.PropertyType == typeof(int) 
                    ? (object) int.Parse(values[property.Name].ToString())
                    : values[property.Name].ToString());
            }
            
            model?.Save();
            Repository.SaveChanges();

            return model;
        }
    }
}