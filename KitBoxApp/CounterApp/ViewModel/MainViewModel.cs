
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
using KitBox.Core.Model;

namespace CounterApp.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
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

        #region
        public ICommand SelectOrderCommand
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    Payment paymentWindow = new Payment();
                    PaymentViewModel paymentVM = new PaymentViewModel(SelectedOrder, Orders);
                    paymentWindow.DataContext = paymentVM;
                    paymentWindow.ShowDialog();
                },true);
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Thread loadCommandThread = new Thread(LoadCommand);
            loadCommandThread.Start();

            Order o1 = new Order();
            o1.SetCustomer("guillaumedemoff@gmail.com", "Guillaume", "de Moffarts", "bla", "bla");
            o1.TotalPrice = 250;
            o1.RemnantSale = 250;
            o1.Id = "6465478";
            o1.State = "blabla";
            Order o2 = new Order();
            o2.SetCustomer("seb478@gmail.com", "Sebastien", "Combefis", "bla", "bla");
            o2.TotalPrice = 320;
            o2.RemnantSale = 320;
            o2.Id = "78P87675";
            o2.State = "blabla";

            Orders = new ObservableCollection<Order>{ o1, o2, o1 };
        }
        #endregion

        #region methods
        public void LoadCommand()
        {
            while(true)
            {
                //add Load fonction here and sort    

            }
        }
        #endregion
    }
}
