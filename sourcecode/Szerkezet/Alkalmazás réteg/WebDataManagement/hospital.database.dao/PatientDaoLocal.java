package WebDataManagement.hospital.database.dao;


/**
 * @Local
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:18
 */
public interface PatientDaoLocal {

	/**
	 * 
	 * @param patient
	 */
	public List<MedicalRecord> getMedicalRecordsOfPatient(Patient patient);

	/**
	 * 
	 * @param procedure
	 */
	public List<Procedure> getProceduresOfMedicalRecord(Procedure procedure);

}