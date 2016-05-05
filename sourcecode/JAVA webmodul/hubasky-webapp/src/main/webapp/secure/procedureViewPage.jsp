<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %>

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
			<a href='<c:url value="/secure/listProcedures?medicalRecordID=${medicalRecordID}"></c:url>'>List of procedures</a>
		</h3>
		<h3>
			Details of procedure with ID <c:out value="${procedureID}" />:
		</h3>
		<table class="table table-striped table-bordered">
			<tr>
				<th>Procedure ID</th>
				<th>Medical Record ID</th>
				<th>Doctor</th>
				<th>Date of Creation</th>
				<th>Date of last modification</th>
				<th>Duration</th>
				<th>Price</th>
				<th>Payment status</th>
			</tr>
			<%-- <c:forEach var="procedure" items="${procedure}"> --%>
				<tr>
					<td><c:out value="${procedure.procedureID}"></c:out></td>
					<td><c:out value="${procedure.medicalRecord.medicalRecordID}"></c:out></td>
					<td><c:out value="${procedure.doctor.name}"></c:out></td>
					<td><c:out value="${procedure.createDate}"></c:out></td>
					<td><c:out value="${procedure.timeOfLastMod}"></c:out></td>
					<td><c:out value="${procedure.duration}"></c:out></td>
					<td><c:out value="${procedure.price}"></c:out></td>
					<td><c:out value="${procedure.state}"></c:out></td>
					
			<%-- </c:forEach> --%>
		</table>
		
		<div class="container">
		<h3 style="bold">Anamnesis</h3>
		<p><c:out value="${procedure.anamnesis}"></c:out></p>
		</div>
		
		<div class="container">
		<h3>Diagnosis</h3>
		<p><c:out value="${procedure.diagnosis}"></c:out></p>
		</div>
		
		<table class="table table-striped table-bordered">
			<tr>
				<th>Attachment ID</th>
				<th>Procedure ID</th>
				<!-- <th>Attachment name</th> -->
				<th>Image</th>
				<th>Download</th>
			</tr>
			<c:forEach var="attachment" items="${attachments}">
				<tr>
					<c:set var="extension" value="${attachment.pathToFile}"/>
					<%-- <td><c:out value="${path}"></c:out></td> --%>
					<td><c:out value="${attachment.attachmentID}"></c:out></td>
					<td><c:out value="${attachment.procedure.procedureID}"></c:out></td>
					<%-- <td><c:out value="${attachment.attachmentName}"></c:out></td> --%>
					<%-- <a href="listProcedures?medicalRecordID=${record.medicalRecordID}">image</a> --%>
					<%-- <td><a href="listProcedures?medicalRecordID=${record.medicalRecordID}">image</a></td> --%>
					<c:choose>
					    <c:when test="${fn:substringAfter(extension, '.').equals('jpg')}">
					        <td> <a href="docs?id=${attachment.attachmentID}" ><img src="docs?id=${attachment.attachmentID}" width=128 height=128 ></a> </td>
					    </c:when>
					    <c:when test="${fn:substringAfter(extension, '.').equals('pdf')}">
					        <td> <a href="docs?id=${attachment.attachmentID}" ><img src="http://icons.iconarchive.com/icons/treetog/file-type/128/pdf-icon.png" width=128 height=128 ></a> </td>
					    </c:when>
					    <c:when test="${fn:substringAfter(extension, '.').equals('docx')}">
					    	<td> <a href="docs?id=${attachment.attachmentID}" ><img src="http://icons.iconarchive.com/icons/treetog/file-type/128/docx-win-icon.png" width=128 height=128 ></a> </td>
					    </c:when>
					    <c:otherwise>
					        <td>Not supported format.</td>>
					    </c:otherwise>
					</c:choose>
					
					<td><a href="docs?id=${attachment.attachmentID}" download="mydocument${attachment.attachmentID}.${fn:substringAfter(extension, '.')}">download</a></td>
			</c:forEach>
		</table>
		
	</div>
</body>
</html>