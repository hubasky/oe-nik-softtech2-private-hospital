package hubasky.webapp.medicalrec;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import javax.ejb.EJB;
import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import hubasky.database.domain.Attachment;
import hubasky.facade.patient.PatientFacade;

@WebServlet(name = "ImageServlet", urlPatterns = {"/secure/docs/*"})
public class LoadAttachment extends HttpServlet{

	
		private static final long serialVersionUID = 1L;
		
		@EJB
	    private PatientFacade patientFacade;
		
		private static final String staticPath = "C:\\Hubasky\\hubasky\\attachments\\";
				
	    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
	            throws ServletException, IOException {


	        int id = Integer.parseInt(request.getParameter("id"));
	        
	        	        
	        String path = ""; 
	        //byte[] image = patientFacade.getAttachment(id);
	        Attachment attachment = patientFacade.getAttachment(id);
	        path = staticPath + attachment.getPathToFile();
	        
	        /*request.setAttribute("path", path);
	        String extension = path.split("\\.")[1];*/
	        
	        File file = new File(path);
	        FileInputStream in = new FileInputStream(file);
	        	        
	        byte[] bytesArray = new byte[4096];
            int bytesRead = -1;
            ServletOutputStream outputStream = response.getOutputStream();
            while ((bytesRead = in.read(bytesArray)) != -1) {
            	outputStream.write(bytesArray, 0, bytesRead);
            }
	        
	        in.close();
	        outputStream.close();
	        
	               	        
	        //response.setContentType("image/jpeg");
	        //ServletOutputStream outputStream = response.getOutputStream();
	        //outputStream.write(image);
	    }

	  
	    @Override
	    protected void doGet(HttpServletRequest request, HttpServletResponse response)
	            throws ServletException, IOException {
	        processRequest(request, response);
	        request.getRequestDispatcher("/secure/procedureViewPage.jsp").forward(request, response);
	    }
	    
	    @Override
	    protected void doPost(HttpServletRequest request, HttpServletResponse response)
	            throws ServletException, IOException {
	        processRequest(request, response);
	    }
	
}
