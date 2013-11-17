using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace top100
{
    class obsluga_xml
    {
        string[] plikiHistoriaKlasa = new string[200];

        public obsluga_xml()
        {
        }


        public string[] stworz_liste_plikow_xml()
        {
            //tworzenie listy plików z folderu do porównywarki.
           
                string[] files = Directory.GetFiles(".", "top100_*.xml");
                int i = 0;
                string data = "", czas = "";
            
                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        XmlTextReader xtr = new XmlTextReader(file);
                        xtr.Read();
                        xtr.Read();
                        xtr.Read();
                        xtr.MoveToAttribute("data");
                        data = xtr.Value;
                        xtr.MoveToAttribute("godzina");
                        czas = xtr.Value;
                        plikiHistoriaKlasa[i] = file.ToString().Substring(2);
                    
                        i++;
                       }

                }
                return plikiHistoriaKlasa;
            }
           
          

        
    }
}
