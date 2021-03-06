﻿using ArmchairExpertsCom.Models.Interfaces;
using ArmchairExpertsCom.Models.Utilities;

namespace ArmchairExpertsCom.Models
{
    public class User : IModel
    {
        public User()
        {
            Images = new DbSet(this);
            Subscribes = new DbSet(this);
            PendingBooks = new DbSet(this);
            PendingFilms = new DbSet(this);
            PendingSerials = new DbSet(this);
            Selections = new DbSet(this);
            Privacy = new DbSet(this);
        }
        
        [MetaData]
        public bool IsNew { get; set; }
        
        [MetaData]
        public bool IsChanged { get; set; }
        
        [MetaData]
        public bool IsDeleted { get; set; }
        
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string PasswordKey { get; set; }
        public string BirthDate { get; set; }
        public string Role { get; set; }
        
        
        [ForeignKey(typeof(Image))]
        public DbSet Images { get; private set;  }
        
        [ForeignKey(typeof(User))]
        public DbSet Subscribes { get; private set; }
        
        [ForeignKey(typeof(Book))]
        public DbSet PendingBooks { get; private set; }

        [ForeignKey(typeof(Film))]
        public DbSet PendingFilms { get; private set; }

        [ForeignKey(typeof(Serial))] 
        public DbSet PendingSerials { get; private set; }

        [ForeignKey(typeof(Selection))]
        public DbSet Selections { get; private set; }
        
        [ForeignKey(typeof(PrivacySettings))]
        public DbSet Privacy { get; private set; }
        
        
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
            Images.Clear();
            Subscribes.Clear();
            PendingBooks.Clear();
            PendingFilms.Clear();
            PendingSerials.Clear();
            Selections.Clear();
            Privacy.Clear();
            IsDeleted = true;
        }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
        
        public override string ToString()
        {
            return $"{Id}.{GetFullName()}";
        }
    }
}