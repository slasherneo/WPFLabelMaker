using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WPFLabelMaker
{
    class FreeTypeDataLoader
    {
        public FreeTypeDataLoader()
        {
            try
            {
                using (StreamReader sr = new StreamReader("c://Label//types.txt"))
                {
                    string alltypes = sr.ReadToEnd();
                    if (alltypes != null && alltypes.Length > 0)
                    {
                        AllTypes = alltypes.Split(';');
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public string[] AllTypes;
    }
}
