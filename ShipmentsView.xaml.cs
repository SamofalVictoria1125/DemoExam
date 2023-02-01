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

namespace DemoExam
{
    /// <summary>
    /// Логика взаимодействия для ShipmentsView.xaml
    /// </summary>
    public partial class ShipmentsView : Window
    {
      

        public ShipmentsView()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void MainGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                Shipment shipment = (Shipment)MainGrid.SelectedItem;
                ShipmentView shipmentView = new ShipmentView(shipment, 1);
                if (shipmentView.ShowDialog() == true)
                {
                    UpdateGrid();
                }
            }

        }


        public void UpdateGrid()
        {
            List<Shipment> shipmentsList = DBModel.SelectAllShipments();
            MainGrid.ItemsSource = shipmentsList;
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            Shipment shipment;
            shipment = new Shipment();
            ShipmentView shipmentView = new ShipmentView(shipment, 0);
            if (shipmentView.ShowDialog() == true)
            {
                UpdateGrid();
            }

        }
    }
}
