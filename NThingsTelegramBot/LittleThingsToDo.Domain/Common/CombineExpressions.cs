using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LittleThingsToDo.Domain.Common
{
    // Credit goes to https://github.com/denis-tsv
    internal static class CombineExpressions
    {
        public static Expression<T> Combine<T>(this Expression<T> left, Expression<T> right,
            Func<Expression, Expression, Expression> merge)
        {
            var parametersMap = left.Parameters
                .Select((lParam, i) => new {lParam, rParam = right.Parameters[i]})
                .ToDictionary(p => p.rParam, p => p.lParam);

            var rightBody = ParameterRebinder.ReplaceParameters(parametersMap, right.Body);

            return Expression.Lambda<T>(merge(left.Body, rightBody), left.Parameters);
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (_map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}