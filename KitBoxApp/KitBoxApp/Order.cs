using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KitBoxApp
{
    public class Order
    {

        #region Attributes

        private string id = null;
        private int totalPrice = 0;
        private Customer customer = null;
        private string state;

        private List<Dictionary<string, string>> components = new List<Dictionary<string, string>>();

        #endregion
        #region Constructors

        public Order(string id)
        {
            this.id = id;
        }

        #endregion
        #region Getters and Setters

        public string Id
        {
            get => id;

            set
            {
                id = value;
            }
        }

        public int TotalPrice
        {
            get => totalPrice;

            set
            {
                totalPrice = value;
            }
        }

        public Customer Customer
        {
            get => customer;

        }

        public string State
        {
            get => state;

            set
            {
                state = value;
            }
        }

        public List<Dictionary<string, string>> Components { get => components; set => components = value; }


        /*Functions if we created an Order*/
        public void SetCustomer(string email, string firstName, string lastName, string street, string town)
        {
            customer = new Customer(email, firstName, lastName, street, town);
        }

        #endregion
        #region Methods

        //---Methods
        public void ComputePrice()
        {
            foreach (Dictionary <string, string> component in components)
            {
                string value;
                if(component.TryGetValue("price", out value))
                {
                    totalPrice += Int32.Parse(value);
                }
            }
        }
    }

        #endregion
}
