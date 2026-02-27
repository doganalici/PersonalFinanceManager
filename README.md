📌 Personal Finance Manager

Basit ve katmanlı mimari kullanılarak geliştirilmiş, ADO.NET tabanlı bir Konsol Finans Yönetim Uygulaması.

---

🚀 Proje Amacı

Bu proje ile aşağıdaki konular pratiğe dökülmüştür:

* C# temel programlama
* Katmanlı mimari (Business & Data ayrımı)
* ADO.NET ile veritabanı işlemleri
* Parametreli SQL kullanımı
* CRUD (Create, Read, Update, Delete) operasyonları
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
│<br>
└── Program.cs

---

📂 Data Katmanı

Veritabanı bağlantı işlemlerini içerir.

📂 Business Katmanı

Uygulama mantığını içerir.

* User CRUD işlemleri
* Category CRUD işlemleri

---

👤 User İşlemleri

* Kullanıcı Ekleme
* Kullanıcı Listeleme
* Kullanıcı Güncelleme
* Kullanıcı Silme

Parametreli sorgular kullanılarak SQL Injection’a karşı güvenli yapı oluşturulmuştur.

---

🗂️ Category İşlemleri

* Kategori Ekleme
* Kategori Listeleme
* Kategori Güncelleme
* Kategori Silme

Kategori adı ve tipi için validasyon kontrolleri eklenmiştir.

---

🔐 Güvenlik ve İyi Pratikler

✔ Parametreli SQL kullanımı<br>
✔ Using blokları ile bağlantı yönetimi<br>
✔ Try-Catch ile hata kontrolü<br>
✔ Input doğrulama (Null / WhiteSpace kontrolü)<br>
✔ Etkilenen satır kontrolü (ExecuteNonQuery)<br>

---

🗄️ Veritabanı Yapısı (Örnek)<br>
Users

* Id (int, PK)
* Name (varchar)

Categories

* Id (int, PK)
* CategoryName (varchar)
* Type (varchar)
