using System;
using System.Linq.Expressions;

namespace LittleThingsToDo.Domain.Common
{
    public class Spec<T>
    {
        public static Spec<T> operator &(Spec<T> spec1, Spec<T> spec2)
            => new Spec<T>(spec1._expression.Combine(spec2._expression, Expression.AndAlso));

        public static Spec<T> operator |(Spec<T> spec1, Spec<T> spec2)
            => new Spec<T>(spec1._expression.Combine(spec2._expression, Expression.OrElse));

        public static Spec<T> operator !(Spec<T> spec)
        {
            var result =
                Expression.Lambda<Func<T, bool>>(Expression.Not(spec._expression.Body), spec._expression.Parameters);

            return new Spec<T>(result);
        }

        public static implicit operator Expression<Func<T, bool>>(Spec<T> spec)
            => spec._expression;

        public static implicit operator Spec<T>(Expression<Func<T, bool>> expression)
            => new Spec<T>(expression);
        
        private readonly Expression<Func<T, bool>> _expression;

        public Spec(Expression<Func<T, bool>> expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }
    }
}