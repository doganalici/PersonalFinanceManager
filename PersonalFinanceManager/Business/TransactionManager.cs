using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceManager.Data;
using System.Data;
using System.Data.SqlClient;

namespace PersonalFinanceManager.Business
{
    public class TransactionManager
    {
        private DbConnection db = new DbConnection();

        public void AddTransaction()
        {
            Console.WriteLine("---- YENİ İŞLEM EKLE ----");

            UserManager userManager = new UserManager();
            userManager.ListUser();

            Console.Write("Kullanıcı Id giriniz : ");
            int userId;
            while (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.Write("Geçerli bir kullanıcı Id giriniz: ");
            }

            CategoryManager categoryManager = new CategoryManager();
            categoryManager.ListCategory();

            Console.Write("Kategori Id giriniz : ");
            int categoryId;
            while (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.Write("Geçerli bir kategori Id giriniz: ");
            }


            Console.Write("Tutar giriniz : ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Geçerli ve pozitif bir tutar giriniz: ");
            }

            DateTime dateTime = DateTime.Now;

            Console.Write("Açıklama giriniz (isteğe bağlı) : ");
            string description = Console.ReadLine();


            using (SqlConnection connection = db.GetConnection())
            {
                string queryAdd = "INSERT INTO Transactions (UserId,CategoryId,Amount,TransactionDate,Description) VALUES (@userId,@categoryId,@amount,@dateTime,@description)";
                SqlCommand command = new SqlCommand(queryAdd, connection);

                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                command.Parameters.Add("@categoryId", SqlDbType.Int).Value = categoryId;
                command.Parameters.Add("@amount", SqlDbType.Decimal).Precision = 18;
                command.Parameters["@amount"].Scale = 2;
                command.Parameters["@amount"].Value = amount;
                command.Parameters.Add("@dateTime", SqlDbType.DateTime).Value = dateTime;
                command.Parameters.Add("@description", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] İşlem oluşturuldu");
                    }
                    else
                    {
                        Console.WriteLine("\nİşlem eklenemedi!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void ListTransaction()
        {
            using (SqlConnection connection = db.GetConnection())
            {
                string query = @"SELECT u.UserName,
                                c.CategoryName,
                                t.Amount,
                                t.TransactionDate,
                                t.Description 
                                FROM Transactions t 
                                INNER JOIN Users u ON t.UserId=u.Id
                                INNER JOIN Categories c ON t.CategoryId=c.Id
                                ORDER BY t.TransactionDate DESC";

                SqlCommand command = new SqlCommand(query, connection);
                bool hasData = false;
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasData = true;
                            Console.WriteLine("----- İŞLEM -----");
                            Console.WriteLine($"Kullanıcı : {reader["UserName"]}\n" +
                                $"Kategori  : {reader["CategoryName"]}\n" +
                                $"Tutar     : {reader["Amount"]}\n" +
                                $"Tarih     : {reader["TransactionDate"]}\n" +
                                $"Açıklama  : {reader["Description"]}");
                            Console.WriteLine("---------------------------");
                        }
                        if (!hasData)
                        {
                            Console.WriteLine("Hiç işlem bulunamadı.");
                        }
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
