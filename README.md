📌 Personal Finance Manager

Basit ve katmanlı mimari kullanılarak geliştirilmiş, ADO.NET tabanlı bir Console Finans Yönetim Uygulaması.

---

🚀 Proje Amacı

Bu proje ile aşağıdaki konular pratiğe dökülmüştür:

* C# temel programlama
* Katmanlı mimari (Business & Data ayrımı)
* ADO.NET ile veritabanı işlemleri
* Parametreli SQL kullanımı
* CRUD (Create, Read, Update, Delete) operasyonları
* INNER JOIN kullanımı
* Foreign Key ilişkileri
* Exception handling
* Input validation
* SQL Server ile bağlantı yönetimi

---

🛠️ Kullanılan Teknolojiler

* C#
* .NET
* ADO.NET
* SQL Server
* SSMS

---

🧱 Proje Yapısı<br>
PersonalFinanceManager<br>
│<br>
├── Data<br>
│   └── DbConnection.cs<br>
│<br>
├── Business<br>
│   ├── UserManager.cs<br>
│   └── CategoryManager.cs<br>
│   └── TransactionManager.cs<br>
│<br>
└── Program.cs

---

📂 Data Katmanı

Veritabanı bağlantı işlemlerini içerir.

* SQL Server bağlantı yönetimi
* SqlConnection nesnesi üretimi

📂 Business Katmanı

Uygulama mantığını içerir.

* User CRUD işlemleri
* Category CRUD işlemleri
* Transaction işlemleri
* JOIN ile ilişkili veri çekme

---

👤 User İşlemleri

* Kullanıcı Ekleme
* Kullanıcı Listeleme
* Kullanıcı Güncelleme
* Kullanıcı Silme

✔ Parametreli sorgular kullanılmıştır<br>
✔ Input validation uygulanmıştır<br>
✔ ExecuteNonQuery ile etkilenen satır kontrolü yapılmaktadır

---

🗂️ Category İşlemleri

* Kategori Ekleme
* Kategori Listeleme
* Kategori Güncelleme
* Kategori Silme

✔ Kategori adı boş olamaz kontrolü<br>
✔ Tip alanı için validasyon<br>
✔ Güvenli SQL parametreleri

---

💰 Transaction İşlemleri

* İşlem ekleme
* INNER JOIN ile işlem listeleme
* Kullanıcı ve kategori adını birlikte gösterme
* Tarihe göre sıralama (ORDER BY DESC)
* Decimal precision & scale ayarlama
* Nullable Description desteği

---

🔗 JOIN Kullanımı

Transactions tablosu:

* UserId → Users tablosuna bağlı (FK)
* CategoryId → Categories tablosuna bağlı (FK)

INNER JOIN Users u ON t.UserId = u.Id<br>
INNER JOIN Categories c ON t.CategoryId = c.Id<br><br>
kullanılarak ilişkili veriler tek sorguda çekilmektedir.

---

🔐 Güvenlik ve İyi Pratikler

✔ Parametreli SQL kullanımı (SQL Injection koruması)<br>
✔ Using blokları ile bağlantı yönetimi<br>
✔ Try-Catch ile hata kontrolü<br>
✔ Input doğrulama (TryParse, Null / WhiteSpace kontrolü)<br>
✔ Decimal Precision & Scale ayarlama<br>
✔ Foreign Key ilişkisel yapı<br>

---

🗄️ Veritabanı Yapısı (Örnek)<br>
Users

* Id (int, PK)
* Name (varchar)

Categories

* Id (int, PK)
* CategoryName (varchar)
* Type (varchar)

Transactions

* Id (int, PK)
* UserId (int, FK)
* CategoryId (int, FK)
* Amount (decimal 18,2)
* TransactionDate (datetime)
* Description (varchar, nullable)
