﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace GUI_Prototype
{
   public class XPDLManager
    {
        static XNamespace content = XNamespace.Get("http://www.wfmc.org/2008/XPDL2.1");
        static XDocument doc = XDocument.Load(Utils.xpdlFileName);
        static IEnumerable<XElement> activities = doc.Root.Elements(content + "WorkflowProcesses").Elements(content + "WorkflowProcess").Elements(content + "Activities").Elements(content + "Activity");
        static IEnumerable<XElement> transitions = doc.Root.Elements(content + "WorkflowProcesses").Elements(content + "WorkflowProcess").Elements(content + "Transitions").Elements(content + "Transition");
        static string currentId {get; set; }
        static string path = Utils.xpdlFileName;

        /* pobierz ID pierwszej aktywnosci zawartej w xpdlu*/
        public static string GetFirstActivity()
        {
            XElement first = activities.First<XElement>();
            return first.Attribute("Id").Value;
        }

        /* wypisz ID kolejnej aktywnosci (lub kilku rownorzednych) wzgledem aktualnie zapisanej aktywnosci */
        public static string[] GetNext()
        {
            string[] elements = new string[4];
            int counter = 0;
            
            foreach(XElement t in transitions)
            {
                if(t.Attribute("From").Value == currentId)
                {
                    elements[counter++] = t.Attribute("To").Value;
                }
            }

            return elements;
        }

        /* znajduje aktywność(aktywności) występującą po zadanej aktywności; zwraca tablicę stringów */
        public static string[] GetNext(string activityId)
        {
            string[] elements = new string[4];
            int counter = 0;
            
            foreach(XElement t in transitions)
            {
                if(t.Attribute("From").Value == activityId)
                {
                    elements[counter++] = t.Attribute("To").Value;
                }
            }

            return elements;
        }

        /* pobierz ID poprzedniej aktywnosci względem aktualnie zapisanej aktywności */
        public static string GetPreviousActivity()
        {
            foreach(XElement t in transitions)
            {
                if(t.Attribute("To").Value == currentId)
                {
                    return t.Attribute("From").Value;
                }
            }
            return null;
        }

        /* pobierz ID poprzedniej aktywności względem zadanej aktywności */
        public static string GetPreviousActivity(string activityId)
        {
            foreach(XElement t in transitions)
            {
                if(t.Attribute("To").Value == activityId)
                {
                    return t.Attribute("From").Value;
                }
            }
            return null;
        }

        /* zapamietaj ID biezacej aktywnosci oraz ustaw date zakonczenia starej aktywnosci i rozpoczecia nowej aktywnosci */
        public static void SetCurrentActivity(string activityId)
        {
            if(currentId != null)
                SetFinishDate(currentId);
            currentId = activityId;
            SetStartDate(currentId);
        }

        /* pobierz ID biezacej aktywnosci */
        public static string GetCurrentActivity()
        {
            return currentId;
        }

        /* pobierz nazwe aktywnosci wg zadanego ID tej aktywnosci */
        public static string GetActivityName(string activityId)
        {
            foreach(XElement a in activities)
            {
                if(a.Attribute("Id").Value == activityId){
                    return a.Attribute("Name").Value;
                }
            }

            return null;
        }

        /*dodaj atrybut z data rozpoczecia wykonywania aktywnosci o podanym ID*/
        public static void SetStartDate(string activityId)
        {
            DateTime date = DateTime.Now;
            string text = "\t\t\t\t\t\t<xpdl:ExtendedAttribute Name=\"InputDate\" Value=\"" + date + "\"/>";
            List<string> contentXPDL = new List<string>();
            bool isAdded = false;

            using(StreamReader streamReader = File.OpenText(path))
            {
                do
                {
                    string line = streamReader.ReadLine();
                    contentXPDL.Add(line);

                    if(line.Contains("<xpdl:Activity Id=\"" + activityId))
                    {
                        do
                        {
                            line = streamReader.ReadLine();
                            contentXPDL.Add(line);

                        }while(!line.Contains("<xpdl:ExtendedAttributes>"));
                        
                        if (line.Contains("<xpdl:ExtendedAttributes>") && !isAdded)
                        {
                                contentXPDL.Add(text);
                                isAdded = true;
                        }
                    }

                }while(!streamReader.EndOfStream);
            }

            using (StreamWriter streamWriter = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                foreach(string line in contentXPDL)
                    streamWriter.WriteLine(line);
            }

            doc = XDocument.Load(path); 
        }

        /*dodaj atrybut z data zakonczenia wykonywania aktywnosci o zadanym ID*/
        public static void SetFinishDate(string activityId)
        {
            DateTime date = DateTime.Now;
            string text = "\t\t\t\t\t\t<xpdl:ExtendedAttribute Name=\"OutputDate\" Value=\"" + date + "\"/>";  
            List<string> contentXPDL = new List<string>();
            bool isAdded = false;

            using(StreamReader streamReader = File.OpenText(path))
            {
                do
                {
                    string line = streamReader.ReadLine();
                    contentXPDL.Add(line);

                    if(line.Contains("<xpdl:Activity Id=\"" + activityId))
                    {
                        do
                        {
                            line = streamReader.ReadLine();
                            contentXPDL.Add(line);

                        }while(!line.Contains("<xpdl:ExtendedAttributes>"));
                        
                        if (line.Contains("<xpdl:ExtendedAttributes>") && !isAdded)
                        {
                                contentXPDL.Add(text);
                                isAdded = true;
                        }
                    }

                }while(!streamReader.EndOfStream);
            }

            using (StreamWriter streamWriter = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                foreach(string line in contentXPDL)
                    streamWriter.WriteLine(line);
            }

            doc = XDocument.Load(path);             
        }
    }
}
