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
    /// Logique d'interaction pour OrderRecap.xaml
    /// </summary>
    public partial class OrderRecap : Window
    {
        private MainWindow mainWindow;
        public OrderRecap(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            if (firstNameCombo.Text.Trim() == "" && lastNameCombo.Text.Trim() == "" && emailCombo.Text.Trim() == "" && adressCombo.Text.Trim() == "")
                MessageBox.Show("You must complete all the field");
            else
                MessageBox.Show("Thank you for ordering");
        }
    }
}
