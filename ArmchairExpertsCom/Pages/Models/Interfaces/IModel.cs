using System.Collections;
using System.Collections.Generic;
using Npgsql;

namespace ArmchairExpertsCom.Pages
{
    public interface IModel
    {
        public int Id { get; set; }
        public bool IsInDataBase { get; set; }
        public void Save();
        public void Delete();
    }
}