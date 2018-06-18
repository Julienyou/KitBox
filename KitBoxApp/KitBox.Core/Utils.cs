using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using KitBox.Core.Model;
using System.Collections.ObjectModel;
using KitBox.Core.Enum;
using Newtonsoft.Json.Linq;

namespace KitBox.Core
{
    public static class Utils
    {
        public static SQLiteConnection DBConnection { get; set; }

        private static Object dblock = new Object();

        public static void UpdateInventory(string code, int quantity)
        {
            string sql = "UPDATE Component SET Stock=@stock WHERE Code=@code";
            lock(dblock)
            {
                DBConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@stock") { Value = quantity });
                command.Parameters.Add(new SQLiteParameter("@code") { Value = code });
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }
        public static JArray GetInventory()
        {
            JArray components = new JArray();
            string sql = "SELECT Code, Stock FROM Component";
            lock (dblock)
            {
                DBConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    JObject c = new JObject();
                    c["Code"] = reader.GetString(0);
                    c["Quantity"] = reader.GetInt32(1);
                    components.Add(c);
                }
                DBConnection.Close();
            }
            
            return components;
        }

        public static void UpdateRemnantSale(string Id, double price)
        {
            string sql = "UPDATE 'Order' SET RemnantSale = @price WHERE PK_IDOrder = @Id";

            lock (dblock)
            {
                DBConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@price") { Value = price });
                command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id });
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }

