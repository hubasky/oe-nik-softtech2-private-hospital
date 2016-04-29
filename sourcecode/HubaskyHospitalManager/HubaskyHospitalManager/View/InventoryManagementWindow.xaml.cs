using HubaskyHospitalManager.Model.InventoryManagement;
using System.Windows;

namespace HubaskyHospitalManager.View
{
    public partial class InventoryManagementWindow : Window
    {
        InventoryControl inv { get; set; }

        public InventoryManagementWindow(InventoryManager inventoryManagement)
        {
            InitializeComponent();
            inv = inventoryControl;
            inv.vm = inventoryManagement;
            DataContext = inv.vm;
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (inv.vm.SelectedInventoryItem != null)
            {
                inv.vm.AddToUsage();
                inv.dataGrid.Items.Refresh();
                listBox.Items.Refresh();
            }
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (inv.vm.SelectedInventoryUsage != null)
            {
                inv.vm.RemoveFromUsage();
                inv.dataGrid.Items.Refresh();
                listBox.Items.Refresh();
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            inv.vm.UpdateInventory();
            DialogResult = true;
        }
    }
}
