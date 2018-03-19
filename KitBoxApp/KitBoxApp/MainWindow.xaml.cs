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
        private Cupboard cupboard = new Cupboard();
        

        public MainWindow()
        {
            
            InitializeComponent();
            CupboardConstraint cupboardConstraint = new CupboardConstraint(new List<int> { 1,2,3}, new List<int> { 100, 200, 300 },150);
            widthComboBox.ItemsSource = cupboardConstraint.Widths;
            depthComboBox.ItemsSource = cupboardConstraint.Depths;
            cupboardConfig.DataContext = cupboard;
            Box box = new Box(cupboard);
            cupboard.AddBox(box);
            boxesConfig.DataContext = cupboard.Boxes;

            paneColorCombo.ItemsSource = new List<string> { "rouge franboise", "rose fluo", "paquerette" };
            doorStyleCombo.ItemsSource = new List<string> { "None","Verre", "Vert", "Ver", "Vair" };
            boxHeighCombo.ItemsSource = new List<int> { 50, 60, 70 };
            steelCornerCombo.ItemsSource = new List<string> { "Beige des bois", "Rouge nuit", "Noir jour" };

            drawBox.DataContext = cupboard;
            

        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("button Pressed");
            Box box = new Box(cupboard);
            cupboard.AddBox(box);
            
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                cupboard = new Cupboard();
                cupboard.AddBox(new Box(cupboard));
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
