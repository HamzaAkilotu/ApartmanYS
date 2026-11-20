# Apartman Yönetim Sistemi

C# ve DevExpress ile geliştirilen bir Windows Forms uygulaması. Yönetici, dört kattaki on iki dairenin aidat durumunu tek ekrandan takip edebilir. Yeşil kart ödenmiş, sarı kart bekleyen, kırmızı kart gecikmiş aidatı gösterir. Kart seçildiğinde dairenin detayları açılır.

| İşlem | İlgili kod |
| --- | --- |
| Oturum açma | [Giriş formu](ApartmanYS.Forms/Forms/FrmLogin.cs) |
| Daireleri ve ödeme durumunu görüntüleme | [Ana ekran](ApartmanYS.Forms/Forms/FrmAnaEkran.cs) |
| Aidat, duyuru ve toplantı yönetimi | [Yönetici paneli](ApartmanYS.Forms/Forms/FrmAdminPanel.cs) |

## Çalıştırma

1. Windows üzerinde Visual Studio ile [ApartmanYS.sln](ApartmanYS.sln) dosyasını aç. Proje .NET Framework 4.8, DevExpress 25.1 ve SQL Server kullanır.
2. [App.config](ApartmanYS.Forms/App.config) içindeki `ApartmanYSConnection` bağlantısını proje şemasına sahip bir geliştirme veritabanına yönlendir.
3. Çözümü derleyip F5 ile başlat.

[Kullanım kılavuzunda](KULLANIM.txt) ekran davranışları ve örnek oturumlar açıklanır. Kılavuzda adı geçen `Veritabani.sql` betiği bu sürümde depoda bulunmuyor.
