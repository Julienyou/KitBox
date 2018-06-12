﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using KitBox.Core.Model;
using System.Collections.ObjectModel;
using KitBox.Core.Enum;
using System.Runtime.CompilerServices;

namespace KitBox.Core
{
    public static class Utils
    {
        static private SQLiteConnection dbConnection = new SQLiteConnection("Data Source="+ Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KitBox\db.sqlite;Version=3;");

        [MethodImpl(MethodImplOptions.Synchronized)]
        static public void UpdateRemnantSale(string Id, double price)
        {
            string sql = "UPDATE 'Order' SET RemnantSale = @price WHERE PK_IDOrder = @Id";
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@price") { Value = price, });
            command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id, });
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        static public void UpdateStatus(string Id, PaymentStatus state)
        {
            string sql = "UPDATE 'Order' SET FK_State = @state WHERE PK_IDOrder = @Id";
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@state") { Value = (int)state, });
            command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id, });
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        static public void UpdatePreparationStatus(string Id, PreparationStatus state)
        {
            string sql = "UPDATE 'Order' SET PrepState = @state WHERE PK_IDOrder = @Id";
            dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@state") { Value = (int)state, });
            command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id, });
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

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

            /*If the customer and the id to order are good*/
            string sql = "insert into `Order` ('FK_Customer', 'FK_State', 'RemnantSale', 'TotalPrice', 'PrepState')" +
                         "values ('" + customer.Email + "','" + "1" + "','" +
                         order.RemnantSale.ToString() + "','" + order.TotalPrice.ToString() + "','4'"+")";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            /*Recuperation of the last order's id*/
            sql = "SELECT MAX(PK_IDOrder) as max " +
                  "FROM `Order`";

            command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                order.Id = reader["max"].ToString();
            }


            foreach (Dictionary<string, string> component in order.Components)
            {
                /*Add data in OrderComponentLink for each component located in components*/
                sql = "insert into `OrderComponentLink` ('FK_Order', 'FK_Component', 'Quantity')" +
                      "values ('" + order.Id + "','" + component["code"] + "','" + component["quantity"] + "')";

                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();


                /*Remove stock in Component table*/
                sql = "SELECT * " +
                      "FROM `Component` " +
                      "WHERE `Component`.`Code`='" + component["code"] + "'";

                command = new SQLiteCommand(sql, dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int newStock = Convert.ToInt32(reader["Stock"]);
                    newStock -= Convert.ToInt32(component["quantity"]);

                    sql = "UPDATE `Component`" +
                          "SET Stock='" + newStock.ToString() + "'" +
                          "WHERE `Component`.`Code`='" + component["code"] + "'";

                    command = new SQLiteCommand(sql, dbConnection);
                    command.ExecuteNonQuery();

                }
            }

            /*End connection DataBase*/
            dbConnection.Close();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        static public ObservableCollection<Order> ImportAllOrders()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            List<string> ids = new List<string>();
            string sql = "SELECT PK_IDOrder FROM 'Order'";

            dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                ids.Add(reader["PK_IDOrder"].ToString());
            }
            dbConnection.Close();

            foreach(string id in ids)
            {
                orders.Add(ImportFromDatabase(id));
            }
            return orders;
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
            Order order = new Order();
            order.Id = id;

            /*Start connection DataBase*/
            dbConnection.Open();


            /*Recuperation to customer and TotalPrice data*/
            string sql = "SELECT FK_State, Pk_Email, Firstname, Lastname, Street, Town, TotalPrice, RemnantSale, PrepState " +
                         "FROM `Order`" +
                         "INNER JOIN Customer ON `Order`.`FK_Customer`=`Customer`.`PK_Email`" +
                         "WHERE `Order`.`PK_IDOrder`='" + order.Id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.Customer = new Customer(reader["Pk_Email"].ToString(), reader["Firstname"].ToString(),
                                  reader["Lastname"].ToString(), reader["Street"].ToString(),
                                  reader["Town"].ToString());

                order.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);

                order.RemnantSale = Convert.ToInt32(reader["RemnantSale"]);

                order.State = (PaymentStatus)reader.GetInt32(0);
                order.PreparationState = (PreparationStatus)reader.GetInt32(8);
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
                KitComposer.AddComponent(order, new Dictionary<string, string> {
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

        /// <summary>
        ///     Allows to fetch informations for components such as the code and the price.
        /// </summary>
        /// <param name="components">List of components that need to be looked up in the database</param>
        static public void FetchFromDataBase(List<Dictionary<string, string>> components)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            foreach (Dictionary<string, string> component in components)
            {
                List<string> listjoin = new List<string>();
                string sql = "SELECT * " +
                         "FROM ComponentData " +
                         "WHERE ";

                foreach (KeyValuePair<string,string> criteria in component)
                {
                    if (!criteria.Key.Equals("quantity"))
                    {
                        listjoin.Add(criteria.Key + "='" + criteria.Value + "'");
                    }
                }
                sql += String.Join(" AND ", listjoin);

                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    component.Add("code", reader["code"].ToString());
                    component.Add("price", reader["price"].ToString());
                }
            }

            /*End connection DataBase*/
            dbConnection.Close();
        }
        /// <summary>
        ///     Retrieves the smallest available steelcorner lenght form the database
        /// </summary>

        static public string GetCornersLength(string color, int minheight)
        {
            /*Start connection DataBase*/
            dbConnection.Open();

            string length = "";
            string sql =
                "SELECT MIN(height) AS height " +
                "FROM `ComponentData` " +
                "WHERE reference='Cornières' AND color='" + color + "' AND height>'" + minheight.ToString() + "'";

            /*
                "SELECT * " +
                "FROM ComponentData " +
                "WHERE reference = 'Cornières' AND Color = " + color + " AND height >  " + minheight +
                "ORDER BY ASC";
             */


            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                length = reader["height"].ToString();
            }

            /*End connection DataBase*/
            dbConnection.Close();

            return length;
        }

        /// <summary>
        ///     This method searchs all components which have their stock under the minimum stock
        /// </summary>
        /// <returns>The component's list which need to restock</returns>
        static public List<Dictionary<string, List<Dictionary<string, string>>>> Restock()
        {
            List<Dictionary<string, List<Dictionary<string,string>>>> listStock = new List<Dictionary<string, List<Dictionary<string, string>>>>();

            string sql = "SELECT `Reference`.`Name` AS ref, `Component`.`Code` AS code, `Component`.`Stock` AS stock, " +
                         "`Component`.`StockMinimum` AS stockmin, `Supplier`.`Name` as nameSupp, " +
                         "LinkSupplierComponent.SupplierPrice AS price, LinkSupplierComponent.SupplierDelay AS delay " +
                         "FROM `Component` " +
                         "INNER JOIN `Reference` ON `Component`.`FK_Reference`=`Reference`.`PK_IDRef` " +
                         "INNER JOIN `LinkSupplierComponent` ON `Component`.`Code`=`LinkSupplierComponent`.`FK_Component` " +
                         "INNER JOIN `Supplier` ON `LinkSupplierComponent`.`FK_Suppliers`=`Supplier`.`PK_IDSupplier` " +
                         "WHERE `Component`.`Stock`<=`Component`.`StockMinimum`";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                /*if the code is already in the list*/
                if (listStock.Exists(x => x.ContainsKey(reader["code"].ToString()))) {
                    foreach (Dictionary<string, List<Dictionary<string, string>>> component in listStock)
                    {
                        foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in component)
                        {
                            if (kvp.Key == reader["code"].ToString()) {
                                kvp.Value.Add( new Dictionary<string, string> {
                                    { "nameSupp", reader["nameSupp"].ToString() },
                                    { "price", reader["price"].ToString() },
                                    { "delay", reader["delay"].ToString() }
                                });
                            }
                        }
                    }
                }
                else {
                    List<Dictionary<string, string>> listData = new List<Dictionary<string, string>>();

                    listData.Add(new Dictionary<string, string> {
                        { "ref", reader["ref"].ToString() },
                        { "code", reader["code"].ToString() },
                        { "stock", reader["stock"].ToString() },
                        { "stockmin", reader["stockmin"].ToString() }
                    });

                    listData.Add(new Dictionary<string, string> {
                        { "nameSupp", reader["nameSupp"].ToString() },
                        { "price", reader["price"].ToString() },
                        { "delay", reader["delay"].ToString() }
                    });

                    listStock.Add(new Dictionary<string, List<Dictionary<string, string>>>{
                        { reader["code"].ToString(), listData }
                    });
                }                
            }

            return listStock;
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

    }
}