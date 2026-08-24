# Otobot

.NET 8 Windows Forms ile geliştirilmiş Chrome pencere izleme ve işlem otomasyonu.

## Kurulum ve güncelleme

Son kullanıcılar GitHub Releases sayfasındaki `Otobot-win-Setup.exe` dosyasını kullanarak
kurulum yapmalıdır. Bu kurulumla gelen sürüm, uygulama açıldığında yeni kararlı
sürümleri denetler ve kullanıcı onayıyla indirip kurar.

Geliştirici sürüm yayımlama adımları için `RELEASING.md` dosyasına bakın.

Bu sürümde iki sorun birlikte düzeltildi:

1. Pencereler sekmesindeki mevcut butonların kaybolması düzeltildi.
2. Süreler artık gerçekten ayarlanabilir ve kodun sabit değerleri yerine bu
   ayarlar kullanılıyor.

Ayarlar sekmesinde:
- Sayfa yenileme sonrası bekleme: 1-600 saniye (varsayılan 30)
- Tarama döngüleri arasındaki bekleme: 1-600 saniye (varsayılan 60)
- İşlem tıklamaları arasındaki bekleme: 50-10000 ms (varsayılan 500)

Değerler `timing_settings.json` içine kaydedilir ve program açıldığında yüklenir.

Önemli:
- Pencereler sekmesindeki butonlar geri getirildi.
- Yenileme ve hata tarama akışı korunuyor.
- URL sistemi korunuyor.
- Tek işlem koordinatı değiştirme korunuyor.
