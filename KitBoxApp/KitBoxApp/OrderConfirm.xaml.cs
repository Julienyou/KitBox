using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KitBoxApp
{
    /// <summary>
    /// Logique d'interaction pour OrderConfirm.xaml
    /// </summary>
    public partial class OrderConfirm : Window
    {
        private MainWindow main;
        public OrderConfirm(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            
            // Create a FlowDocument

           

            FlowDocument mcFlowDoc = new FlowDocument();


            // Create a paragraph with text

            Paragraph cbPar = new Paragraph();

            List mainList = new List();
            ListItem cupboardItem = new ListItem();
            ListItem boxesItem = new ListItem();

            List cupboardList = new List();
            List boxesList = new List();

            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Height: " + main.Cupboard.Height + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Width: " + main.Cupboard.Width + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Depth: " + main.Cupboard.Depth + " cm"))));
            cupboardList.ListItems.Add(new ListItem(new Paragraph(new Run("Corner Color: " + main.Cupboard.SteelCornerColor))));

            
            
            int i = 1;
            foreach(Box box in main.Cupboard.Boxes)
            {

                ListItem boxDescpriptionItem = new ListItem();
                List boxDescription = new List();

                boxDescription.ListItems.Add(new ListItem(new Paragraph(new Run("Height: "+box.Height))));
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

            mcFlowDoc.Blocks.Add(mainList);


            // Create RichTextBox, set its hegith and width

           textBox.Document = mcFlowDoc;

        }

        private void order_button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OrderRecap(main);
            this.Close();
            w.ShowDialog();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
