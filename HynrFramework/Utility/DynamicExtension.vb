Imports System.Reflection
Imports System.Reflection.Emit

Class DynamicExtension(Of T)
    Public Function ExtendWith(Of K)() As K
        Dim assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(New AssemblyName("Assembly"), AssemblyBuilderAccess.Run)
        Dim [module] = assembly.DefineDynamicModule("Module")
        Dim type = [module].DefineType("Class", TypeAttributes.[Public], GetType(T))

        type.AddInterfaceImplementation(GetType(K))

        For Each v As Object In GetType(K).GetProperties()
            Dim field = type.DefineField("_" + v.Name.ToLower(), v.PropertyType, FieldAttributes.[Private])
            Dim [property] = type.DefineProperty(v.Name, PropertyAttributes.None, v.PropertyType, New Type(-1) {})
            Dim getter = type.DefineMethod("get_" + v.Name, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.Virtual, v.PropertyType, New Type(-1) {})
            Dim setter = type.DefineMethod("set_" + v.Name, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.Virtual, Nothing, New Type() {v.PropertyType})

            Dim getGenerator = getter.GetILGenerator()
            Dim setGenerator = setter.GetILGenerator()

            getGenerator.Emit(OpCodes.Ldarg_0)
            getGenerator.Emit(OpCodes.Ldfld, field)
            getGenerator.Emit(OpCodes.Ret)

            setGenerator.Emit(OpCodes.Ldarg_0)
            setGenerator.Emit(OpCodes.Ldarg_1)
            setGenerator.Emit(OpCodes.Stfld, field)
            setGenerator.Emit(OpCodes.Ret)

            [property].SetGetMethod(getter)
            [property].SetSetMethod(setter)

            type.DefineMethodOverride(getter, v.GetGetMethod())
            type.DefineMethodOverride(setter, v.GetSetMethod())
        Next

        Return DirectCast(Activator.CreateInstance(type.CreateType()), K)
    End Function
End Class
