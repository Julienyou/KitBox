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
        private string m_Email;
        private string m_FirstName;
        private string m_LastName;
        private string m_Street;
        private string m_Town;
        #endregion

        #region Constructor
        public Customer(string email, string firstName, string lastName, string street, string town)
        {
            this.m_Email = email;
            this.m_FirstName = firstName;
            this.m_LastName = lastName;
            this.m_Street = street;
            this.m_Town = town;
        }
        #endregion

        #region Properties
        public string Email
        {
            get => m_Email;

            set
            {
                m_Email = value;
            }
        }

        public string FirstName
        {
            get => m_FirstName;

            set
            {
                m_FirstName = value;
            }
        }

        public string LastName
        {
            get => m_LastName;

            set
            {
                m_LastName = value;
            }
        }

        public string Street
        {
            get => m_Street;

            set
            {
                m_Street = value;
            }
        }

        public string Town
        {
            get => m_Town;

            set
            {
                m_Town = value;
            }
        }

        #endregion
    }
}
