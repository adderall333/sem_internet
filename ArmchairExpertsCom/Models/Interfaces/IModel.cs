using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using ArmchairExpertsCom.Models.Utilities;
using Npgsql;

namespace ArmchairExpertsCom.Models.Interfaces
{
    public interface IModel
    {
        public bool IsNew { get; set; }
        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }
        
        public int Id { get; set; }

        public void Save();
        public void Delete();
        
        public static PropertyInfo[] GetBasicProperties(IModel model)
        {
            return model
                .GetType()
                .GetProperties()
                .Where(p => !p.GetCustomAttributes(typeof(MetaDataAttribute), false).Any())
                .Where(p => !p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any())
                .ToArray();
        }
    }
}