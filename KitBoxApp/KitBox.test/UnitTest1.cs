using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KitBox.Core;
using KitBox.Core.Model;
using System.Data.SQLite;

namespace KitBox.test
{
    [TestClass]
    public class UnitTest
    {
        public UnitTest()
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
        }

        [TestMethod]
        public void ImportFromDatabase()
        {
            Order order = Utils.ImportFromDatabase("0");

            Assert.AreEqual(order.TotalPrice, 75);
        }
    }
}
