using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KitBox.Core.Constraint
{
    public static class ConstraintBuilder
    {
        #region Attributes
        private static SQLiteConnection dbConnection = Utils.DBConnection;
        #endregion

        #region methods
        public static int GetMaximumHeight()
        {
            
            string sql = "SELECT MAX(Height) FROM Component WHERE FK_Reference=1";
            int result = 0;
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            if(reader.Read())
            {

                result = reader.GetInt32(0);
            }
            dbConnection.Close();
            return result;
        }

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
            IEnumerable<int> distinctWidths = widths.Distinct();

            dbConnection.Close();
            return distinctWidths.ToList();
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
            colors.Insert(0, "None");
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
            IEnumerable<string> distinctColors = colors.Distinct();
            dbConnection.Close();
            return distinctColors.ToList();

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
        #endregion
    }
}

