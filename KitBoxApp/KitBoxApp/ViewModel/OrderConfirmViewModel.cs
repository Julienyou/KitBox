using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Xps;
using KitBox.Core;
using KitBox.Core.Model;

namespace KitBox.ViewModel
{
    public class OrderConfirmViewModel : IDataErrorInfo
    {

        #region Attributes
        private Order m_Order;
        private readonly FlowDocument m_Document;
        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        #endregion

        #region ICommands
        public ICommand AddCustomerCommand
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    string[] addr = Address.Split(','); 
                    m_Order.Customer = new Customer(Email, FirstName, LastName, addr[0], addr[1]);
                    Utils.ExportToDatabase(m_Order);
                    ((Window)x).DialogResult = true;
                    ((Window)x).Close();
                    DoThePrint(m_Document);
                }, true);
            }
        }

        public ICommand CancelOrderCommand
        {
            get
            {
                return new CommandHandler((x) =>
                {
                    ((Window)x).DialogResult = false;
                    ((Window)x).Close();
                }, true);
            }
        }

        #endregion

        #region Constructor
        public OrderConfirmViewModel(Order order, FlowDocument document)
        {
            Utils.DBConnection = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KitBox\db.sqlite;Version=3;");
            Address = "";
            m_Order = order;
            m_Document = document;
        }
        #endregion

        #region Methods
        private void DoThePrint(System.Windows.Documents.FlowDocument document)
        {
            // Clone the source document's content into a new FlowDocument.
            // This is because the pagination for the printer needs to be
            // done differently than the pagination for the displayed page.
            // We print the copy, rather that the original FlowDocument.
            MemoryStream s = new MemoryStream();
            TextRange source = new TextRange(document.ContentStart, document.ContentEnd);
            source.Save(s, DataFormats.Xaml);
            FlowDocument copy = new FlowDocument();
            TextRange dest = new TextRange(copy.ContentStart, copy.ContentEnd);
            dest.Load(s, DataFormats.Xaml);

            string[] address = Address.Split(',');

            Paragraph price = new Paragraph(new Run("Total Price: " + m_Order.TotalPrice + "€"));
            price.TextAlignment = TextAlignment.Right;
            price.Margin = new Thickness(0, 30, 0, 0);

            Paragraph customerInfo = new Paragraph(new Run(String.Format("{0} {1}\n{2}\n{3}", FirstName, LastName, address[0].Trim(), address[1].Trim())));
            customerInfo.Margin = new Thickness(0, 0, 0, 50);
            copy.Blocks.Add(price);
            copy.Blocks.InsertBefore(copy.Blocks.FirstBlock, customerInfo);

            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            PrintDocumentImageableArea ia = null;
            XpsDocumentWriter docWriter = PrintQueue.CreateXpsDocumentWriter(ref ia);


            if (docWriter != null && ia != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);
                Thickness t = new Thickness(72);  // copy.PagePadding;
                copy.PagePadding = new Thickness(
                                 Math.Max(ia.OriginWidth, t.Left),
                                   Math.Max(ia.OriginHeight, t.Top),
                                   Math.Max(ia.MediaSizeWidth - (ia.OriginWidth + ia.ExtentWidth), t.Right),
                                   Math.Max(ia.MediaSizeHeight - (ia.OriginHeight + ia.ExtentHeight), t.Bottom));

                copy.ColumnWidth = double.PositiveInfinity;

                // Send content to the printer.
                docWriter.Write(paginator);
            }

        }
        #endregion

        #region DataErrorInfo members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (String.IsNullOrWhiteSpace(FirstName))
                        result = "First Name can not be empty";

                }

                if (columnName == "LastName")
                {
                    if (String.IsNullOrEmpty(LastName) || String.IsNullOrWhiteSpace(LastName))
                        result = "Last Name can not be empty";

                }

                if (columnName == "Email")
                {
                    if (Email == null)
                        result = "Email can not be empty";

                    try
                    {
                        new MailAddress(Email);
                    }
                    catch
                    {
                        result = "Enter a valid Email";
                    }

                }

                if (columnName == "Address")
                {
                    Regex rgx = new Regex(@"^\D+ \d{1,4}, (?:(?:[1-9])(?:\d{3})) \D+$");
                    if (!rgx.IsMatch(Address))
                        result = "Address must be: street n°, Code Town ";

                    if (String.IsNullOrEmpty(Address) || String.IsNullOrWhiteSpace(Address))
                        result = "Address can not be empty";
                }

                return result;
            }

        }
        #endregion
    }
}
