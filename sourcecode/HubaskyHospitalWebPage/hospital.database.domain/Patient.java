package WebDataManagement.hospital.database.domain;


/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:18
 */
public class Patient {

	private String address;
	private Date dateOfBirth;
	private enum gender;
	private String name;
	private String password;
	private String phoneNumber;
	private int socialSecurityNumber;
	private List<MedicalRecord> medicalHistory;

	public Patient(){

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

}