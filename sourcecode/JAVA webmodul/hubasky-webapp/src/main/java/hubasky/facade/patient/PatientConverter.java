package hubasky.facade.patient;

import javax.ejb.Stateless;

import hubasky.database.domain.Patient;
import hubasky.webapp.domain.PatientView;

@Stateless
public class PatientConverter {
	
	public PatientView convert(Patient patient)
	{
		PatientView patientView = new PatientView();
		patientView.setAddress(patient.getAddress());
		patientView.setGender(patient.getGender());
		patientView.setName(patient.getName());
		patientView.setPhoneNumber(patient.getPhoneNumber());
		patientView.setSocialSecurityNumber(patient.getSocialSecurityNumber());
		patientView.setMedicalRecords(patient.getMedicalRecords());
		return patientView;
				
	}
	
}
