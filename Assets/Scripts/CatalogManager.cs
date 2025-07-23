// File: CatalogManager.cs
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic; // Dibutuhkan untuk List

public class CatalogManager : MonoBehaviour
{
    public string apiUrl = "http://localhost:8000/api/batiks"; 

    public GameObject productItemPrefab;
    public Transform catalogContainer;

    void Start()
    {
        StartCoroutine(FetchProductsFromAPI());
    }

    IEnumerator FetchProductsFromAPI()
    {
        Debug.Log("Mengambil data dari API...");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = webRequest.downloadHandler.text;
            ProductListData productList = JsonUtility.FromJson<ProductListData>(jsonResponse);
            
            GenerateCatalog(productList.data);

            Debug.Log($"Berhasil mengambil {productList.data.Count} produk dari API.");
            foreach (var product in productList.data)
            {
                // Perbaiki cara menampilkan log untuk size
                string sizesString = "";
                foreach(var sizeInfo in product.size)
                {
                    sizesString += $"{sizeInfo.size} (ID: {sizeInfo.id}), ";
                }
                // list gambar produk
                sizesString = sizesString.TrimEnd(',', ' '); // Hapus koma terakhir
                
                Debug.Log($"ID Produk: {product.id}, Nama: {product.nama}, Ukuran Tersedia: {sizesString}. Gambar Produk: {string.Join(", ", product.gambar)}");
            }
        }
            else
            {
                Debug.LogError("Gagal fetch data: " + webRequest.error);
            }
        }
    }

    // Ubah parameter menjadi List<Product>
    void GenerateCatalog(List<Product> products)
    {
        foreach (Transform child in catalogContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var productData in products)
        {
            GameObject itemObject = Instantiate(productItemPrefab, catalogContainer);
            ProductItemUI itemUI = itemObject.GetComponent<ProductItemUI>();
            if (itemUI != null)
            {
                // Kirim data 'Product' ke UI item
                itemUI.Setup(productData);
            }
        }
    }
}