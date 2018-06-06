using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBox.Core.Model
{
    public class Order : INotifyPropertyChanged
    {
        #region Property changed member
        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Attributes

        private string id = null;
        private double totalPrice = 0;
        private int remnantSale = 0;
        private Customer customer = null;
        private string state;

        private List<Dictionary<string, string>> components = new List<Dictionary<string, string>>();

        #endregion

        #region Constructors

        public Order() {}

        #endregion

        #region Getters and Setters

        public string Id
        {
            get => id;

            set
            {
                id = value;
                Notify("Id");
            }
        }

        public double TotalPrice
        {
            get => totalPrice;

            set
            {
                totalPrice = value;
                Notify("TotalPrice");
            }
        }

        public int RemnantSale
        {
            get => remnantSale;

            set
            {
                remnantSale = value;
                Notify("RemnantSale");
            }
        }

        public Customer Customer
        {
            get => customer;
            set
            {
                customer = value;
            }
        }

        public string State
        {
            get => state;

            set
            {
                state = value;
                Notify("State");
            }
        }

        public List<Dictionary<string, string>> Components
        {
            get => components;
            set { components = value; Notify("Components"); }
        }

        #endregion

        #region Methods

        //---Methods
        public void ComputePrice()
        {
            foreach (Dictionary<string, string> component in components)
            {
                totalPrice += Convert.ToDouble(component["price"]) * Convert.ToDouble(component["quantity"]);
            }
        }
    }

        #endregion
}
