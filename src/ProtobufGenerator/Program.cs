// See https://aka.ms/new-console-template for more information
using ProtoBuf.Meta;
using ProtoBuf;
using ProtobufGenerator;
using System.Reflection;
using ProtobufGenerator.Model;

ContextFinder finder = new ContextFinder();
//var types = finder.GetAllTypesInContextDbSets(Assembly.GetEntryAssembly());
var types = new List<Type>() { typeof(SuperHeroResponse), typeof(Appearance), typeof(Biography), typeof(Connections), /*typeof(Images),*/ typeof(Powerstats), typeof(Work) };

ClassGenerator generator = new ClassGenerator();
var protoTypes = types.Select(x => generator.CreateType(x));

foreach (var protoType in protoTypes)
{
    Console.WriteLine(GenerateProtoFile(protoType));
}

static string GenerateProtoFile(Type protoType)
{
    MethodInfo methodInfo = typeof(Serializer).GetMethod(nameof(Serializer.GetProto), new[] { typeof(ProtoSyntax) });
    MethodInfo genericMethod = methodInfo.MakeGenericMethod(protoType);
    return (string)genericMethod.Invoke(null, new object[] { ProtoSyntax.Proto3 });
}
