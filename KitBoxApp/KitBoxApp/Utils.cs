using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace KitBoxApp
{
    static class Utils
    {
        static private SQLiteConnection dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");

        static public void ExportToDatabase(Order order)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            Customer customer = order.Customer;

            CheckCustomer(customer);
            CheckOrderId(order);

            /*If the customer and the id to order are good*/
            string sql = "insert into `Order` ('PK_IDOrder', 'FK_Customer', 'FK_State', 'TotalPrice')" +
                         "values ('" + order.Id + "','" + customer.Email + "','" + "1" + "','" + order.TotalPrice.ToString() + "')";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();


            /*Add data inOrderComponentLink for each component located in components*/
            foreach (Component component in order.Components)
            {
                sql = "insert into `OrderComponentLink` ('FK_Order', 'FK_Component')" +
                         "values ('" + order.Id + "','" + component.code + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }

            /*End connection DataBase*/
            dbConnection.Close();
        }

        static public Order ImportToDatabase(string id)
        {
            Order order = new Order(id);

            /*Start connection DataBase*/
            dbConnection.Open();


            /*Recuperation to customer and TotalPrice data*/
            string sql = "SELECT * FROM `Order`" +
                         "INNER JOIN Customer ON `Order`.`FK_Customer`=`Customer`.`PK_Email`" +
                         "WHERE `Order`.`PK_IDOrder`='" + order.Id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.SetCustomer(reader["Pk_Email"].ToString(), reader["Firstname"].ToString(),
                                        reader["Lastname"].ToString(), reader["Street"].ToString(),
                                        reader["Town"].ToString());

                order.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
            }


            /*Recuperation of components data*/
            sql = "SELECT * FROM `OrderComponentLink`" +
                  "INNER JOIN Component ON `OrderComponentLink`.`FK_Component`=`Component`.`Code`" +
                  "INNER JOIN Color ON `Component`.`FK_Color`=`Color`.`PK_IDColor`" +
                  "INNER JOIN Reference ON `Component`.`FK_Reference`=`Reference`.`PK_IDRef`" +
                  "WHERE OrderComponentLink.FK_Order='" + id + "'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.AddComponent(new Component(reader["FK_Reference"].ToString(), reader["Name"].ToString(),
                                                 reader["code"].ToString(), Convert.ToInt32(reader["Price"]),
                                                 4, true));
            }


            /*End connection DataBase*/
            dbConnection.Close();

            return order;
        }


        /*Private function*/
        static private void CheckCustomer(Customer customer)
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

        static private void CheckOrderId(Order order)
        {
            /*Verification if the id of order exists*/
            string sql = "select * from `Order` where PK_IDOrder='" + order.Id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read()) //if id does not exists
            {
                throw new Exception("This order id exists already");
            }
        }
    }
}
