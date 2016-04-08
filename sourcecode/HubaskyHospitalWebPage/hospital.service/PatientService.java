package WebDataManagement.hospital.service;

import WebDataManagement.hospital.database.dao.PatientDao;
import WebDataManagement.hospital.database.domain.Patient;

/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:19
 */
public class PatientService {

	private PatientDao patientDaoLocal;

	public PatientService(){

	}

	public void finalize() throws Throwable {

	}

	/**
	 * 
	 * @param patient
	 */
	public List<MedicalRecord> getMedicalRecordsOfPatient(Patient patient){
		return null;
	}

	/**
	 * 
	 * @param procedure
	 */
	public List<Procedure> getProceduresOfMedicalRecord(Procedure procedure){
		return null;
	}

}