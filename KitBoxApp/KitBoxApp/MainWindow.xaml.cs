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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KitBoxApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private CupboardConstraint cupboardConstraint = new CupboardConstraint(new List<int> { 1, 2, 3 }, new List<int> { 100, 200, 300 }, new List<string> { "Beige des bois", "Rouge nuit", "Noir jour" }, 250);
        private BoxConstraint boxConstraint = new BoxConstraint(new List<int> { 50, 60, 70 }, new List<string> { "rouge franboise", "rose fluo", "paquerette" }, new List<string> { "Blanc", "Brun" });
        private DoorConstraint doorConstraint = new DoorConstraint(new List<string> { "None", "Verre", "Vert", "Ver", "Vair" }, null);

        private Cupboard cupboard;

        public MainWindow()
        {
            
            InitializeComponent();
            
            cupboard = new Cupboard(cupboardConstraint.Widths[0], cupboardConstraint.Depths[0], cupboardConstraint.SteelCornerColors[0]);

            widthComboBox.ItemsSource = cupboardConstraint.Widths;
            depthComboBox.ItemsSource = cupboardConstraint.Depths;
            steelCornerCombo.ItemsSource = cupboardConstraint.SteelCornerColors;

            boxHeighCombo.ItemsSource = boxConstraint.Heights;
            paneColorCombo.ItemsSource = boxConstraint.VColors;
            hPaneColorCombo.ItemsSource = boxConstraint.HColors;
           
            doorStyleCombo.ItemsSource = doorConstraint.Colors;

            cupboardConfig.DataContext = cupboard;
            Box box = new Box(cupboard,boxConstraint.Heights[0], boxConstraint.VColors[0], boxConstraint.HColors[0]);
            box.AddAccessory(new Door(doorConstraint.Colors[1]));
            cupboard.AddBox(box);

            boxesConfig.DataContext = cupboard.Boxes;
            drawBox.DataContext = cupboard;

        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Box box = new Box(cupboard, boxConstraint.Heights[0], boxConstraint.VColors[0], boxConstraint.HColors[0]);
            box.AddAccessory(new Door(doorConstraint.Colors[1]));
            if (cupboard.GetHeight()+box.Height <= cupboardConstraint.MaxHeight)
            {
                cupboard.AddBox(box);
            }
            else
            {
                MessageBox.Show("You have reashed max Height");
            }
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                cupboard = new Cupboard(cupboardConstraint.Widths[0], cupboardConstraint.Depths[0], cupboardConstraint.SteelCornerColors[0]);
                Box box = new Box(cupboard, boxConstraint.Heights[0], boxConstraint.VColors[0], boxConstraint.HColors[0]);
                box.AddAccessory(new Door(doorConstraint.Colors[1]));
                cupboard.AddBox(box);
                drawBox.DataContext = cupboard;
                boxesConfig.DataContext = cupboard.Boxes;
                cupboardConfig.DataContext = cupboard;
            }
        }

        private void validate_button_Click(object sender, RoutedEventArgs e)
        {
            
            Window w = new OrderRecap();
            w.ShowDialog();
        }

        private void drawBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            cupboardHeight.Text = cupboard.GetHeight().ToString();
        }

        private void delete_buttn_Click(object sender, RoutedEventArgs e)
        {
                cupboard.RemoveBox((Box)drawBox.SelectedItem);
        }
    }
}
