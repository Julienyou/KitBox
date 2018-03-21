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


        static public void BuildCupboardConstraint()
        {
            List<int> heights = new List<int>();
            List<int> widths = new List<int>();
            
            int maxHeight;

            CupboardConstraint cc = new CupboardConstraint();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                widths.Add(Convert.ToInt32(reader["width"]));
            }

            cc.Widths = widths;

        }

        static public void GetAvailableDepth(int width)
        {
            List<int> depths = new List<int>();

            sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5', `width`=" + Convert.ToString(height);

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                depths.Add(Convert.ToInt32(reader["depth"]));
            }

            cc.Depths = depths;
        }

        static public void GetAvailableHeight(int width)
        {
            List<int> depths = new List<int>();

            sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5', `width`=" + Convert.ToString(width) + ", `depth`=" + Convert.ToString(depth);

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                depths.Add(Convert.ToInt32(reader["height"]));
            }

            bc.Depths = depths;
        }



        /*sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='1'";

        SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            heights.Add(Convert.ToInt32(reader["height"]));
        }



        maxHeight = heights.Max();


        /*End connection DataBase*/
        /*dbConnection.Close();

         new CupboardConstraint(depths, widths,null,maxHeight);
     }*/

        static public void BuildBoxConstraint()
        {
            List<int> heights = new List<int>();
            List<string> colors = new List<string>();
            List<int> widths = new List<int>();

            BoxConstraint bc = new BoxConstraint();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='4'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["height"]));
            }

            sql = "SELECT * FROM `Component`" +
                "INNER JOIN `Color` ON Component.FK_Color=Color.PK_Color" +
                "WHERE `CupboardConstraint`.`FK_Reference='5'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            /*End connection DataBase*/
            dbConnection.Close();

            new BoxConstraint(heights, colors, null);
        }

        static public void BuildDoorConstraint()
        {
            List<Tuple<int, int>> doorDimensions = new List<Tuple<int, int>>();
            List<string> colors = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='6'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                doorDimensions.Add(new Tuple<int, int>(Convert.ToInt32(reader["Height"]), Convert.ToInt32(reader["Width"])));
            }

            sql = "SELECT * FROM `Component`" +
                "INNER JOIN `Color` ON Component.FK_Color=Color.PK_Color" +
                "WHERE `CupboardConstraint`.`FK_Reference='6'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }


            /*End connection DataBase*/
            dbConnection.Close();

            new DoorConstraint(colors, doorDimensions);
        }

        static public void InterfaceConstraint()
        {
            List<int> widths = List<int>();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                widths.Add(Convert.ToInt32(reader["width"]));
            }
        }
    }
}
