using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PersonalFinanceManager.Data;

namespace PersonalFinanceManager.Business
{
    public class CategoryManager
    {
        private DbConnection db = new DbConnection();

        public void AddCategory()
        {
            Console.WriteLine("Kategori adını giriniz :");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Kategori adı boş olamaz!");
                return;
            }

            Console.WriteLine("Kategori tipini giriniz :");
            string type = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Kategori tipi boş olamaz!");
                return;
            }


            using (SqlConnection connection = db.GetConnection())
            {
                string queryAdd = "INSERT INTO Categories (CategoryName,Type) VALUES (@categoryName,@type)";
                SqlCommand command = new SqlCommand(queryAdd, connection);

                command.Parameters.Add("@categoryName", SqlDbType.VarChar, 70).Value = name;
                command.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type;
                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kategori oluşturuldu");
                    }
                    else
                    {
                        Console.WriteLine("\nKategori eklenemedi!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void ListCategory()
        {
            using (SqlConnection connection = db.GetConnection())
            {
                string queryList = "SELECT Id,CategoryName,Type FROM Categories";
                SqlCommand command = new SqlCommand(queryList, connection);
                bool hasData = false;
                try
                {
                    connection.Open();
                    using (SqlDataReader read = command.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            hasData = true;
                            Console.WriteLine($"Id : {read["Id"]}\n" +
                                $"Kategori Adı : {read["CategoryName"]}\n" +
                                $"Tipi : {read["Type"]}");
                            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                        }
                    }
                    if (!hasData)
                    {
                        Console.WriteLine("Hiç kategori bulunamadı.");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void DeleteCategory()
        {
            Console.Write("Silmek istediğiniz kategori Id numarasını giriniz :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            using (SqlConnection connection = db.GetConnection())
            {
                string queryDelete = "DELETE FROM Categories Where Id=@id";
                SqlCommand command = new SqlCommand(queryDelete, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kategori başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("\nKategori silinemedi! Belki Id yanlış veya kategori yok.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void UpdateCategory()
        {
            Console.Write("Güncellemek istediğiniz kategorinin Id numarasını giriniz : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            Console.Write("Güncel kategori adını giriniz : ");
            string newName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                Console.WriteLine("Kategori adı boş olamaz!");
                return;
            }

            Console.Write("Güncel kategori tipini giriniz : ");
            string newType = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newType))
            {
                Console.WriteLine("Kategori tipi boş olamaz!");
                return;
            }

            using (SqlConnection connection = db.GetConnection())
            {
                string queryUpdate = "UPDATE Categories SET CategoryName=@newName,Type=@newType WHERE Id=@id";
                SqlCommand command = new SqlCommand(queryUpdate, connection);
                command.Parameters.Add("@newName", SqlDbType.VarChar, 70).Value = newName;
                command.Parameters.Add("@newType", SqlDbType.VarChar, 50).Value = newType;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kategori başarıyla güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("\nKategori güncellenemedi!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }
    }
}
