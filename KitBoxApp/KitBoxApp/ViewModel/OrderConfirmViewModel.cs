using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Model;

namespace KitBox.ViewModel
{
    public class OrderConfirmViewModel:IDataErrorInfo
    {

        #region Attributes
        private Order m_Order;
        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        #endregion

        #region ICommands
        public ICommand AddCustomerCommand
        {
            get
            {
                return new CommandHandler((x) => { m_Order.Customer = new Customer(Email,FirstName,LastName,Address,""); Utils.ExportToDatabase(m_Order); ((Window)x).Close(); }, true);
            }
        }

        public ICommand CancelOrderCommand
        {
            get
            {
                return new CommandHandler((x) => { ((Window)x).Close(); }, true);
            }
        }

        #endregion

        #region Constructor
        public OrderConfirmViewModel (Window window, Order order)
        {
            Address = "";
            m_Order = order;
        }
        #endregion


        #region DataErrorInfo members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (String.IsNullOrWhiteSpace(FirstName))
                        result = "First Name can not be empty";

                }

                if (columnName == "LastName")
                {
                    if (String.IsNullOrEmpty(LastName) || String.IsNullOrWhiteSpace(LastName))
                        result = "Last Name can not be empty";

                }

                if (columnName == "Email")
                {
                    if (Email == null)
                        result = "Email can not be empty";

                    try
                    {
                        new MailAddress(Email);
                    }
                    catch
                    {
                        result = "Enter a valid Email";
                    }

                }

                if (columnName == "Address")
                {
                    string[] s = Address.Split(',');
                    if (s.Count() != 2 | String.IsNullOrEmpty(s[0]) || String.IsNullOrWhiteSpace(s[0]) || String.IsNullOrEmpty(s[1]) || String.IsNullOrWhiteSpace(s[1]))
                        result = "Address must be: street , Town ";

                    if (String.IsNullOrEmpty(Address) || String.IsNullOrWhiteSpace(Address))
                        result = "Address can not be empty";
                }

                return result;
            }

        }
        #endregion
    }
}
