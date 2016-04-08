package WebDataManagement.hostpital.webapp;

import WebDataManagement.hospital.service.PatientService;

/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:18
 */
public class PatientFacade {

	private PatientConverter patientConverter;
	private PatientService patientService;

	public PatientFacade(){

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
	 * @param medicalRecord
	 */
	public List<Procedure> getProceduresOfMedicalRecord(MedicalRecord medicalRecord){
		return null;
	}

}