public static class ProductDataSharer
{
    // Untuk mengirim data produk utama ke Detail Scene
    public static Product SelectedProduct { get; set; }

    // Untuk mengirim ID spesifik (dari ukuran) ke Calibration Scene
    public static int SelectedTryOnID { get; set; } 
}