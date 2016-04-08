package WebDataManagement.hostpital.webapp;

import WebDataManagement.hospital.database.domain.Procedure;

/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:19
 */
public class PatientView {

	private String address;
	private Date dateOfBirth;
	private enum gender;
	private String name;
	private String phoneNumber;
	private int socialSecurityNumber;
	private Procedure medicalHistory;

	public PatientView(){

	}

	public void finalize() throws Throwable {

	}

	public String getAddress(){
		return address;
	}

	public Date getDateOfBirth(){
		return dateOfBirth;
	}

	public Enum getGender(){
		return gender;
	}

	public String getName(){
		return name;
	}

	public String getPhoneNumber(){
		return phoneNumber;
	}

	public int getSocialSecurityNumber(){
		return socialSecurityNumber;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setAddress(String newVal){
		address = newVal;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setDateOfBirth(Date newVal){
		dateOfBirth = newVal;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setGender(Enum newVal){
		gender = newVal;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setName(String newVal){
		name = newVal;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setPhoneNumber(String newVal){
		phoneNumber = newVal;
	}

	/**
	 * 
	 * @param newVal
	 */
	public void setSocialSecurityNumber(int newVal){
		socialSecurityNumber = newVal;
	}

}