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
        
        private CupboardConstraint cupboardConstraint = new CupboardConstraint();
        private BoxConstraint boxConstraint = new BoxConstraint(new List<int> { 50, 60, 70 }, new List<string> { "rouge franboise", "rose fluo", "paquerette" }, new List<string> { "Blanc", "Brun" });
        private DoorConstraint doorConstraint = new DoorConstraint(new List<string> { "None", "Verre", "Vert", "Ver", "Vair" }, null);

        private Cupboard cupboard;

        public MainWindow()
        {
            
            InitializeComponent();

            
            cupboard = new Cupboard();
            Box box = new Box(cupboard);
            box.AddAccessory(new Door());


            cupboardConstraint.Widths = ConstraintBuilder.BuildWidthsList();
            widthComboBox.ItemsSource = cupboardConstraint.Widths;
            cupboard.Width = cupboardConstraint.Widths[0];

            cupboardConstraint.Depths = ConstraintBuilder.GetAvailableDepth(cupboard.Width);
            depthComboBox.ItemsSource = cupboardConstraint.Depths;
            cupboard.Depth = cupboardConstraint.Depths[0];

            boxConstraint.Heights = ConstraintBuilder.GetAvailableHeight();
            boxHeighCombo.ItemsSource = boxConstraint.Heights;
            box.Height = boxConstraint.Heights[0];

            boxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(cupboard.Width,cupboard.Depth);
            hPaneColorCombo.ItemsSource = boxConstraint.HColors;
            box.HorizontalColor = boxConstraint.HColors[0];

            boxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(cupboard.Width, cupboard.Depth, box.Height);
            paneColorCombo.ItemsSource = boxConstraint.VColors;
            box.LateralColor = boxConstraint.VColors[0];

            doorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(cupboard.Width,box.Height);
            doorConstraint.Colors.Insert(0,"None");
            doorStyleCombo.ItemsSource = doorConstraint.Colors;
            ((Door)box.Accessories[0]).Color = doorConstraint.Colors[2];

            cupboardConstraint.SteelCornerColors = ConstraintBuilder.GetAvailableSteelCornerColor(cupboard.GetHeight());
            cupboardConstraint.MaxHeight = 300;
            steelCornerCombo.ItemsSource = cupboardConstraint.SteelCornerColors;


            cupboardConfig.DataContext = cupboard;
            boxesConfig.DataContext = cupboard.Boxes;
            drawBox.DataContext = cupboard;
            cupboard.AddBox(box);

        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Box box = new Box(cupboard);

            boxConstraint.Heights = ConstraintBuilder.GetAvailableHeight();
            boxHeighCombo.ItemsSource = boxConstraint.Heights;
            box.Height = boxConstraint.Heights[0];

            boxConstraint.HColors = ConstraintBuilder.GetAvailableHPaneColor(cupboard.Width, cupboard.Depth);
            hPaneColorCombo.ItemsSource = boxConstraint.HColors;
            box.HorizontalColor = boxConstraint.HColors[0];

            boxConstraint.VColors = ConstraintBuilder.GetAvailableVPaneColor(cupboard.Width, cupboard.Depth, box.Height);
            paneColorCombo.ItemsSource = boxConstraint.VColors;
            box.LateralColor = boxConstraint.VColors[0];

            box.AddAccessory(new Door());

            doorConstraint.Colors = ConstraintBuilder.GetAvailableDoorStyle(cupboard.Width, box.Height);
            doorConstraint.Colors.Insert(0, "None");
            doorStyleCombo.ItemsSource = doorConstraint.Colors;
            ((Door)box.Accessories[0]).Color = doorConstraint.Colors[2];


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
                cupboard = new Cupboard();
                Box box = new Box(cupboard);
                box.AddAccessory(new Door());
                cupboard.AddBox(box);
                drawBox.DataContext = cupboard;
                boxesConfig.DataContext = cupboard.Boxes;
                cupboardConfig.DataContext = cupboard;
            }
        }

        private void validate_button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new OrderConfirm(this);
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

        public Cupboard Cupboard { get => cupboard; }
    }
}
