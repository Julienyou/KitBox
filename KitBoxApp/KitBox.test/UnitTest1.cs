using System;
using KitBox.Core;
using NUnit.Framework;
using System.Data.SQLite;

namespace KitBox.test
{
    [TestFixture()]
    public class UtilsTest
    {

        [SetUp()]
        public void Init()
        {
            Utils.DBConnection = new SQLiteConnection("db.sqlite");
        }

        [Test()]
        public void ImportFromDatabase()
        {
            Utils.ImportFromDatabase("0");
        }
    }
}
