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
        private static SQLiteConnection dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");


        public static List<int> BuildWidthsList()
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

        public static List<int> GetAvailableDepth(int width)
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

        public static List<int> GetAvailableHeight()
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

        public static List<string> GetAvailableHPaneColor(int width, int depth)
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

        public static List<string> GetAvailableDoorStyle(int width, int height)
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

        public static List<string> GetAvailableVPaneColor(int width, int depth, int height)
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

        public static List<string> GetAvailableSteelCornerColor(int height)
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






        public static CupboardConstraint BuildCupboardConstraint()
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

            sql = "SELECT * FROM `CupboardConstraint` WHERE `FK_Reference`='1'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                widths.Add(Convert.ToInt32(reader["width"]));
                depths.Add(Convert.ToInt32(reader["depth"]));
            }

            IEnumerable<int> distinctWidths = widths.Distinct();
            IEnumerable<int> distinctHeights = heights.Distinct();
            IEnumerable<int> distinctDepths = depths.Distinct();

            maxHeight = heights.Max();

            /*End connection DataBase*/
            dbConnection.Close();

            return new CupboardConstraint(distinctDepths.ToList(), distinctWidths.ToList(), null, maxHeight);
        }

        public static int GetMaxHeight()
        {
            int maxHeight;
            List<int> heights = new List<int>();

            /*Start connection DataBase*/
            dbConnection.Open();
            string sql = "SELECT `height` FROM `CupboardConstraint` WHERE `FK_Reference`='1'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["height"]));
            }

            IEnumerable<int> distinctHeights = heights.Distinct();

            maxHeight = heights.Max();

            /*End connection DataBase*/
            dbConnection.Close();

            return maxHeight;
        }

        public static BoxConstraint BuildBoxConstraint()
        {
            List<int> heights = new List<int>();
            List<string> vColors = new List<string>();
            List<string> hColors = new List<string>();
            List<int> widths = new List<int>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT `height` FROM `CupboardConstraint` WHERE `FK_Reference`='4'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                heights.Add(Convert.ToInt32(reader["height"]));
            }

            sql = "SELECT `Name` FROM `Component` " +
                "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                "WHERE `FK_Reference`='4'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                vColors.Add(reader["Name"].ToString());
            }

            sql = "SELECT `Name` FROM `Component` " +
                "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                "WHERE `FK_Reference`='5'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                hColors.Add(reader["Name"].ToString());
            }

            IEnumerable<string> distinctHColors = hColors.Distinct();
            IEnumerable<string> distinctVColors = vColors.Distinct();
            IEnumerable<int> distinctHeights = heights.Distinct();
            /*End connection DataBase*/
            dbConnection.Close();

            return new BoxConstraint(distinctHeights.ToList(), distinctVColors.ToList(), distinctHColors.ToList());
        }

        public static DoorConstraint BuildDoorConstraint()
        {
            List<Tuple<int, int>> doorDimensions = new List<Tuple<int, int>>();
            List<string> colors = new List<string>();

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "SELECT * FROM `CupboardConstraint` WHERE `FK_Reference`='6'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                doorDimensions.Add(new Tuple<int, int>(Convert.ToInt32(reader["Height"]), Convert.ToInt32(reader["Width"])));
            }

            sql = "SELECT `Name` FROM `Component` " +
                "INNER JOIN `Color` ON Component.FK_Color=Color.PK_IDColor " +
                "WHERE `FK_Reference`='6'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                colors.Add(reader["Name"].ToString());
            }

            IEnumerable<Tuple<int,int>> distinctDoorDimensions = doorDimensions.Distinct();
            IEnumerable<string> distinctColors = colors.Distinct();

            /*End connection DataBase*/
            dbConnection.Close();

            return new DoorConstraint(distinctColors.ToList(), distinctDoorDimensions.ToList());
        }
    }
}
