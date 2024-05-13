using System.ComponentModel;
using System.Reflection;

namespace dbf_api.Configuration;

public abstract class EnvironmentConfigurator
{
    protected EnvironmentConfigurator()
    {
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var envName = property.GetCustomAttribute<EnvName>()?.Name;
            if (envName == null) continue;
            var envVar = Environment.GetEnvironmentVariable(envName);
            switch (envVar)
            {
                case null when property.GetCustomAttribute<EnvName>()?.Required == false:
                    continue;
                case null:
                    throw new ArgumentException($"Environment variable {envName} is not set!");
            }

            if (TryConvert(envVar, property.PropertyType,  out var result))
            {
                var cast = Convert.ChangeType(result, property.PropertyType);
                try
                {
                    property.SetValue(this, cast);
                }
                catch
                {
                    throw new NotSupportedException(
                        $"Property '{property.Name}' is missing setter, make sure the property has a accessible setter!!!");
                }
            }
            else
            {
                throw new NotSupportedException(
                    $"Environment variable {envName} could not be converted to {property.PropertyType}!");
            }
        }
    }
    private static bool TryConvert(string input, Type resultType, out object result)
    {
        if (TypeDescriptor.GetConverter(resultType) is { } converter)
        {
            try
            {
                result = converter.ConvertFromString(input)!;
                return true;
            }
            catch (NotSupportedException)
            {
                result = default!;
                return false;
            }
        }
        result = default!;
        return false;
    
    }

    protected class EnvName(string name, bool required = true) : Attribute
    {
        public string Name { get; } = name;
        public bool Required { get; } = required;
    }
    
}
