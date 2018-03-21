﻿using System;
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

        /// <summary>
        ///     This method allows to create a order in Database.
        ///     First, we check if the customer exists, if he does not exists we creat it. Then
        ///         we look if the order's ID does not already exist.
        ///     Second, we create the order with the customer's email and the total price.
        ///     Third, we add the link of the order's id at the component's code in the
        ///         OrderComponentLink table.
        /// </summary>
        /// <param name="order">This is the order we want to push in database</param>
        static public void ExportToDatabase(Order order)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            Customer customer = order.Customer;

            CheckCustomer(customer);
            CheckOrderId(order);

            /*If the customer and the id to order are good*/
            string sql = "insert into `Order` ('PK_IDOrder', 'FK_Customer', 'FK_State', 'RemnantSale', 'TotalPrice')" +
                         "values ('" + order.Id + "','" + customer.Email + "','" + "1" + "','" + 
                         order.RemnantSale.Tostring() + order.TotalPrice.ToString() + "')";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();


            /*Add data inOrderComponentLink for each component located in components*/
            foreach (Dictionary<string, string> component in order.Components)
            {
                sql = "insert into `OrderComponentLink` ('FK_Order', 'FK_Component', 'Quantity')" +
                         "values ('" + order.Id + "','" + component["code"] + "','" + component["quantity"] + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }

            /*End connection DataBase*/
            dbConnection.Close();
        }


        /// <summary>
        ///     This method permits to pull a order in the database.
        ///     First, we create a Order object with the id wished.
        ///     Second, we recover the customer's data and the total price.
        ///     Third, we recover the component's data for create an list.
        /// </summary>
        /// <param name="id">Order's id</param>
        /// <returns>The method return the order who has been created</returns>
        static public Order ImportFromDatabase(string id)
        {
            Order order = new Order(id);

            /*Start connection DataBase*/
            dbConnection.Open();


            /*Recuperation to customer and TotalPrice data*/
            string sql = "SELECT * " +
                         "FROM `Order`" +
                         "INNER JOIN Customer ON `Order`.`FK_Customer`=`Customer`.`PK_Email`" +
                         "INNER JOIN State ON `Order`.`FK_State`=`State`.`PK_State`" +
                         "WHERE `Order`.`PK_IDOrder`='" + order.Id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.SetCustomer(reader["Pk_Email"].ToString(), reader["Firstname"].ToString(),
                                        reader["Lastname"].ToString(), reader["Street"].ToString(),
                                        reader["Town"].ToString());

                order.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);

                order.RemnantSale = reader["RemnantSale"].ToString();

                order.State = reader["Name"].ToString();
            }


            /*Recuperation of components data in ImportData view*/
            sql = "SELECT * " +
                  "FROM `ImportData` " +
                  "WHERE `ImportData`.`FK_Order`='" + id + "'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                /*Warning not instock*/
                order.AddComponent(new Dictionary<string, string> {
                    { "code", reader["code"].ToString() },
                    { "reference", reader["ref_name"].ToString() },
                    { "color", reader["color"].ToString()},
                    { "height", reader["height"].ToString() },
                    { "width",  reader["width"].ToString() },
                    { "depth", reader["depth"].ToString() },
                    { "quantity", reader["quantity"].ToString() }
                });
            }


            /*End connection DataBase*/
            dbConnection.Close();

            return order;
        }


        /// <summary>
        ///     This method permits to change the order's state.
        /// </summary>
        /// <param name="id">This is the order's id</param>
        /// <param name="state">This is the state's id</param>
        static public void ChangeState(string id, string state)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "UPDATE `Order` " +
                         "SET FK_State='" + state + "'" +
                         "WHERE `Order`.`PK_IDOrder`='" + id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            command.ExecuteNonQuery();

            /*End connection DataBase*/
            dbConnection.Close();
        }


        static public void FetchFromDataBase(List<Dictionary<string, string>> components)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            foreach (Dictionary<string, string> component in components)
            {
                List<string> listjoin = new List<string>();
                string sql = "SELECT * " +
                         "FROM ComponentData" +
                         "WHERE ";

                foreach (KeyValuePair<string,string> criteria in component)
                {
                    listjoin.Add(criteria.Key + "=" + criteria.Value);
                }
                sql += String.Join(" AND ", listjoin);

                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    component["code"] = reader["code"].ToString();
                    component["price"] = reader["price"].ToString();
                }
            }

            /*End connection DataBase*/
            dbConnection.Close();
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
