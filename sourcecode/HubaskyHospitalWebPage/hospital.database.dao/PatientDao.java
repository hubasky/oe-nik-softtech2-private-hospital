package WebDataManagement.hospital.database.dao;

import WebDataManagement.hospital.database.domain.Patient;

/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:18
 */
public class PatientDao implements PatientDaoLocal {

	private EntityManager entityManager;

	public PatientDao(){

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