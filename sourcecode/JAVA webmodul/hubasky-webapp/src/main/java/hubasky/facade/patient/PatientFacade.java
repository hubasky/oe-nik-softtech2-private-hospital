package hubasky.facade.patient;

import java.util.List;

import javax.ejb.EJB;
import javax.ejb.Stateless;

import hubasky.database.domain.Attachment;
import hubasky.database.domain.MedicalRecord;
import hubasky.database.domain.Patient;
import hubasky.database.domain._Procedure;
import hubasky.service.PatientService;

@Stateless
public class PatientFacade {
	
	@EJB
	private PatientService patientService;
	
	@EJB
	private PatientConverter patientConverter;
	
	public List<MedicalRecord> getMedicalRecordsOfPatient(String ssn) {
		return patientService.getMedicalRecordsOfPatient(ssn);
	}
	
	public Patient getPatient(String ssn){
		return patientService.getPatient(ssn);
	}
	
	public List<Patient> getPatients() {
		return patientService.getPatients();
	}

	public List<_Procedure> getProceduresOfMedicalRecord(int medicalRecordID) {
		return patientService.getProceduresOfMedicalRecord(medicalRecordID);
	}

	public _Procedure getProcedure(int procedureID) {
		return patientService.getProcedure(procedureID);
	}
	
	public List<Attachment> getAttachmentsOfProcedure(int procedureID) {
		return patientService.getAttachmentsOfProcedure(procedureID);
	}
	
	public Attachment getAttachment(int attachmentID) {
		return patientService.getAttachment(attachmentID);
	}
}
