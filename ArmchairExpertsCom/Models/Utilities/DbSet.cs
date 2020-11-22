using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmchairExpertsCom.Models.Interfaces;

namespace ArmchairExpertsCom.Models.Utilities
{
    public class DbSet : IEnumerable<IModel>
    {
        [MetaData]
        public bool IsChanged { get; set; }

        private readonly List<IModel> modelsList;
        
        public IModel Parent { get; }
        public List<IModel> NewModels { get; }
        public List<IModel> RemovedModels { get; }

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

        public IModel First()
        {
            return modelsList?[0];
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