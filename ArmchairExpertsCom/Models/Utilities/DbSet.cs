using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models.Interfaces;

namespace ArmchairExpertsCom.Models.Utilities
{
    public class DbSet : IEnumerable<IModel>
    {
        private readonly List<IModel> modelsList;
        
        public IModel Parent { get; }
        
        public List<IModel> NewModels { get; }
        public List<IModel> RemovedModels { get; }

        public DbSet(IModel parent)
        {
            Parent = parent;
            modelsList = new List<IModel>();
            NewModels = new List<IModel>();
            RemovedModels = new List<IModel>();
        }
        
        public DbSet(IModel parent, IEnumerable<IModel> models)
        {
            Parent = parent;
            modelsList = new List<IModel>();
            NewModels = new List<IModel>();
            foreach (var model in models)
            {
                Add(model);
            }
            RemovedModels = new List<IModel>();
        }
        
        public DbSet()
        {
            modelsList = new List<IModel>();
            NewModels = new List<IModel>();
            RemovedModels = new List<IModel>();
        }

        public void Add(IModel model)
        {
            modelsList.Add(model);
            NewModels.Add(model);
        }

        public void Remove(IModel model)
        {
            modelsList.Remove(model);
            if (NewModels.Contains(model))
                NewModels.Remove(model);
            else
                RemovedModels.Add(model);
        }

        public void Clear()
        {
            foreach (var model in modelsList)
            {
                if (NewModels.Contains(model))
                    NewModels.Remove(model);
                else
                    RemovedModels.Add(model);
            }
            
            modelsList.Clear();
        }

        public IModel First()
        {
            if (modelsList is null)
                return null;

            return modelsList.Count == 0 ? null : modelsList[0];
        }

        public IEnumerator<IModel> GetEnumerator()
        {
            return modelsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}