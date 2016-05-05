package hubasky.webapp.medicalrec;

import java.io.IOException;

import javax.ejb.EJB;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import hubasky.facade.patient.PatientFacade;

@WebServlet("/secure/listMedicalRecords")
public class ListMedicalRecords extends HttpServlet {

		private static final long serialVersionUID = 1L;

		@EJB
		private PatientFacade patientFacade;

		@Override
		protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
			req.setAttribute("ssn", req.getUserPrincipal().getName());
			req.setAttribute("records", patientFacade.getMedicalRecordsOfPatient(req.getUserPrincipal().getName()));
			req.setAttribute("patient", patientFacade.getPatient(req.getUserPrincipal().getName()).getName());
			req.getRequestDispatcher("/secure/medicalHistoryPage.jsp").forward(req, resp);
		}

	
	
}
