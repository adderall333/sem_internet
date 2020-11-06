using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public interface IModel
    {
        public int Id { get; set; }
        
        //metadata
        public bool _isNew { get; set; }
        public bool _isChanged { get; set; }
        public bool _isDeleted { get; set; }

        public void Save()
        {
            if (!Repository.Contains(this))
            {
                Repository.Add(this);
                _isNew = true;
            }
            else
                _isChanged = true;
        }
        
        public void Delete()
        {
            _isDeleted = true;
        }
        
        public static IEnumerable<T> All<T>()
        {
            return Repository.All<T>();
        }

        public static IEnumerable<T> Filter<T>(Func<T, bool> condition)
        {
            return Repository.Filter<T>(condition);
        }
        
        public static T Get<T>(Func<T, bool> condition)
        {
            return Repository.Get<T>(condition);
        }
    }
}