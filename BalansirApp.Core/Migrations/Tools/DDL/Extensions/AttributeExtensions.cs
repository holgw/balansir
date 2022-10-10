using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BalansirApp.Core.Migrations.Tools.DDL.Extensions
{
    static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true)
                .FirstOrDefault() as TAttribute;

            if (att != null)
            {
                return valueSelector(att);
            }

            return default;
        }

        public static PropertyInfo GetProperty<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                string errMsg = $"Expression '{propertyLambda}' refers to a method, not a property.";
                throw new ArgumentException(errMsg);
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                string errMsg = $"Expression '{propertyLambda}' refers to a field, not a property.";
                throw new ArgumentException(errMsg);
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                string errMsg = $"Expression '{propertyLambda}' refers to a property that is not from type {type}.";
                throw new ArgumentException(errMsg);
            }

            return propInfo;
        }
    }
}
