# Otobot çalışma düzeni

- Bu klasör Otobot'un ana ve güncel kaynak kodu olarak kullanılmalıdır.
- İstenen her değişiklik önce bu yerel Git deposunda uygulanmalıdır.
- Her değişiklikten sonra uygun derleme ve testler çalıştırılmalıdır.
- Tamamlanan değişiklikler `main` dalına commit edilip `origin` GitHub deposuna gönderilmelidir.
- Kullanıcıya dağıtılacak değişikliklerde proje sürümü artırılmalı, aynı sürüm etiketi gönderilmeli ve GitHub Actions sürümü doğrulanmalıdır.
- Yerel kurulum çıktıları bu deponun üst klasöründeki `Releases\v<version>` klasöründe tutulmalıdır; derleme çıktıları Git'e eklenmemelidir.
- GitHub Release içindeki `Otobot-win-Setup.exe` yerel sürüm klasörüne de indirilip indirme bütünlüğü doğrulanmalıdır.
