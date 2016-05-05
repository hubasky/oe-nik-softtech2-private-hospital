<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
<!DOCTYPE html>
<html>
<head>
<link rel="stylesheet"
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/>
</head>
<body>
    <div class="container">
    	<h3>
			Hello, <c:out value="${patient}"/>!
		</h3> 
		<h3>
			<a href='<c:url value="/secure/logout"></c:url>'>Logout</a>
		</h3>
		<h3>
			<a href='<c:url value="/secure/listMedicalRecords"></c:url>'>Medical Records</a>
		</h3>
		<h3>
			List of procedures regarding medical record with ID <c:out value="${medicalRecordID}" />:
		</h3>
		<table class="table table-striped table-bordered">
			<tr>
				<th>Procedure ID</th>
				<th>Medical Record ID</th>
				<th>Doctor</th>
				<th>Time of Creation</th>
				<th>Time of last modification</th>
				<th>Details</th>
			</tr>
			<c:forEach var="procedure" items="${procedures}">
				<tr>
					<td><c:out value="${procedure.procedureID}"></c:out></td>
					<td><c:out value="${procedure.medicalRecord.medicalRecordID}"></c:out></td>
					<td><c:out value="${procedure.doctor.name}"></c:out></td>
					<td><c:out value="${procedure.createDate}"></c:out></td>
					<td><c:out value="${procedure.timeOfLastMod}"></c:out></td>
					<td><a href="procedure?procedureID=${procedure.procedureID}&medicalRecordID=${medicalRecordID}">details</a></td>
			</c:forEach>
		</table>
		
	</div>
</body>
</html>