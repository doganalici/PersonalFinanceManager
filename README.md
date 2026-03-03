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
* GROUP BY, ORDER BY sorguları
* Kullanıcı bazlı raporlama

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
* Using blokları ile güvenli bağlantı kapatma

📂 Business Katmanı

Uygulama mantığını içerir.

* User CRUD işlemleri
* Category CRUD işlemleri
* Transaction CRUD işlemleri
* JOIN ile ilişkili veri çekme
* Kullanıcı bazlı işlem listeleme
* Aylık raporlama
* En çok harcanan kategori analizi

---

👤 User İşlemleri

* Kullanıcı Ekleme
* Kullanıcı Listeleme
* Kullanıcı Güncelleme
* Kullanıcı Silme

✔ Parametreli sorgular kullanılmıştır<br>
✔ Input validation uygulanmıştır<br>
✔ ExecuteNonQuery ile etkilenen satır kontrolü yapılmaktadır<br>
✔ Id doğrulama kontrolü

---

🗂️ Category İşlemleri

* Kategori Ekleme
* Kategori Listeleme
* Kategori Güncelleme
* Kategori Silme

✔ Kategori adı boş olamaz kontrolü<br>
✔ Tip alanı için validasyon<br>
✔ Güvenli SQL parametreleri<br>
✔ Türkçe tip gösterimi (Income → Gelir, Expense → Gider)

---

💰 Transaction İşlemleri

* İşlem ekleme
* İşlem Listeleme (Kullanıcı bazlı)
* İşlem Güncelleme (Seçilen kullanıcıya göre)
* INNER JOIN ile ilişkili veri çekme
* Tarihe göre sıralama (ORDER BY DESC)
* Kullanıcı ve kategori adını birlikte gösterme
* Tarihe göre sıralama (ORDER BY DESC)
* Nullable Description desteği
* Decimal precision & scale ayarlama
* Kullanıcı ve kategori Id doğrulama

---

🔗 JOIN Kullanımı

Transactions tablosu:

* UserId → Users tablosuna bağlı (FK)
* CategoryId → Categories tablosuna bağlı (FK)

INNER JOIN Users u ON t.UserId = u.Id<br>
INNER JOIN Categories c ON t.CategoryId = c.Id<br><br>
kullanılarak ilişkili veriler tek sorguda çekilmektedir.

---

📊 Raporlama Özellikleri<br>
📅 Aylık Rapor (Kullanıcı Bazlı)

* Kullanıcı seçimi
* Ay ve yıl seçimi
* Gelir toplamı
* Gider toplamı
* Bakiye hesaplama (Gelir - Gider)
* GROUP BY ile tip bazlı hesaplama

---

🥇 En Çok Harcanan Kategori (Kullanıcı + Ay Bazlı)

* Kullanıcı seçimi
* Ay ve yıl filtresi
* Sadece gider işlemleri dikkate alınır
* TOP 1 + ORDER BY DESC ile en yüksek harcama kategorisi
* SUM ile toplam harcama hesaplama

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
