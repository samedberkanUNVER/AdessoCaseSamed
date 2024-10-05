# Adesso World League Uygulama Adımları

## 2. Gerekli konfigurasyonlar ve Veri tabanı oluşturma

1.Bağlantı Dizesini Düzenleme: appsettings klasörü içerisinde, yerel sisteminizde çalıştırmak istediğiniz veri tabanına ait bağlantı dizesini uygun şekilde güncelleyiniz.

2.Package Manager Console Açma: Ayarlar menüsünden "Package Manager Console" seçeneğini açınız.

3.Proje Seçimi: Açılan konsolda "Default Project" olarak Persistence katmanını seçiniz.

4.Veri Tabanını Güncelleme: Konsola update-database komutunu yazınız.

5.Veri Tabanına Erişim: Oluşturduğunuz veri tabanına erişim sağlamak için SQL Server Object Explorer aracını kullanabilirsiniz.

6.Not: Veri setleri, Persistence katmanındaki konfigürasyon dosyaları aracılığıyla otomatik olarak seed işlemleri ile oluşturulmuştur. (Takım ve ülke bilgileri)
## 3. Derleme ve Çalıştırma

1.Proje Ayarları: İlk olarak, WebApi projesine sağ tıklayarak bu projeyi başlangıç projesi olarak seçiniz.

2.Çalıştırma: Ardından, üst kısımdaki "Oynat" butonuna tıklayarak projeyi başlatınız.

3.Swagger UI: Uygulama başlatıldığında, Swagger UI arayüzü ile karşılaşacaksınız.


## 4. SİMÜLASYON
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


