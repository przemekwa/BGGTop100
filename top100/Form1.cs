using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace top100
{
  

    public partial class Form1 : Form
    {
        
        string[] pliki_historia = new string[200];
        string aktualne_notowanie_xml;
        string[,] lista_lista_przebojow = new string[100, 2];
        

        public Form1()
        {
            string[,] najnowsze_notowanie = new string[100,1];
            
            InitializeComponent();

            obsluga_xml bgg_xml = new obsluga_xml();
          
           /* 
            pliki_historia= bgg_xml.stworz_liste_plikow_xml();
            foreach (string plik in pliki_historia)
            {
                comboBox1.Items.Add(plik);
            }
            */
                
           stworz_liste_plikow_xml();

           // wczytanie strony bgg do plku teksowego z którego czerpiemy dane do zestawienia
            www_txt bgg = new www_txt("top100.txt", "http://boardgamegeek.com/browse/boardgame/page/1");

           
            string poprzednie_notowanie_xml = podaj_xml();

            najnowsze_notowanie = laduj_nazwy_gier(bgg.pobierz_stronę_www());
            
            stworz_xml(najnowsze_notowanie);
            
            aktualne_notowanie_xml = podaj_xml();
            
            lista_lista_przebojow = porownaj_gry( aktualne_notowanie_xml,poprzednie_notowanie_xml);
            
            pokaz_notowania(lista_lista_przebojow);

            komunikaty raport = new komunikaty(lista_lista_przebojow);
            textBox1.Text=raport.raport();




            
            

        }

        private void stworz_liste_plikow_xml()
        {
            try
            {

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
                        comboBox1.Items.Add(data + " " + czas);
                        pliki_historia[i] = file.ToString().Substring(2);
                        i++;
                        
                    }

                }
            }
            catch 
            {
                MessageBox.Show("Błąd w składni w jakimś pliku XML w katalogu. Najprościej usuń wszystkie pliki z katalogu","Błąd odczytu plików XML",MessageBoxButtons.OK,MessageBoxIcon.Asterisk );

            }

        }


        private string[,] porownaj_gry(string aktualne_notowanie_xml, string poprzednie_notowanie_xml)
        {
            int wzrost = 0, spadek = 0, bez_zmian=0, nowosc=0;
            string[,] aktualne_notowanie_gier = new string[100, 4];
            string[,] poprzednie_notowanie_gier = new string[100, 3];

            aktualne_notowanie_gier = konwersja_xml_do_array(aktualne_notowanie_xml);
            poprzednie_notowanie_gier = konwersja_xml_do_array(poprzednie_notowanie_xml);

            for(int i=0;i<aktualne_notowanie_gier.Length/4;i++)
            {
                aktualne_notowanie_gier[i, 3] = (i+1).ToString();
                // sprawdzamy różnicę w pozycjach
                if (aktualne_notowanie_gier[i, 0] != poprzednie_notowanie_gier[i, 0])
                {
                    //szukamy poprzedniej pozycji gry
                    bool czy_gra_jest_w_notowaniu = false;
                    for (int y = 0; y < aktualne_notowanie_gier.Length / 4; y++)
                    {
                        if (aktualne_notowanie_gier[i, 0] == poprzednie_notowanie_gier[y, 0])
                        {
                            aktualne_notowanie_gier[i, 1] = (y + 1).ToString();
                            if ((i + 1) > (y + 1))
                            {
                                for (int a = 0; a < ((i + 1) - (y + 1)); a++)
                                {
                                    spadek++;
                                }
                            }
                            else if ((i + 1) < (y + 1))
                            {
                                for (int a = 0; a < ((y + 1) - (i + 1)); a++)
                                {
                                    wzrost++;
                                }
                            }

                            czy_gra_jest_w_notowaniu = true;
                        }
                    }

                    //przypadek gdy w akutlanym notowaniu mamy nowość do poprzedniego
                    if (!czy_gra_jest_w_notowaniu)
                    {
                        aktualne_notowanie_gier[i, 1] = "Nowość";
                        nowosc++;

                    }
                }
                else
                {
                    bez_zmian++;
                }



            }


            label1.Text = wzrost.ToString();
            label2.Text = (bez_zmian).ToString();
            label3.Text = spadek.ToString();
            label4.Text = nowosc.ToString();
            return aktualne_notowanie_gier;





        }

        private string podaj_xml()
        {


            string najnowsze_notowanie_w_katalogu;
            string[] files = Directory.GetFiles(".", "top100_*.xml");

            //czy są pliki.
            if (files.Length == 0)
            {
                najnowsze_notowanie_w_katalogu = "BRAK";
            }
            else
            {
                najnowsze_notowanie_w_katalogu = files[files.Length - 1];
                najnowsze_notowanie_w_katalogu = najnowsze_notowanie_w_katalogu.Substring(2, najnowsze_notowanie_w_katalogu.Length - 2);
            }



            

            return najnowsze_notowanie_w_katalogu;
  }

        private string[,] konwersja_xml_do_array(string p)
        {
            
                
                string[,] lista_gier = new string[100, 4];


            if (p != "BRAK")
            {
                XmlDocument oXmlDocument = new XmlDocument();
                try
                {
                    oXmlDocument.Load(p);
                }
                catch
                {
                    MessageBox.Show("Podczas próby odczytu pliku  " + p + " wystąpił bład. Możesz go skasować lub spróbować naprawić go samodzielnie.  ","Błąd odczytu pliku XML" , MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


                XmlNodeList oPersonNodesList = oXmlDocument.GetElementsByTagName("gra");

              
                foreach (XmlNode oPerson in oPersonNodesList)
                {
                    lista_gier[Int32.Parse(oPerson.Attributes["pozycja"].InnerText)-1, 0] = oPerson.InnerText;

                    if (oPerson.Attributes.Count == 2)
                    {
                        lista_gier[Int32.Parse(oPerson.Attributes["pozycja"].InnerText) - 1, 2] = oPerson.Attributes["rok_produkcji"].InnerText;
                    }
                }
            }

           

            return lista_gier;

        }
           
        string stworz_xml(string[,] tablica)
        {
            string[] files = Directory.GetFiles(".", "top100_*.xml");

            string nazwa_pliku="ss";
            
            if (files.Length == 0)
            {
                nazwa_pliku = "top100_001.xml";
            }
            else
            {
                string nazwa_najnowszego_pliku = files[files.Length - 1];
                nazwa_najnowszego_pliku = nazwa_najnowszego_pliku.Substring(9, nazwa_najnowszego_pliku.Length - 13);

                int pierwsza, druga, trzecia;

                trzecia = Int32.Parse(nazwa_najnowszego_pliku.Substring(0,1));
                druga = Int32.Parse(nazwa_najnowszego_pliku.Substring(1,1));
                pierwsza = Int32.Parse(nazwa_najnowszego_pliku.Substring(2, 1));

                if (pierwsza == 9)
                {
                   pierwsza = 0;
                   druga++;
                   if (druga == 9)
                   {
                       druga = 0;
                       trzecia++;
                   }
                }
                else
                {
                    pierwsza++;
                }

                nazwa_pliku = "top100_" + trzecia + druga + pierwsza + ".xml";


            }

            

            
            XmlTextWriter nowy_xml = new XmlTextWriter(nazwa_pliku, null);
            
            nowy_xml.WriteStartDocument();
            nowy_xml.WriteComment("sss");
            nowy_xml.WriteStartElement("top100");
            nowy_xml.WriteAttributeString("data", System.DateTime.Now.ToString().Substring(0,10));
            nowy_xml.WriteAttributeString("godzina", System.DateTime.Now.ToString().Substring(11));
            nowy_xml.WriteStartElement("gry");

            for (int i = 0; i < tablica.Length-100; i++)
            {
                nowy_xml.WriteStartElement("gra");
                nowy_xml.WriteAttributeString("pozycja", (i+1).ToString());

                try
                {
                    nowy_xml.WriteAttributeString("rok_produkcji", tablica[i, 1].ToString());
                }
                catch (NullReferenceException e)
                {
                    MessageBox.Show("BGG odmawia posłuszeństwa, spróbuj ponowanie za chwile. Błąd " + e, "Błąd BGG", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                nowy_xml.WriteString(tablica[i,0]);
                nowy_xml.WriteEndElement();
            }
            
            nowy_xml.WriteEndDocument();
            nowy_xml.Close();

            return nazwa_pliku;
        }

        private string[,] laduj_nazwy_gier(string nazwa_pliku)
        {
            string[,] tablica = new string[100,2];

            StreamReader plik = new StreamReader(nazwa_pliku);

            string linia = plik.ReadLine();
            int pozycja = 1;
           
            while ( linia != null)
            {



                if (Regex.IsMatch(linia, "a name=\""+pozycja+"\""))
                { 
                    for (int i = 0; i < 6; i++)
                    {
                        linia = plik.ReadLine();
                    }
                    
                    string nazwa = (linia.Substring(29, (linia.IndexOf("><img") - 31)));
                    string nazwa_gry = nazwa.Substring(nazwa.IndexOf("/") + 1, (nazwa.Length - (nazwa.IndexOf("/")) - 1));

                    tablica[pozycja-1,0] = nazwa_gry;
                      
                }
                if (Regex.IsMatch(linia, "smallerfont dull"))
                {
                    string rok_produkcji = linia.Substring(39, linia.Length - 47);
                
                    tablica[pozycja - 1, 1] = rok_produkcji;
                    pozycja++; 
                }

                linia = plik.ReadLine();
              
            }
            return tablica;

        }

       
        
        private void pokaz_notowania(string[,] tablica_gier)
        {
            for (int i = 0, y = 1; i < 100 ; i++,y++)
            {
              dataGridView1.Rows.Add(tablica_gier[i, 3],tablica_gier[i, 0],tablica_gier[i, 1],tablica_gier[i, 2]);
             }

            
            



                }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string[,] lista_lista_przebojow = new string[100, 1];
            lista_lista_przebojow = porownaj_gry(aktualne_notowanie_xml, pliki_historia[comboBox1.SelectedIndex]);
            pokaz_notowania(lista_lista_przebojow);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string[,] posortowane = posortuj_wg_parametru(lista_lista_przebojow,2);
            pokaz_notowania(posortowane);

        }

        private string[,] posortuj_wg_parametru(string[,] notowanie_gier,int p)
        {
          string[,] posortowane_notowanie_gier = new string[100, 4];
          List<string> elementy_sortujace = new List<string>();
          elementy_sortujace =  wybierz_elementy_sortujace(notowanie_gier,p);
           // sortowanie gier na podstawie elemetów_sortujących
            elementy_sortujace.Sort();
            int z=0;
            foreach (string element in elementy_sortujace)
            {
                for (int x = 0; x < notowanie_gier.Length/4; x++)
                {
                    if (element == notowanie_gier[x, p])
                    {
                        posortowane_notowanie_gier[z, 0] = notowanie_gier[x, 0];
                        posortowane_notowanie_gier[z, 1] = notowanie_gier[x, 1];
                        posortowane_notowanie_gier[z, 2] = notowanie_gier[x, 2];
                        posortowane_notowanie_gier[z++, 3] = (x+1).ToString();
                    }

                }
            }
            return posortowane_notowanie_gier;
        }

        private string[,] best3InYear(string[,] notowanie_gier, int p)
        {
            string[,] posortowane_notowanie_gier = new string[100, 4];
            List<string> elementy_sortujace = new List<string>();
            elementy_sortujace = wybierz_elementy_sortujace(notowanie_gier, p);
            // sortowanie gier na podstawie elemetów_sortujących
            elementy_sortujace.Sort();
         
            
            foreach (string element in elementy_sortujace)
            {
                int dosc = 3; 
                for (int x = 0; x < notowanie_gier.Length / 4; x++)
                {
                    if (dosc >= 1)
                    {
                        if (element == notowanie_gier[x, p])
                        {
                            posortowane_notowanie_gier[dosc, 0] = notowanie_gier[x, 0];
                            posortowane_notowanie_gier[dosc, 1] = notowanie_gier[x, 1];
                            posortowane_notowanie_gier[dosc, 2] = notowanie_gier[x, 2];
                            posortowane_notowanie_gier[dosc, 3] = (x + 1).ToString();
                            dosc--;
                        }
                    }
                }
            }
            return posortowane_notowanie_gier;
        }

        private List<string> wybierz_elementy_sortujace(string[,] notowanie_gier,int p)
        {
            //string[] elementy_sortujace = new string[100];
            List<string> elementy_sortujace = new List<string>();
            bool czy_wystepuje = true;

            //zebranie elemntów po, kótrych będziemy sortować.
            for (int i = 0; i < notowanie_gier.Length / 4; i++)
            {
                czy_wystepuje = true;
                for (int y = 0; y < elementy_sortujace.Count; y++)
                {
                    if ((notowanie_gier[i, p] == elementy_sortujace.ElementAt(y)))
                    {
                        czy_wystepuje = false;
                    }
                }
                if (czy_wystepuje)
                {
                    elementy_sortujace.Add(notowanie_gier[i, p]);
                }
            }


            return elementy_sortujace;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            


        }

       
       
    }
}

