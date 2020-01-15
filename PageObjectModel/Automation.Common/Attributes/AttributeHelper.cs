using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace CommonHelper.Helper.Attributes
{
    public static class AttributeHelper
    {
        #region - Methods -
        public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expression '{0}' doesn't refers to a property.", propertyLambda.ToString()));
            }
            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));
            }
            return propInfo;
        }

        public static object[] GetCustomAttributes<T>(Expression<Func<T>> propertyLambda, Type attributeType)
        {
            PropertyInfo info = GetPropertyInfo(propertyLambda);
            return attributeType == null ? info.GetCustomAttributes(true) : info.GetCustomAttributes(attributeType, true);
        }

        public static object GetCustomAttribute<T>(Expression<Func<T>> propertyLambda, Type attributeType)
        {
            PropertyInfo info = GetPropertyInfo(propertyLambda);
            return info.GetCustomAttribute(attributeType, true);
        }

        public static object Get<T>(Expression<Func<T>> propertyLambda, Type attributeType, string propertyName)
        {
            object attribute = GetCustomAttribute(propertyLambda, attributeType);
            PropertyInfo info = attributeType.GetProperty(propertyName);
            return info.GetValue(attribute);
        }
        #endregion
    }
}
