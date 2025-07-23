// File: ProductItemUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProductItemUI : MonoBehaviour
{
    public Image gambarUI;
    public TextMeshProUGUI namaUI;
    public TextMeshProUGUI hargaUI;

    //variabel untuk menyimpan data produk seecaa sp
    private Product currentProduct;

    // UBAH parameter dari 'ProductData' menjadi 'Product'
    public void Setup(Product data)
    {
        currentProduct = data; // Simpan data produk untuk referensi nanti

        // Gunakan nama variabel dari class Product
        namaUI.text = data.nama;
        hargaUI.text = "Rp " + data.harga.ToString("N0");

        // Ganti 'urlGambar' menjadi 'gambar' (sesuai class Product)
        // if (!string.IsNullOrEmpty(data.gambar))
        // {
        //     StartCoroutine(LoadImageFromURL(data.gambar));
        // }
        
        // --- LOGIKA BARU UNTUK MENGAMBIL GAMBAR PERTAMA ---
        
        // Cek apakah list 'gambar' ada dan tidak kosong
        if (data.gambar != null && data.gambar.Count > 0)
        {
            // Ambil HANYA URL gambar pertama dari list (indeks ke-0)
            string firstImageUrl = data.gambar[0];
            
            // Jalankan coroutine untuk memuat gambar pertama tersebut
            if (!string.IsNullOrEmpty(firstImageUrl))
            {
                StartCoroutine(LoadImageFromURL(firstImageUrl));
            }
        }
        else
        {
            // Opsional: Atur gambar default jika produk tidak punya gambar sama sekali
            Debug.LogWarning($"Produk '{data.nama}' tidak memiliki gambar.");
        }
    }

    //produk on click fungsi
    public void OnProductClick()
    {
        // 1. menyimpan data produk yang dipilih ke 'data sharer'
        ProductDataSharer.SelectedProduct = currentProduct;
        //log untuk debugging klik produk
        Debug.Log($"Produk dipilih: {currentProduct.nama}, Harga: {currentProduct.harga}, Id: {currentProduct.id}");
        
        SceneManager.LoadScene("DetailProductScene");
    }

    IEnumerator LoadImageFromURL(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                gambarUI.sprite = sprite;
            }
            else
            {
                // Debug.LogError("Gagal memuat gambar dari URL: " + url + "\nError: " + webRequest.error);
                Debug.LogError("Gagal memuat gambar dari URL: " + url + 
                   "\nError: " + webRequest.error +
                   "\nServer Response: " + webRequest.downloadHandler.text); // <-- TAMBAHKAN INI
            }
        }
    }
}