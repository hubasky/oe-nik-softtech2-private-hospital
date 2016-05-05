package hubasky.database.domain;

import java.io.Serializable;
import javax.persistence.*;

import org.hibernate.annotations.Proxy;

import java.util.List;


/**
 * The persistent class for the Doctor database table.
 * 
 */
@Entity
@Proxy(lazy=false)
@Table(name="Employees")
@NamedQuery(name="Doctor.findAll", query="SELECT d FROM Doctor d")
public class Doctor implements Serializable {
	private static final long serialVersionUID = 1L;

	@Id
	@Column(name="Username")
	private String employeeID;

	@Column(name="Address")
	private String address;

	@Column(name="Role")
	private String role;

	@Column(name="Name")
	private String name;

	@Column(name="Phone")
	private String phoneNumber;
	
	//bi-directional many-to-one association to _Procedure
	@OneToMany(mappedBy="doctor")
	private List<_Procedure> procedures;

	public Doctor() {
	}

	
	
	public String getEmployeeID() {
		return this.employeeID;
	}

	public void setEmployeeID(String employeeID) {
		this.employeeID = employeeID;
	}

	public String getAddress() {
		return this.address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getRole() {
		return this.role;
	}

	public void setRole(String role) {
		this.role = role;
	}

	public String getName() {
		return this.name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPhoneNumber() {
		return this.phoneNumber;
	}

	public void setPhoneNumber(String phoneNumber) {
		this.phoneNumber = phoneNumber;
	}

	public List<_Procedure> getProcedures() {
		return this.procedures;
	}

	public void setProcedures(List<_Procedure> procedures) {
		this.procedures = procedures;
	}

	public _Procedure addProcedure(_Procedure procedure) {
		getProcedures().add(procedure);
		procedure.setDoctor(this);

		return procedure;
	}

	public _Procedure removeProcedure(_Procedure procedure) {
		getProcedures().remove(procedure);
		procedure.setDoctor(null);

		return procedure;
	}

}