using UnityEngine;
using UnityEngine.SceneManagement;

public class ProductButton : MonoBehaviour
{
    public int productId; // Set ID produk dari Inspector

    // Fungsi ini dipanggil ketika tombol produk diklik
    public void OnProductClicked()
    {
        // Simpan ID produk yang dipilih ke PlayerPrefs
        PlayerPrefs.SetInt("selectedProductId", productId);

        // Pindah ke scene detailgl
        SceneManager.LoadScene("DetailProductScene");
    }
}
