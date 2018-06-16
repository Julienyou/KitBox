
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Enum;
using KitBox.Core.Model;
using KitBox.WPFcore;
using Newtonsoft.Json.Linq;

namespace StockKeeperApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Property changed member
        // INotifyPropertyChanged Member
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the orders
        /// </summary>
        public ObservableCollection<Order> Orders { get; set; }

        /// <summary>
        /// Get or set the selected Order
        /// </summary>
        public Order SelectedOrder { get; set; }
        #endregion

        #region ICommand
        public ICommand SelectOrderCommand
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    BOMWindow bOMWindow = new BOMWindow();
                    bOMWindow.DataContext = new BOMViewModel(SelectedOrder);
                    bOMWindow.ShowDialog();
                }, true);
            }
        }

        public ICommand ShowInvetoryCommand
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    InventoryWindow w = new InventoryWindow();
                    w.DataContext = new InventoryViewModel();
                    w.Show();
                }, true);
            }
        }

        public ICommand SupplierOrderCommand
        {
            get
            {
                return new CommandHandler((x) => { WpfMessageBox.Show("Restock Informations", JArray.FromObject(Utils.Restock()).ToString(), MessageBoxType.Information); }, true);
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KitBox\db.sqlite;Version=3;");

            Thread loadCommandThread = new Thread(LoadCommand);
            loadCommandThread.IsBackground = true;
            loadCommandThread.Start();
        }
        #endregion

        #region methods
        private void LoadCommand()
        {
            while (true)
            {
                Orders = new ObservableCollection<Order>(Utils.ImportAllOrders().Where(x => x.State != PaymentStatus.Canceled && x.State != PaymentStatus.Unpayed && x.PreparationState == PreparationStatus.NotProcessed).OrderBy(x => x.PreparationState));
                Notify("Orders");
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
