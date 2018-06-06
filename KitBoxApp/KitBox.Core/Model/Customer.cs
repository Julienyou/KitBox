using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBox.Core.Model
{
    public class Customer
    {
        #region Attributes
        private string email;
        private string firstName;
        private string lastName;
        private string street;
        private string town;
        #endregion

        #region Constructor
        public Customer(string email, string firstName, string lastName, string street, string town)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.town = town;
        }
        #endregion

        #region Properties
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

        #endregion
    }
}
