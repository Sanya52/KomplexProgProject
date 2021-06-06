using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
using System.Threading;


namespace Szamlazoprog
{

    class Program
    {
        Dictionary<string, string> users;
        bool ja = false;
        public void beleptetes()
        {
            Console.WriteLine("Belépéshez kérem a felhasználóneved: ");
            string user = Console.ReadLine();
            Console.WriteLine("Kérem a jelszót: ");
            string pass = Console.ReadLine();

            foreach (var item in users)
            {
                if (pass == item.Value && user == item.Key)
                {
                    ja = true;
                    break;
                }
            }
            if (ja == true)
            {
                Console.Clear();
                Console.WriteLine("Belépés engedélyezve!\nÜdvözöllek a munka világában {0}!", user);
                parancsok();
            }
            else
            {
                Console.WriteLine("Belépés megtagadva! Nincs ilyen felhasználó!");
            }
        }

        async void exit()
        {
            Console.WriteLine("Jogosulatlan felhasználói bejelentkezési kísérlet! A program bezár!");
            await Task.Delay(5000);
            Environment.Exit(0);
        }
        public void parancsok()
        {
            Console.WriteLine("Parancsok:\nx:kilépés a programból!\nb:Aktuális készlet kiírása!\nf: új termék felvétele\np: parancsok megtekintése!\ne: termék eladása");

        }

        class Adatok
        {
            
            public string nev;
            public int db;
            public int ar;
            public Adatok(string s)
            {
                string[] sor = s.Split(' ');
                nev = sor[0];
                db = int.Parse(sor[1]);
                ar = int.Parse(sor[2]);


            }
        }
        List<Adatok> lista = new List<Adatok>();
        public void keszletkiiras()
        {

            foreach (var item in lista)
            {
                Console.WriteLine(item.nev + " " + item.ar + " " + item.db);
            }
            Console.WriteLine();
            parancsok();

        }
        
        public void ujtermek()
        {
            
            Console.WriteLine("Add meg az új termék nevét: ");
            string termeknev = Console.ReadLine();
            Console.WriteLine("Add meg az új termék darabszámát: ");
            int db = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Add meg az új termék kívánt eladási árát: ");
            int ar = Convert.ToInt32(Console.ReadLine());
            lista.Add(new Adatok(termeknev+" "+db + " " + ar));
            StreamWriter sw = new StreamWriter("adatok2.txt");
            
        }
        public void eladas()
        {
            Console.WriteLine("Add meg az eladni kívánt termék nevét: ");
            string neelad = Console.ReadLine();
            Console.WriteLine("Most kérem az eladni kívánt darabszámot: ");
            int dbelad = int.Parse(Console.ReadLine());
            bool van = false;
            int arra = 0;
            foreach (var item in lista.Where(x=>x.nev==neelad && x.db>dbelad))
            {
                van = true;
            }
            if (van==true)
            {
                foreach (var item in lista.Where(x=>x.nev==neelad))
                {
                    item.db -= dbelad;
                    arra = item.ar;
                }
                Console.WriteLine("{0} termékből {1} db {2} áron értékesített! Szép munka!",neelad,dbelad,arra*dbelad);


            }
            else
            {
                Console.WriteLine("Nincs ilyen termék vagy nincs elég darabszám készleten, próbáld újra!");
            }


        }

        

        static void Main(string[] args)
        {
            Program pr = new Program();
            StreamReader sr = new StreamReader("adatok.txt");

            sr.ReadLine();
            while (!sr.EndOfStream)
            {

                pr.lista.Add(new Adatok(sr.ReadLine()));
            }
            Console.WriteLine("Ez itt az ÁFAMENTES számlázóprogram!");

            pr.users = new Dictionary<string, string>();
            pr.users.Add("Zoli","user");
            pr.users.Add("Admin","jelszo");
            pr.users.Add("Sanyi","anyaszeretlek");
            pr.users.Add("Józsi","apucifia");
            pr.beleptetes();
            
            if (pr.ja==true)
            {
                while (true)
                {
                    char kom =Console.ReadKey(true).KeyChar;
                    Console.Clear();
                    
                    switch (kom)
                    {
                        case 'x': Environment.Exit(0); break;
                        case 'b': pr.keszletkiiras(); break;
                        case 'f': pr.ujtermek();pr.parancsok(); break;
                        case 'p':pr.parancsok();break;
                        case 'e':pr.eladas();break;
                    }
                }
                
            }
            else
            {
                pr.exit();
            }
            
            
            
            Console.ReadKey();
        }
    }
}
