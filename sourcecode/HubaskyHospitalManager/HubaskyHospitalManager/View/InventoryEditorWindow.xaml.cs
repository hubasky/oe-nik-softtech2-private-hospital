﻿using HubaskyHospitalManager.Model.InventoryManagement;
using System.Windows;

namespace HubaskyHospitalManager.View
{
    public partial class InventoryEditorWindow : Window
    {
        InventoryControl inv { get; set; }

        public InventoryEditorWindow(InventoryManager inventoryManagement)
        {
            InitializeComponent();
            inv = inventoryControl;
            inv.vm = new InventoryManager(inventoryManagement.AppManager);
            inv.dataGrid.IsReadOnly = false;
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            inv.vm.UpdateInventory();
            inv.dataGrid.Items.Refresh();
        }
    }
}
