using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace KpoLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите номер задачи (от 1 до 7)");
                switch (Console.ReadLine())
                {
                    case "1":
                        LinqXml1();
                        Console.ReadLine();
                        break;
                    case "2":
                        LinqXml2();
                        Console.ReadLine();
                        break;
                    case "3":
                        LinqXml3();
                        Console.ReadLine();
                        break;
                    case "4":
                        LinqXml4();
                        Console.ReadLine();
                        break;
                    case "5":
                        LinqXml5();
                        Console.ReadLine();
                        break;
                    case "6":
                        LinqXml6();
                        Console.ReadLine();
                        break;
                    case "7":
                        LinqXml7();
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Такой задачи нет!");
                        Console.ReadLine();
                        break;
                }
            }
        }
        static void LinqXml1()
        {
            const string xmlDoc = "people.xml";
            XDocument xdoc = new XDocument();
            XElement XMLFile1 = new XElement("people");
            xdoc.Add(XMLFile1);


            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\TextFile1.txt";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    XElement str = new XElement("person");
                    XAttribute personNameAttr = new XAttribute("name", line);
                    str.Add(personNameAttr);
                    XMLFile1.Add(str);
                }
            }
            xdoc.Save(xmlDoc);
            Console.WriteLine(xdoc);
        }
        static void LinqXml2()
        {
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile2.xml");
            var result = xdoc.Root.Elements().GroupBy(p => p.Attribute("name").Value).Select(c => new { Name = c.Key, Count = c.Count() });
            foreach (var name in result)
            {
                Console.WriteLine($"Имя: {name.Name}, Количество: {name.Count}");
            }
        }
        static void LinqXml3()
        {
            const string s = @"Krasnikov Vladimir";
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile3.xml");
            Console.WriteLine(xdoc);
            xdoc.Root.Elements().Where(x => x.Attribute("name").Value == s).Remove();
            Console.WriteLine();
            Console.WriteLine(xdoc);
        }
        static void LinqXml4()
        {
            const string s1 = "user";
            const string s2 = "NewClone";
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile4.xml");
            Console.WriteLine(xdoc);
            foreach (var item in xdoc.Root.Elements(s1))
            {
                item.AddAfterSelf(new XElement(s2, item.Attributes(), item.Nodes()));
            }
            Console.WriteLine();
            Console.WriteLine(xdoc);
        }
        static void LinqXml5()
        {
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile5.xml");
            Console.WriteLine(xdoc);

            foreach (var elem in xdoc.Root.Elements())
            {
                if (elem.HasElements)
                    elem.Add(new XAttribute("sum", elem.Elements().Where(x => x.Value != "").Sum(s => Math.Round(Convert.ToDouble(s.Value), 2))));
            }

            Console.WriteLine();
            Console.WriteLine(xdoc);
        }
        static void LinqXml6()
        {
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile6.xml");
            Console.WriteLine(xdoc);

            foreach (var elem1 in xdoc.Root.Elements())
            {
                string s = elem1.GetDefaultNamespace().NamespaceName;
                elem1.LastNode.AddAfterSelf(new XElement("namespace", elem1.GetDefaultNamespace().NamespaceName));
            }

            Console.WriteLine();
            Console.WriteLine(xdoc);
        }
        static void LinqXml7()
        {
            XDocument xdoc = XDocument.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\XMLFile7.xml");
            Console.WriteLine(xdoc);

            List<XElement> listElements = xdoc.Root.Elements().ToList();

            for (int i = 0; i < listElements.Count; i++)
            {
                var elem1 = listElements[i];
                var elements2arr = new XElement[elem1.Elements().Count()];
                elem1.Elements().ToList().CopyTo(elements2arr);

                var year = DateTime.Parse(elements2arr[1].Value).Year;
                var month = DateTime.Parse(elements2arr[1].Value).Month;

                var listAttributes = new List<XAttribute>();
                listAttributes.Add(new XAttribute("id", elements2arr[0].Value));
                listAttributes.Add(new XAttribute("year", year));
                listAttributes.Add(new XAttribute("month", month));

                elem1.AddBeforeSelf(new XElement("time", elements2arr[2].Value, listAttributes));
                elem1.Remove();
            }

            Console.WriteLine();
            Console.WriteLine(xdoc);
        }
    }
}
