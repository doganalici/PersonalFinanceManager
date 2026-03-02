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
            using (SqlConnection connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    // Kullanıcıları Listele
                    Console.WriteLine("----- KULLANICILAR -----");

                    string listUsersQuery = "SELECT Id, Name FROM Users";
                    SqlCommand listUsersCmd = new SqlCommand(listUsersQuery, connection);
                    SqlDataReader userReader = listUsersCmd.ExecuteReader();

                    bool hasUser = false;

                    while (userReader.Read())
                    {
                        hasUser = true;
                        Console.WriteLine($"Id : {userReader["Id"]} - {userReader["Name"]}");
                    }

                    userReader.Close();

                    if (!hasUser)
                    {
                        Console.WriteLine("Sistemde kayıtlı kullanıcı yok.");
                        return;
                    }

                    // ---------------- USER ID ----------------
                    Console.Write("\nKullanıcı Id giriniz : ");
                    int userId;
                    while (!int.TryParse(Console.ReadLine(), out userId))
                    {
                        Console.Write("Geçerli bir Id giriniz : ");
                    }

                    string userCheckQuery = "SELECT COUNT(*) FROM Users WHERE Id=@id";
                    SqlCommand userCheckCmd = new SqlCommand(userCheckQuery, connection);
                    userCheckCmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;

                    int userExists = (int)userCheckCmd.ExecuteScalar();
                    if (userExists == 0)
                    {
                        Console.WriteLine("Böyle bir kullanıcı bulunamadı!");
                        return;
                    }


                    Console.WriteLine("\n----- KATEGORİLER -----");

                    string listCategoriesQuery = "SELECT Id, CategoryName FROM Categories";
                    SqlCommand listCategoriesCmd = new SqlCommand(listCategoriesQuery, connection);
                    SqlDataReader categoryReader = listCategoriesCmd.ExecuteReader();

                    bool hasCategory = false;

                    while (categoryReader.Read())
                    {
                        hasCategory = true;
                        Console.WriteLine($"Id : {categoryReader["Id"]} - {categoryReader["CategoryName"]}");
                    }

                    categoryReader.Close();

                    if (!hasCategory)
                    {
                        Console.WriteLine("Sistemde kayıtlı kategori yok.");
                        return;
                    }

                    // ---------------- CATEGORY ID ----------------
                    Console.Write("Kategori Id giriniz : ");
                    int categoryId;
                    while (!int.TryParse(Console.ReadLine(), out categoryId))
                    {
                        Console.Write("Geçerli bir Id giriniz : ");
                    }

                    string categoryCheckQuery = "SELECT COUNT(*) FROM Categories WHERE Id=@id";
                    SqlCommand categoryCheckCmd = new SqlCommand(categoryCheckQuery, connection);
                    categoryCheckCmd.Parameters.Add("@id", SqlDbType.Int).Value = categoryId;

                    int categoryExists = (int)categoryCheckCmd.ExecuteScalar();
                    if (categoryExists == 0)
                    {
                        Console.WriteLine("Böyle bir kategori bulunamadı!");
                        return;
                    }

                    // ---------------- AMOUNT ----------------
                    Console.Write("Tutar giriniz: ");
                    decimal amount;
                    while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                    {
                        Console.Write("Geçerli ve 0'dan büyük bir tutar giriniz : ");
                    }

                    // ---------------- DATE ----------------
                    DateTime transactionDate = DateTime.Now;

                    // ---------------- DESCRIPTION ----------------
                    Console.Write("Açıklama giriniz (boş bırakabilirsiniz) : ");
                    string description = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        description = null;
                    }

                    // ---------------- INSERT ----------------
                    string insertQuery = @"INSERT INTO Transactions 
                                   (UserId, CategoryId, Amount, TransactionDate, Description)
                                   VALUES 
                                   (@userId, @categoryId, @amount, @date, @description)";

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@categoryId", SqlDbType.Int).Value = categoryId;
                    command.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = transactionDate;

                    if (description == null)
                        command.Parameters.Add("@description", SqlDbType.VarChar).Value = DBNull.Value;
                    else
                        command.Parameters.Add("@description", SqlDbType.VarChar, 250).Value = description;

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("\n[BİLGİ] İşlem başarıyla eklendi.");
                    }
                    else
                    {
                        Console.WriteLine("\nİşlem eklenemedi.");
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
                                t.UserId,
                                t.CategoryId,
                                u.Name AS UserName,
                                c.CategoryName AS CategoryName,
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

                            string typeValue = reader["Type"].ToString();
                            string typeText = typeValue == "Income" ? "Gelir" : "Gider";

                            DateTime date = Convert.ToDateTime(reader["TransactionDate"]);
                            string formattedDate = date.ToString("dd.MM.yyyy");

                            Console.WriteLine("----- İŞLEM -----");
                            Console.WriteLine($"Id : {reader["Id"]}\n" +
                                $"Kullanıcı : {reader["UserName"]} (Id: {reader["UserId"]})\n" +
                                $"Kategori  : {reader["CategoryName"]} (Id: {reader["CategoryId"]})\n" +
                                $"Tür       : {typeText}\n" +
                                $"Tutar     : {reader["Amount"]} TL\n" +
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
            Console.Write("Silmek istediğiniz işlem Id numarasını giriniz : ");
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
            using (SqlConnection connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    // 1️⃣ Kullanıcıları Listele
                    Console.WriteLine("----- KULLANICILAR -----");

                    string listUsersQuery = "SELECT Id, Name FROM Users";
                    SqlCommand listUsersCmd = new SqlCommand(listUsersQuery, connection);
                    SqlDataReader userReader = listUsersCmd.ExecuteReader();

                    bool hasUser = false;

                    while (userReader.Read())
                    {
                        hasUser = true;
                        Console.WriteLine($"Id : {userReader["Id"]} - {userReader["Name"]}");
                    }

                    userReader.Close();

                    if (!hasUser)
                    {
                        Console.WriteLine("Sistemde kayıtlı kullanıcı yok.");
                        return;
                    }

                    // 2️⃣ Kullanıcı Seç
                    Console.Write("\nGüncelleme yapmak istediğiniz kullanıcı Id giriniz : ");
                    int selectedUserId;
                    while (!int.TryParse(Console.ReadLine(), out selectedUserId))
                    {
                        Console.Write("Geçerli bir Id giriniz : ");
                    }

                    // Kullanıcı var mı kontrol
                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Id=@id";
                    SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, connection);
                    checkUserCmd.Parameters.AddWithValue("@id", selectedUserId);

                    int userExists = (int)checkUserCmd.ExecuteScalar();

                    if (userExists == 0)
                    {
                        Console.WriteLine("Böyle bir kullanıcı bulunamadı!");
                        return;
                    }

                    // 3️⃣ Seçilen kullanıcının işlemi var mı?
                    string checkTransactionQuery = "SELECT COUNT(*) FROM Transactions WHERE UserId=@uid";
                    SqlCommand checkTransactionCmd = new SqlCommand(checkTransactionQuery, connection);
                    checkTransactionCmd.Parameters.AddWithValue("@uid", selectedUserId);

                    int transactionCount = (int)checkTransactionCmd.ExecuteScalar();

                    if (transactionCount == 0)
                    {
                        Console.WriteLine("Bu kullanıcıya ait işlem bulunmamaktadır.");
                        return;
                    }

                    // 4️⃣ Kullanıcının işlemlerini listele
                    Console.WriteLine("\n----- KULLANICININ İŞLEMLERİ -----");

                    string listTransactionsQuery = @"
                SELECT 
                    t.Id,
                    t.UserId,
                    t.CategoryId,
                    u.Name AS UserName,
                    c.CategoryName,
                    c.Type,
                    t.Amount,
                    t.TransactionDate,
                    t.Description
                FROM Transactions t
                INNER JOIN Users u ON t.UserId = u.Id
                INNER JOIN Categories c ON t.CategoryId = c.Id
                WHERE t.UserId = @uid";

                    SqlCommand listTransactionsCmd = new SqlCommand(listTransactionsQuery, connection);
                    listTransactionsCmd.Parameters.AddWithValue("@uid", selectedUserId);

                    SqlDataReader transactionReader = listTransactionsCmd.ExecuteReader();

                    while (transactionReader.Read())
                    {
                        string typeValue = transactionReader["Type"].ToString();
                        string typeText = typeValue == "Income" ? "Gelir" : "Gider";

                        Console.WriteLine("----------------------------");
                        Console.WriteLine(
                            $"Id        : {transactionReader["Id"]}\n" +
                            $"Kategori  : {transactionReader["CategoryName"]} (Id: {transactionReader["CategoryId"]})\n" +
                            $"Tür       : {typeText}\n" +
                            $"Tutar     : {transactionReader["Amount"]}\n" +
                            $"Tarih     : {Convert.ToDateTime(transactionReader["TransactionDate"]).ToString("dd.MM.yyyy")}\n" +
                            $"Açıklama  : {transactionReader["Description"]}"
                        );
                    }

                    transactionReader.Close();

                    // 5️⃣ Güncellenecek işlem Id al
                    Console.Write("\nGüncellemek istediğiniz işlem Id giriniz : ");
                    int transactionId;
                    while (!int.TryParse(Console.ReadLine(), out transactionId))
                    {
                        Console.Write("Geçerli bir Id giriniz : ");
                    }

                    // İşlem o kullanıcıya ait mi kontrol
                    string checkTransactionOwnership = "SELECT COUNT(*) FROM Transactions WHERE Id=@tid AND UserId=@uid";
                    SqlCommand checkOwnershipCmd = new SqlCommand(checkTransactionOwnership, connection);
                    checkOwnershipCmd.Parameters.AddWithValue("@tid", transactionId);
                    checkOwnershipCmd.Parameters.AddWithValue("@uid", selectedUserId);

                    int transactionExists = (int)checkOwnershipCmd.ExecuteScalar();

                    if (transactionExists == 0)
                    {
                        Console.WriteLine("Bu işlem bu kullanıcıya ait değil veya bulunamadı.");
                        return;
                    }

                    // 6️⃣ Güncelleme alanları
                    Console.Write("Yeni kategori Id giriniz : ");
                    int newCategoryId;
                    while (!int.TryParse(Console.ReadLine(), out newCategoryId))
                    {
                        Console.Write("Geçerli bir Id giriniz : ");
                    }

                    Console.Write("Yeni tutar giriniz : ");
                    decimal newAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out newAmount) || newAmount <= 0)
                    {
                        Console.Write("Geçerli bir tutar giriniz : ");
                    }

                    Console.Write("Yeni tarih giriniz : ");
                    DateTime newDate;
                    while (!DateTime.TryParse(Console.ReadLine(), out newDate))
                    {
                        Console.Write("Geçerli bir tarih giriniz : ");
                    }

                    Console.Write("Yeni açıklama giriniz : ");
                    string newDescription = Console.ReadLine();

                    // 7️⃣ Update
                    string updateQuery = @"
                UPDATE Transactions
                SET CategoryId=@cid,
                    Amount=@amount,
                    TransactionDate=@date,
                    Description=@desc
                WHERE Id=@tid";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@cid", newCategoryId);
                    updateCmd.Parameters.AddWithValue("@amount", newAmount);
                    updateCmd.Parameters.AddWithValue("@date", newDate);
                    updateCmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(newDescription) ? DBNull.Value : (object)newDescription);
                    updateCmd.Parameters.AddWithValue("@tid", transactionId);

                    updateCmd.ExecuteNonQuery();

                    Console.WriteLine("\nİşlem başarıyla güncellendi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu : {ex.Message}");
                }
            }
        }
    }
}
