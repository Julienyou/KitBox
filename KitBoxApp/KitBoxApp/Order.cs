using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Order
    {
        private string id = null;
        private int totalPrice = 0;
        private Customer customer;
        private Employee employee = null;

        /*classes not yet created*/
        //private List<Component> components;
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

        public Customer Customer
        {
            get => Customer;
        }

        public Employee Employee
        {
            get => employee;
        }


        /*Functions if we created an Order*/
        public void SetCustomer(int id, string firstName, string lastName, string street, string town)
        {
            customer = new Customer(id, firstName, lastName, street, town);
        }        

        public void SetEmployee(int id, string firstName, string lastName)
        {
            employee = new Employee(id, firstName, lastName);
        }
    }
}
