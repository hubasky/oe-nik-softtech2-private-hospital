package hubasky.database.jpa;

import java.util.List;

import hubasky.database.domain.Attachment;
import hubasky.database.domain.MedicalRecord;
import hubasky.database.domain.Patient;
import hubasky.database.domain._Procedure;

public interface PatientDAOLocal {

	List<Patient> getPatients();

	Patient getPatient(String ssn);

	_Procedure getProcedure(int procedureID);

	List<MedicalRecord> getMedicalRecordsOfPatient(String ssn);

	List<_Procedure> getProceduresOfMedicalRecord(int medicalRecordID);

	List<Attachment> getAttachmentsOfProcedure(int procedureID);

	Attachment getAttachment(int attachmentID);

}