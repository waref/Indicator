using Depences.Infrastructure.Interfaces.IDataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Depences.Infrastructure.Static
{
    public static class QueryableUtils
    {
        public static IQueryable<T> FindAsQueryable<T>(this DbSet<T> dbEntitySet, Expression<Func<T, bool>> filter)
            where T : class, IEntity
        {
            if (dbEntitySet == null)
            {
                throw new ArgumentNullException(nameof(dbEntitySet), $"{nameof(dbEntitySet)} should have a value !");
            }

            return dbEntitySet.Where(filter);
        }

        public static IQueryable<T> SortAsQueryable<T>(this IQueryable<T> query, string sortExpression, string parametersSeparator = ",")
            where T : class, IEntity
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (string.IsNullOrWhiteSpace(sortExpression))
            {
                return query;
            }

            var sortItems = sortExpression.Split(parametersSeparator, StringSplitOptions.RemoveEmptyEntries).ToArray();
            for (var i = 0; i < sortItems.Length; i++)
            {
                if (i == 0)
                {
                    query = query.FirstSort(sortItems[i]);
                    continue;
                }

                query = query.NextSort(sortItems[i]);
            }

            return query;
        }

        private static IQueryable<T> FirstSort<T>(this IQueryable<T> query, string property)
          where T : class, IEntity
        {
            return property.StartsWith("-", StringComparison.Ordinal) ? query.OrderByDescending(property.Substring(1)) : query.OrderBy(property);
        }

        private static IQueryable<T> NextSort<T>(this IQueryable<T> query, string property)
            where T : class, IEntity
        {
            return property.StartsWith("-", StringComparison.Ordinal) ? query.ThenByDescending(property.Substring(1)) : query.ThenBy(property);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string property)
          where T : class, IEntity
        {
            if (query == null)
                return query;
            return query.ApplyOrder(property, "OrderByDescending");
        }

        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> query, string property)
            where T : class, IEntity
        {
            if (query == null)
                return query;
            return query.ApplyOrder(property, "ThenByDescending");
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string property)
           where T : class, IEntity
        {
            if (query == null)
                return query;
            return query.ApplyOrder(property, "OrderBy");
        }

        public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string property)
            where T : class, IEntity
        {
            if (query == null)
                return query;
            return query.ApplyOrder(property: property, "ThenBy");
        }

        private static IQueryable<T>? ApplyOrder<T>(this IQueryable<T> query, string property, string methodName)
          where T : class, IEntity
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), $"{nameof(query)} should have a value !");
            }

            if (string.IsNullOrEmpty(property))
            {
                return query;
            }

            var entityType = typeof(T);

            // Only look for properties, IgnoreCase is used because parameters name should start with an upper case in back and lower case in front
            var propertyInfo = Array.Find(entityType.GetProperties(), p => string.Equals(p.Name, property, StringComparison.InvariantCultureIgnoreCase));

            // If property does not exist, code should throw an exception or ignore it ?
            if (propertyInfo == null)
            {
                return query;
            }

            var arg = Expression.Parameter(entityType);
            var expr = Expression.Property(arg, propertyInfo);
            var delegateType = typeof(Func<,>).MakeGenericType(entityType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(delegateType, expr, arg);
            return typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(entityType, propertyInfo.PropertyType)
                .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
    }
}
