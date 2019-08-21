using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using DynamoDBMapper.Utility;

namespace DynamoDBMapper.Mapping
{
    public class MappingExtensions
    {
        public static T MapModel<T>(Dictionary<string, AttributeValue> rawModel) where T : DynamoDBMapper.Model.Model
        {
            Type type = typeof(T);
            T instance = type.Assembly.CreateInstance(type.FullName) as T;
            TypeInfo info = type.GetTypeInfo();
            IEnumerable<PropertyInfo> attributes = info.DeclaredProperties;
            List<PropertyInfo> list = new List<PropertyInfo>(attributes);

            foreach (KeyValuePair<string, AttributeValue> item in rawModel)
            {
                List<PropertyInfo> searchedProperty = list.FindAll((PropertyInfo propInfo) => propInfo.Name == item.Key);
                if (searchedProperty.Count > 0)
                {
                    Type propertyType = searchedProperty[0].PropertyType;
                    if (propertyType.IsGenericType)
                    {
                        IList listType = (IList)Activator.CreateInstance(propertyType);
                        foreach (AttributeValue listValue in item.Value.L)
                        {
                            object mappedNewObject = CallMapModelByType(propertyType.GetGenericArguments()[0], listValue.M);
                            listType.Add(mappedNewObject);
                        }
                        searchedProperty[0].SetValue(instance, listType);
                    }
                    else
                    {
                        object val = RawValueForType(propertyType, item.Value);
                        searchedProperty[0].SetValue(instance, val);
                    }
                }
                else
                {
                    // Error Logging
                }
            }
            return instance;
        }

        private static object RawValueForType(Type type, AttributeValue value)
        {
            if (type.IsEnum)
            {
                return value.S.GetEnum(Type.GetType(type.FullName));
            }

            switch (type.Name)
            {
                case "String": return value.S;
                case "Single": return Convert.ToSingle(value.N);
                case "Int32": return Convert.ToInt32(value.N);
                case "Boolean": return value.BOOL;
            }
            return value.NULL;
        }

        private static object CallMapModelByType(Type type, Dictionary<string, AttributeValue> rawModel)
        {
            MethodInfo method = typeof(MappingExtensions).GetMethod("MapModel", BindingFlags.Static | BindingFlags.Public);
            MethodInfo generic = method.MakeGenericMethod(type);
            object[] parms = new object[] { rawModel };
            return generic.Invoke(null, parms);
        }
    }   
}
