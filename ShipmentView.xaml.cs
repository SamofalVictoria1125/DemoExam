using DemoExam.Models;
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
using Xceed.Wpf.Toolkit;

namespace DemoExam
{
    /// <summary>
    /// Логика взаимодействия для ShipmentView.xaml
    /// </summary>
    public partial class ShipmentView : Window
    {
        List<ShipmentContent> shipmentContent;
        Shipment model;

        public ShipmentView(Shipment model,int openMode)
        {
            Employee employee;
            ContactPerson contactPerson;
           
            this.model = model;
            InitializeComponent();
            textBoxID.Text = this.model.ID.ToString();
            employee = DBModel.SelectEmployeeByID(this.model.IDManager);
            contactPerson = DBModel.SelectContactPersonByID(employee.IDContact);
            textBoxManager.Text = contactPerson.LastName + " " + (contactPerson.FirstName != null ? contactPerson.FirstName.Substring(0, 1) : "") + "." + (contactPerson.Patronymic != null ? contactPerson.Patronymic.Substring(0, 1) : "" )+ ".";
            dateTimePickerShipmentDateTime.Value = this.model.ShipmentDate;
            shipmentContent = DBModel.SelectShipmentContentByShipmentID(this.model.ID);
            DataGridPositions.ItemsSource = shipmentContent;

            
        }

        

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
