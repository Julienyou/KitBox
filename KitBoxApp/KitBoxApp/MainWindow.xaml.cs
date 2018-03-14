﻿using System;
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
            CupboardConstraint cupboardConstraint = new CupboardConstraint(new List<int> { 1,2,3}, new List<int> { 4, 5, 6 },150);
            widthComboBox.ItemsSource = cupboardConstraint.Widths;
            depthComboBox.ItemsSource = cupboardConstraint.Depths;
            cupboardConfig.DataContext = cupboard;
            cupboard.AddBox(new Box());
            boxesConfig.DataContext = cupboard.Boxes;

            paneColorCombo.ItemsSource = new List<string> { "rouge franboise", "rose fluo", "paquerette" };
            doorStyleCombo.ItemsSource = new List<string> { "Verre", "Vert", "Ver", "Vair" };
            boxHeighCombo.ItemsSource = new List<int> { 50, 60, 70 };
            steelCornerCombo.ItemsSource = new List<string> { "Beige des bois", "Rouge nuit", "Noir jour" };
             
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            cupboard.AddBox(new Box());
        }
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            cupboardHeight.Text = cupboard.GetHeight().ToString();
            MessageBox.Show(cupboard.GetHeight().ToString());
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            cupboardHeight.Text = cupboard.GetHeight().ToString();
            MessageBox.Show(cupboard.GetHeight().ToString());
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cupboardHeight.Text = cupboard.GetHeight().ToString();
        }
    }
}
