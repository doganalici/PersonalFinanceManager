using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceManager.Data;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            string categoryType;
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT Type FROM Categories WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = categoryId;
                conn.Open();
                categoryType = (string)cmd.ExecuteScalar();
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
                string query = @"SELECT 
                                t.Id,
                                u.Name AS UserName,
                                c.Name AS CategoryName,
                                c.Type,
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
                            string description = reader["Description"] == DBNull.Value
                         ? "-"
                         : reader["Description"].ToString();

                            DateTime date = Convert.ToDateTime(reader["TransactionDate"]);
                            string formattedDate = date.ToString("dd.MM.yyyy");

                            Console.WriteLine("----- İŞLEM -----");
                            Console.WriteLine($"Id : {reader["Id"]}\n" +
                                $"Kullanıcı : {reader["UserName"]}\n" +
                                $"Kategori  : {reader["CategoryName"]}\n" +
                                $"Tür       : {reader["Type"]}\n" +
                                $"Tutar     : {reader["Amount"]}\n" +
                                $"Tarih     : {formattedDate}\n" +
                                $"Açıklama  : {description}");
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

        public void DeleteTransaction()
        {
            Console.Write("Silmek istediğiniz işlem Id numarasını giriniz :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            using (SqlConnection connection = db.GetConnection())
            {
                string queryDelete = "DELETE FROM Transactions WHERE Id=@id";
                SqlCommand command = new SqlCommand(queryDelete, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] İşlem başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("\nİşlem silinemedi! Belki Id yanlış veya işlem yok.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[HATA] Bir sorun oluştu : {ex.Message}");
                }
            }
        }

        public void UpdateTransaction()
        {
            Console.Write("Güncellemek istediğiniz işlem Id numarasını giriniz : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            Console.Write("Güncel kullanıcı Id numarasını giriniz : ");
            int newUserId;
            while (!int.TryParse(Console.ReadLine(), out newUserId))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            Console.Write("Güncel kategori Id numarasını giriniz : ");
            int newCategoryId;
            while (!int.TryParse(Console.ReadLine(), out newCategoryId))
            {
                Console.Write("\nListede bulunan bir Id numarası giriniz! ");
            }

            Console.Write("Güncel tutarı giriniz : ");
            decimal newAmount;
            while (!decimal.TryParse(Console.ReadLine(), out newAmount) || newAmount <= 0)
            {
                Console.Write("\nGeçerli ve pozitif bir tutar giriniz! ");
            }

            Console.Write("Güncel tarihi giriniz : ");
            DateTime newDate;
            while (!DateTime.TryParse(Console.ReadLine(), out newDate))
            {
                Console.Write("\nListede geçerli bir tarih giriniz! ");
            }

            Console.Write("Açıklama giriniz (isteğe bağlı) : ");
            string newDescription = Console.ReadLine();

            using (SqlConnection connection = db.GetConnection())
            {
                string queryUpdate = "UPDATE Transactions SET UserId=@newUserId,CategoryId=@newCategoryId,Amount=@newAmount,TransactionDate=@newDate,Description=@newDescription WHERE Id=@id";
                SqlCommand command = new SqlCommand(queryUpdate, connection);
                command.Parameters.Add("@newUserId", SqlDbType.Int).Value = newUserId;
                command.Parameters.Add("@newCategoryId", SqlDbType.Int).Value = newCategoryId;
                var paramAmount = command.Parameters.Add("@newAmount", SqlDbType.Decimal);
                paramAmount.Precision = 18;  // toplam basamak sayısı
                paramAmount.Scale = 2;       // virgülden sonraki basamak
                paramAmount.Value = newAmount;
                command.Parameters.Add("@newDate", SqlDbType.DateTime).Value = newDate;
                command.Parameters.Add("@newDescription", SqlDbType.VarChar, 100).Value = string.IsNullOrWhiteSpace(newDescription) ? DBNull.Value : (object)newDescription;

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] İşlem başarıyla güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("\nİşlem güncellenemedi!");
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
