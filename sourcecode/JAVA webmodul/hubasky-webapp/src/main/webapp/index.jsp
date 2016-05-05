<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<!DOCTYPE html>
<html>
<head>
<link rel="stylesheet"
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
</head>
<body>
	<div class="container">
		<h2>
			HUBASKY Hospital
		</h2>
		<form method="post" action="j_security_check">
			<p>
				     SSN: <input type="text" name="j_username" />
			</p>
			<p>
				Password: <input type="password" name="j_password" />
			</p>
			<p>
				<input type="submit" value="Login" />
			</p>
		</form>
	</div>
</body>
</html>
