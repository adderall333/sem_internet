﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArmchairExpertsCom.Models.Utilities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArmchairExpertsCom.Services
{
    public static class Editor
    {
        public static IHtmlContent Edit(this IHtmlHelper helper, object model)
        {
            var content = new HtmlContentBuilder();
            foreach (var str in GetForm(model))
            {
                content.AppendHtml(str);
            }

            return content;
        }
        
        private static readonly Dictionary<Type, string> InputTypes = new Dictionary<Type, string>
        {
            {typeof(int), "number"},
            {typeof(long), "number"},
            {typeof(bool), "checkbox"},
            {typeof(string), "text"}
        };

        private static IEnumerable<string> GetForm(object obj)
        {
            var root = new PropertyNode(obj);
            foreach (var str in root
                .Type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .SelectMany(p => Process(new PropertyNode(p.GetValue(obj), p, root))))
                yield return str;
        }
        
        private static IEnumerable<string> Process(PropertyNode node)
        {
            yield return $"<div class='editor-label'><label for='{node.Name}'>{node.Name}</label></div>";

            if (InputTypes.Keys.Contains(node.Type))
            {
                yield return GetInput(node);
            }
            else if (node.Type.IsEnum)
            {
                yield return GetSelect(node);
            }
            else if (node.Type.IsClass)
            {
                node.Parent.CheckType(node.Type);
                foreach (var str in node
                    .Type
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => !p.GetCustomAttributes(typeof(MetaDataAttribute), false).Any())
                    .Where(p => !p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any())
                    .SelectMany(p => Process(new PropertyNode(p.GetValue(node.Value), p, node))))
                    yield return str;
            }
            else
            {
                throw new NotSupportedException("Модель содержит неподдерживаемые свойства");
            }
        }

        private static string GetInput(PropertyNode node)
        {
            return $"<div class='editor-field'>" +
                   $"<input " +
                   $"type='{InputTypes[node.Type]}' " +
                   $"id='{node.Name}' " +
                   $"name='{node.Name}' " +
                   $"value='{node.Value}'" +
                   $"{Checked(node)}>" +
                   $"</div>";
        }

        private static string GetSelect(PropertyNode node)
        {
            return $"<div class='editor-field'>" +
                   $"<select name='{node.Name}'>" +
                   node
                       .Type
                       .GetEnumNames()
                       .Select(option =>
                           $"<option value='{option}'{Selected(node, option)}>{option}</option>")
                       .Aggregate((current, next) => current + next) +
                   $"</select>" +
                   $"</div>";
        }

        private static string Selected(PropertyNode node, string option)
        {
            return node.Value.ToString() == option ? " selected" : "";
        }

        private static string Checked(PropertyNode node)
        {
            return node.Type == typeof(bool) && 
                   (bool) node.Value ? " checked" : "";
        }
    }

    class PropertyNode
    {
        public string Name { get; }
        public PropertyNode Parent { get; }
        public object Value { get; }
        public Type Type { get; }

        public PropertyNode(object value, PropertyInfo property = null, PropertyNode parent = null)
        {
            Value = value;
            Type = value.GetType();
            Name = property?.Name;
            Parent = parent;
        }
        
        public void CheckType(Type type)
        {
            if (Type == type)
                throw new NotSupportedException("Произошло зацикливание");

            Parent?.CheckType(type);
        }
    } 
}