        public static void UpdateStatus(string Id, PaymentStatus state)
        {
            string sql = "UPDATE 'Order' SET FK_State = @state WHERE PK_IDOrder = @Id";

            lock (dblock)
            {
                DBConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@state") { Value = (int)state });
                command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id });
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
        }

        public static void UpdatePreparationStatus(string Id, PreparationStatus state)
        {
            string sql = "UPDATE 'Order' SET PrepState = @state WHERE PK_IDOrder = @Id";

            lock (dblock)
            {
                DBConnection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@state") { Value = (int)state });
                command.Parameters.Add(new SQLiteParameter("@Id") { Value = Id });
                command.ExecuteNonQuery();
                DBConnection.Close();
            }
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
        public static void ExportToDatabase(Order order)
        {
            lock (dblock)
            {

                /*Start connection DataBase*/
                DBConnection.Open();

                Customer customer = order.Customer;

                CheckCustomer(customer);

                /*If the customer and the id to order are good*/

                string sql = "INSERT INTO `Order` ('FK_Customer', 'FK_State', 'RemnantSale', 'TotalPrice', 'PrepState')" +
                             "VALUES (@CustomerId, @StateId, @RemSale, @TotalPrice, @PrepState)";

                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@CustomerId") { Value = customer.Email });
                command.Parameters.Add(new SQLiteParameter("@StateId") { Value = (int)PaymentStatus.Unpayed });
                command.Parameters.Add(new SQLiteParameter("@RemSale") { Value = order.RemnantSale });
                command.Parameters.Add(new SQLiteParameter("@TotalPrice") { Value = order.RemnantSale });
                command.Parameters.Add(new SQLiteParameter("@PrepState") { Value = (int)PreparationStatus.NotProcessed });

                command.ExecuteNonQuery();

                /*Recuperation of the last order's id*/
                sql = "SELECT MAX(PK_IDOrder) as max " +
                      "FROM `Order`";

                command = new SQLiteCommand(sql, DBConnection);
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

                    command = new SQLiteCommand(sql, DBConnection);
                    command.ExecuteNonQuery();


                    /*Remove stock in Component table*/
                    sql = "SELECT * " +
                          "FROM `Component` " +
                          "WHERE `Component`.`Code`='" + component["code"] + "'";

                    command = new SQLiteCommand(sql, DBConnection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int newStock = Convert.ToInt32(reader["Stock"]);
                        newStock -= Convert.ToInt32(component["quantity"]);

                        sql = "UPDATE `Component`" +
                              "SET Stock='" + newStock.ToString() + "'" +
                              "WHERE `Component`.`Code`='" + component["code"] + "'";

                        command = new SQLiteCommand(sql, DBConnection);
                        command.ExecuteNonQuery();

                    }
                }

                /*End connection DataBase*/
                DBConnection.Close();

            }
        }

        public static ObservableCollection<Order> ImportAllOrders()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            List<string> ids = new List<string>();
            string sql = "SELECT PK_IDOrder FROM 'Order'";

            lock (dblock)
            {
                DBConnection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ids.Add(reader["PK_IDOrder"].ToString());
                }
                DBConnection.Close();
            }

            foreach (string id in ids)
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
        /// [MethodImpl(MethodImplOptions.Synchronized)]
        public static Order ImportFromDatabase(string id)
        {
            Order order = new Order();
            order.Id = id;

            /*Start connection DataBase*/
            lock (dblock)
            {
                DBConnection.Open();


                /*Recuperation to customer and TotalPrice data*/
                string sql = "SELECT FK_State, Pk_Email, Firstname, Lastname, Street, Town, TotalPrice, RemnantSale, PrepState " +
                             "FROM `Order`" +
                             "INNER JOIN Customer ON `Order`.`FK_Customer`=`Customer`.`PK_Email`" +
                             "WHERE `Order`.`PK_IDOrder`='" + order.Id + "'";

                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    order.Customer = new Customer(reader["Pk_Email"].ToString(), reader["Firstname"].ToString(),
                                      reader["Lastname"].ToString(), reader["Street"].ToString(),
                                      reader["Town"].ToString());

                    order.TotalPrice = reader.GetDouble(6);

                    order.RemnantSale = reader.GetDouble(7);

                    order.State = (PaymentStatus)reader.GetInt32(0);
                    order.PreparationState = (PreparationStatus)reader.GetInt32(8);
                }


                /*Recuperation of components data in ImportData view*/
                sql = "SELECT * " +
                      "FROM `ImportData` " +
                      "WHERE `ImportData`.`FK_Order`='" + id + "'";

                command = new SQLiteCommand(sql, DBConnection);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string instock = "false";

                    if ((Convert.ToInt32(reader["stock"]) - Convert.ToInt32(reader["quantity"])) >= 0)
                    {
                        instock = "true";
                    }

                    KitComposer.AddComponent(order, new Dictionary<string, string> {
                        { "code", reader["code"].ToString() },
                        { "reference", reader["ref_name"].ToString() },
                        { "color", reader["color"].ToString()},
                        { "height", reader["height"].ToString() },
                        { "width",  reader["width"].ToString() },
                        { "depth", reader["depth"].ToString() },
                        { "quantity", reader["quantity"].ToString() },
                        { "instock", instock }
                    });
                }


                /*End connection DataBase*/
                DBConnection.Close();
            }

            return order;
        }


        /// <summary>
        ///     This method permits to change the order's state.
        /// </summary>
        /// <param name="id">This is the order's id</param>
        /// <param name="state">This is the state's id</param>
        public static void ChangeState(string id, string state)
        {
            lock (dblock)
            {

                /*Start connection DataBase*/
                DBConnection.Open();

                string sql = "UPDATE `Order` " +
                             "SET FK_State='" + state + "'" +
                             "WHERE `Order`.`PK_IDOrder`='" + id + "'";

                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);

                command.ExecuteNonQuery();

                /*End connection DataBase*/
                DBConnection.Close();
            }
        }

        /// <summary>
        ///     Allows to fetch informations for components such as the code and the price.
        /// </summary>
        /// <param name="components">List of components that need to be looked up in the database</param>
        public static void FetchFromDataBase(List<Dictionary<string, string>> components)
        {
            lock (dblock)
            {
                /*Start connection DataBase*/
                DBConnection.Open();

                foreach (Dictionary<string, string> component in components)
                {
                    List<string> listjoin = new List<string>();
                    string sql = "SELECT * " +
                             "FROM ComponentData " +
                             "WHERE ";

                    foreach (KeyValuePair<string, string> criteria in component)
                    {
                        if (!criteria.Key.Equals("quantity") && !criteria.Key.Equals("instock"))
                        {
                            listjoin.Add(criteria.Key + "='" + criteria.Value + "'");
                        }
                    }
                    sql += String.Join(" AND ", listjoin);

                    SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        component.Add("code", reader["code"].ToString());
                        component.Add("price", reader["price"].ToString());

                        string instock = "false";
                        if ((Convert.ToInt32(reader["stock"]) - Convert.ToInt32(reader["quantity"])) >= 0)
                        {
                            instock = "true";
                        }

                        component.Add("instock", instock);    
                    }
                }

                /*End connection DataBase*/
                DBConnection.Close();
            }
        }
        /// <summary>
        ///     Retrieves the smallest available steelcorner lenght form the database
        /// </summary>

        public static string GetCornersLength(string color, int minheight)
        {
            string length = "";

            /*Start connection DataBase*/
            lock (dblock)
            {
                DBConnection.Open();

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


                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    length = reader["height"].ToString();
                }

                /*End connection DataBase*/
                DBConnection.Close();
            }
            return length;
        }

        /// <summary>
        ///     This method searchs all components which have their stock under the minimum stock
        /// </summary>
        /// <returns>The component's list which need to restock</returns>
        public static List<Dictionary<string, List<Dictionary<string, string>>>> Restock()
        {
            List<Dictionary<string, List<Dictionary<string, string>>>> listStock = new List<Dictionary<string, List<Dictionary<string, string>>>>();

            lock (dblock)
            {
                DBConnection.Open();

                string sql = "SELECT `Reference`.`Name` AS ref, `Component`.`Code` AS code, `Component`.`Stock` AS stock, " +
                             "`Component`.`StockMinimum` AS stockmin, `Supplier`.`Name` as nameSupp, " +
                             "LinkSupplierComponent.SupplierPrice AS price, LinkSupplierComponent.SupplierDelay AS delay " +
                             "FROM `Component` " +
                             "INNER JOIN `Reference` ON `Component`.`FK_Reference`=`Reference`.`PK_IDRef` " +
                             "INNER JOIN `LinkSupplierComponent` ON `Component`.`Code`=`LinkSupplierComponent`.`FK_Component` " +
                             "INNER JOIN `Supplier` ON `LinkSupplierComponent`.`FK_Suppliers`=`Supplier`.`PK_IDSupplier` " +
                             "WHERE `Component`.`Stock`<=`Component`.`StockMinimum`";

                SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    /*if the code is already in the list*/
                    if (listStock.Exists(x => x.ContainsKey(reader["code"].ToString())))
                    {
                        foreach (Dictionary<string, List<Dictionary<string, string>>> component in listStock)
                        {
                            foreach (KeyValuePair<string, List<Dictionary<string, string>>> kvp in component)
                            {
                                if (kvp.Key == reader["code"].ToString())
                                {
                                    kvp.Value.Add(new Dictionary<string, string> {
                                        { "nameSupp", reader["nameSupp"].ToString() },
                                        { "price", reader["price"].ToString() },
                                        { "delay", reader["delay"].ToString() }
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
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

                DBConnection.Close();
            }

            return listStock;
        }

        /*Private function*/
        private static void CheckCustomer(Customer customer)
        {
            /*Verification if the customer exists*/
            string sql = "SELECT * FROM Customer WHERE PK_Email='" + customer.Email + "'";

            SQLiteCommand command = new SQLiteCommand(sql, DBConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (!reader.Read()) //Customer creation if he does not exists
            {
                sql = "INSERT INTO Customer ('PK_Email', 'Firstname', 'Lastname', 'Street', 'Town')" +
                          "VALUES (@email, @firstName, @lastName, @street, @town)";

                command = new SQLiteCommand(sql, DBConnection);
                command.Parameters.Add(new SQLiteParameter("@email") { Value = customer.Email });
                command.Parameters.Add(new SQLiteParameter("@firstName") { Value = customer.FirstName });
                command.Parameters.Add(new SQLiteParameter("@lastName") { Value = customer.LastName });
                command.Parameters.Add(new SQLiteParameter("@street") { Value = customer.Street });
                command.Parameters.Add(new SQLiteParameter("@town") { Value = customer.Town });

                command.ExecuteNonQuery();
            }
        }

    }
}
