using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace TuringL.DServices
{
    public class LambdaConstruct
    {
        public static Expression<Func<Tsource, bool>> ParameterPropertyEqualConstant<Tsource>(string propertyName, object value)
        {
            var p = Expression.Parameter(typeof(Tsource), "it");
            var property = Expression.Property(p, propertyName);
            var body=Expression.Equal(property,Expression.Constant(value));
            return Expression.Lambda<Func<Tsource,bool>>(body, p);
        }
    }
}
