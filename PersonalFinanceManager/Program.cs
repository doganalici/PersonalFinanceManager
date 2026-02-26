using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" **** KİŞİSAL FİNANS YÖNETİCİ UYGULAMASI ****\n");

            bool state = true;
            while (state)
            {
                Console.WriteLine("Kullanıcı Menüsü");
                Console.WriteLine("1 - Kullanıcı Ekle");
                Console.WriteLine("2 - Kullanıcıları Listele");
                Console.WriteLine("3 - Kullanıcı Sil");
                Console.WriteLine("4 - Kullanıcı Güncelle");

                Console.WriteLine("\nKategori Menüsü");
                Console.WriteLine("5 - Kategori Ekle");
                Console.WriteLine("6 - Kategorileri Listele");
                Console.WriteLine("7 - Kategori Sil");
                Console.WriteLine("8 - Kategori Güncelle");

                Console.WriteLine("\nGelir Menüsü");
                Console.WriteLine("9 - Gelir Ekle");
                Console.WriteLine("10 - Gelirleri Listele");
                Console.WriteLine("11 - Gelir Sil");
                Console.WriteLine("12 - Gelir Güncelle");

                Console.WriteLine("\nGider Menüsü");
                Console.WriteLine("13 - Gider Ekle");
                Console.WriteLine("14 - Gider Listele");
                Console.WriteLine("15 - Gider Sil");
                Console.WriteLine("16 - Gider Güncelle");

                Console.WriteLine("\nDiğer İşlemler");
                Console.WriteLine("17 - İşlemleri Listele");
                Console.WriteLine("18 - Aylık Rapor");
                Console.WriteLine("19 - En Çok Harcanan Kategori");

                Console.WriteLine("\n20 - Çıkış");

                Console.Write("\nSeçiminiz : ");
                int option;
                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.Write("\nListe dışında bir tuşlama yaptınız\n" +
                        "Lütfen tekrar deneyin : ");
                }

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI EKLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI LİSTESİ ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI GÜNCELLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI SİL ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ EKLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ LİSTESİ ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ SİL ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ GÜNCELLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 9:
                        Console.Clear();
                        Console.WriteLine("--- GELİR EKLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 10:
                        Console.Clear();
                        Console.WriteLine("--- GELİR LİSTELE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 11:
                        Console.Clear();
                        Console.WriteLine("--- GELİR SİL ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 12:
                        Console.Clear();
                        Console.WriteLine("--- GELİR GÜNCELLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 13:
                        Console.Clear();
                        Console.WriteLine("--- GİDER EKLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 14:
                        Console.Clear();
                        Console.WriteLine("--- GİDER LİSTELE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 15:
                        Console.Clear();
                        Console.WriteLine("--- GİDER SİL ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 16:
                        Console.Clear();
                        Console.WriteLine("--- GİDER GÜNCELLE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 17:
                        Console.Clear();
                        Console.WriteLine("--- İŞLEMLERİ LİSTELE ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 18:
                        Console.Clear();
                        Console.WriteLine("--- AYLIK RAPOR ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 19:
                        Console.Clear();
                        Console.WriteLine("--- EN ÇOK HARCAMA YAPILAN KATEGORİ ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 20:
                        Console.Clear();
                        Console.WriteLine("Çıkış yapılıyor...\n" +
                            "Devam etmek için bir tuşa basınız.");
                        state = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Liste dışında bir tuşlama yaptınız\n" +
                        "Lütfen tekrar deneyin!");
                        Clear();
                        break;
                }
            }

        }
        public static void Clear()
        {
            Console.WriteLine("Devam etmek için bir tuşa basınız.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
