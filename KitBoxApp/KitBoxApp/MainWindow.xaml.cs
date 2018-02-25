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
        private Constrain constrains = new Constrain();
        private Cupboard cupboard = new Cupboard();
        public MainWindow()
        {
            InitializeComponent();
            heightComboBox.ItemsSource = constrains.Height;
            widthComboBox.ItemsSource = constrains.Width;
            depthComboBox.ItemsSource = constrains.Depth;
            stackPanel.DataContext = cupboard;
        }
    }
}
