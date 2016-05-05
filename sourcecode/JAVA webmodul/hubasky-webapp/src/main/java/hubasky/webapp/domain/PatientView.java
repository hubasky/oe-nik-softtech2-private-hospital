package hubasky.webapp.domain;

import java.util.List;

import hubasky.database.domain.MedicalRecord;

public class PatientView {
	
	private String socialSecurityNumber;
	
	private String address;

	private int gender;

	private String name;

	private String phoneNumber;

	private List<MedicalRecord> medicalRecords;
	
			
	public PatientView() {
		super();
	}

	public String getSocialSecurityNumber() {
		return socialSecurityNumber;
	}

	public void setSocialSecurityNumber(String string) {
		this.socialSecurityNumber = string;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public int getGender() {
		return gender;
	}

	public void setGender(int i) {
		this.gender = i;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPhoneNumber() {
		return phoneNumber;
	}

	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}

	public List<MedicalRecord> getMedicalRecords() {
		return medicalRecords;
	}

	public void setMedicalRecords(List<MedicalRecord> medicalRecords) {
		this.medicalRecords = medicalRecords;
	}

	
	
}
