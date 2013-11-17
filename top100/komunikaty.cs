using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace top100
{
    class komunikaty
    {
        string[,] tablica = new string[100, 2];
        string[] gora = new string[20];
        string[] dol = new string[20];
        string[] nowosc = new string[20];
        string[] brak = new string[20];


         public komunikaty(string[,] lista)
        {
            tablica = lista;
        }

        public string raport()
        {
            string tekst = "" ;
            Slowniki();
            bool brakZmian = false;

            for (int i = 0; i < tablica.Length / 4; i++)
            {
                if (tablica[i, 1] != null)
                {
                    brakZmian = true;
                    if (tablica[i, 1] == "Nowość")
                    {
                        tekst += DajWyrazenie(2) + tablica[i, 0];

                    }
                    else
                    {
                        if (Int32.Parse(tablica[i, 1]) > Int32.Parse(tablica[i, 3]))
                        {

                            tekst += " " + tablica[i, 0] + DajWyrazenie(0) + (i + 1);
                        }
                        if (Int32.Parse(tablica[i, 1]) < Int32.Parse(tablica[i, 3]))
                        {
                            tekst += " " + tablica[i, 0] + DajWyrazenie(1) + (i + 1);
                        }
                    }


                };
            


            }
            if (brakZmian == false)
            {
                tekst = DajWyrazenie(3);
            }



                return tekst;
            
        }

        private void Slowniki()
        {
            gora[0] = " idze w górę na ";
            gora[1] = " poszło w górę na";
            gora[2] = " coraz wyżej na ";
         
            dol[0] = " poszło w dół na ";
            dol[1] = " niestety ale poszło w dół na ";
            dol[1] = " kurcze poszło w dół na ";

            nowosc[0] = " WOW Prawdziwa nowosc ";
            nowosc[1] = " WOW Prawdziwa nowosc ";
            nowosc[2] = " WOW Prawdziwa nowosc ";

            brak[0] = " Brak zmian w dziesiejszym notowaniu ";
            brak[1] = " Niestety nic się nie zmieniło od ostatniego razu ";
            brak[2] = " Dziś nic nowego ani żadnych zmian ";
        }

        private string DajWyrazenie(int paramentr)
        {
            Random r = new Random();
            int wgora = r.Next(0, 3);
            int wdol = r.Next(0, 3);
            int wnowosc = r.Next(0, 3);
            int wbrak = r.Next(0, 3);
            string wyrazenie = "";
            

            switch (paramentr)
            {
                case 0:
                    //w gore
                    wyrazenie = gora[wgora];
                    break;
                case 1:
                    //w dol
                    wyrazenie = dol[wdol];
                    break;
                case 2:
                    //nowosc
                    wyrazenie = nowosc[wnowosc];
                    break;
                case 3:
                    //brak
                    wyrazenie = brak[wbrak];
                    break;
            }

            return wyrazenie;
        }





    }
}
