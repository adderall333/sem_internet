using System;

namespace ArmchairExpertsCom.Pages.Models.Utilities
{
    public class ForeignKeyAttribute : Attribute
    {
        public Type Type { get; }

        public ForeignKeyAttribute(Type type)
        {
            Type = type;
        }
    }
}