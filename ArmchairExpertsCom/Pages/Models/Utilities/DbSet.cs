using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ArmchairExpertsCom.Pages.Models.Interfaces;

namespace ArmchairExpertsCom.Pages.Models.Utilities
{
    public class DbSet : IEnumerable
    {
        [MetaData]
        public bool IsChanged { get; set; }

        private readonly List<IModel> models;
        
        public IModel Parent { get; }
        public List<IModel> NewModels { get; }
        public List<IModel> RemovedModels { get; }

        public DbSet(IModel parent, IEnumerable<IModel> models)
        {
            Parent = parent;
            this.models = models.ToList();
            NewModels = new List<IModel>();
            RemovedModels = new List<IModel>();
        }
        
        public DbSet()
        {
            models = new List<IModel>();
            NewModels = new List<IModel>();
            RemovedModels = new List<IModel>();
        }

        public void Add(IModel model)
        {
            models.Add(model);
            NewModels.Add(model);
        }

        public void Remove(IModel model)
        {
            models.Remove(model);
            if (NewModels.Contains(model))
                NewModels.Remove(model);
            else
                RemovedModels.Add(model);
        }

        public IEnumerator<IModel> GetEnumerator()
        {
            return models.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}