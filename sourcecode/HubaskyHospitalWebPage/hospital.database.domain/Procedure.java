package WebDataManagement.hospital.database.domain;


/**
 * @author justo
 * @version 1.0
 * @created 07-ápr.-2016 12:45:19
 */
public class Procedure {

	private List<File> attachments;
	private Date date;
	private String diagnosis;
	private int duration;
	private String name;
	private enum paymentStatus;
	private int price;
	private Doctor doctor;
	private MedicalRecord medicalRecord;

	public Procedure(){

	}

	public void finalize() throws Throwable {

	}

	public List<String> getAttachments(){
		return attachments;
	}

	public Date getDate(){
		return date;
	}

	public String getDiagnosis(){
		return diagnosis;
	}

	public int getDuration(){
		return duration;
	}

	public String getName(){
		return name;
	}

	public Enum getPaymentStatus(){
		return paymentStatus;
	}

	public int getPrice(){
		return price;
	}

}