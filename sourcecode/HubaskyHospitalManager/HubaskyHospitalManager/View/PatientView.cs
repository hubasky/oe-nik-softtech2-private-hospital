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
    public class PatientView
    {
        public virtual ObservableCollection<MedicalRecordView> MedicalHistory { get; set; }
        public MedicalRecordView SelectedMedicalRecord { get; set; }
        public Patient ModelPatient { get; set; }
        //public string Name { get; set; }

        public PatientView(Patient patient)
        {
            ModelPatient = patient;
            //Name = ModelPatient.Name;
            MedicalHistory = new ObservableCollection<MedicalRecordView>();

            //Console.WriteLine("ModelPatient.Name: " + ModelPatient.Name);

            //Patient.medicalrecord ---> pView.mRecView
            //if (!(patient.MedicalHistory == null)) //elvileg erre nem kellene, hogy szükség legyen...
            //{
                foreach (MedicalRecord medRec in patient.MedicalHistory)
                {
                    MedicalHistory.Add(new MedicalRecordView(medRec));
                }
            //}

        }
    }
}
