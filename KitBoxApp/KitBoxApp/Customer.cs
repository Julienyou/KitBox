using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Customer
    {
        private string email;
        private string firstName;
        private string lastName;
        private string street;
        private string town;

        public Customer(string email, string firstName, string lastName, string street, string town)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.town = town;
        }

        public string Email
        {
            get => email;

            set
            {
                email = value;
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
