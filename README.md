# Basınç Düşüşü İzleme Sistemi
Bu proje, sahadaki elektronik kartlardan alınan basınç verilerini merkezi bir sunucuya ileterek anlık izleme, kayıt ve analiz imkanı sunar. Sistem, **REST API** kullanarak verilerin güvenli bir şekilde sunucuya gönderilmesini sağlar. Gelen veriler zaman damgası ile birlikte veritabanına kaydedilir ve yetkili kullanıcıların erişebileceği yönetim panelinde grafiksel olarak görüntüenebilir.

  
  ## Amaç ve Hedefler
- Sahadaki cihazlardan gelen basınç verilerinin merkezi bir sunucuda toplanması

- Basınç düşüşü gibi kritik durumların izlenmesi

- Yetkisiz erişimlerin engellenmesi

- Sade bir arayüz ile verilerin görsel analizinin sağlanması

- Yetkili kullanıcıların veri akışını kontrol edebilmesi

 ## Sistem Bileşenleri
 ### Cihaz Tarafı (Client)
- Basınç sensörlerinden aldığı veriye göre karar verir.

- Basınç düşerse sunucuya false, normale dönerse true değeri gönderir.

- REST API üzerinden HTTP POST isteği ile veri iletir.

- Her cihaz kendine ait bir kimlik bilgisine sahiptir.


 ### Sunucu Tarafı (Backend)

- Gelen verileri zaman damgası ile birlikte veritabanına kaydeder.

- Her API isteğini loglar.

- Veritabanı olarak MSSQL kullanılır

- Yetkili kullanıcıların giriş yapabileceği bir yönetim paneli sunar.


  ### Yönetim Paneli (Frontend)
- HTML ile tasarlanmış sade bir arayüz.

- Giriş yapıldıktan sonra:

- Anlık gelen veriler görüntülenebilir.

- Veri akışı başlatılabilir veya durdurulabilir.

- Veriler grafiklerle analiz edilebilir.


 ## Güvenlik Özellikleri
- Yetkili Cihaz Doğrulama: API, sadece güvenilir cihazlardan gelen verileri kabul eder.

- Kullanıcı Giriş Sistemi: Yönetim paneline erişim sadece kullanıcı adı ve şifre ile mümkündür.

- Şifreleme: Kullanıcı şifreleri MD5 algoritması ile şifrelenip veritabanında saklanır.

- Veri Loglama: Tüm veri girişleri tarih-saat bilgisiyle birlikte loglanır.

- HTTPS: Verilerin iletimi güvenli bağlantı (SSL) üzerinden yapılır.

- İstek Doğrulama: Gelen veriler format, içerik ve yetki açısından kontrol edilir.


  ## RESTful API Nedir?
- REST (Representational State Transfer), istemci ile sunucu arasında veri alışverişi için kullanılan bir web servis mimarisidir. RESTful API, bu mimariye uygun çalışan sistemleri ifade eder.

- Projemizde RESTful API, cihazdan gelen basınç verilerini sunucuya iletmek için kullanılır. JSON formatında gönderilen bu veriler backend tarafından işlenip veritabanına kaydedilir.


  ## RESTful API'de Güvenlik Nasıl Sağlanır?
- API Token: Her cihaz benzersiz bir erişim anahtarı (API Key veya Token) ile doğrulanır.

- Rate Limiting: Aşırı istek yapan cihazlar engellenebilir.

- Girdi Kontrolü: Tüm gelen veriler doğruluk ve bütünlük açısından kontrol edilir.

- Sunucu Kayıtları: Her isteğin log'u tutulur, kimden ne zaman geldiği takip edilebilir.

