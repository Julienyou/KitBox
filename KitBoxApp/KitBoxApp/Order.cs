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

        #region Attributes

        private string id = null;
        private int totalPrice = 0;
        private Customer customer = null;
        private string state;

        private List<Dictionary<string, string>> components = new List<Dictionary<string, string>>();

        #endregion
        #region Constructors

        public Order(string id)
        {
            this.id = id;
        }

        #endregion
        #region Getters and Setters

        public string Id
        {
            get => id;

            set
            {
                id = value;
            }
        }

        public int TotalPrice
        {
            get => totalPrice;

            set
            {
                totalPrice = value;
            }
        }

        public Customer Customer
        {
            get => customer;

        }

        public string State
        {
            get => state;

            set
            {
                state = value;
            }
        }

        public List<Dictionary<string, string>> Components { get => components; set => components = value; }


        /*Functions if we created an Order*/
        public void SetCustomer(string email, string firstName, string lastName, string street, string town)
        {
            customer = new Customer(email, firstName, lastName, street, town);
        }

        #endregion
        #region Methods

        //---Methods

        public void ComputePrice()
        {
            
            foreach (Dictionary <string, string> component in components)
            {
                string value;
                if(component.TryGetValue("price", out value))
                {
                    totalPrice += Int32.Parse(value);
                }
            }
        }

        public void AddComponent(Dictionary<string, string> mycomponent)
        {
            bool compfound = false;
            foreach(Dictionary<string, string> component in this.components)
            {
                bool compexists = true;
                foreach(string key in mycomponent.Keys) //Could use a while but mycomponent.keys is not indexable directly. Maybe there is a way to make it so.
                {
                    if (component.ContainsKey(key) & compexists)
                    {
                        if (key != "quantity")
                        {
                            compexists = (component[key] == mycomponent[key]);
                        }
                    }
                }

                if (compexists)
                {
                    int quantity = Int32.Parse(component["quantity"]) + Int32.Parse(mycomponent["quantity"]);
                    component["quantity"] = quantity.ToString();
                    compfound = true;
                }
            }
            if (!compfound)
            {
                components.Add(mycomponent);
            }
        }

        public void ComposeOrder(Cupboard cupboard)
        {
            this.AddComponent(new Dictionary<string, string> {
                { "reference", "Cornières" },
                { "color", cupboard.SteelCornerColor },
                { "height", cupboard.GetHeight().ToString() },
                { "quantity" , "4"}
            });

            foreach (Box box in cupboard.Boxes)
            {
                AddCrosspieceAr(box);
                AddCrosspieceAv(box);
                AddCrosspieceGD(box);
                AddMount(box);
                AddPaneAr(box);
                AddPaneGD(box);
                AddPaneHB(box);
            }
        }

            #region ComposeOrder sub-methods

        private void AddPaneGD(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Panneau GD" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "depth", box.Cupboard.Depth.ToString()},
                { "quantity" , "2"}
            });
        }

        private void AddPaneHB(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Panneau HB" },
                { "color", box.HorizontalColor},
                { "depth", box.Cupboard.Depth.ToString()},
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        private void AddPaneAr( Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Panneau Ar" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "1"}
                });
        }

        private void AddMount(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Tasseau" },
                { "height", box.Height.ToString()},
                { "quantity" , "4"}
            });
        }

        private void AddCrosspieceGD(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Traverse GD" },
                { "depth", box.Cupboard.Depth.ToString()},
                { "quantity" , "4"}
            });
        }

        private void AddCrosspieceAr(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Traverse Ar" },
                { "depth", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        private void AddCrosspieceAv(Box box)
        {
            this.AddComponent(new Dictionary<string, string>() {
                { "reference", "Traverse Av" },
                { "depth", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        private void AddAccessories(Box box)
        {
            foreach(IAccessory accessory in box.Accessories)
            {
                if(accessory is Door)
                {
                    this.AddComponent(new Dictionary<string, string>() { 
                        { "reference", "Porte" },
                        { "width",  box.Cupboard.Width.ToString() },
                        { "quantity", "2" }
                    });
                }
            }
        }
            #endregion


        public void RetrieveDetails()
        {
            Utils.FetchFromDataBase(Components);
        }
    }
}
        #endregion

