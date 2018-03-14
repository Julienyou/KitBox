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
        private Boxes boxes = new Boxes();
        private BoxesShape boxesShape = new BoxesShape();
        public MainWindow()
        {
            InitializeComponent();
            widthComboBox.ItemsSource = constrains.Width;
            depthComboBox.ItemsSource = constrains.Depth;
            stackPanel.DataContext = cupboard;
            //boxChoiseCombo.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            paneColorCombo.ItemsSource = new List<string> { "rouge franboise", "rose fluo", "paquerette" };
            doorStyleCombo.ItemsSource = new List<string> { "Verre", "Vert", "Ver", "Vair" };
            boxHeighCombo.ItemsSource = new List<int> { 50, 60, 70 };
            steelCornerCombo.ItemsSource = new List<string> { "Beige des bois", "Rouge nuit", "Noir jour" };
            //boxesTab.DataContext = boxes;
            boxesTab.DataContext = boxesShape;
            drawBox.DataContext = boxesShape;
            //boxesShape[1].
            
            


        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            boxes.Add(new Box());
            BoxShape bx = new BoxShape();
        }
    }
}
