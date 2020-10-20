﻿using System;

namespace ArmchairExpertsCom.Models
{
    public interface IWriting
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Comment[] Comments { get; set; }
    }
}