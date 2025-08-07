<?php

namespace Database\Seeders;

use Database\Factories\BatikFactory;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use App\Models\Batik;
use App\Models\BatikImage;

class BatikSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $batiks = [
            // ----------------------------------------------------------------------
            // =========================== MODEL 3D START ===========================
            // ----------------------------------------------------------------------
            [
                'name' => 'Kaos Kerah Lengan Pendek Pria',
                'deskripsi' => 'Cocok untuk tampil santai namun tetap elegan. Kaos batik tulis ini menggunakan motif khas dengan kerah rapi dan nyaman dikenakan. Ukuran all size yang pas untuk berbagai postur tubuh.',
                'harga' => 85000,
                'size' => 'ALL',
                'gambar' => ['KAOS_PENDEK.png'],
                // 'model3d' => 'nama_file_model_3d.gltf', // Contoh jika ada file 3D
            ],
            [
                'name' => 'Kemeja Pria Motif Kembang Waluh Hijau',
                'deskripsi' => 'Perpaduan warna hijau dan biru menciptakan kesan segar dan berkelas. Dibuat dengan teknik batik tulis yang detail dan eksklusif. Ideal untuk acara formal maupun kasual.',
                'harga' => 365000,
                'size' => 'ALL',
                'gambar' => ['KemejaHijau.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Kemeja Pria Motif Kembang Waluh Merah',
                'deskripsi' => 'Motif kembang waluh berwarna merah ini memberi kesan tegas dan maskulin. Ukuran L pas untuk pria dengan postur sedang. Cocok dipadukan dengan celana bahan atau jeans.',
                'harga' => 365000,
                'size' => 'L',
                'gambar' => ['KemejaMerah.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Kemeja Pria Motif Kembang Waluh Merah',
                'deskripsi' => 'Kesan mewah dan elegan dari motif batik tulis merah. Ukuran XL untuk kenyamanan ekstra. Sangat cocok dipakai ke acara resmi atau semi-formal.',
                'harga' => 365000,
                'size' => 'XL',
                'gambar' => ['KemejaMerah.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Kemeja Pria Motif Kembang Waluh Merah',
                'deskripsi' => 'Kemeja batik tulis dengan sentuhan warna merah cerah, tampil menonjol dan berkelas. Ukuran XXL nyaman untuk pria berpostur besar. Kualitas batik tulis asli, awet dan tidak luntur.',
                'harga' => 365000,
                'size' => 'XXL',
                'gambar' => ['KemejaMerah.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Baju Batik  Motif Kencana Ungu_Hitam',
                'deskripsi' => 'Desain khas batik Tuban yang anggun dan unik. Warna ungu gelap memberi nuansa misterius dan elegan. Cocok dipakai harian maupun acara keluarga.',
                'harga' => 83000,
                'size' => 'ALL',
                'gambar' => ['DressPanjang_LenganPendek_Hitam.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Baju Batik Motif Kencana Ungu_Oren',
                'deskripsi' => 'Warna oranye yang cerah memberi kesan ceria dan energik. Dibuat dari bahan adem dan menyerap keringat. Pilihan tepat untuk penampilan etnik kasual.',
                'harga' => 83000,
                'size' => 'ALL',
                'gambar' => ['DressPanjang_LenganPendek_Oren.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Batik Cap Remaja Abu',
                'deskripsi' => 'Gaya simpel dan modern dengan nuansa abu-abu netral. Nyaman dipakai oleh remaja untuk berbagai aktivitas. Motif cap tradisional yang cocok untuk kegiatan santai.',
                'harga' => 79000,
                'size' => 'ALL',
                'gambar' => ['DressPendek_abu.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Batik Cap Remaja Merah',
                'deskripsi' => 'Tampil ekspresif dengan warna merah berani. Potongan baju dirancang agar pas untuk remaja aktif. Motif batik cap memberi kesan etnik namun tetap kekinian.',
                'harga' => 79000,
                'size' => 'ALL',
                'gambar' => ['DressPendek_merah.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],
            [
                'name' => 'Gamis Batik',
                'deskripsi' => 'Gamis batik cap ini menghadirkan nuansa hangat dan lembut. Model longgar yang nyaman serta cocok dipakai di berbagai acara. Warna oranye membuat penampilan terlihat cerah dan bersahaja.',
                'harga' => 104000,
                'size' => 'ALL',
                'gambar' => ['gamis.png'],
                // 'model3d' => 'nama_file_model_3d.gltf',
            ],

            // ----------------------------------------------------------------------
            // =========================== MODEL 2D START ===========================
            // ----------------------------------------------------------------------
            [
                'name' => 'Kain Batik',
                'deskripsi' => 'Kain batik klasik dengan motif tradisional yang elegan. Cocok dijadikan bahan pakaian formal maupun kreasi fashion modern. Bahannya halus dan nyaman dikenakan.',
                'harga' => 250000,
                'size' => 'ALL',
                'gambar' => ['kainbatik.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Kain Batik Kembang Waluh',
                'deskripsi' => 'Motif kembang waluh yang unik memberi kesan anggun dan artistik. Sangat cocok untuk kain bawahan atau atasan semi-formal. Batik ini hadir dengan nuansa etnik yang kuat.',
                'harga' => 155000,
                'size' => 'ALL',
                'gambar' => ['kainbatik2.png', 'kainbatik2-kuning.png', 'kainbatik2-merah.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Outer Shaqu Batik',
                'deskripsi' => 'Outer dengan motif burung lokcan, melambangkan kebajikan dan kewibawaan. Dibuat dari katun batik berkualitas dengan pewarna sintetis. Ukuran all size yang fleksibel, cocok untuk gaya kasual maupun formal.',
                'harga' => 750000,
                'size' => 'ALL',
                'gambar' => ['outerShaquBatik.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Outer Kimono Batik',
                'deskripsi' => 'Desain kimono yang trendi berpadu dengan motif batik khas nusantara. Nyaman dipakai untuk berbagai aktivitas, baik indoor maupun outdoor. Pilihan tepat bagi pecinta gaya etnik modern.',
                'harga' => 750000,
                'size' => 'ALL',
                'gambar' => ['Outer Kimono Batik.png', 'outer kimono batik3.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Outer Tenun Sabu x Tenun Gedog',
                'deskripsi' => 'Perpaduan dua tenun tradisional: Sabu dan Gedog yang menampilkan tekstur khas dan warna etnik. Ukuran L cocok untuk siluet ramping dan menawan. Nilai artistik tinggi cocok untuk acara spesial.',
                'harga' => 950000,
                'size' => 'L',
                'gambar' => ['sabuXgedog.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Vest Batik Gedog',
                'deskripsi' => 'Vest batik bergaya kontemporer dengan sentuhan tradisional Gedog. Memberi kesan profesional dan elegan. Ideal dipadukan dengan kemeja polos untuk tampilan semi-formal.',
                'harga' => 850000,
                'size' => 'ALL',
                'gambar' => ['vestbatik1.png', 'vestbatik2.png', 'vestbatik3.png', 'vestbatik4.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Outer Batik',
                'deskripsi' => 'Outer model rompi modern dengan sentuhan batik. Nyaman digunakan sebagai pelapis outfit harian. Pilihan tepat untuk tampil beda namun tetap simpel.',
                'harga' => 800000,
                'size' => 'ALL',
                'gambar' => ['outerbatik1.png', 'outerbatik2.png', 'outerbatik3.png', 'outerbatik4.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Batik Gedog Sogo Pipit Natural',
                'deskripsi' => 'Fabric Type: Tenun. Pattern Name: BATIK SOGO PIPIT NATURAL. Batik eksklusif dengan motif burung lokcan yang menyimbolkan kelembutan dan keabadian. Ditenun dengan tangan menggunakan pewarna alami, memberi sentuhan ramah lingkungan. Ukuran 280x80 cm, cocok untuk kreasi busana premium.',
                'harga' => 3000000,
                'size' => 'ALL',
                'gambar' => ['batik gedog sogo pipit natural.png', 'batik gedog sogo pipit slide2.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Batik Gedog Burung Lokcan Sintetis',
                'deskripsi' => 'Motif burung lokcan memberi simbol kewibawaan dan kebaikan dalam balutan batik Gedog. Ukuran besar (80x280 cm), ideal untuk gamis atau kemeja pria. Menggunakan pewarna sintetis dengan daya tahan warna yang kuat.',
                'harga' => 2000000,
                'size' => 'ALL',
                'gambar' => ['Batik Gedog Sintetis Burung Lokcan.png'],
                // 'model3d' => null,
            ],
            [
                'name' => 'Sarung Bantal Batik',
                'deskripsi' => 'Sarung bantal modern dengan sentuhan tradisional dari motif batik',
                'harga' => 120000,
                'size' => 'XL',
                'gambar' => ['sarungBantal.png'],
                // 'model3d' => null,
            ],


        ];

        // foreach ($batiks as $batik) {
        //     BatikFactory::new()->create([
        //         'name' => $batik['name'],
        //         'deskripsi' => $batik['deskripsi'],
        //         'harga' => $batik['harga'],
        //         'size' => $batik['size'],
        //         'gambar' => $batik['gambar'],
        //         // 'model3d' => $batik['model3d'],
        //     ]);
        // }
        foreach ($batiks as $batikData) {
            $batik = Batik::create([
                'name' => $batikData['name'],
                'deskripsi' => $batikData['deskripsi'],
                'harga' => $batikData['harga'],
                'size' => $batikData['size'],
                // 'model3d' => $batikData['model3d'] ?? null,
            ]);

            foreach ($batikData['gambar'] as $img) {
                BatikImage::create([
                    'batik_id' => $batik->id,
                    'image_path' => $img,
                ]);
            }
        }
    }
}
