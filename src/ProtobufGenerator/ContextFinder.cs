using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProtobufGenerator;

internal class ContextFinder
{
    public IEnumerable<Type> GetAllTypesInContextDbSets(Assembly assembly)
    {
        return GetContextTypes(assembly)
            .Select(x => GetDataSetTypes(x))
            .SelectMany(x => x)
            .Select(x => x.GetGenericArguments()[0]);
    }

    private IEnumerable<Type> GetContextTypes(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(DbContext)));
    }

    private IEnumerable<Type> GetDataSetTypes(Type context)
    {
        return context.GetProperties()
            .Select(x => x.PropertyType)
            .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(DbSet<>));
    }
}
