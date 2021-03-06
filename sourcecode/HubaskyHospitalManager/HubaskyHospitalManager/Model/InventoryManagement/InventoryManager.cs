///////////////////////////////////////////////////////////
//  InventoryManager.cs
//  Implementation of the Class InventoryManager
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:17
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using HubaskyHospitalManager.Data;
using System.Linq;
using System.Data.Entity;

namespace HubaskyHospitalManager.Model.InventoryManagement
{
	public class InventoryManager : INotifyPropertyChanged //IInventoryManagement
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private ApplicationManager appManager;

        public ApplicationManager AppManager
        {
            get { return appManager; }
            set { appManager = value; }
        }

        ObservableCollection<InventoryItem> inventory;

        public ObservableCollection<InventoryItem> Inventory
        {
            get { return inventory; }
            private set { inventory = value; OnPropertyChanged(); }
        }

        ObservableCollection<InventoryItem> inventoryUsage;

        public ObservableCollection<InventoryItem> InventoryUsage
        {
            get { return inventoryUsage; }
            private set { inventoryUsage = value; OnPropertyChanged(); }
        }

        InventoryItem selectedInventoryItem;

        public InventoryItem SelectedInventoryItem
        {
            get { return selectedInventoryItem; }
            set { selectedInventoryItem = value; OnPropertyChanged(); }
        }

        InventoryItem selectedInventoryUsage;

        public InventoryItem SelectedInventoryUsage
        {
            get { return selectedInventoryUsage; }
            set { selectedInventoryUsage = value; OnPropertyChanged(); }
        }

        public InventoryManager(ApplicationManager appMgr)
        {
            AppManager = appMgr;
            Inventory = new ObservableCollection<InventoryItem>();
            InventoryUsage = new ObservableCollection<InventoryItem>();

            AppManager.ApplicationDb.Inventory.ToList().ForEach(item=> Inventory.Add(item));
        }

        public void UpdateStatus(int id)
        {
            if (id > 0)
            {
                InventoryItem item = Inventory.Single(i => i.Id == id);

                if (item.Status == InventoryRecordStatus.Commited)
                {
                    item.Status = InventoryRecordStatus.Modified;

                    int index = Inventory.IndexOf(item);

                    Inventory.Remove(item);
                    Inventory.Insert(index, item);
                }
            }
        }

        internal void Search(string text)
        {
            Inventory.Clear();

            if (text.Length==0)
            {
                AppManager.ApplicationDb.Inventory.ToList().ForEach(item => Inventory.Add(item));
            }
            else
            {
                AppManager.ApplicationDb.Inventory.Where(item => item.Name.ToLower().Contains(text.ToLower())).ToList().ForEach(found => Inventory.Add(found));
            } 
        }

        public void UpdateInventory()
        {
            var db = AppManager.ApplicationDb;

            foreach (InventoryItem modifiedItem in (Inventory.Where(i => i.Status == InventoryRecordStatus.Modified)))
            {
                db.Inventory.Attach(modifiedItem);
                db.Entry(modifiedItem).State = EntityState.Modified;

                modifiedItem.Status = InventoryRecordStatus.Commited;
            }

            foreach (InventoryItem modifiedItem in (Inventory.Where(i => i.Status == InventoryRecordStatus.New)))
            {
                db.Inventory.Add(modifiedItem);

                modifiedItem.Status = InventoryRecordStatus.Commited;
            }

            db.SaveChanges();
        }

        public void AddToUsage()
        {
            if (SelectedInventoryItem.Quantity < 1) return;

            if (InventoryUsage.Count(i => i.Id == SelectedInventoryItem.Id) == 1)
            {
                SelectedInventoryItem.Quantity--;
                InventoryUsage.Single(i => i.Id == SelectedInventoryItem.Id).Quantity++;
            }
            else
            {
                InventoryItem item = new InventoryItem(SelectedInventoryItem.Id, SelectedInventoryItem.Name, 1, SelectedInventoryItem.Unit, SelectedInventoryItem.Status);

                SelectedInventoryItem.Quantity--;

                InventoryUsage.Add(item);
            }
        }

        public void RemoveFromUsage()
        {
            if (SelectedInventoryUsage.Quantity < 1) return;

            SelectedInventoryUsage.Quantity--;
            Inventory.Single(i => i.Id == SelectedInventoryUsage.Id).Quantity++;

            if (SelectedInventoryUsage.Quantity == 0)
            {
                InventoryUsage.Remove(SelectedInventoryUsage);
            }
        }

    }//end InventoryManager

}//end namespace InventoryManagement