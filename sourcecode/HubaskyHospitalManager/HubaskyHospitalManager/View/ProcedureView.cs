using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.View
{
    public class ProcedureView
    {
        public virtual ObservableCollection<InventoryItem> InventoryUsage { get; set; } //nem kell view?
        public InventoryItem SelectedInventoryItem { get; set; } //nem kell view?

        //modell eredeti procedure
        public Procedure ModelProcedure { get; set; }

        public ProcedureView(Procedure proc)
        {
            ModelProcedure = proc;
            InventoryUsage = new ObservableCollection<InventoryItem>();

            foreach (InventoryItem invItem in proc.InventoryUsage) //nem kell view?
            {
                InventoryUsage.Add(invItem);
            }
            
        }
    }
}
