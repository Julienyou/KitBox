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
            CheckOrderId();

            /*If the customer and the id to order are good*/
            string sql = "insert into `Order` ('PK_IDOrder', 'FK_Customer', 'FK_State', 'TotalPrice')" +
                         "values ('" + id + "','" + customer.Email + "','" + "1" + "','" + totalPrice.ToString() + "')";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();


            /*Add data inOrderComponentLink for each component located in components*/
            int idLink = GetIdLink();

            foreach (Component component in components)
            {
                sql = "insert into `OrderComponentLink` ('PK_Link', 'FK_Order', 'FK_Component')" +
                         "values ('" + idLink.ToString() + "','" + id + "','" + component.code + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                idLink++;
            }

            dbConnection.Close();            
        }

        private void CheckCustomer()
        {
            /*Verification if the customer exists*/
            string sql = "select * from Customer where PK_Email='" + customer.Email + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (!reader.Read()) //Customer creation if he does not exists
            {
                sql = "insert into Customer ('PK_Email', 'Firstname', 'Lastname', 'Street', 'Town')" +
                          "values ('" + customer.Email + "','" + customer.FirstName + "','" + customer.LastName +
                          "','" + customer.Street + "','" + customer.Town + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
        }

        private void CheckOrderId()
        {
            /*Verification if the id of order exists*/
            string sql = "select * from `Order` where PK_IDOrder='" + id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read()) //if id does not exists
            {
                throw new Exception("This order id exists already");
            }
        }

        private int GetIdLink()
        {
            /*Give the last PK_Link of the table OrderComponentLink*/

            string sql = "selec max(PK_Link) from OrderComponentLink";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return Convert.ToInt32(reader["PK_link"]) + 1;
            }
            else
            {
                throw new Exception("Error, nothing PK_ID finds");
            }

        }
    }
}
