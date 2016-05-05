package hubasky.service;

import java.util.List;

import javax.ejb.EJB;
import javax.ejb.Stateless;

import hubasky.database.domain.Attachment;
import hubasky.database.domain.MedicalRecord;
import hubasky.database.domain.Patient;
import hubasky.database.domain._Procedure;
import hubasky.database.jpa.PatientDAOLocal;

@Stateless
public class PatientService {
	
	@EJB
	private PatientDAOLocal patientDAO;
	
	public List<MedicalRecord> getMedicalRecordsOfPatient(String ssn) {
		return patientDAO.getMedicalRecordsOfPatient(ssn);
	}
	
	public Patient getPatient(String ssn) {
		return patientDAO.getPatient(ssn);
	}
	
	public List<Patient> getPatients() {
		return patientDAO.getPatients();
	}
	
	public _Procedure getProcedure(int procedureID) {
		return patientDAO.getProcedure(procedureID);
	}

	public List<_Procedure> getProceduresOfMedicalRecord(int medicalRecordID) {
		return patientDAO.getProceduresOfMedicalRecord(medicalRecordID);
	}
	
	public List<Attachment> getAttachmentsOfProcedure(int procedureID) {
		return patientDAO.getAttachmentsOfProcedure(procedureID);
	}
	
	public Attachment getAttachment(int attachmentID) {
		return patientDAO.getAttachment(attachmentID);
	}
}
