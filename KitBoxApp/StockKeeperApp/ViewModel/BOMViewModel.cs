using KitBox.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKeeperApp.ViewModel
{
    public class BOMViewModel
    {

        #region Properties
        public Order Order{ get; set; }
        #endregion

        #region constructor
        public BOMViewModel(Order order)
        {
            Order = order;
            Order.Components = new List<Dictionary<string, string>>();
            Order.Components.Add(new Dictionary<string, string>());
            Order.Components[0]["Id"] = "Id1";
            Order.Components[0]["Name"] = "Name1";
        }
        #endregion
    }
}
