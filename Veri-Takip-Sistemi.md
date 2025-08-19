# 📊 Akıllı Veri Takip Sistemi - Kapsamlı Kullanım ve Geliştirici Rehberi

---

## 🚀 Proje Tanımı

Akıllı Veri Takip Sistemi, cihazlardan gelen basınç verilerini alıp saklayan, analiz eden ve kullanıcıların bu verilere web arayüzü veya API yoluyla erişmesini sağlayan bir uygulamadır.  
Yerel ağda çalışır, kullanıcı doğrulaması yapar ve verileri güvenli şekilde yönetir.

---

## 🧰 Gereksinimler

- .NET SDK 8.0.412 (https://dotnet.microsoft.com/en-us/download/dotnet)
- MySQL 8.0 veya üzeri veritabanı  (https://dev.mysql.com/downloads/mysql/)
- Kod editörü (VS Code, Rider, nano, vim vb.) 
- Terminal veya komut satırı erişimi 

---

## ⚙️ Kurulum Adımları

1. **Projeyi klonla:**

```bash
git clone https://github.com/Erendnzr/Staj-2025
cd Staj-2025
```

2. **Veritabanı ayarlarını yap:**

`appsettings.json` dosyasında `ConnectionStrings` bölümünü kendi veritabanı bilgilerine göre düzenle.

3. **Veritabanını oluştur:**

```bash
dotnet ef database update
```
4. **Veritabanına sql dosyalarındaki tabloları ve tablo satırlarını girin:**
   
   kullanicilar.sql , veriler.sql , cihazlar.sql
   
5. **Projeyi çalıştır:**

```bash
dotnet run
```

Varsayılan olarak `localhost:5048` adresinden erişilebilir.

---

## 🔐 Kullanıcı Girişi ve Veri İzleme Paneli

- Kullanıcılar kullanıcı adı ve şifre ile giriş yapar. 
- Giriş başarılıysa veri izleme paneline yönlendirilir. 
- Veri izleme panelinde:
  - 📈 Son 10 Veri 
  - 🕒 Son 10 Dakika 
  - 🕐 Son 1 Saat 
  - 📄 Tüm Veriler 
  butonları ile veri görüntüleme sağlanır.

---

## 🌐 REST API Kullanımı

### Genel Bilgiler

- Tüm API çağrılarında header olarak **`x-api-key`** zorunludur.  
- Geçersiz veya eksik API key durumunda **401 Unauthorized** döner.

---

### API Endpoint

| Yöntem | Endpoint                  | Parametreler                                 | Açıklama                             |
|--------|---------------------------|---------------------------------------------|-------------------------------------|
| GET    | `/api/veriler/veriler`    | `tip`, `sayfa`, `baslangic`, `bitis`       | Veri listeleme, tip filtreli        |
| POST   | `/api/veriler/veri-ekle`  | JSON body                                   | Yeni veri ekleme                    |

---

### `GET /api/veriler/veriler`

#### Parametreler:

| Parametre  | Tür       | Açıklama                                   | Örnek          |
|------------|-----------|--------------------------------------------|----------------|
| tip        | string    | Veri tipi: `son10`, `son10dakika`, `son1saat`, `tumveriler` | `son10`        |
| sayfa      | int       | Sayfa numarası (sayfalama için)            | `1`            |
| baslangic  | datetime? | Başlangıç tarihi (sadece `tumveriler` için) | `2025-07-01`   |
| bitis      | datetime? | Bitiş tarihi (sadece `tumveriler` için)    | `2025-07-25`   |

---

#### Örnek cURL İstekleri:

- Son 10 veri:

```bash
curl -X GET "http://localhost:5048/api/veriler/veriler?tip=son10" -H "x-api-key: ApiKey123"
```

- Son 10 dakika:

```bash
curl -X GET "http://localhost:5048/api/veriler/veriler?tip=son10dakika" -H "x-api-key: ApiKey123"
```

- Son 1 saat:

```bash
curl -X GET "http://localhost:5048/api/veriler/veriler?tip=son1saat" -H "x-api-key: ApiKey123"
```

- Tüm veriler, tarih aralığı ve sayfa 1:

```bash
curl -X GET "http://localhost:5048/api/veriler/veriler?tip=tumveriler&baslangic=2025-07-01&bitis=2025-07-25&sayfa=1" -H "x-api-key: ApiKey123"
```

---

### `POST /api/veriler/veri-ekle`

Yeni veri eklemek için kullanılır.

#### İstek Gövdesi (JSON):

```json
{
  "CihazId": "cihaz1",
  "Deger": 100,
  "Zaman": "2025-07-24T10:55:00"
}
```

#### Örnek Post İsteği:

```import requests

url = "http://localhost:5048/api/veriler/veri-ekle"

headers = {
    "Content-Type": "application/json",
    "X-Api-Key": "ApiKey123"
}

data = {
    "CihazId": "Cihaz1",
    "Deger": 200,
    "Zaman": "2025-08-29T14:00:00"
}

response = requests.post(url, headers=headers, json=data)

print("Status Code:", response.status_code)
print("Response:", response.json())

```

---

## ⚠️ Hata Senaryoları ve Çözümleri

| Durum                     | Açıklama                              | Çözüm                                     |
|---------------------------|-------------------------------------|-------------------------------------------|
| 401 Unauthorized          | API Key yanlış veya eksik            | `x-api-key` header'ını kontrol et          |
| 400 Bad Request           | Veri eksik veya hatalı               | Gönderilen JSON yapısını kontrol et        |
| 404 Not Found             | Endpoint yanlış veya yok             | URL'yi doğru yazdığından emin ol           |
|MySQL Tabloları Eksik ya da Hatalı Yüklenmesi| Migrations dosyaları hatalı olabilir | Dosyaları silin ve (dotnet ef migrations add InitialCreate) kodunu girin girdikten sonra (dotnet ef database update) kodunu tekrar deneyin |

---

## 🛠️ Ek Notlar

- **Sayfalama:**  
  `GET /api/veriler/veriler` endpointinde `sayfa` parametresi ile sayfalama yapılır. 

- **Tarih Filtresi:**  
  `tip=tumveriler` seçildiğinde `baslangic` ve `bitis` parametreleriyle tarih aralığı filtrelenebilir.

- **Cihaz ID Kontrolü:**  
  Veri eklerken cihazın kayıtlı olması gerekir. Kayıtlı olmayan cihazlar için istek reddedilir.

---

## 📬 Destek ve İletişim

Her türlü soru ve destek için:  
[https://github.com/Erendnzr](https://github.com/Erendnzr)



