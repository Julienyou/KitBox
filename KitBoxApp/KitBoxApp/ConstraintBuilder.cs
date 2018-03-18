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
        

        static public void BuildCupboardConstraint(string id)
        {
            List<int> heights = new List<int>();
            List<int> widths = new List<int>();
            List<int> depths = new List<int>();
            int maxHeight;

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='1'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["height"]));
            }
            
            sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                widths.Add(Convert.ToInt32(reader["width"]));
                depths.Add(Convert.ToInt32(reader["depth"]));
            }

            maxHeight = heights.Max();


            /*End connection DataBase*/
            dbConnection.Close();

            new CupboardConstraint(depths, widths, maxHeight);
        }        
    }
}
