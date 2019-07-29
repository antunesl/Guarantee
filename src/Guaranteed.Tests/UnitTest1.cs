using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Guaranteed.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsNotNull()
        {
            Guarantee.That.IsNotNull("");
            Guarantee.That.IsNotNull(1, "int");
            Guarantee.That.IsNotNull(Guid.Empty, "guid");
            Guarantee.That.IsNotNull(DateTime.Now, "datetime");
            Guarantee.That.IsNotNull(new Object(), "object");
        }

        [Fact]
        public void IsNotNullOrEmpty()
        {
            Guarantee.That.IsNotNullOrEmpty("_");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guarantee.That.IsNotNull(null));
        }


        [Fact]
        public void GuaranteeBoolIsTrue()
        {
            Guarantee.That.IsTrue(true);
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsTrue(false));
        }

        [Fact]
        public void GuaranteeBoolIsFalse()
        {
            Guarantee.That.IsFalse(false);
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsFalse(true));
        }



        [Theory]
        [InlineData("", typeof(string))]
        [InlineData("content", typeof(string))]
        [InlineData(1, typeof(int))]
        [InlineData(1.0f, typeof(float))]
        public void GuaranteeTypeIfOfType(object value, Type type)
        {
            Guarantee.That.IsOfType(value, type);
        }

        [Fact]
        public void GuaranteeTypeIfOfTypeGeneric()
        {
            Guarantee.That.IsOfType<string>("content");
            Guarantee.That.IsOfType<int>(1);
            Guarantee.That.IsOfType<float>(1.0f);
            Guarantee.That.IsOfType<decimal>(1.0M);
            Guarantee.That.IsOfType<object>(new object());
            Guarantee.That.IsOfType<UnitTest1>(new UnitTest1());
        }


        [Fact]
        public void GuaranteeTypeIfOfTypeGenericThrows()
        {
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<int>("content"));
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<string>(1));
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<decimal>(1.0f));
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<float>(1.0M));
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<UnitTest1>(new object()));
            Assert.Throws<ArgumentException>(() => Guarantee.That.IsOfType<object>(new UnitTest1()));
        }

        [Fact]
        public void GuaranteeEnumerableSizeIsGt()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.SizeIsGt(data, 1);
            Guarantee.That.SizeIsGt(data, 9);
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsGt(data, 10));
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsGt(data, 11));
        }

        [Fact]
        public void GuaranteeEnumerableSizeIsGte()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.SizeIsGte(data, 1);
            Guarantee.That.SizeIsGte(data, 9);
            Guarantee.That.SizeIsGte(data, 10);
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsGt(data, 11));
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsGt(data, 12));
        }

        [Fact]
        public void GuaranteeEnumerableSizeIsLt()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.SizeIsLt(data, 12);
            Guarantee.That.SizeIsLt(data, 11);
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsLt(data, 10));
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsLt(data, 9));
        }

        [Fact]
        public void GuaranteeEnumerableSizeIsLte()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.SizeIsLte(data, 12);
            Guarantee.That.SizeIsLte(data, 11);
            Guarantee.That.SizeIsLte(data, 10);
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsLte(data, 9));
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIsLte(data, 8));
        }

        [Fact]
        public void GuaranteeEnumerableSizeIsEqual()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.SizeIs(data, 10);
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIs(data, 9));
            Assert.Throws<ArgumentException>(() => Guarantee.That.SizeIs(data, 11));
        }

        [Fact]
        public void GuaranteeEnumerableHasItems()
        {
            var data = Enumerable.Range(1, 10);
            Guarantee.That.HasItems(data);
        }

        [Fact]
        public void GuaranteeEnumerableHasItemsFalse()
        {
            var data = new List<int>();
            Assert.Throws<ArgumentException>(() => Guarantee.That.HasItems(data));
        }



        [Fact]
        public void GuaranteeThis()
        {
            var data = new List<int>();
            Guarantee.This(() => true);
            Assert.Throws<Exception>(() => Guarantee.This(() => false));
        }

        [Fact]
        public void GuaranteeThisGeneric()
        {
            var data = new List<int>();

            Guarantee.This((lst) => lst.Count == 0, data);
            Assert.Throws<Exception>(() => Guarantee.This((lst) => lst.Count == 1, data));
        }
    }
}
