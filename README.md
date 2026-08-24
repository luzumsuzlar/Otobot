# Otobot

## Telegram bağlantısı

`Ayarlar > Telegram Rapor Ayarları` bölümünde BotFather tokeni maskeli alana girilir.
`SOHBETİ BUL VE KAYDET` düğmesi, bota daha önce `/start` gönderen son sohbeti otomatik
bulur. Token Windows kullanıcı hesabına bağlı DPAPI şifrelemesiyle yerel olarak saklanır;
Git deposuna veya uygulama paketine eklenmez. `TEST MESAJI GÖNDER` bağlantıyı doğrular.
Rapor penceresi tablodan seçilebilir. Otomatik rapor açıldığında belirlenen aralıkta bakiye
görüntüsü ile turnuva sıralamasındaki ilk 7 kişi tek bir Telegram görseli olarak gönderilir.

.NET 8 Windows Forms ile geliştirilmiş Chrome pencere izleme ve işlem otomasyonu.

## Kurulum ve güncelleme

Son kullanıcılar GitHub Releases sayfasındaki `Otobot-win-Setup.exe` dosyasını kullanarak
kurulum yapmalıdır. Bu kurulumla gelen sürüm, uygulama açıldığında yeni kararlı
sürümleri denetler ve kullanıcı onayıyla indirip kurar.

Geliştirici sürüm yayımlama adımları için `RELEASING.md` dosyasına bakın.

## Görsel tabanlı işlem düğmeleri

Pencereler sekmesindeki `GÖRSEL MODU` seçim kutusu işaretliyken Otobot, yenileme
düğmesini ve üç işlem düğmesini kaydedilen görsellerle bulur. Seçim kutusu
kapatıldığında yenileme ve işlemler eski koordinat sistemiyle çalışır. Son
kullanılan mod program kapatılıp açılsa da korunur.

### Görsel modu

1. Chrome pencerelerini tarayın.
2. `Yenileme Görselini Kaydet` düğmesine basın ve Chrome araç çubuğundaki
   yenileme simgesinin ortasına tıklayın.
3. `İşlem 1 Görselini Kaydet` düğmesine basın.
4. Chrome'daki hedef düğmenin ortasına bir kez tıklayın. Kayıt tıklaması gerçek
   web sayfasına gönderilmez.
5. Aynı işlemi İşlem 2 ve İşlem 3 için tekrarlayın.
6. Tablodan bir Chrome penceresi seçip `YENİLEME GÖRSELİNİ TEST ET` ve
   `İŞLEM GÖRSELLERİNİ TEST ET` düğmeleriyle eşleşme oranlarını tıklama yapmadan
   kontrol edin.
7. Gerekirse Ayarlar bölümündeki ayrı `Yenileme görseli eşik değeri` ve
   `İşlem görselleri eşik değeri` ayarlarını düzenleyin.

Görseller `%LOCALAPPDATA%\Otobot` içinde saklanır. Bir görsel bulunamazsa güvenlik
için tıklama yapılmaz. Uyarı, formun altındaki kırmızı bildirim alanında gösterilir;
modal pencere açılmaz ve otomatik tarama ayarlanan bekleme süresinden sonra sonraki
döngüyle devam eder. Yenileme görseli yalnızca Chrome penceresinin üst araç
çubuğunda aranır.

### Engellemeyen uyarılar

v4.18.4 ile hata ve uyarılar `MessageBox` yerine formun altındaki sabit bildirim
alanında kırmızı olarak gösterilir. Bu alan programın çalışmasını veya düğmelerin
kullanılmasını engellemez ve `TEMİZLE` düğmesiyle sıfırlanabilir. Görsel test
sonuçları da aynı alanda bilgi mesajı olarak gösterilir. Yalnızca bir güncellemeyi
indirip kurmak gibi kullanıcı onayı gerektiren işlemler soru penceresi açar.

### %33 sayfa yakınlaştırması

v4.18.3 ile görsel kaydı yapılırken fare hedef düğmeden otomatik olarak
uzaklaştırılır ve hover görünümünün kapanması beklenir. İşlem görseli alanı
%33 yakınlaştırmada küçük kalan düğmelere uygun olacak şekilde daraltılmıştır.
Eşleştirmede gri tonla birlikte kenar/şekil bilgisi de kullanılır.

Bu iyileştirmelerden yararlanmak için v4.18.3 kurulduktan sonra yenileme ve üç
işlem görselini, sayfa yakınlaştırması `%33` durumundayken yeniden kaydedin.
Önerilen başlangıç değerleri yenileme için `%72`, işlem görselleri için `%65`tir.
Önce test düğmeleriyle birkaç farklı Chrome penceresinde doğrulama yapın; yanlış
eşleşme görülürse ilgili eşik değerini yükseltin.

### Koordinat modu

1. `GÖRSEL MODU` seçim kutusunu kapatın.
2. Tablodan bir Chrome penceresi seçin.
3. Yenileme ile İşlem 1, 2 ve 3 koordinat kayıt düğmelerini kullanıp hedef
   noktalara tıklayın.
4. Her Chrome penceresi için koordinatları kaydedin veya `TÜM KOORDİNATLARI TOPLA`
   akışını kullanın.
5. F12 ile başlatıldığında Otobot görsel aramak yerine kaydedilen koordinatlara
   tıklar.

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
