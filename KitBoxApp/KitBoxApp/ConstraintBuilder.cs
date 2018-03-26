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


        static public List<int> BuildWidthsList()
        {
            List<int> widths = new List<int>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT `width` FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference`='5'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                widths.Add(Convert.ToInt32(reader["width"]));
            }

            widths.Sort();

            dbConnection.Close();
            return widths;
        }

        static public List<int> GetAvailableDepth(int width)
        {
            List<int> depths = new List<int>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference`='5' AND `width`=" + Convert.ToString(width);

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                depths.Add(Convert.ToInt32(reader["depth"]));
            }

            depths.Sort();
            IEnumerable<int> distinctDepths = depths.Distinct();
            dbConnection.Close();
            return distinctDepths.ToList();
        }

        static public List<int> GetAvailableHeight()
        {
            List<int> heights = new List<int>();

            /*Start connection DataBase*/
            dbConnection.Open();

            //string sql = "SELECT * FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference='5', `width`=" + Convert.ToString(width) + ", `depth`=" + Convert.ToString(depth);
            string sql = "SELECT `height` FROM `CupboardConstraint` WHERE `CupboardConstraint`.`FK_Reference`='7'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["height"]));
            }

            heights.Sort();
            IEnumerable<int> distinctHeights = heights.Distinct();
            dbConnection.Close();
            return distinctHeights.ToList();
        }

        static public List<string> GetAvailableHPaneColor(int width, int depth)
        {
            List<string> colors = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT `Name` FROM `Component`" +
                         "INNER JOIN `Color` ON Component.FK_Color = Color.PK_IDColor " + 
                         "WHERE `FK_Reference`= '5' AND `width`="+ Convert.ToString(width) +" AND `depth`=" + Convert.ToString(depth);

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            colors.Sort();
            IEnumerable<string> distinctColors = colors.Distinct();
            dbConnection.Close();
            return distinctColors.ToList();
        }

        static public List<string> GetAvailableDoorStyle(int width, int height)
        {
            List<string> colors = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT `Name` FROM `Component`" +
                 "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                 "WHERE `FK_Reference`='6' AND `width`=" + Convert.ToString(width) + " AND `height`=" + Convert.ToString(height);

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            colors.Sort();
            IEnumerable<string> distinctColors = colors.Distinct();
            dbConnection.Close();
            return distinctColors.ToList();
        }

        static public List<string> GetAvailableVPaneColor(int width, int depth, int height)
        {
            List<string> colors = new List<string>();
            List<string> colorsOnly = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `Component` " +
                 "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                 "WHERE `FK_Reference`='4' AND `depth`=" + Convert.ToString(depth) + " AND `height`=" + Convert.ToString(height) +
                 " OR `FK_Reference`='3' AND `width`=" + Convert.ToString(width) + " AND `height`=" + Convert.ToString(height);

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            colors.Sort();
            int count = 0;
            foreach (string i in colors)
            {
                if (!colorsOnly.Contains(i))
                    colorsOnly.Add(i);
                else
                {
                    count += 1;
                }
            }

            if (colors.Count()/2 == count)
            {
                IEnumerable<string> distinctColors = colors.Distinct();
                dbConnection.Close();
                return distinctColors.ToList();
            }
            else
            {
                return new List<string>();
            }
            
        }

        static public List<string> GetAvailableSteelCornerColor(int height)
        {
            List<string> colors = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `Component` " +
                 "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                 "WHERE `FK_Reference`='1' AND `height` >=" + Convert.ToString(height);

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            colors.Sort();
            IEnumerable<string> distinctColors = colors.Distinct();
            dbConnection.Close();
            return distinctColors.ToList();
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
    }
}
