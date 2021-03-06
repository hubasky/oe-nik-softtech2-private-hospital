﻿using System;
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
        private static List<string> wards;

        public static Dictionary<string, string> Procedures
        {
            get { return XmlReader.procedures; }
            set { XmlReader.procedures = value; }
        }

        private string id;
        private string description;

        public XDocument XDoc
        {
            get { return xDoc; }
            set { xDoc = value; }
        }

        public XmlReader()
        {
            xDoc = XDocument.Load(@"..\kezelesek.xml");
            procedures = new Dictionary<string,string>();
            wards = new List<string>();
            LoadProcedures();
        }

        private void LoadProcedures()
        {
            int numberOfWards = xDoc.Descendants("ward").Count();
            int numberOfProcedures = 0;
            for (int i = 0; i < numberOfWards; i++)
            {
                numberOfProcedures = xDoc.Descendants("ward").ElementAt(i).Descendants("procedure").Count();
                wards.Add((string)xDoc.Descendants("ward").ElementAt(i).Descendants("name").ElementAt(0));
                for (int j = 0; j < numberOfProcedures; j++)
                {
                    id = (string)xDoc.Descendants("ward").ElementAt(i).
                        Descendants("procedure").ElementAt(j).
                        Descendants("id").ElementAt(0);
                    description = (string)xDoc.Descendants("ward").ElementAt(i).
                        Descendants("procedure").ElementAt(j).
                        Descendants("description").ElementAt(0);

                    if (id != null && description != null)
                        procedures.Add(id, description);
                }
            }
        }
    }
}
