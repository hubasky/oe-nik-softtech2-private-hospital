package hubasky.database.domain;

import java.io.Serializable;
import javax.persistence.*;

import org.hibernate.annotations.Proxy;


/**
 * The persistent class for the Attachments database table.
 * 
 */
@Entity
@Proxy(lazy=false)
@Table(name="Attachments")
@NamedQuery(name="Attachment.findAll", query="SELECT a FROM Attachment a")
public class Attachment implements Serializable {
	private static final long serialVersionUID = 1L;

	@Id
	@Column(name="Id")
	@GeneratedValue(strategy=GenerationType.SEQUENCE)
	private int attachmentID;


	//private String attachmentName;
	
	@Column(name="[File]")
	private String pathToFile;

	//bi-directional many-to-one association to _Procedure
	@ManyToOne
	@JoinColumn(name="Procedure_Id", insertable=false, updatable=false)
	private _Procedure procedure;
	
	/*@Lob
	private byte[] picture;
	

	public byte[] getPicture() {
		return picture;
	}


	public void setPicture(byte[] picture) {
		this.picture = picture;
	}*/

	
	
	public Attachment() {
	}

	@Override
	public String toString() {
		return "Attachment [attachmentID=" + attachmentID + ", pathToFile=" + pathToFile + "]";
	}

	public int getAttachmentID() {
		return this.attachmentID;
	}

	public void setAttachmentID(int attachmentID) {
		this.attachmentID = attachmentID;
	}

	/*public String getAttachmentName() {
		return this.attachmentName;
	}

	public void setAttachmentName(String attachmentName) {
		this.attachmentName = attachmentName;
	}*/

	public String getPathToFile() {
		return this.pathToFile;
	}

	public void setPathToFile(String pathToFile) {
		this.pathToFile = pathToFile;
	}

	public _Procedure getProcedure() {
		return this.procedure;
	}

	public void setProcedure(_Procedure procedure) {
		this.procedure = procedure;
	}

}