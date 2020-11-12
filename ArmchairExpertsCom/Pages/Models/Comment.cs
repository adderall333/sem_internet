using System;
using ArmchairExpertsCom.Pages.Models.Interfaces;
using ArmchairExpertsCom.Pages.Models.Utilities;
using Npgsql;

namespace ArmchairExpertsCom.Pages.Models
{
    public class Comment : IModel
    {
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }

        
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        
        
        [ForeignKey(typeof(User))]
        public DbSet User { get; set; } = new DbSet();
        
        
        public void Save()
        {
            if (!Repository.Contains(this))
            {
                Repository.Add(this);
                IsNew = true;
            }
            else
                IsChanged = true;
        }
        
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}