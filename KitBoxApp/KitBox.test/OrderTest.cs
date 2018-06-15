using System;
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
    }
}
