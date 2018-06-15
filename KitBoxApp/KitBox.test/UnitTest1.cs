using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KitBox.Core;
using KitBox.Core.Model;
using System.Data.SQLite;

namespace KitBox.test
{
    [TestClass]
    public class UtilsTest
    {
        public UtilsTest()
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
        }

        [TestMethod]
        public void ImportFromDatabase()
        {
            Order order = Utils.ImportFromDatabase("5");

            Assert.AreEqual(order.TotalPrice, 175);
            Assert.AreEqual(Convert.ToInt32(order.Components[0]["quantity"]), 4);
        }
    }
}
