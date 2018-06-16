using KitBox.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace StockKeeperApp.ViewModel
{
    public class InventoryViewModel
    {
        #region Properties
        public JArray Components { get; private set; }
        #endregion

        #region Constructor
        public InventoryViewModel()
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KitBox\db.sqlite;Version=3;");
            Components = Utils.GetInventory();

            foreach(JObject c in Components)
            {
                c.PropertyChanged += OnComponentChanged;
            }
        }
        #endregion

        #region Events
        private void OnComponentChanged (object sender, EventArgs e)
        {
            JObject component = (JObject)sender;
            Utils.UpdateInventory((string)component["Code"],(int)component["Quantity"]);
        }
        #endregion
    }
}
