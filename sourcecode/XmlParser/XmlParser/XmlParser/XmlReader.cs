using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XmlParser
{
    class XmlReader
    {
        private static XDocument xDoc;
        private static Dictionary<string, string> procedures;

        public XDocument XDoc
        {
            get { return xDoc; }
            set { xDoc = value; }
        }

        public XmlReader()
        {
            xDoc = XDocument.Load(@"..\kezelesek.xml");
            LoadProcedures();
        }

        private void LoadProcedures()
        {
            //int = xDoc.Descendants("").Count();
            /*for (int i = 1; i <= ; i++)
            {
                XElement element = xDoc.Descendants("Figure").Where(f => int.Parse(f.Attribute("id").Value) == i).FirstOrDefault();
                if (element != null)
                {
                    
                }
            }*/
        }
    }
}
