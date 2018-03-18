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

        /// <summary>
        ///     This method permits to create a order in Database.
        ///     First, we check if the customer exists, if he does not exists we creat him. After 
        ///         we look at if the order's ID does not already exist.
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


        /// <summary>
        ///     This method permits to pull a order in the database.
        ///     First, we create a Order object with the id wished.
        ///     Second, we recover the customer's data and the total price.
        ///     Third, we recover the component's data for create an list.
        /// </summary>
        /// <param name="id">This is the order's id</param>
        /// <returns>The method return the order who has been created</returns>
        static public Order ImportToDatabase(string id)
        {
            Order order = new Order(id);

            /*Start connection DataBase*/
            dbConnection.Open();


            /*Recuperation to customer and TotalPrice data*/
            string sql = "SELECT * " +
                         "FROM `Order`" +
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


            /*Recuperation of components data in ImportData view*/
            sql = "SELECT * " +
                  "FROM `ImportData` " +
                  "WHERE `ImportData`.`FK_Order`='" + id + "'";

            command = new SQLiteCommand(sql, dbConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.AddComponent(new Component(reader["ref_name"].ToString(), reader["color"].ToString(),
                                                 reader["code"].ToString(), Convert.ToInt32(reader["price"]),
                                                 4, true));
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
            /*Change the Order State in database*/

            /*Start connection DataBase*/
            dbConnection.Open();

            string sql = "UPDATE `Order` " +
                         "SET State='" + state + "'" +
                         "WHERE `Order`.`PK_IDOrder`='" + id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            
            command.ExecuteNonQuery();
        }

        static public void ComposeOrder(Order order, Cupboard cupboard)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Cornières" },
                { "color", cupboard.SteelCornerColor },
                { "height", cupboard.GetHeight().ToString() },
            });

            foreach (Box box in cupboard.Boxes)
            {
                AddCrosspieceAr(order, box);
                AddCrosspieceAv(order, box);
                AddCrosspieceGD(order, box);
                AddMount(order, box);
                AddPaneAr(order, box);
                AddPaneGD(order, box);
                AddPaneHB(order, box);
            }
        }


        static private void AddPaneGD(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Panneau GD" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "depth", box.Cupboard.Depth.ToString()}
            });
        }

        static private void AddPaneHB(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Panneau HB" },
                { "color", box.HorizontalColor},
                { "depth", box.Cupboard.Depth.ToString()},
                { "width", box.Cupboard.Width.ToString()}
            });
        }

        static private void AddPaneAr(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Panneau Ar" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "width", box.Cupboard.Width.ToString()}
                });
        }

        static private void AddMount(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Tasseau" },
                { "height", box.Height.ToString()},
            });
        }

        static private void AddCrosspieceGD(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Traverse GD" },
                { "depth", box.Cupboard.Depth.ToString()}
            });
        }

        static private void AddCrosspieceAr(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Traverse Ar" },
                { "depth", box.Cupboard.Width.ToString()}
            });
        }

        static private void AddCrosspieceAv(Order order, Box box)
        {
            order.Components.Add(new Dictionary<string, string> {
                { "reference", "Traverse Av" },
                { "depth", box.Cupboard.Width.ToString()},
            });
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
