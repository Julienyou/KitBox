using KitBox.Core.Model;
using Newtonsoft.Json.Linq;
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
        public JArray Components { get => JArray.FromObject(Order.Components);}
        #endregion

        #region constructor
        public BOMViewModel(Order order)
        {
            Order = order;
            Order.Components = new List<Dictionary<string, string>>();
            Order.Components.Add(new Dictionary<string, string>());
            Order.Components[0]["Id"] = "Id1";
            Order.Components[0]["Name"] = "Name1";
            Order.Components[0]["IsChecked"] = "False";
            Order.Components.Add(new Dictionary<string, string>());
            Order.Components[1]["Id"] = "Id2";
            Order.Components[1]["Name"] = "Name2";
            Order.Components[0]["IsChecked"] = "True";

        }
        #endregion
    }
}
