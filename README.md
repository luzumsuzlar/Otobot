# Otobot

.NET 8 Windows Forms ile geliştirilmiş Chrome pencere izleme ve işlem otomasyonu.

## Kurulum ve güncelleme

Son kullanıcılar GitHub Releases sayfasındaki `Otobot-win-Setup.exe` dosyasını kullanarak
kurulum yapmalıdır. Bu kurulumla gelen sürüm, uygulama açıldığında yeni kararlı
sürümleri denetler ve kullanıcı onayıyla indirip kurar.

Geliştirici sürüm yayımlama adımları için `RELEASING.md` dosyasına bakın.

## Görsel tabanlı işlem düğmeleri

Otobot, üç işlem düğmesine sabit koordinatlarla tıklamak yerine kaydedilen
görselleri Chrome penceresi içinde arar ve yalnızca yeterli eşleşme bulunduğunda
düğmeye tıklar.

1. Chrome pencerelerini tarayın.
2. `İşlem 1 Görselini Kaydet` düğmesine basın.
3. Chrome'daki hedef düğmenin ortasına bir kez tıklayın. Kayıt tıklaması gerçek
   web sayfasına gönderilmez.
4. Aynı işlemi İşlem 2 ve İşlem 3 için tekrarlayın.
5. Tablodan bir Chrome penceresi seçip `İŞLEM GÖRSELLERİNİ TEST ET` düğmesiyle
   eşleşme oranlarını tıklama yapmadan kontrol edin.
6. Gerekirse Ayarlar bölümündeki `İşlem görseli eşik değeri`ni ayarlayın.

Görseller `%LOCALAPPDATA%\Otobot` içinde saklanır. Bir görsel bulunamazsa güvenlik
için tıklama yapılmaz ve tarama açıklayıcı bir hatayla durur. Yenileme konumu,
Chrome araç çubuğundaki düğme için mevcut koordinat sistemiyle çalışmaya devam
eder.

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
