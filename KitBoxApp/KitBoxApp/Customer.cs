using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Customer
    {
        private int id;
        private string firstName;
        private string lastName;
        private string street;
        private string town;

        public Customer(int id, string firstName, string lastName, string street, string town)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.town = town;
        }

        public int Id
        {
            get => id;

            set
            {
                id = value;
            }
        }

        public string FirstName
        {
            get => firstName;

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;

            set
            {
                lastName = value;
            }
        }

        public string Street
        {
            get => street;

            set
            {
                street = value;
            }
        }

        public string Town
        {
            get => town;

            set
            {
                town = value;
            }
        }
    }
}
