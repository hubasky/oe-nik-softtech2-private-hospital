CREATE TABLE Patient
(
socialSecurityNumber int PRIMARY KEY,
name varchar(255),
gender varchar(255),
_address varchar(255),
_password varchar(255),
phoneNumber varchar(255),
);

CREATE TABLE MedicalRecord
(
medicalRecordID int IDENTITY(1,1) PRIMARY KEY,
socialSecurityNumber int,
createDate date,
FOREIGN KEY (socialSecurityNumber) REFERENCES Patient(socialSecurityNumber)	
);


CREATE TABLE Doctor
(
employeeID int IDENTITY(1,1) PRIMARY KEY,
name varchar(255),
_role varchar(255),
_address varchar(255),
phoneNumber varchar(255)	
);

CREATE TABLE _Procedure
(
procedureID int IDENTITY(1,1) PRIMARY KEY,
medicalRecordID int,
employeeID int,
createDate date,
diagnosis varchar(255),
duration int,
price int,
name varchar(255),
paymentStatus varchar(255),
FOREIGN KEY (employeeID) REFERENCES Doctor(employeeID),	
FOREIGN KEY (medicalRecordID) REFERENCES MedicalRecord(medicalRecordID)	
);

CREATE TABLE Attachments
(
attachmentID int IDENTITY(1,1) PRIMARY KEY,
procedureID int,
pathToFile varchar(255),
attachmentName varchar(255)
FOREIGN KEY (procedureID) REFERENCES _Procedure(procedureID)	
);



INSERT INTO Patient (socialSecurityNumber, name, gender, _address, _password, phoneNumber)
    VALUES (1234576, 'Timmy', 'male', 'teszt1', 'teszt1', 'teszt1'),
		   (6541432, 'Claire', 'female', 'teszt2', 'teszt2', 'teszt2'),
           (7894225, 'Sally', 'female', 'teszt3', 'teszt3', 'teszt3');

INSERT INTO MedicalRecord (socialSecurityNumber, createDate)
    VALUES (1234576, '1912-10-25'),
		   (6541432, '1922-09-15'),
           (7894225, '1912-12-25'),
		   (1234576, '1945-05-05'),
		   (6541432, '1999-01-17'),
           (7894225, '2009-06-02');

INSERT INTO Doctor (name, _role, _address, phoneNumber)
    VALUES ('Hannibal Lecter', 'male', 'teszt1', 'teszt1'),
		   ('Hasfelmetsző Jack', 'female', 'teszt2', 'teszt2'),
           ('Dr Manhattan', 'female', 'teszt3', 'teszt3');

INSERT INTO _Procedure (medicalRecordID, createDate, diagnosis, duration, price, name, paymentStatus, employeeID)
    VALUES (1, '1912-10-25', 'teszt1', '132412', '2435', 'teszt1', 'teszt1', 1),
		   (2, '1922-09-15', 'teszt1', '12341234', '23452345', 'teszt2', 'teszt1', 1),
           (3, '1912-12-25', 'teszt1', '123412', '1324234', 'teszt3', 'teszt1', 2),
		   (4, '1945-05-05', 'teszt1', '46654', '12341234', 'teszt1', 'teszt1',2),
		   (5, '1999-01-17', 'teszt1', '4352', '1123', 'teszt2', 'teszt1', 2),
           (6, '2009-06-02', 'teszt1', '1234', '34252435', 'teszt3', 'teszt1', 3);

INSERT INTO Attachments(procedureID, pathToFile, attachmentName)
    VALUES (1, 'teszt1', 'agafdg'),
		   (2, 'teszt1', '1234asdfasdf1234'),
           (2, 'teszt1', 'asdfasdf'),
		   (4, 'teszt1', 'ghfjghjg'),
		   (4, 'teszt1', 'fgjhjh'),
           (6, 'teszt1', 'sdfgsdfgsdf');

		   
ALTER TABLE Attachments
ADD picture image

insert into Attachments (picture) 
SELECT BulkColumn 
FROM Openrowset( Bulk 'F:\Repok\images\images4.jpg', Single_Blob) as image