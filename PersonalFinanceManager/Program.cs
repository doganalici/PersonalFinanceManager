using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceManager.Business;

namespace PersonalFinanceManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            CategoryManager categoryManager = new CategoryManager();
            TransactionManager transactionManager = new TransactionManager();
            UserManager userManager = new UserManager();

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

                Console.WriteLine("\nİşlem Menüsü");
                Console.WriteLine("9 - İşlem Ekle");
                Console.WriteLine("10 - İşlem Listele");
                Console.WriteLine("11 - İşlem Sil");
                Console.WriteLine("12 - İşlem Güncelle");

                Console.WriteLine("\nDiğer İşlemler");
                Console.WriteLine("13 - Aylık Rapor");
                Console.WriteLine("14 - En Çok Harcanan Kategori");

                Console.WriteLine("\n15 - Çıkış");

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
                        userManager.AddUser();
                        Clear();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI LİSTESİ ---\n");
                        userManager.ListUser();
                        Clear();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI SİL ---\n");
                        userManager.DeleteUser();
                        Clear();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("--- KULLANICI GÜNCELLE ---\n");
                        userManager.UpdateUser();
                        Clear();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ EKLE ---\n");
                        categoryManager.AddCategory();
                        Clear();
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ LİSTESİ ---\n");
                        categoryManager.ListCategory();
                        Clear();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ SİL ---\n");
                        categoryManager.DeleteCategory();
                        Clear();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine("--- KATEGORİ GÜNCELLE ---\n");
                        categoryManager.ListCategory();
                        Clear();
                        break;

                    case 9:
                        Console.Clear();
                        Console.WriteLine("--- İŞLEM EKLE ---\n");
                        transactionManager.AddTransaction();
                        Clear();
                        break;

                    case 10:
                        Console.Clear();
                        Console.WriteLine("--- İŞLEM LİSTELE ---\n");
                        transactionManager.ListTransaction();
                        Clear();
                        break;

                    case 11:
                        Console.Clear();
                        Console.WriteLine("--- İŞLEM SİL ---\n");
                        transactionManager.DeleteTransaction();
                        Clear();
                        break;

                    case 12:
                        Console.Clear();
                        Console.WriteLine("--- İŞLEM GÜNCELLE ---\n");
                        transactionManager.UpdateTransaction();
                        Clear();
                        break;

                    case 13:
                        Console.Clear();
                        Console.WriteLine("--- AYLIK RAPOR ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 14:
                        Console.Clear();
                        Console.WriteLine("--- EN ÇOK HARCAMA YAPILAN KATEGORİ ---\n");
                        Console.WriteLine("Buraya kod gelecek...\n");
                        Clear();
                        break;

                    case 15:
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
