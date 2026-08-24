# Chrome11Bot sürüm yayımlama

Kullanıcılar uygulamayı ilk kez GitHub Releases sayfasındaki `Setup.exe` ile
kurmalıdır. Velopack ile kurulan uygulamalar sonraki kararlı sürümleri açılışta
denetler ve kullanıcı onayıyla otomatik olarak yükler.

## Yeni sürüm yayımlama

1. `Chrome11Bot.csproj` içindeki `<Version>` değerini artırın.
2. Değişiklikleri test edip `main` dalına gönderin.
3. Aynı sürüm numarasıyla etiket oluşturup gönderin:

   ```powershell
   git tag v4.18.0
   git push origin v4.18.0
   ```

GitHub Actions Windows uygulamasını self-contained olarak derler, Velopack
installer ve güncelleme paketlerini üretir, ardından GitHub Release olarak
yayımlar. Alternatif olarak GitHub'daki Actions sayfasından iş akışını elle
çalıştırıp sürüm numarası girebilirsiniz.

## Önemli

- Uygulamadaki güncelleme kaynağı `https://github.com/luzumsuzlar/Chrome11Bot`
  adresidir.
- Jeton gömülmediği için son kullanıcı güncellemelerinin çalışması adına bu
  depo veya güncelleme paketlerinin bulunduğu depo herkese açık olmalıdır.
- Kurulum dosyalarını kod imzalama sertifikasıyla imzalamak Windows SmartScreen
  uyarılarını azaltır. Sertifika edinildiğinde workflow'a imzalama adımı ekleyin.
