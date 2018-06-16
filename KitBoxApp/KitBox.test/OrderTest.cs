using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KitBox.Core;
using KitBox.Core.Model;
using System.Data.SQLite;

namespace KitBox.test
{
    [TestClass]
    public class OrderTest
    {

        private Order order;

        public OrderTest()
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            order = Utils.ImportFromDatabase("5");
        }

        [TestMethod]
        public void IsInStock()
        {
            Assert.AreEqual(order.IsInStock, true);
        }

        [TestMethod]
        public void TotalPrice()
        {
            Order order = new Order();

            KitComposer.AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Cornières" },
                { "color", "Brun" },
                { "height", "276" },
                { "quantity" , "4"}
            });

            KitComposer.AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Cornières" },
                { "color", "Brun" },
                { "height", "276" },
                { "quantity" , "4"}
            });

            Utils.FetchFromDataBase(order.Components);
            order.ComputePrice();

            Assert.AreEqual(order.Components[0]["quantity"], "8");
            Assert.AreEqual(order.TotalPrice, 16);

        }
    }
}
