using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KitBoxApp
{
    static class ConstraintBuilder
    {
        static private SQLiteConnection dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
        static List<int> heights = new List<int>();
        static List<int> widths = new List<int>();

        static public List<Cupboard> ImportFromDatabase(string id)
        {

            /*Start connection DataBase*/
            dbConnection.Open();


            /*Recuperation to customer and TotalPrice data*/
            string sql = "SELECT Height FROM `Component` WHERE FK_Reference = 1'" +
                          ";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["Height"]));
            }


            /*Recuperation of components data*/
            sql = "SELECT * FROM `OrderComponentLink`" +
                  "INNER JOIN Component ON `OrderComponentLink`.`FK_Component`=`Component`.`Code`" +
                  "INNER JOIN Color ON `Component`.`FK_Color`=`Color`.`PK_IDColor`" +
                  "INNER JOIN Reference ON `Component`.`FK_Reference`=`Reference`.`PK_IDRef`" +
                  "WHERE OrderComponentLink.FK_Order='" + id + "'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.AddComponent(new Component(reader["FK_Reference"].ToString(), reader["Name"].ToString(),
                                                 reader["code"].ToString(), Convert.ToInt32(reader["Price"]),
                                                 4, true));
            }


            /*End connection DataBase*/
            dbConnection.Close();

            return order;
        }


        public static void CupboardBuilder()
        {

        }
    }
}
