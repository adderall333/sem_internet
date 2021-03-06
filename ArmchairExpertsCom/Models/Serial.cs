﻿using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class Serial : IModel, IArtwork
    {
        public Serial()
        {
            Reviews = new DbSet(this);
            Genres = new DbSet(this);
            Images = new DbSet(this);
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Producers { get; set; }
        public string Actors { get; set; }
        
        
        [ForeignKey(typeof(SerialReview))]
        public DbSet Reviews { get; private set; }
        
        [ForeignKey(typeof(SerialGenre))]
        public DbSet Genres { get; private set; }
        
        [ForeignKey(typeof(Image))]
        public DbSet Images { get; private set; }
        
        
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
        
        public override string ToString()
        {
            return $"{Id}.{Title} ({StartYear}-{EndYear})";
        }
    }
}