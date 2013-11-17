using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace top100
{
    class www_txt
    {


        string nazwaPlikuKlasa;
        string adresStronyKlasa;


        public www_txt(string nazwaPliku, string adresStrony)
        {
            nazwaPlikuKlasa = nazwaPliku;
            adresStronyKlasa = adresStrony;
            
        }


        public string pobierz_stronę_www()
        {
            WebClient webClient = new WebClient();
            byte[] reqHTML;
            webClient.Headers.Add("DOM", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            reqHTML = webClient.DownloadData(adresStronyKlasa);
            UTF8Encoding objUTF8 = new UTF8Encoding();
            StreamWriter plik = new StreamWriter(nazwaPlikuKlasa);
            plik.Write(System.Text.Encoding.GetEncoding("UTF-8").GetString(reqHTML));
            plik.Close();
           
            return nazwaPlikuKlasa;
            
        }

            
          


    }
}
