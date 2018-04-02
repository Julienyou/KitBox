using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            InitCupboard();

            cupboard.Boxes.Sum(x => x.Height);


        }


        private void InitCupboard()
        {
            cupboard = new Cupboard();
            cupboard.CupboardConstraint.Widths = ConstraintBuilder.BuildWidthsList();
          
//ajout connect DB
            cupboard.CupboardConstraint.MaxHeight = 150;
            cupboard.Width = cupboard.CupboardConstraint.Widths[0];
            cupboard.AddBox();

            cupboardConfig.DataContext = cupboard;
            mainGrid.DataContext = cupboard.Boxes;
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            cupboard.AddBox();
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                InitCupboard();
            }
        }

        private void validate_button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OrderConfirm(this);
            w.ShowDialog();
        }


        private void delete_buttn_Click(object sender, RoutedEventArgs e)
        {
                cupboard.RemoveBox((Box)drawBox.SelectedItem);
        }

        public Cupboard Cupboard { get => cupboard; }

        private void drawBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(cupboard.Boxes[0].Height);
        }
    }
}
