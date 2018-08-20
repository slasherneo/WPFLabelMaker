using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker
{
    public class PrintSetting
    {
        public PrintSetting()
        {
            try
            {
                using (StreamReader sr = new StreamReader("c://Label//setting.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("PrinterName"))
                        {
                            PrinterName = GetProperty(line);
                        }
                        else if (line.Contains("SCFont1XLoc"))
                        {
                            SCFont1XLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont1XSplit"))
                        {
                            SCFont1XSplit = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont1YLoc"))
                        {
                            SCFont1YLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont1"))
                        {
                            SCFont1 = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont2XLoc"))
                        {
                            SCFont2XLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont2YLoc"))
                        {
                            SCFont2YLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont2"))
                        {
                            SCFont2 = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont3XLoc"))
                        {
                            SCFont3XLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont3YLoc"))
                        {
                            SCFont3YLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("SCFont3"))
                        {
                            SCFont3 = GetIntProperty(line);
                        }
                        else if (line.Contains("Font1XLoc"))
                        {
                            Font1XLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("Font1XSplit"))
                        {
                            Font1XSplit = GetIntProperty(line);
                        }
                        else if (line.Contains("Font1YLoc"))
                        {
                            Font1YLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("Font1"))
                        {
                            Font1 = GetIntProperty(line);
                        }
                        else if (line.Contains("Font2XLoc"))
                        {
                            Font2XLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("Font2YLoc"))
                        {
                            Font2YLoc = GetIntProperty(line);
                        }
                        else if (line.Contains("Font2"))
                        {
                            Font2 = GetIntProperty(line);
                        }                       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private string GetProperty(string input)
        {
            string property = string.Empty;
            string[] properties = input.Split(':');
            if (properties.Length == 2)
            {
                property = properties[1];
            }
            return property;
        }

        private int GetIntProperty(string input)
        {
            int property = 0;
            string[] properties = input.Split(':');
            if (properties.Length == 2)
            {
                property = int.Parse(properties[1]);
            }
            return property;
        }

        public string PrinterName { get; set; }

        public int Font1 { get; set; }

        public int Font1XLoc { get; set; }

        public int Font1XSplit { get; set; }

        public int Font1YLoc { get; set; }

        public int Font2 { get; set; }

        public int Font2XLoc { get; set; }

        public int Font2YLoc { get; set; }

        public int SCFont1 { get; set; }

        public int SCFont1XLoc { get; set; }

        public int SCFont1XSplit { get; set; }

        public int SCFont1YLoc { get; set; }

        public int SCFont2 { get; set; }

        public int SCFont2XLoc { get; set; }

        public int SCFont2YLoc { get; set; }

        public int SCFont3 { get; set; }

        public int SCFont3XLoc { get; set; }

        public int SCFont3YLoc { get; set; }


    }
}
