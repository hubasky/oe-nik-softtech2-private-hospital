///////////////////////////////////////////////////////////
//  InventoryItem.cs
//  Implementation of the Class InventoryItem
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:17
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.Common;

namespace HubaskyHospitalManager.Model.InventoryManagement
{
	public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public InventoryUnit Unit { get; set; }
        public InventoryRecordStatus Status { get; set; }

        public InventoryItem(int id, string name, int quantity, InventoryUnit unit, InventoryRecordStatus status)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Status = status;
        }

        public InventoryItem()
        {

        }

        public override string ToString()
        {
            return String.Format("{2} {3} {1} (id: {0})", Id, Name, Quantity, Unit, Status);
        }

    }//end InventoryItem

}//end namespace InventoryManagement