using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesai
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Global Değişkenler
            List<Personel> personeller = new List<Personel>();
            string devam;

            //Program
            Console.WriteLine("Mesai Ücreti Hesaplama Uygulamasına Hoşgeldin!");
            Console.ReadKey();
            
            do
            {
                //Çalışanın ismi
                string isim;
                Console.WriteLine("Çalışanın adı :");
                isim = Console.ReadLine();

                //Çalışanın soyadı
                string soyad;
                Console.WriteLine("Çalışanın soyadı :");
                soyad = Console.ReadLine();

                //Çalışanın giriş saati
                string giris;
                Console.WriteLine("Çalışanın giriş tarih ve saati (Gün/Ay/Yıl Saat:Dakika) :");
                giris = Console.ReadLine();

                //Çalışanın giriş saatinin Almanya saatine dönüştürülmesi
                DateTime girisDT = DateTime.Parse(giris);
                TimeZoneInfo almanya = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
                girisDT = TimeZoneInfo.ConvertTimeFromUtc(girisDT, almanya);

                //Çalışanın çıkış saati
                string cikis;
                Console.WriteLine("Çalışanın çıkış tarih ve saati (Gün/Ay/Yıl Saat:Dakika) :");
                cikis = Console.ReadLine();

                //Çalışanın çıkış saatinin Almanya saatine dönüştürülmesi
                DateTime cikisDT = DateTime.Parse(cikis);
                cikisDT = TimeZoneInfo.ConvertTimeFromUtc(cikisDT, almanya);

                //Çalışanın mola yaptığı toplam saat
                double mola;
                Console.WriteLine("Toplam mola saati :");
                mola = Convert.ToDouble(Console.ReadLine());

                //Çalışan nesnesinin oluşturulması ve LinkList'e eklenmesi
                Personel newPersonel = new Personel(isim, soyad, girisDT, cikisDT, mola);
                personeller.Add(newPersonel);
                
                //Daha fazla çalışan eklenip eklenmeyeceğinin kontrolü
                Console.WriteLine("Daha fazla personel eklemek istiyor musunuz? (Evet - Hayır)");
                devam = Console.ReadLine();

            } while (devam.Equals("Evet"));

            foreach (var personel in personeller)
            {
                //Çalışanın bilgileri
                Console.WriteLine($"{personel.Ad} {personel.Soyad} için Mesai Bilgileri:");
                Console.ReadKey();
                Console.WriteLine($"Giriş Saati: {personel.GirisSaati}");
                Console.ReadKey();
                Console.WriteLine($"Çıkış Saati: {personel.CikisSaati}");
                Console.ReadKey();
                Console.WriteLine($"Mola Süresi: {personel.MolaSuresi} saat");
                Console.ReadKey();

                TimeSpan calismaSuresi = personel.CikisSaati - personel.GirisSaati;
                calismaSuresi -= TimeSpan.FromHours(personel.MolaSuresi);

                if (calismaSuresi.TotalHours > 8)
                {
                    double mesaiUcreti = (calismaSuresi.TotalHours - 8) * 50;
                    Console.WriteLine($"Toplam Mesai Ücreti: {mesaiUcreti} TL");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Mesai yapılmamış.");
                    Console.ReadKey();
                }

                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }

    class Personel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime GirisSaati { get; set; }
        public DateTime CikisSaati { get; set; }
        public double MolaSuresi { get; set; }

        public Personel(string ad, string soyad, DateTime girisSaati, DateTime cikisSaati, double molaSuresi)
        {
            Ad = ad;
            Soyad = soyad;
            GirisSaati = girisSaati;
            CikisSaati = cikisSaati;
            MolaSuresi = molaSuresi;
        }
    }
}
