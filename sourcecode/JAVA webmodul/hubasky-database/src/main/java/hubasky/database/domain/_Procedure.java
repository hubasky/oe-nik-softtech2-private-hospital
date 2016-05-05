package hubasky.database.domain;

import java.io.Serializable;
import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;

import org.hibernate.annotations.Proxy;


/**
 * The persistent class for the _Procedure database table.
 * 
 */
@Entity
@Proxy(lazy=false)
@Table(name="Procedures")
@NamedQuery(name="_Procedure.findAll", query="SELECT _ FROM _Procedure _")
public class _Procedure implements Serializable {
	private static final long serialVersionUID = 1L;

	@Id
	@Column(name="Id")
	@GeneratedValue(strategy=GenerationType.SEQUENCE)
	private int procedureID;

	@Column(name="CreatedTimestamp")
	private String createDate;
	
	@Column(name="LastModifiedTimestamp")
	private String timeOfLastMod;
	
	@Column(name="Anamnesis")
	private String anamnesis;
	
	@Column(name="Diagnosis")
	private String diagnosis;

	@Column(name="Duration")
	private int duration;
	
	@Column(name="State")
	private int state;

	@Column(name="Price")
	private int price;

	//bi-directional many-to-one association to Attachment
	@OneToMany(mappedBy="procedure")
	private List<Attachment> attachments;

	//bi-directional many-to-one association to Doctor
	@ManyToOne
	@JoinColumn(name="Responsible_Username")
	private Doctor doctor;

	//bi-directional many-to-one association to MedicalRecord
	@ManyToOne
	@JoinColumn(name="MedicalRecord_Id", insertable=false, updatable=false)
	private MedicalRecord medicalRecord;
	
	
	public _Procedure() {
	}

	
	
	public String getTimeOfLastMod() {
		return timeOfLastMod;
	}



	public void setTimeOfLastMod(String timeOfLastMod) {
		this.timeOfLastMod = timeOfLastMod;
	}



	public String getAnamnesis() {
		return anamnesis;
	}



	public void setAnamnesis(String anamnesis) {
		this.anamnesis = anamnesis;
	}



	public int getState() {
		return state;
	}



	public void setState(int state) {
		this.state = state;
	}



	public int getProcedureID() {
		return this.procedureID;
	}

	public void setProcedureID(int procedureID) {
		this.procedureID = procedureID;
	}

	public String getCreateDate() {
		return this.createDate;
	}

	public void setCreateDate(String createDate) {
		this.createDate = createDate;
	}

	public String getDiagnosis() {
		return this.diagnosis;
	}

	public void setDiagnosis(String diagnosis) {
		this.diagnosis = diagnosis;
	}

	public int getDuration() {
		return this.duration;
	}

	public void setDuration(int duration) {
		this.duration = duration;
	}

	public int getPrice() {
		return this.price;
	}

	public void setPrice(int price) {
		this.price = price;
	}

	public List<Attachment> getAttachments() {
		return this.attachments;
	}

	public void setAttachments(List<Attachment> attachments) {
		this.attachments = attachments;
	}

	public Attachment addAttachment(Attachment attachment) {
		getAttachments().add(attachment);
		attachment.setProcedure(this);

		return attachment;
	}

	public Attachment removeAttachment(Attachment attachment) {
		getAttachments().remove(attachment);
		attachment.setProcedure(null);

		return attachment;
	}

	public Doctor getDoctor() {
		return this.doctor;
	}

	public void setDoctor(Doctor doctor) {
		this.doctor = doctor;
	}

	public MedicalRecord getMedicalRecord() {
		return this.medicalRecord;
	}

	public void setMedicalRecord(MedicalRecord medicalRecord) {
		this.medicalRecord = medicalRecord;
	}

}