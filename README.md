# Restaurant Ordering System - QR Kod Tabanlı Sipariş Sistemi

Blazor Server (.NET 8.0) tabanlı, QR kod ile masa bazlı sipariş sistemi. Restoran müşterileri masalarındaki QR kodları tarayarak menüyü görüntüleyebilir ve sipariş verebilirler.

## 🚀 Özellikler

### Müşteri Özellikleri
- **QR Kod Tabanlı Sipariş**: Her masa için benzersiz QR kod
- **Mobil Uyumlu Menü**: Responsive tasarım ile mobil cihazlarda mükemmel görünüm
- **Gerçek Zamanlı Sepet**: Dinamik sepet yönetimi
- **Kolay Sipariş**: Tek tıkla sipariş verme
- **Türkçe Dil Desteği**: Tamamen Türkçe arayüz

### Admin Paneli
- **Dashboard**: Günlük satış, bekleyen siparişler ve genel istatistikler
- **Menü Yönetimi**: Menü kategorileri ve ürünleri düzenleme
- **Sipariş Takibi**: Siparişlerin durumunu güncelleme
- **Masa Yönetimi**: QR kod oluşturma ve masa düzenleme
- **Güvenli Giriş**: ASP.NET Core Identity ile korumalı admin paneli

### Teknik Özellikler
- **Blazor Server**: .NET 8.0 ile modern web uygulaması
- **Entity Framework Core**: Code-first yaklaşım ile veritabanı yönetimi
- **SQL Server**: Güvenilir veritabanı çözümü
- **QR Kod Üretimi**: QRCoder kütüphanesi ile dinamik QR kod oluşturma
- **Tailwind CSS**: Modern ve responsive tasarım
- **Serilog**: Kapsamlı log sistemi

## 📋 Gereksinimler

- .NET 8.0 SDK
- SQL Server (Express veya üzeri)
- Visual Studio 2022 veya VS Code

## 🛠️ Kurulum

### 1. Projeyi Klonlayın veya İndirin

### 2. Veritabanı Bağlantısını Yapılandırın

`appsettings.json` dosyasındaki bağlantı dizesini kendi SQL Server bilgilerinize göre güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SA\\SQLEXPRESS;Initial Catalog=personeldb;User ID=sa;Password=Ma363210;TrustServerCertificate=true"
  }
}
```

### 3. Projeyi Çalıştırın

```bash
dotnet restore
dotnet build
dotnet run
```

Uygulama otomatik olarak veritabanını oluşturacak ve örnek verilerle dolduracaktır.

## 🌐 Erişim

- **Ana Sayfa**: http://localhost:5010
- **Menü Sayfası**: http://localhost:5010/menu?table=1 (Masa 1 için)
- **Admin Paneli**: http://localhost:5010/admin

### Varsayılan Admin Hesabı
- **Email**: admin@restaurant.com
- **Şifre**: Admin123!

## 📱 Kullanım

### Müşteriler İçin
1. Masanızdaki QR kodu telefonunuzla tarayın
2. Açılan menüden istediğiniz ürünleri seçin
3. Sepetinizi kontrol edin ve siparişinizi verin

### Admin Paneli
1. `/admin` adresine gidin
2. Admin hesabı ile giriş yapın
3. Dashboard'dan günlük istatistikleri görün
4. Menü, masa ve siparişleri yönetin

## 🗂️ Proje Yapısı

```
RestaurantOrdering/
├── Components/
│   ├── Pages/
│   │   ├── Menu.razor          # Müşteri menü sayfası
│   │   └── Admin/              # Admin paneli sayfaları
│   └── Layout/                 # Layout bileşenleri
├── Data/
│   └── ApplicationDbContext.cs # Veritabanı context
├── Models/                     # Veri modelleri
│   ├── Menu.cs
│   ├── MenuItem.cs
│   ├── Order.cs
│   └── Table.cs
├── Repositories/               # Veri erişim katmanı
├── Services/
│   └── QrCodeService.cs       # QR kod üretimi
└── wwwroot/                   # Statik dosyalar
```

## 🔧 Yapılandırma

### QR Kod URL'si

`appsettings.json` dosyasında `BaseUrl` değerini kendi domain adresinizle güncelleyin:

```json
{
  "BaseUrl": "https://yourdomain.com"
}
```

### Loglama

Loglar `logs/` klasöründe günlük dosyalar halinde saklanır:
- `restaurant-20250821.txt`

## 🎯 Özellik Geliştirmeleri

Bu proje aşağıdaki ek özelliklerle geliştirilebilir:

- **Ödeme Entegrasyonu**: Kredi kartı ve mobil ödeme desteği
- **Bildirim Sistemi**: Sipariş durumu için push bildirimleri
- **Çoklu Dil Desteği**: İngilizce ve diğer dil seçenekleri
- **Raporlama**: Detaylı satış ve performans raporları
- **Garson Uygulaması**: Garsonlar için mobil uygulama
- **Mutfak Ekranı**: Siparişleri görüntülemek için mutfak ekranı

## 🤝 Katkıda Bulunma

1. Bu repository'yi fork edin
2. Yeni bir feature branch oluşturun (`git checkout -b feature/YeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik: XYZ'`)
4. Branch'inizi push edin (`git push origin feature/YeniOzellik`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 🆘 Destek

Sorun yaşarsanız:

1. Issues bölümünde mevcut sorunları kontrol edin
2. Yeni bir issue oluşturun
3. Gerekli log dosyalarını ekleyin

---

**Restaurant Ordering System** ile modern, QR kod tabanlı sipariş deneyimi yaşayın! 🍽️
