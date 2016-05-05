package hubasky.database.domain;

import java.io.Serializable;
import javax.persistence.*;

import org.hibernate.annotations.Proxy;

import java.util.List;


/**
 * The persistent class for the Patient database table.
 * 
 */
@Entity
@Proxy(lazy=false)
@Table(name="Patients")
@NamedQuery(name="Patient.findAll", query="SELECT p FROM Patient p")
public class Patient implements Serializable {
	
	private static final long serialVersionUID = 1L;

	@Id
	@Column(name="Ssn")
	private String socialSecurityNumber;

	@Column(name="Address")
	private String address;

	@Column(name="Password")
	private String password;
	
	@Column(name="Gender")
	private int gender;
	
	@Column(name="Name")
	private String name;
	
	@Column(name="Phone")
	private String phoneNumber;
	
	@Column(name="DateOfBirth")
	private String dateOfBirth;

	//bi-directional many-to-one association to MedicalRecord
	@OneToMany(mappedBy="patient")
	private List<MedicalRecord> medicalRecords;
	
	
	
	public String getSocialSecurityNumber() {
		return socialSecurityNumber;
	}



	public void setSocialSecurityNumber(String socialSecurityNumber) {
		this.socialSecurityNumber = socialSecurityNumber;
	}



	public String getAddress() {
		return address;
	}



	public void setAddress(String address) {
		this.address = address;
	}



	public String getPassword() {
		return password;
	}



	public void setPassword(String password) {
		this.password = password;
	}



	public int getGender() {
		return gender;
	}



	public void setGender(int gender) {
		this.gender = gender;
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



	public String getDateOfBirth() {
		return dateOfBirth;
	}



	public void setDateOfBirth(String dateOfBirth) {
		this.dateOfBirth = dateOfBirth;
	}

	
	public Patient() {
	}

	

	public List<MedicalRecord> getMedicalRecords() {
		return this.medicalRecords;
	}

	public void setMedicalRecords(List<MedicalRecord> medicalRecords) {
		this.medicalRecords = medicalRecords;
	}

	public MedicalRecord addMedicalRecord(MedicalRecord medicalRecord) {
		getMedicalRecords().add(medicalRecord);
		medicalRecord.setPatient(this);

		return medicalRecord;
	}

	public MedicalRecord removeMedicalRecord(MedicalRecord medicalRecord) {
		getMedicalRecords().remove(medicalRecord);
		medicalRecord.setPatient(null);

		return medicalRecord;
	}

}