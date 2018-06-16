using KitBox.Core;
using KitBox.Core.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitBox.Core.Enum;
using System.Data.SQLite;

namespace StockKeeperApp.ViewModel
{
    public class BOMViewModel
    {


        #region Properties
        public Order Order{ get; set; }
        public JArray Components { get => JArray.FromObject(Order.Components);}
        #endregion

        #region ICommands
        public ICommand ValidateCommand
        {
            get
            {
                return new CommandHandler((x) => { Utils.UpdatePreparationStatus(Order.Id,PreparationStatus.Ready); ((Window)x).Close(); }, true);
            }
        }
        #endregion

        #region constructor
        public BOMViewModel(Order order)
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KitBox\db.sqlite;Version=3;");
            Order = order;
        }
        #endregion
    }
}
