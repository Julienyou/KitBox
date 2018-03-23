using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    static class KitComposer
    {
        /// <summary>
        ///     Completes a specific order with the cupboard given in parameter
        ///     and generates a list of components.
        /// </summary>
        /// <param name="order">Represents the order that is going to receive the list of components</param>
        /// <param name="cupboard">The cupboard that is going to be analysed to generate the component list.</param>
        static public void ComposeKit(Order order, Cupboard cupboard)
        {
            ComposeOrder(order, cupboard);
            Utils.FetchFromDataBase(order.Components);
        }

        /// <summary>
        ///     Automatically add a component to the order. If the component 
        ///     already exists in the order, it automatically increments its
        ///     quantity value by the quantity from the component to add.
        /// </summary>
        /// <param name="order">Represents the order to add to.</param>
        /// <param name="mycomponent">Represents the component dictionnary to add.</param>
        static public void AddComponent(Order order, Dictionary<string, string> mycomponent)
        {
            bool compfound = false;
            foreach (Dictionary<string, string> component in order.Components)
            {
                bool compexists = true;
                foreach (string key in mycomponent.Keys) //Could use a while but mycomponent.keys is not indexable directly. Maybe there is a way to make it so.
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
                order.Components.Add(mycomponent);
            }
        }


        /// <summary>
        ///     Generates the components for the specified cupboard for the 
        ///     specified order and determines what are the relevant creterias 
        ///     for a database commponent checkout.
        /// </summary>
        /// <param name="order">Represents the order that is going to receive the list of components.</param>
        /// <param name="cupboard">Represents the cupboard that is going to be analysed to generate the component list.</param>
        static private void ComposeOrder(Order order, Cupboard cupboard)
        {
            AddComponent(order, new Dictionary<string, string> {
                { "reference", "Cornières" },
                { "color", cupboard.SteelCornerColor },
                { "height", cupboard.GetHeight().ToString() },
                { "quantity" , "4"}
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

        #region ComposeOrder sub-methods

        /// <summary>
        ///     Adds side panes components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive theses panes.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddPaneGD(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Panneau GD" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "depth", box.Cupboard.Depth.ToString()},
                { "quantity" , "2"}
            });
        }
        
        /// <summary>
        ///     Adds top and bottom panes components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these panes.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddPaneHB(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Panneau HB" },
                { "color", box.HorizontalColor},
                { "depth", box.Cupboard.Depth.ToString()},
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        /// <summary>
        ///     Adds the rear pane component based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive this pane.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddPaneAr(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Panneau Ar" },
                { "color", box.LateralColor},
                { "height", box.Height.ToString()},
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "1"}
                });
        }

        /// <summary>
        ///     Adds the mounts components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these mounts.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddMount(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Tasseau" },
                { "height", box.Height.ToString()},
                { "quantity" , "4"}
            });
        }

        /// <summary>
        ///     Adds the cross pieces components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these cross pieces.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddCrosspieceGD(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse GD" },
                { "depth", box.Cupboard.Depth.ToString()},
                { "quantity" , "4"}
            });
        }

        /// <summary>
        ///     Adds the cross pieces components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these cross pieces.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddCrosspieceAr(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse Ar" },
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        /// <summary>
        ///     Adds the cross pieces components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these cross pieces.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddCrosspieceAv(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse Av" },
                { "width", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        /// <summary>
        ///     Adds the accessory components based on box infos to the order.  
        /// </summary>
        /// <param name="order">Represents the order that is going to receive these accessories.</param>
        /// <param name="box">Represents the box to analyse to retrieve the component details.</param>
        static private void AddAccessories(Order order, Box box)
        {
            foreach (IAccessory accessory in box.Accessories)
            {
                if (accessory is Door)
                {
                    AddComponent(order, new Dictionary<string, string>() {
                        { "reference", "Porte" },
                        { "width",  box.Cupboard.Width.ToString() },
                        { "height ", box.Height.ToString() },
                        { "quantity", "2" }
                    });
                    Door door = (Door)accessory;
                    if (door.Knop)
                    {
                        AddComponent(order, new Dictionary<string, string>() {
                        { "reference", "Coupelles" },
                        { "quantity", "2" }
                    });
                    }
                }
            }
        }
        #endregion
    }
}
