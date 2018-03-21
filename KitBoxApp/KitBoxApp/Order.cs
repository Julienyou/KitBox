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
        private string id = null;
        private int totalPrice = 0;
        private Customer customer = null;
//      private List<Component> components;

        /*classes not yet created*/
        //private State state; 

        public Order(string id)
        {
            this.id = id;
        }

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
            get => Customer;
        }

 /*       public List<Component> Components
        {
            get => components;
        }
*/
        public void ComputePrice()
        {
            /*Class component not yet created*/

            /*
            foreach (Component component in components)
            {
                totalPrice += component.Price;
            }
            */
        }


        /*Functions if we created an Order*/
        public void SetCustomer(string email, string firstName, string lastName, string street, string town)
        {
            customer = new Customer(email, firstName, lastName, street, town);
        }
/*
        public void AddComponent(Component component)
        {
            components.Add(component);
        }
*/
    }
}
