package hubasky.database.jpa;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.ejb.Stateless;

import hubasky.database.domain.*;

@Stateless
public class PatientDAO implements PatientDAOLocal { //implements Autoclosable 
	
	@PersistenceContext(unitName = "GenericDBAdapter")
	private EntityManager em;

	/*public PatientDAO(){
		emf = Persistence.createEntityManagerFactory("GenericDBAdapter");		
	}*/
	
	
	@Override
	public List<Patient> getPatients(){
		//EntityManager em = emf.createEntityManager();
		List<Patient> patientList;
		/*try {
			em.getTransaction().begin();*/
			TypedQuery<Patient> patientQuery = em.createQuery("SELECT p from Patient p", Patient.class);
			patientList = patientQuery.getResultList();
			/*em.getTransaction().commit();
		}
		finally {
			em.close();
		}*/
		return patientList;
	}
	
	/*public void close() throws Exception {
		if (emf != null){
			emf.close();
		}
		
	}*/
	
	@Override
	public Patient getPatient(String ssn){
		Patient patient = em.find(Patient.class, ssn);
		return patient;
	}
	
	@Override
	public _Procedure getProcedure(int procedureID) {
		_Procedure procedure = em.find(_Procedure.class, procedureID);
		return procedure;
	}
	
	@Override
	public List<MedicalRecord> getMedicalRecordsOfPatient(String ssn){
		Patient patient = em.find(Patient.class, ssn);
		TypedQuery<MedicalRecord> query = em.createQuery("SELECT m FROM MedicalRecord m WHERE m.patient = :patient", MedicalRecord.class);
		query.setParameter("patient", patient);
		return new ArrayList<>(query.getResultList());		
	}

	@Override
	public List<_Procedure> getProceduresOfMedicalRecord(int medicalRecordID) {
		MedicalRecord medicalRecord = em.find(MedicalRecord.class, medicalRecordID);
		TypedQuery<_Procedure> query = em.createQuery("SELECT p FROM _Procedure p WHERE p.medicalRecord = :medicalRecord", _Procedure.class);
		query.setParameter("medicalRecord", medicalRecord);
		return new ArrayList<>(query.getResultList());
	}
	
	@Override
	public List<Attachment> getAttachmentsOfProcedure(int procedureID){
		_Procedure procedure = em.find(_Procedure.class, procedureID);
		TypedQuery<Attachment> query = em.createQuery("SELECT a FROM Attachment a WHERE a.procedure = :procedure", Attachment.class);
		query.setParameter("procedure", procedure);
		return new ArrayList<>(query.getResultList());
	}
	
	@Override
	public Attachment getAttachment(int attachmentID){
		Attachment attachment = em.find(Attachment.class, attachmentID);
		return attachment;
	}
	
	
}
