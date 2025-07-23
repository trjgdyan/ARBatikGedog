// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; // Tetap sertakan ini untuk TextMeshPro
// using System.Collections.Generic;

// [System.Serializable]
// public class Product
// {
//     public string nama;
//     public string harga;
//     public Sprite gambar; // Properti ini bisa tetap ada, tidak masalah
// }

// public class ProductLoaderStatis : MonoBehaviour
// {
//     public GameObject itemPrefab;
//     public Transform contentParent;

//     // public List<Sprite> gambarProduk; // <-- List gambar dinonaktifkan

//     void Start()
//     {
//         List<Product> products = new List<Product>()
//         {
//             new Product() { nama = "Kaos Hitam", harga = "Rp50.000" },
//             new Product() { nama = "Jaket Jeans", harga = "Rp120.000" },
//             new Product() { nama = "Sweater Abu", harga = "Rp80.000" },
//         };

//         foreach (Product p in products) // Kita bisa pakai foreach lagi karena tidak perlu indeks gambar
//         {
//             GameObject item = Instantiate(itemPrefab, contentParent);

//             // Baris untuk mengatur gambar dinonaktifkan
//             // item.transform.Find("ProductItemTemplate/Canvas/Image").GetComponent<Image>().sprite = p.gambar;
            
//             // Baris untuk mengatur nama dan harga tetap aktif
//             item.transform.Find("ProductItemTemplate/Canvas/Nama").GetComponent<TextMeshProUGUI>().text = p.nama;
//             item.transform.Find("ProductItemTemplate/Canvas/Harga").GetComponent<TextMeshProUGUI>().text = p.harga;
//         }
//     }
// }    