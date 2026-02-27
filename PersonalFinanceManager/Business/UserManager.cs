using PersonalFinanceManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalFinanceManager.Business
{
    public class UserManager
    {
        private DbConnection db = new DbConnection();
        public void AddUser()
        {
            Console.Write("Kullanıcı adını giriniz : ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Kullanıcı adı boş olamaz!");
                return;
            }


            using (SqlConnection connection = db.GetConnection())
            {

                string queryAdd = "INSERT INTO Users (Name) VALUES (@name)";
                SqlCommand command = new SqlCommand(queryAdd, connection);

                command.Parameters.Add("@name", SqlDbType.VarChar, 40).Value = name;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kullanıcı oluşturuldu");
                    }
                    else
                    {
                        Console.WriteLine("\nKullanıcı eklenemedi!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void ListUser()
        {
            using (SqlConnection connection = db.GetConnection())
            {
                string queryList = "SELECT Id, Name FROM Users";
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
                                $"Kullanıcı adı : {read["Name"]}");
                            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                        }
                    }
                    if (!hasData)
                    {
                        Console.WriteLine("Hiç kullanıcı bulunamadı.");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }

            }
        }

        public void DeleteUser()
        {
            Console.Write("Silmek istediğiniz kullanıcı Id numarasını giriniz :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            using (SqlConnection connection = db.GetConnection())
            {
                string queryDelete = "DELETE FROM Users WHERE Id=@id";
                SqlCommand command = new SqlCommand(queryDelete, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kullanıcı başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("\nKullanıcı silinemedi! Belki Id yanlış veya kullanıcı yok.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void UpdateUser()
        {
            Console.Write("Güncellemek istediğiniz kullanıcı Id numarasını giriniz : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }
            Console.Write("Güncel kullanıcı adını giriniz : ");
            string newName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Kullanıcı adı boş olamaz!");
                return;
            }

            using (SqlConnection connection = db.GetConnection())
            {
                string queryUpdate = "UPDATE Users SET Name=@newName WHERE Id=@id";
                SqlCommand command = new SqlCommand(queryUpdate, connection);
                command.Parameters.Add("@newName", SqlDbType.VarChar, 40).Value = newName;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] Kullanıcı başarıyla güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("\nKullanıcı güncellenemedi!");
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
