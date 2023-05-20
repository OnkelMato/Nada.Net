using System.Reflection;

namespace MagicBox.Extensions
{
    /// <summary>
    /// extension methods for object-type.
    /// </summary>
    public static class ObjectExtensions
    {
        public static object GetPropValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) return null;

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) return null;

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            var retval = GetPropValue(obj, name);
            if (retval == null) return default;

            // throws InvalidCastException if types are incompatible
            return (T) retval;
        }

        public static void ObjectToObject(this object source, object destination)
        {
            // Purpose : Use reflection to set property values of objects that share the same property names.
            var s = source.GetType();
            var d = destination.GetType();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var objSourceProperties = s.GetProperties(flags);
            var objDestinationProperties = d.GetProperties(flags);

            var propertyNames = objSourceProperties
                .Select(c => c.Name)
                .ToList();

            foreach (var properties in objDestinationProperties.Where(properties =>
                propertyNames.Contains(properties.Name)))
                try
                {
                    var piSource = source.GetType().GetProperty(properties.Name);

                    properties.SetValue(destination, piSource.GetValue(source, null), null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public static List<T> CopyList<T>(this List<T> lst)
        {
            var lstCopy = new List<T>();

            foreach (var item in lst)
            {
                var instanceOfT = Activator.CreateInstance<T>();
                ObjectToObject(item, instanceOfT);
                lstCopy.Add(instanceOfT);
            }

            return lstCopy;
        }
    }
}