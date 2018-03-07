using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KitBoxApp
{
    class Order
    {
        private string id = null;
        private int totalPrice = 0;
        private Customer customer = null;
        private List<Component> components;

        /*Init variables for communicate with the DataBase*/
        private SQLiteConnection dbConnection;
        SQLiteCommand command;
        SQLiteDataReader reader;

        /*classes not yet created*/
        //private State state; 

        public Order(string id)
        {
            this.id = id;

            /*Start connection DataBase*/
            dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            dbConnection.Open();
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

        public void ExportToDatabase()
        {
            CheckCustomer();

            dbConnection.Close();
        }

        private void CheckCustomer()
        {
            /*Verification if the customer exists*/
            string sql = "select * from Customer where PK_Email='" + customer.Email + "'";

            command = new SQLiteCommand(sql, dbConnection);
            reader = command.ExecuteReader();

            if (!reader.Read())
            {
                sql = "insert into Customer ('PK_Email', 'Firstname', 'Lastname', 'Street', 'Town')" +
                          "values ('" + customer.Email + "','" + customer.FirstName + "','" + customer.LastName +
                          "','" + customer.Street + "','" + customer.Town + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
        }
    }
}
