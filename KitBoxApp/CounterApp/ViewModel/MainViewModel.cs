
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitBox.Core;
using KitBox.Core.Enum;
using KitBox.Core.Model;

namespace CounterApp.ViewModel
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
                    if (SelectedOrder.State == PaymentStatus.Payed)
                    {
                        Order o = SelectedOrder;
                        if (MessageBox.Show("Ship the order out?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Utils.UpdatePreparationStatus(o.Id, PreparationStatus.ShippedOut);
                        }
                    }
                    else
                    {
                        Payment paymentWindow = new Payment();
                        PaymentViewModel paymentVM = new PaymentViewModel(SelectedOrder, Orders);
                        paymentWindow.DataContext = paymentVM;
                        paymentWindow.ShowDialog();

                    }

                }, true);
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
        public void LoadCommand()
        {
            while (true)
            {
                Orders = new ObservableCollection<Order>(Utils.ImportAllOrders().Where(x => x.State != PaymentStatus.Canceled && x.PreparationState != PreparationStatus.ShippedOut).OrderBy(x => x.State));
                Notify("Orders");
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
