// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// // using UnityEngine.UI;
// using TMPro;
// using UnityEngine.UI;


// [System.Serializable]
// public class ProductWrapper
// {
//     public Product data;
// }


// public class FetchApiDetail : MonoBehaviour
// {
//     [Header("API URL")]
//     public string apiUrlTemplate = "http://localhost:8000/api/batiks/{id}";

//     [Header("UI References")]
//     // public Text nameText;
//     // public Text priceText;
//     // public Text descriptionText;
//     // public Text sizeText;
//     public TextMeshProUGUI nameText;
//     public TextMeshProUGUI priceText;
//     public TextMeshProUGUI descriptionText;
//     public TextMeshProUGUI sizeText;

//     public Image productImage;

//     void Start()
//     {
//         // Ambil ID produk dari PlayerPrefs (default ke 1 kalau belum ada)
//         int productId = PlayerPrefs.GetInt("selectedProductId", 1);

//         // Ganti {id} di URL dengan ID produk yang dipilih
//         string finalUrl = apiUrlTemplate.Replace("{id}", productId.ToString());

//         // Fetch data API
//         StartCoroutine(GetProductDetail(finalUrl));
//     }

//     IEnumerator GetProductDetail(string url)
//     {
//         UnityWebRequest request = UnityWebRequest.Get(url);
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             string json = request.downloadHandler.text;
//             // Product product = JsonUtility.FromJson<Product>(json);
//             ProductWrapper wrapper = JsonUtility.FromJson<ProductWrapper>(json);
//             Product product = wrapper.data;


//             // Isi UI
//             nameText.text = product.nama;
//             priceText.text = "Rp" + product.harga.ToString("N0");
//             descriptionText.text = product.deskripsi;
//             sizeText.text = product.size;

//             // Load gambar
//             StartCoroutine(LoadImage(product.gambar));
//         }
//         else
//         {
//             Debug.LogError("Error fetching product detail: " + request.error);
//         }
//     }

//     IEnumerator LoadImage(string imageUrl)
//     {
//         UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
//             Sprite sprite = Sprite.Create(texture,
//                                           new Rect(0, 0, texture.width, texture.height),
//                                           new Vector2(0.5f, 0.5f));
//             productImage.sprite = sprite;
//         }
//         else
//         {
//             Debug.LogError("Error loading image: " + request.error);
//         }
//     }
// }
