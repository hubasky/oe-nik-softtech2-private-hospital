using HubaskyHospitalManager.Model.PatientManagement;
//using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.View
{
    public class MedicalRecordView
    {
        public virtual ObservableCollection<ProcedureView> Procedures { get; set; } //view!
        public ProcedureView SelectedProcedure { get; set; } //view!
        
        public MedicalRecord ModelMedicalRecord { get; set; }

        public MedicalRecordView(MedicalRecord medRec)
        {
            ModelMedicalRecord = medRec;
            Procedures = new ObservableCollection<ProcedureView>();

            foreach (Procedure proc in medRec.Procedures) //view!
            {
                //Console.WriteLine("Procedureview: {0}", proc.CreatedTimestamp);
                Procedures.Add(new ProcedureView(proc));
            }
            
        }
    }
}
