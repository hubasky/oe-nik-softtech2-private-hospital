///////////////////////////////////////////////////////////
//  Procedure.cs
//  Implementation of the Class Procedure
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:19
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.Common;

namespace HubaskyHospitalManager.Model.PatientManagement
{
	public class Procedure {

        private List<string> attachments;
        private DateTime createdTimestamp;
        private DateTime lastModifiedTimestamp;
        private string name;
        private int price;
        private State state;
        private Employee responsible;
        private List<InventoryItem> inventoryUsage;

        public List<string> Attachments
        {
            get { return attachments; }
            set { attachments = value; }
        }

        public DateTime CreatedTimestamp
        {
            get { return createdTimestamp; }
            set { createdTimestamp = value; }
        }

        public DateTime LastModifiedTimestamp
        {
            get { return lastModifiedTimestamp; }
            set { lastModifiedTimestamp = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public State State
        {
            get { return state; }
            set { state = value; }
        }

        public Employee Responsible
        {
            get { return responsible; }
            set { responsible = value; }
        }

        public List<InventoryItem> InventoryUsage
        {
            get { return inventoryUsage; }
            set { inventoryUsage = value; }
        }

		public Procedure()
        {

		}

		/// 
		/// <param name="attachment"></param>
		public void AddAttachment(string attachment)
        {

		}

		/// 
		/// <param name="item"></param>
		/// <param name="decrement"></param>
		public int DecreaseQuantity(InventoryItem item, int decrement)
        {
			return 0;
		}

		/// 
		/// <param name="name"></param>
		public List<InventoryItem> SearchInventory(string name)
        {
			return null;
		}

	}//end Procedure

}//end namespace PatientManagement