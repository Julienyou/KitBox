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
            Order = order;
        }
        #endregion
    }
}
