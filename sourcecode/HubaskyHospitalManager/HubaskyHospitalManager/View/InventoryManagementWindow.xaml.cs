﻿using HubaskyHospitalManager.Model.InventoryManagement;
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

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for InventoryManagementWindow.xaml
    /// </summary>
    public partial class InventoryManagementWindow : Window
    {
        InventoryManager vm { get; set; }

        public InventoryManagementWindow(InventoryManager inventoryManagement)
        {
            InitializeComponent();
            vm = inventoryManagement;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = vm;
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id") { e.Column.Header = "Azonosító"; e.Column.IsReadOnly = true; }
            if (e.Column.Header.ToString() == "Name") { e.Column.Header = "Megnevezés"; e.Column.Width = 150; }
            if (e.Column.Header.ToString() == "Quantity") { e.Column.Header = "Mennyiség"; }
            if (e.Column.Header.ToString() == "Unit") { e.Column.Header = "Egység"; }
            if (e.Column.Header.ToString() == "Status") e.Column.Visibility = Visibility.Hidden;
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            vm.UpdateInventory();
            dataGrid.Items.Refresh();
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            int id = ((InventoryItem)dataGrid.SelectedItem).Id;
            vm.UpdateStatus(id);
        }

        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            dataGrid.Items.Refresh();
        }

        private void textBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            vm.Search(textBox.Text.ToLower());
            dataGrid.Items.Refresh();
        }

        private void button_Click_Insert(object sender, RoutedEventArgs e)
        {
            dataGrid.ScrollIntoView(dataGrid.Items[dataGrid.Items.Count - 1]);
            dataGrid.SelectedIndex = dataGrid.Items.Count - 1;
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedInventoryItem != null)
            {
                vm.AddToUsage();
                dataGrid.Items.Refresh();
                listBox.Items.Refresh();
            }
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedInventoryUsage != null)
            {
                vm.RemoveFromUsage();
                dataGrid.Items.Refresh();
                listBox.Items.Refresh();
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateInventory();
            DialogResult = true;
        }
    }
}
