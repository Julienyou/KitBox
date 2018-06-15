using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Model;

namespace KitBox.ViewModel
{
    class OrderRecapViewModel 
    {
        #region Attributes
        private Cupboard m_Cupboard;
        #endregion

        #region Properties
        public FlowDocument FlowDocument { get; private set; }

        public Order Order { get; private set; }

        public bool InStock { get; private set; }
        #endregion

        #region ICommand
        public ICommand OrderCommand { get { return new CommandHandler((x) => 
        {
            OrderConfirm w = new OrderConfirm();
            OrderConfirmViewModel orderRecapVM = new OrderConfirmViewModel(w, Order);
            w.DataContext = orderRecapVM;
            ((Window)x).Close();
            w.ShowDialog();
        }, true); } }

        public ICommand CancelCommand { get { return new CommandHandler((x) => { ((Window)x).Close(); }, true); } }
        #endregion

        #region Constructor
        public OrderRecapViewModel(Cupboard cupboard)
        {
            InStock = true;

            m_Cupboard = cupboard;
            Order = new Order();
            KitComposer.ComposeKit(Order,cupboard);

            FlowDocument = new FlowDocument();

            CreateRecap();
        }
        #endregion

        #region Methods
        private void CreateRecap()
        {
            List mainList = new List();
            ListItem cupboardItem = new ListItem();
            ListItem boxesItem = new ListItem();

            List cupboardList = new List();
            List boxesList = new List();

            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Height: " + m_Cupboard.Height + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Width: " + m_Cupboard.Width + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Depth: " + m_Cupboard.Depth + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Corner Color: " + m_Cupboard.SteelCornerColor))));

            int i = 1;
            foreach (Box box in m_Cupboard.Boxes)
            {

                ListItem boxDescpriptionItem = new ListItem();
                List boxDescription = new List();

                boxDescription.ListItems.Add(new ListItem(new Paragraph(new Run("Height: " + box.Height))));
                boxDescription.ListItems.Add(new ListItem(new Paragraph(new Run("Horizontal Color: " + box.HorizontalColor))));
                boxDescription.ListItems.Add(new ListItem(new Paragraph(new Run("Lateral Color: " + box.LateralColor))));
                boxDescription.ListItems.Add(new ListItem(new Paragraph(new Run("Door Style: " + ((Door)box.Accessories[0]).Color))));

                boxDescpriptionItem.Blocks.Add(new Paragraph(new Run("Box " + i)));
                boxDescpriptionItem.Blocks.Add(boxDescription);

                boxesList.ListItems.Add(boxDescpriptionItem);

                i++;
            }

            cupboardItem.Blocks.Add(new Paragraph(new Run("Cupboard Informations:")));
            cupboardItem.Blocks.Add(cupboardList);
            boxesItem.Blocks.Add(new Paragraph(new Run("Boxes Informations:")));
            boxesItem.Blocks.Add(boxesList);

            mainList.ListItems.Add(cupboardItem);
            mainList.ListItems.Add(boxesItem);

            FlowDocument.Blocks.Add(mainList);
        }
        #endregion
    }
}
