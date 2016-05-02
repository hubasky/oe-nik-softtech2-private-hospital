using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.PatientManagement;

namespace HubaskyHospitalManager.Model.HospitalManagement
{
	public interface IManageableProcedures
    {
        void AddNewProcedure(ProcedureType procedureType);
        void UpdateProcedure(ProcedureType source, ProcedureType target);
        void DeleteProcedure(ProcedureType procedureType);
    }
}