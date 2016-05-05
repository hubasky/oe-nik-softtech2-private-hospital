package hubasky.database.domain;

import java.io.Serializable;
import javax.persistence.*;

import org.hibernate.annotations.Proxy;
import java.util.List;


/**
 * The persistent class for the MedicalRecord database table.
 * 
 */
@Entity
@Proxy(lazy=false)
@Table(name="MedicalRecords")
@NamedQuery(name="MedicalRecord.findAll", query="SELECT m FROM MedicalRecord m")
public class MedicalRecord implements Serializable {
	private static final long serialVersionUID = 1L;

	@Id
	@Column(name="Id")
	@GeneratedValue(strategy=GenerationType.SEQUENCE)
	private int medicalRecordID;

	@Column(name="CreatedTimestamp")
	private String timeOfCreation;
	
	@Column(name="LastModifiedTimestamp")
	private String timeOfLastMod;
	
	@Column(name="State")
	private int state;
	
	@Column(name="ShortDescription")
	private String shortDescription;
	
	//bi-directional many-to-one association to Patient
	@ManyToOne
	@JoinColumn(name="Patient_Ssn")
	private Patient patient;

	//bi-directional many-to-one association to _Procedure
	@OneToMany(mappedBy="medicalRecord")
	private List<_Procedure> procedures;

	public MedicalRecord() {
	}

	public int getMedicalRecordID() {
		return this.medicalRecordID;
	}

	public void setMedicalRecordID(int medicalRecordID) {
		this.medicalRecordID = medicalRecordID;
	}

	public Patient getPatient() {
		return this.patient;
	}

	public void setPatient(Patient patient) {
		this.patient = patient;
	}

	public List<_Procedure> getProcedures() {
		return this.procedures;
	}

	public void setProcedures(List<_Procedure> procedures) {
		this.procedures = procedures;
	}

	public _Procedure addProcedure(_Procedure procedure) {
		getProcedures().add(procedure);
		procedure.setMedicalRecord(this);

		return procedure;
	}

	public _Procedure removeProcedure(_Procedure procedure) {
		getProcedures().remove(procedure);
		procedure.setMedicalRecord(null);

		return procedure;
	}

	public String getTimeOfCreation() {
		return timeOfCreation;
	}

	public void setTimeOfCreation(String timeOfCreation) {
		this.timeOfCreation = timeOfCreation;
	}

	public String getTimeOfLastMod() {
		return timeOfLastMod;
	}

	public void setTimeOfLastMod(String timeOfLastMod) {
		this.timeOfLastMod = timeOfLastMod;
	}

	public int getState() {
		return state;
	}

	public void setState(int state) {
		this.state = state;
	}

	public String getShortDescription() {
		return shortDescription;
	}

	public void setShortDescription(String shortDescription) {
		this.shortDescription = shortDescription;
	}

	
	
}