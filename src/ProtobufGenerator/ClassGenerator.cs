using System.Reflection;
using System.Reflection.Emit;

namespace ProtobufGenerator;

internal class ClassGenerator
{
    private readonly ModuleBuilder _moduleBuilder;

    public ClassGenerator()
    {
        var an = new AssemblyName("DynamicProtoAssembly");
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
        _moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicProtoModule");

    }

    public Type CreateType(Type typeToCopy)
    {
        TypeBuilder tb = _moduleBuilder.DefineType(typeToCopy.Name + "Proto",
            TypeAttributes.Public |
            TypeAttributes.Class /*|
            TypeAttributes.AutoClass |
            TypeAttributes.AnsiClass |
            TypeAttributes.BeforeFieldInit |
            TypeAttributes.AutoLayout*/,
            null);

        ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

        var ci = typeof(ProtoBuf.ProtoContractAttribute).GetConstructor(new Type[0]);
        var builder = new CustomAttributeBuilder(ci, new object[0]);

        tb.SetCustomAttribute(builder);

        var propertiesToCopy = typeToCopy.GetProperties();
        for (int i = 0; i < propertiesToCopy.Length; i++)
        {
            var propertyInfo = propertiesToCopy[i];
            CreateProperty(tb, propertyInfo.Name, propertyInfo.PropertyType, i);
        }

        return tb.CreateType();
    }

    private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType, int i)
    {
        FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

        PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
        MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
        ILGenerator getIl = getPropMthdBldr.GetILGenerator();

        getIl.Emit(OpCodes.Ldarg_0);
        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
        getIl.Emit(OpCodes.Ret);

        MethodBuilder setPropMthdBldr =
            tb.DefineMethod("set_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,
                null, new[] { propertyType });

        ILGenerator setIl = setPropMthdBldr.GetILGenerator();
        Label modifyProperty = setIl.DefineLabel();
        Label exitSet = setIl.DefineLabel();

        setIl.MarkLabel(modifyProperty);
        setIl.Emit(OpCodes.Ldarg_0);
        setIl.Emit(OpCodes.Ldarg_1);
        setIl.Emit(OpCodes.Stfld, fieldBuilder);

        setIl.Emit(OpCodes.Nop);
        setIl.MarkLabel(exitSet);
        setIl.Emit(OpCodes.Ret);

        propertyBuilder.SetGetMethod(getPropMthdBldr);
        propertyBuilder.SetSetMethod(setPropMthdBldr);

        var ci = typeof(ProtoBuf.ProtoMemberAttribute).GetConstructor(new[] { typeof(int) });
        var builder = new CustomAttributeBuilder(ci, new object[] { i + 1 });

        propertyBuilder.SetCustomAttribute(builder);
    }
}
