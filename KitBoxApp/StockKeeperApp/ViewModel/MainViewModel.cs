
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Enum;
using KitBox.Core.Model;

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
                    BOMViewModel bOMViewModel = new BOMViewModel(SelectedOrder);
                    BOMWindow bOMWindow = new BOMWindow();
                    bOMWindow.DataContext = bOMViewModel;
                    bOMWindow.ShowDialog();
                },true);
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Thread loadCommandThread = new Thread(LoadCommand);
            loadCommandThread.IsBackground = true;
            loadCommandThread.Start();
        }
        #endregion

        #region methods
        private void LoadCommand()
        {
            while(true)
            {
                Orders = new ObservableCollection<Order>(Utils.ImportAllOrders().Where(x => x.State != PaymentStatus.Canceled && x.State != PaymentStatus.Unpayed && x.PreparationState == PreparationStatus.NotProcessed).OrderBy(x => x.PreparationState));
                Notify("Orders");
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
