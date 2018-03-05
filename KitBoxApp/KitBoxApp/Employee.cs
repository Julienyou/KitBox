using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    class Employee
    {
        private int id;
        private string firstName;
        private string lastName;

        public Employee(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
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
    }
}
