using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Npgsql;

namespace ArmchairExpertsCom.Pages.Models.Interfaces
{
    public interface IModel
    {
        public bool IsNew { get; set; }
        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }
        
        public int Id { get; set; }

        public void Save();
        public void Delete();
    }
}