using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitBoxApp
{
    static class KitComposer
    {
        static public void ComposeKit(Order order, Cupboard cupboard)
        {
            ComposeOrder(order, cupboard);
            Utils.FetchFromDataBase(order.Components);
        }

        static private void AddComponent(Order order, Dictionary<string, string> mycomponent)
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

        static private void AddMount(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Tasseau" },
                { "height", box.Height.ToString()},
                { "quantity" , "4"}
            });
        }

        static private void AddCrosspieceGD(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse GD" },
                { "depth", box.Cupboard.Depth.ToString()},
                { "quantity" , "4"}
            });
        }

        static private void AddCrosspieceAr(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse Ar" },
                { "depth", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        static private void AddCrosspieceAv(Order order, Box box)
        {
            AddComponent(order, new Dictionary<string, string>() {
                { "reference", "Traverse Av" },
                { "depth", box.Cupboard.Width.ToString()},
                { "quantity" , "2"}
            });
        }

        static private void AddAccessories(Order order, Box box)
        {
            foreach (IAccessory accessory in box.Accessories)
            {
                if (accessory is Door)
                {
                    AddComponent(order, new Dictionary<string, string>() {
                        { "reference", "Porte" },
                        { "width",  box.Cupboard.Width.ToString() },
                        { "quantity", "2" }
                    });
                }
            }
        }
        #endregion
    }
}
