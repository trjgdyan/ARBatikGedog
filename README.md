# AR BATIK GEDOG TUBAN

---

## Langkah-langkah menjalankan project

1. Clone Repository `ARBatikGedog`

### A. Menjalankan Frontend di Unity

1. Buka Project `ARBatikGedog` yang sudah di clone di Unity Hub
2. Untuk menjalankan project gunakan versi Unity Editor 2021.3.24f1
3. Untuk menjalankan project dapat dimulai dari scene `Welcome`.

### B. Menjalankan Backend Laravel

1. Buka folder `ar-api` didalam project yang sudah di clone. `ar-api` merupakan folder untuk backend laravel yang mengelola informasi produk
2. Jalankan project laravel melalui langkah-langkah berikut: \
     - Composser update \
     - copy `.env.example` menjadi `.env` \
     - Generate key dengan perintah `php artisan key:generate` \
     - Lalu migrate ulang dengan `php artisan migrate` \
     - Terakhir jalankan project dengan perintah `php artisan serve`. Pastikan sudah mengaktifkan database MySQL.

## Cara Menggunakan Aplikasi Katalog Produk UMKM Batik Gedog Tuban dengan fitur Virtual Try-on

1. Setelah clone project, Anda dapat membuka folder `Output`
2. Untuk menjalankan aplikasi, Anda dapat membuka file `AplikasiKatalogBatikGedog.exe` dan akan terbuka halaman Welcome
3. Kemudian Anda dapat memilih menu `Catalog` untuk melihat seluruh produk.
4. Klik salah satu produk di halaman katalog untuk melihat detail produk di halaman detail produk.
5. Pada halaman `Detail Produk` terdapat button `VTO/Try-on` yang akan muncul jika produk memiliki model 3D untuk digunakan VTO
6. Klik button `VTO` untuk mencoba fitur Virtual Try-on.
7. Untuk mencoba produk, berdirilah kurang lebih (±) 1 meter dari kamera.
8. Setelah pose Anda terdeteksi, lakukanlah kalibrasi selama 10 detik dengan klik key `C`. Pada saat kalibrasi gunakanlah T pose.
9. Setelah 10 detik, anda dapat mencoba produk secara virtual.

*Selamat Mencoba*😊 

![Demo GIF](https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExaHF2cnd0dXQwbWppY29xbXR6a3loeXhzdWJ5MXgyYTd5Z3p1bzBmcCZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/11sBLVxNs7v6WA/giphy.gif)

