// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// [System.Serializable]
// public class ProductData
// {
//     public List<Product> data; // wrapper untuk JSON { "data": [...] }
// }

// public class ProductFetcher : MonoBehaviour
// {
//     public string apiUrl = "http://localhost:8000/api/batiks";

//     void Start()
//     {
//         StartCoroutine(GetProducts());
//     }

//     IEnumerator GetProducts()
//     {
//         UnityWebRequest request = UnityWebRequest.Get(apiUrl);
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             string json = request.downloadHandler.text;

//             ProductData productData = JsonUtility.FromJson<ProductData>(json);

//             foreach (Product p in productData.data)
//             {
//                 Debug.Log($"[ALL] Nama: {p.nama}, Harga: {p.harga}, Ukuran: {p.size}");
//             }
//         }
//         else
//         {
//             Debug.LogError("Error fetching all products: " + request.error);
//         }
//     }
// }
