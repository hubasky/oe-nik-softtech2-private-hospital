package hubasky.webapp.medicalrec;

import java.io.IOException;

import javax.ejb.EJB;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import hubasky.facade.patient.PatientFacade;

@WebServlet("/secure/procedure")
public class LoadProcedure extends HttpServlet {

		private static final long serialVersionUID = 1L;

		@EJB
		private PatientFacade patientFacade;
		
		@Override
		protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
			int medicalRecordID = Integer.valueOf(req.getParameter("medicalRecordID"));
			int procedureID = Integer.valueOf(req.getParameter("procedureID"));
			req.setAttribute("patient", patientFacade.getPatient(req.getUserPrincipal().getName()).getName());
			req.setAttribute("procedureID", procedureID);
			req.setAttribute("procedure", patientFacade.getProcedure(procedureID));
			req.setAttribute("medicalRecordID", medicalRecordID);
			req.setAttribute("attachments", patientFacade.getAttachmentsOfProcedure(procedureID));
			req.getRequestDispatcher("/secure/procedureViewPage.jsp").forward(req, resp);
		}

		
		
	
	
}