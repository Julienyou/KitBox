using KitBox;
using KitBox.Core;
using KitBox.Core.Model;
using KitBox.Core.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CounterApp
{

    class PaymentViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        #region Attributes
        private double m_Payment;
        private Order m_Order;
        private ObservableCollection<Order> m_Orders;
        private double m_RemainingPayment;
        private double m_TotalPayment;
        #endregion

        #region Property changed member
        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Properties

        /// <summary>
        /// Get or Set the amount that the client is paying
        /// </summary>
        public double Payment { get { return m_Payment; } set { m_Payment = value; Notify("Payment"); } }

        /// <summary>
        /// Get the amount of the order that is not yet payed
        /// </summary>
        public double RemainingPayment { get { return m_RemainingPayment; } }

        /// <summary>
        /// Get the price Of the order tha the client have to pay
        /// </summary>
        public double TotalPayment { get { return m_TotalPayment; } }

        #endregion

        #region ICommands
        public ICommand Pay
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    m_Order.RemnantSale -= Payment;
                    m_Order.RemnantSale = Math.Round(m_Order.RemnantSale,2);
                    Utils.UpdateRemnantSale(m_Order.Id, m_Order.RemnantSale);
                    if (m_Order.RemnantSale == 0)
                    {
                        Utils.UpdateStatus(m_Order.Id, PaymentStatus.Payed);
                    }
                    else
                    {
                        Utils.UpdateStatus(m_Order.Id, PaymentStatus.Prepaid);
                    }
                    ((Window)x).Close();
                }, true);
            }
        }


        public ICommand CancelOrder
        {
            get
            {
                return new CommandHandler((x) => { Utils.UpdateStatus(m_Order.Id, PaymentStatus.Canceled); ((Window)x).Close(); }, true);
            }
        }

        #endregion

        #region Constructor
        public PaymentViewModel(Order order, ObservableCollection<Order> orders)
        {
            m_Order = order;
            m_Orders = orders;
            m_Payment = 10;
            m_RemainingPayment = order.RemnantSale;
            m_TotalPayment = order.TotalPrice;
        }
        #endregion

        #region DataErrorInfo
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Payment")
                {
                    if (Payment > RemainingPayment)
                        result = "Arnaker va";

                    try
                    {
                        Convert.ToDouble(Payment);
                    }
                    catch
                    {
                        result = "";
                    }
                }

                return result;
            }

        }
        #endregion
    }
}
