using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

[Serializable]
public class ClassTable
{
    [SerializeField] private string className = null;
    [SerializeField] private List<ClassProperty> properties = null;

    /// <summary>
    /// Creates an instance of the class defined in this table.
    /// </summary>
    public object CreateClass()
    {
        // Create an instance of the class and get its properties.
        Type classType = Type.GetType(className);
        object newClass = Activator.CreateInstance(Type.GetType(className));
        PropertyInfo[] classProperties = classType.GetProperties();
        
        // Iterate through properties and assign values to them.
        foreach (PropertyInfo property in classProperties)
        {
            object value = GetValue(property.Name, property.PropertyType);
            if (value != null)
                property.SetValue(newClass, value);
        }

        return newClass;
    }

    private object GetValue(string name, Type type)
    {
        ClassProperty property = properties.FirstOrDefault(p => p.Name == name);
        if (property != null)
            return ConvertStringToType(property.Value, type);

        return null;
    }

    private object ConvertStringToType(string valueAsString, Type type)
    {
        var converter = TypeDescriptor.GetConverter(type);
        return converter.ConvertFromInvariantString(valueAsString);
    }
}

[Serializable]
public class ClassProperty
{
    public string Name = null;
    public string Value = null;
}

