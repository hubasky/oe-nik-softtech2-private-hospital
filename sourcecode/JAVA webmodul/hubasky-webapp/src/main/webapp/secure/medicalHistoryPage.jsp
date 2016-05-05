<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
<!DOCTYPE html>
<html>
<head>
<link rel="stylesheet"
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
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
			List of Medical Records:
		</h3>
		<table class="table table-striped table-bordered">
			<tr>
				<th>ID</th>
				<th>Time of creation</th>
				<th>Time of last modification</th>
				<!-- <th>Created by</th> -->
				<th>Details</th>
			</tr>

			<c:forEach var="record" items="${records}">
				<tr>
					<td><c:out value="${record.medicalRecordID}"></c:out></td>
					<td><c:out value="${record.timeOfCreation}"	 ></c:out></td>
					<td><c:out value="${record.timeOfLastMod}"	 ></c:out></td>
					<%-- <td><c:out value="${thread.user.name}"></c:out></td> --%>
					<td><a href="listProcedures?medicalRecordID=${record.medicalRecordID}">details</a></td>
			</c:forEach>
		</table>
	</div>
</body>
</html>