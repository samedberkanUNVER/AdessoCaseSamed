# Adesso World League Uygulama Adımları

## 1. Gerekli konfigurasyonlar ve Veri tabanı oluşturma

1.Bağlantı Dizesini Düzenleme: appsettings klasörü içerisinde, yerel sisteminizde çalıştırmak istediğiniz veri tabanına ait bağlantı dizesini uygun şekilde güncelleyiniz.

2.Package Manager Console Açma: Ayarlar menüsünden "Package Manager Console" seçeneğini açınız.

3.Proje Seçimi: Açılan konsolda "Default Project" olarak Persistence katmanını seçiniz.

4.Veri Tabanını Güncelleme: Konsola update-database komutunu yazınız.

5.Veri Tabanına Erişim: Oluşturduğunuz veri tabanına erişim sağlamak için SQL Server Object Explorer aracını kullanabilirsiniz.

6.Not: Veri setleri, Persistence katmanındaki konfigürasyon dosyaları aracılığıyla otomatik olarak seed işlemleri ile oluşturulmuştur. (Team, Country, Group ve Picker bilgileri)
## 2. Derleme ve Çalıştırma

1.Proje Ayarları: İlk olarak, WebApi projesine sağ tıklayarak bu projeyi başlangıç projesi olarak seçiniz.

2.Çalıştırma: Ardından, üst kısımdaki "Oynat" butonuna tıklayarak projeyi başlatınız.

3.Swagger UI: Uygulama başlatıldığında, Swagger UI arayüzü ile karşılaşacaksınız.


## 3. SİMÜLASYON
Draw Endpoint'ine Erişim Sağlama:

İlk olarak, Draw Endpoint altında bulunan /api/Draws adresine gidiniz.
1.Grupların Büyüklüğünü Belirleme:

Açılan sayfada, groupCount parametresi için 4 ila 8 arasında bir değer giriniz. Bu değer, oluşturulacak grupların büyüklüğünü belirleyecektir.
Kura Çekme İşlemi İçin Picker Seçimi:

Kurayı çekecek kişinin Idsini giriniz. Bunun için:
Yeni bir Picker oluşturmak üzere Picker bölümüne gidip gerekli bilgileri doldurabilirsiniz.
Alternatif olarak, mevcut bir Picker'ın Idsini de seçebilirsiniz.
Kura Adını Girme:

drawName alanına, kura için uygun bir isim giriniz. Bu isim, kura çekim sürecinin tanımlanmasına yardımcı olacaktır.
Kura Çekme İşlemi:

Tüm gerekli bilgileri doldurduktan sonra, kurayı çekmek için ilgili butona tıklayarak işlemi gerçekleştirebilirsiniz.


# Projenin Kapsamı ve Kullanılan Teknolojiler 

#### 1. .NET Core: 

Projenin temel geliştirme platformu olarak yalnızca .NET Core kullanılacaktır. 

2. ASP.NET Core Web API: 

Projenin kullanıcı arayüzü ile arka uç arasında bir iletişim köprüsü kurmak için ASP.NET Core Web API kullanılacaktır. 

RESTful mimari prensiplerine uygun olarak tasarlanacak ve HTTP istekleri üzerinden veri alışverişi yapılacaktır. 

3. Entity Framework Core: 

Veritabanı işlemleri için Entity Framework Core kullanılacaktır. Bu, veritabanıyla etkileşimde bulunmayı ve veri yönetimini kolaylaştırır. ORM (Object-Relational Mapping) yetenekleri sayesinde, veritabanındaki verilerle nesne tabanlı bir yaklaşım kullanarak işlem yapmamıza olanak tanır. 

4. MSSQL: 

Verilerin depolanması için Microsoft SQL Server kullanılacaktır.  

MSSQL, ilişkisel bir veritabanı yönetim sistemi (RDBMS) olarak, verilerin tablo şeklinde yapılandırılmasına ve bu tablolar arasında ilişkilerin kurulmasına olanak tanır. 

5. MediatR: 

Projede, uygulama içindeki iletişimi ve sorgu yönetimini basitleştirmek amacıyla MediatR kütüphanesi kullanılacaktır.  

6. JSON: 

API ile yapılan veri alışverişlerinde JSON formatı kullanılacaktır. JSON, hafif yapısı ve kolay okunabilirliği ile API yanıtlarını ve isteklerini yönetmek için ideal bir veri formatıdır. 


# Veritabanı Yapısı 

Adesso World League projesinde, verilerin organize bir şekilde depolanması ve yönetilmesi için bir veritabanı yapısı oluşturulacaktır. Bu yapı, tablolar ve bu tablolar arasındaki ilişkileri içerecek şekilde tasarlanacaktır. Aşağıda, veritabanı tabloları, ilişkileri ve kura sonuçlarının kaydedilme yapısı detaylandırılmaktadır. 

## Veritabanı Tabloları 

1.Teams (Takımlar) 

Tanım: Bu tablo, ligde yarışan takımlara ait bilgileri saklayacaktır. 

Alanlar:  

TeamID (int, Primary Key): Takımın benzersiz kimliği. 

Name (nvarchar(100)): Takım adı. 

CountryID (int, Foreign Key): Ülkeler tablosundan referans. 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 

 

2. Countries (Ülkeler) 

Tanım: Bu tablo, ligdeki takımların ait olduğu ülkelerin bilgilerini saklayacaktır. 

Alanlar: 

CountryID (int, Primary Key): Ülkenin benzersiz kimliği. 

Name (nvarchar(100)): Ülke adı. 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 

 

3.Groups (Gruplar) 

Tanım: Bu tablo, oluşturulan grupların bilgilerini içerecektir. 

Alanlar: 

GroupID (int, Primary Key): Grup kimliği  

GroupName (nvarchar(50)): Grup adı. (A, B, C, ...). 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 


4.GroupTeams (Grup-Takım İlişkisi) 

Tanım: Bu tablo, hangi takımların hangi grupta yer aldığını ve kura çekimine ait olduğu bilgisini içerecektir. 

Alanlar: 

TeamID (int, Foreign Key): Takımlar tablosundan referans. 

GroupID (int, Foreign Key): Gruplar tablosundan referans. 

DrawID (int, Foreign Key): Kura tablosundan referans. 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 

 

İlişki: 

GroupID, Groups tablosuna (1-N ilişkisi). 

TeamID, Teams tablosuna (1-N ilişkisi). 

DrawID, Draws tablosuna (1-N ilişkisi). 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 

 
5.Draws (Kura Sonuçları) 

Tanım: Bu tablo, kura çekimi sonuçlarının kaydedileceği alandır. 

Alanlar: 

DrawID (int, Primary Key): Kuranın benzersiz kimliği. 

PickerID (int, Foreign Key): Kura çeken kişinin kimliğini belirten alan. 

GroupID (int, Foreign Key): Gruplar tablosundan referans. 

TeamID (int, Foreign Key): Takımlar tablosundan referans. 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 

 

6.Pickers (Kura Çeken Kişiler) 

Tanım: Bu tablo, kura çeken kişilerin bilgilerini içerecektir. 

Alanlar: 

Id (int, Primary Key): Kura çeken kişinin benzersiz kimliği. 

Name (nvarchar(100)): Kura çeken kişinin adı. 

Surname (nvarchar(100)): Kura çeken kişinin soyadı. 

Email (nvarchar(100)): Kura çeken kişinin E-posta adresi. 

CreatedDate (datetime): Kayıt oluşturulma tarihi. 

UpdatedDate (datetime, Nullable): Kayıt güncellenme tarihi. 

DeletedDate (datetime, Nullable): Kayıt silinme tarihi. 


