using KitBox.Core.Enum;
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

        private string m_Id = null;
        private double m_TotalPrice = 0;
        private double m_RemnantSale = 0;
        private Customer m_Customer = null;
        private PaymentStatus m_State;
        private PreparationStatus m_PreparationState;
      

        private List<Dictionary<string, string>> components = new List<Dictionary<string, string>>();

        #endregion

        #region Properties

        public string Id
        {
            get => m_Id;

            set
            {
                m_Id = value;
                Notify("Id");
            }
        }

        public double TotalPrice
        {
            get => m_TotalPrice;

            set
            {
                m_TotalPrice = value;
                Notify("TotalPrice");
            }
        }

        public double RemnantSale
        {
            get => m_RemnantSale;

            set
            {
                m_RemnantSale = value;
                Notify("RemnantSale");
            }
        }

        public Customer Customer
        {
            get => m_Customer;
            set
            {
                m_Customer = value;
            }
        }

        public PaymentStatus State
        {
            get => m_State;

            set
            {
                m_State = value;
                Notify("State");
            }
        }

        public PreparationStatus PreparationState
        {
            get => m_PreparationState;
            set
            {
                m_PreparationState = value;
                Notify("PreparationState");
            }
        }

        public List<Dictionary<string, string>> Components
        {
            get => components;
            set { components = value; Notify("Components"); }
        }

        public bool IsInStock { get; private set; }

        #endregion

        #region Methods

        public void ComputePrice()
        {
            foreach (Dictionary<string, string> component in components)
            {
                m_TotalPrice += Convert.ToDouble(component["price"].Replace(".", ",")) * Convert.ToInt32(component["quantity"]);
            }
        }

        public void CheckStock()
        {
            foreach (Dictionary<string, string> component in components)
            {
                if (component["instock"] == "false")
                {
                    IsInStock = false;
                    break;
                }
            }

            IsInStock = true;
        }
    }

    #endregion
}
