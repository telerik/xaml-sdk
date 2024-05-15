using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BindingToICustomTypeProvider.Helpers
{
    class MyType : Type
    {
        private readonly Type baseType;
        private List<MyPropertyInfo> customProperties;

        public MyType(Type delegatingType, List<MyPropertyInfo> properties)
        {
           baseType = delegatingType;
           customProperties = properties;
        }

        public override Assembly Assembly
        {
            get { return baseType.Assembly; }
        }

        public override string AssemblyQualifiedName
        {
            get { return baseType.AssemblyQualifiedName; }
        }

        public override Type BaseType
        {
            get { return baseType.BaseType; }
        }

        public override string FullName
        {
            get { return baseType.FullName; }
        }

        public override Guid GUID
        {
            get { return baseType.GUID; }
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            return baseType.Attributes;
        }

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
#if WPF 
                return baseType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
#endif
            throw new NotImplementedException();
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return baseType.GetConstructors(bindingAttr);
        }

        public override Type GetElementType()
        {
            return baseType.GetElementType();
        }

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return baseType.GetEvent(name, bindingAttr);
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return baseType.GetEvents(bindingAttr);
        }

        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return baseType.GetField(name, bindingAttr);
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return baseType.GetFields(bindingAttr);
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            return baseType.GetInterface(name, ignoreCase);
        }

        public override Type[] GetInterfaces()
        {
            return baseType.GetInterfaces();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return baseType.GetMembers(bindingAttr);
        }

        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return baseType.GetMethods(bindingAttr);
        }

        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return baseType.GetNestedType(name, bindingAttr);
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return baseType.GetNestedTypes(bindingAttr);
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return baseType.GetProperties(bindingAttr)
                .Concat(customProperties).ToArray();
        }

        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            return GetProperties(bindingAttr).FirstOrDefault(prop => prop.Name == name)
                ?? customProperties.FirstOrDefault(prop => prop.Name == name);
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
        {
            return baseType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            return baseType.IsPrimitive;
        }

        public override Module Module
        {
            get { return baseType.Module; }
        }

        public override string Namespace
        {
            get { return baseType.Namespace; }
        }

        public override Type UnderlyingSystemType
        {
            get { return baseType.UnderlyingSystemType; }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return baseType.GetCustomAttributes(attributeType, inherit);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return baseType.GetCustomAttributes(inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return baseType.IsDefined(attributeType, inherit);
        }

        public override string Name
        {
            get { return baseType.Name; }
        }

        public override bool ContainsGenericParameters
        {
            get { return baseType.ContainsGenericParameters; }
        }

        public override MethodBase DeclaringMethod
        {
            get { return baseType.DeclaringMethod; }
        }

        public override Type DeclaringType
        {
            get { return baseType.DeclaringType; }
        }

        public override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            return baseType.FindMembers(memberType, bindingAttr, filter, filterCriteria);
        }

        public override GenericParameterAttributes GenericParameterAttributes
        {
            get { return baseType.GenericParameterAttributes; }
        }

        public override int GenericParameterPosition
        {
            get { return baseType.GenericParameterPosition; }
        }

#if WPF
        public override Type[] GenericTypeArguments
        {
            get { return baseType.GenericTypeArguments; }
        }

        public override System.Collections.Generic.IList<CustomAttributeData> GetCustomAttributesData()
        {
            return baseType.GetCustomAttributesData();
        }

        public override MemberInfo[] GetDefaultMembers()
        {
            return baseType.GetDefaultMembers();
        }

        public override string GetEnumName(object value)
        {
            return baseType.GetEnumName(value);
        }

        public override string[] GetEnumNames()
        {
            return baseType.GetEnumNames();
        }

        public override Type GetEnumUnderlyingType()
        {
            return baseType.GetEnumUnderlyingType();
        }

        public override Array GetEnumValues()
        {
            return baseType.GetEnumValues();
        }

        public override int GetArrayRank()
        {
            return baseType.GetArrayRank();
        }

        public override bool Equals(Type o)
        {
            return baseType == o;
        }

        public override Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
        {
            return baseType.FindInterfaces(filter, filterCriteria);
        }

        public override System.Collections.Generic.IEnumerable<CustomAttributeData> CustomAttributes
        {
            get { return baseType.CustomAttributes; }
        }

        public override bool IsConstructedGenericType
        {
            get { return baseType.IsConstructedGenericType; }
        }

        public override bool IsEnum
        {
            get { return baseType.IsEnum; }
        }

        public override bool IsEnumDefined(object value)
        {
            return baseType.IsEnumDefined(value);
        }

        public override bool IsEquivalentTo(Type other)
        {
            return baseType.IsEquivalentTo(other);
        }

           public override bool IsSerializable
        {
            get { return baseType.IsSerializable; }
        }

        public override System.Runtime.InteropServices.StructLayoutAttribute StructLayoutAttribute
        {
            get { return baseType.StructLayoutAttribute; }
        }
#endif

        public override EventInfo[] GetEvents()
        {
            return baseType.GetEvents();
        }

        public override Type[] GetGenericArguments()
        {
            return baseType.GetGenericArguments();
        }

        public override Type[] GetGenericParameterConstraints()
        {
            return baseType.GetGenericParameterConstraints();
        }

        public override Type GetGenericTypeDefinition()
        {
            return baseType.GetGenericTypeDefinition();
        }

        public override InterfaceMapping GetInterfaceMap(Type interfaceType)
        {
            return baseType.GetInterfaceMap(interfaceType);
        }

        public override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
        {
            return baseType.GetMember(name, bindingAttr);
        }

        public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
        {
            return baseType.GetMember(name, type, bindingAttr);
        }

        public override bool IsAssignableFrom(Type c)
        {
            return baseType.IsAssignableFrom(c);
        }

        public override bool IsGenericParameter
        {
            get { return baseType.IsGenericParameter; }
        }

        public override bool IsGenericType
        {
            get { return baseType.IsGenericType; }
        }

        public override bool IsGenericTypeDefinition
        {
            get { return baseType.IsGenericTypeDefinition; }
        }

        public override bool IsInstanceOfType(object o)
        {
            return baseType.IsInstanceOfType(o);
        }

        public override bool IsSubclassOf(Type c)
        {
            return baseType.IsSubclassOf(c);
        }

        public override RuntimeTypeHandle TypeHandle
        {
            get { return baseType.TypeHandle; }
        }

        public override Type MakeArrayType()
        {
            return baseType.MakeArrayType();
        }

        public override Type MakeArrayType(int rank)
        {
            return baseType.MakeArrayType(rank);
        }

        public override Type MakeByRefType()
        {
            return baseType.MakeByRefType();
        }

        public override Type MakeGenericType(params Type[] typeArguments)
        {
            return baseType.MakeGenericType(typeArguments);
        }

        public override Type MakePointerType()
        {
            return baseType.MakePointerType();
        }

        public override MemberTypes MemberType
        {
            get { return baseType.MemberType; }
        }

        public override int MetadataToken
        {
            get { return baseType.MetadataToken; }
        }

        public override Type ReflectedType
        {
            get { return baseType.ReflectedType; }
        }

        public override string ToString()
        {
            return baseType.ToString();
        }
    }
}
