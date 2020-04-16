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
            var spec = new Spec<int>(v => v > 5);
            Expression<Func<int, bool>> expression = spec;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(5)]
        [TestCase(0)]
        [TestCase(Int32.MinValue)]
        public void SpecIsFalse(int value)
        {
            var spec = new Spec<int>(v => v > 5);
            Expression<Func<int, bool>> expression = spec;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }

        [Test]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(9)]
        public void SpecAndSpecAreTrue(int value)
        {
            var left = new Spec<int>(v => v > 5);
            var right = new Spec<int>(v => v < 10);
            Expression<Func<int, bool>> expression = left & right;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(6)]
        [TestCase(8)]
        public void SpecAndSpecAreTrue_MultipleConditions(int value)
        {
            var left = new Spec<int>(v => v > 5 && v % 2 == 0);
            var right = new Spec<int>(v => v < 10);
            Expression<Func<int, bool>> expression = left & right;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == true);
        }

        [Test]
        [TestCase(1)]
        [TestCase(8)]
        public void SpecAndSpecAreFalse(int value)
        {
            var left = new Spec<int>(v => v > 5);
            var right = new Spec<int>(v => v < 10);
            Expression<Func<int, bool>> expression = left & right;

            var result = expression.Compile().Invoke(value);

            Assert.That(result == false);
        }
    }
}