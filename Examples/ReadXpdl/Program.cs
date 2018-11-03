using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            xmlns="http://www.wfmc.org/2008/XPDL2.1" 
            xmlns:xpdl="http://www.wfmc.org/2008/XPDL2.1"> from xpdl file*/

            XNamespace content = XNamespace.Get("http://www.wfmc.org/2008/XPDL2.1");   // add text   xpdl:   to nodes

            XDocument doc = XDocument.Load(@"xpdl.xml");  // carefully write path, default read from bin/Debug
            IEnumerable<XElement> partElements = doc.Root.Elements(content + "WorkflowProcesses").Elements(content + "WorkflowProcess")
                .Elements(content + "Activities").Elements(content + "Activity");  // walk on xml-tree

            foreach (XElement partElement in partElements)
            {
                // read attribute value
                try
                {
                    string keyName = partElement.Attribute("Id").Value;
                    Console.WriteLine("Activity ID: {0}", keyName);

                    string keyName2 = partElement.Attribute("Name").Value;
                    Console.WriteLine("Activity Name: {0}", keyName2);
                }
                catch
                {
                    Console.WriteLine("attribute empty");
                    // catch NULL error
                }

                // iterate through childnodes
                foreach (XElement partChildElement in partElement.Elements())
                {
                    // check the name
                    if (partChildElement.Name == "XPDLVersion")
                    {
                        int value = (int)partChildElement; // casting from element to its [int] content
                        Console.WriteLine("XPDLVersion: {0}", value);

                        // do stuff for <SpaceSep> element
                    }
                    if (partChildElement.Name == content + "Created")
                    {
                        string text = (string)partChildElement; // casting from element to its [string] content
                        Console.WriteLine("Text: {0}", text);

                        // do stuff for <Created>
                    }
                    // and so on for all possible node name
                }
            }
            Console.ReadKey();
        }
    }
}

