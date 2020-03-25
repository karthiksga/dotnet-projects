using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.DataAccess;

namespace ShoppingCart.Test
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void VerifyBuildData()
        {
            //Arrange
            ShoppingCartMockData sData = new ShoppingCartMockData();

            //Act           

            //Assert
            Assert.IsTrue(sData.Products.Any());
            Assert.IsTrue(sData.Discounts.Any());
            Assert.IsTrue(sData.Rules.Any());
            Assert.IsTrue(sData.StateRules.Any());
        }
    }
}
