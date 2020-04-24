using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LittleThingsToDo.Domain.Common;
using NUnit.Framework;

namespace LittleThingsToDo.Domain.Tests
{
    public class SpecTests
    {
        private static readonly List<Expression<Func<int, bool>>> _expressionsForConstructor
            = new List<Expression<Func<int, bool>>>
        {
                value => value == 1,
                value => value == Int32.MaxValue,
                value => value == Int32.MinValue,
        };

        private readonly Spec<int> _odd = new Spec<int>(v => v % 2 == 1);
        private readonly Spec<int> _even = new Spec<int>(v => v % 2 == 0);

        private readonly Spec<int> _lessThanFive = new Spec<int>(v => v < 5);
        private readonly Spec<int> _evenAndLessThanFive = new Spec<int>(v => v < 5 && v % 2 == 0);
        private readonly Spec<int> _evenAndMoreThanFive = new Spec<int>(v => v > 5 && v % 2 == 0);
        private readonly Spec<int> _moreThanFive = new Spec<int>(v => v > 5);
        private readonly Spec<int> _lessThanTen = new Spec<int>(v => v < 10);
        private readonly Spec<int> _moreThanTen = new Spec<int>(v => v > 10);
        private readonly Spec<int> _oddAndMoreThanTen = new Spec<int>(v => v > 10 && v % 2 == 1);
        private readonly Spec<int> _oddAndLessThanTen = new Spec<int>(v => v < 10 && v % 2 == 1);

        [Test]
        public void ConstructorThrowsIfNull()
        {
            Assert.That(() => new Spec<int>(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCaseSource(nameof(_expressionsForConstructor))]
        public void ConstructorDoesNotThrowOnExpressions(Expression<Func<int, bool>> expression)
        {
            Assert.That(() => new Spec<int>(expression), Throws.Nothing);
        }

        [Test]
        [TestCase(6)]
        [TestCase(34173)]
        [TestCase(Int32.MaxValue)]
        public void SpecIsTrue(int value)
        {
            Expression<Func<int, bool>> expression = _moreThanFive;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(5)]
        [TestCase(0)]
        [TestCase(Int32.MinValue)]
        public void SpecIsFalse(int value)
        {
            Expression<Func<int, bool>> expression = _moreThanFive;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }

        [Test]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(9)]
        public void SpecAndSpecAreTrue(int value)
        {
            Expression<Func<int, bool>> expression = _moreThanFive & _lessThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(6)]
        [TestCase(8)]
        public void SpecAndSpecAreTrue_MultipleConditions(int value)
        {
            Expression<Func<int, bool>> expression = _evenAndMoreThanFive & _lessThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(1)]
        [TestCase(11)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void SpecAndSpecAreFalse(int value)
        {
            Expression<Func<int, bool>> expression = _moreThanFive & _lessThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(int.MinValue)]
        public void NotSpecIsTrue(int value)
        {
            Expression<Func<int, bool>> expression = !_moreThanFive;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(10)]
        [TestCase(7)]
        [TestCase(int.MaxValue)]
        public void NotSpecIsFalse(int value)
        {
            Expression<Func<int, bool>> expression = !_moreThanFive;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(0)]
        [TestCase(4)]
        [TestCase(11)]
        public void SpecOrSpecAreTrue(int value)
        {
            Expression<Func<int, bool>> expression = _lessThanFive | _moreThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(4)]
        [TestCase(0)]
        [TestCase(11)]
        [TestCase(13)]
        public void SpecOrSpecAreTrue_MultipleConditions(int value)
        {
            Expression<Func<int, bool>> expression = _evenAndLessThanFive | _oddAndMoreThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(6)]
        [TestCase(9)]
        public void SpecOrSpecAreFalse(int value)
        {
            Expression<Func<int, bool>> expression = _lessThanFive | _moreThanTen;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-7)]
        [TestCase(-1000)]
        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(int.MaxValue)]
        public void SpecOrSpecEqualsExpressionOrExpressionSpec(int value)
        {
            var specOrSpec = _moreThanFive & _even;
            var expressionOrExpressionSpec = _evenAndMoreThanFive;

            var specOrSpecResult = ((Expression<Func<int, bool>>) specOrSpec).Compile().Invoke(value);
            var expressionOrExpressionSpecResult = ((Expression<Func<int, bool>>) expressionOrExpressionSpec).Compile().Invoke(value);

            Assert.That(specOrSpecResult == expressionOrExpressionSpecResult);
        }
    }
}