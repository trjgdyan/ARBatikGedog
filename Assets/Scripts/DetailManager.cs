using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DetailManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI namaProdukText;
    public TextMeshProUGUI hargaProdukText;
    public TextMeshProUGUI deskripsiProdukText;
    public GameObject image2DContainer;
    public RawImage gambarProdukRawImage;

    [Header("Image Slider")]
    public Button nextImageButton;
    public Button previousImageButton;
    
    [Header("3D Models")]
    public GameObject models3DContainer;
    public List<GameObject> allModels;

    [Header("Size Button Elements")]
    public GameObject sizeButtonPrefab;
    public Transform sizeButtonContainer;
    public Color selectedColor = new Color32(0x80, 0x19, 0x20, 0xFF);
    public Color deselectedColor = Color.white;

    [Header("Action Buttons")]
    public Button tryOnButton;

    private Product currentProduct;
    private int selectedIdForTryOn;
    private List<SizeButtonUI> spawnedSizeButtons = new List<SizeButtonUI>();
    
    private int currentImageIndex = 0;

    void Start()
    {
        currentProduct = ProductDataSharer.SelectedProduct;

        if (currentProduct != null)
        {
            DisplayProductDetails(currentProduct);
        }
        else
        {
            Debug.LogError("Tidak ada produk yang dipilih untuk ditampilkan!");
        }
    }

    void DisplayProductDetails(Product product)
    {
        // 1. Atur data teks
        namaProdukText.text = product.nama;
        hargaProdukText.text = "Rp " + product.harga.ToString("N0");
        deskripsiProdukText.text = product.deskripsi;
        
        // 2. Buat tombol ukuran secara dinamis
        GenerateSizeButtons(product.size);

        // 3. Tentukan apakah produk ini punya model 3D berdasarkan ID produk UTAMA
        bool has3DModel = product.id <= 10;
        
        // Atur tombol "Try On" berdasarkan ketersediaan model 3D
        if (tryOnButton != null)
        {
            tryOnButton.gameObject.SetActive(has3DModel);
        }

        // 4. Tampilkan visual yang sesuai
        if (has3DModel)
        {
            // TAMPILKAN MODEL 3D
            image2DContainer.SetActive(false);
            models3DContainer.SetActive(true);
            // Activate3DModel() akan dipanggil secara otomatis saat ukuran pertama dipilih
            
            // Jika punya model 3d maka button next dan previous image dihide (dinonaktifkan game objectnya)
            nextImageButton.gameObject.SetActive(false);
            previousImageButton.gameObject.SetActive(false);
            
        }
        else
        {
            // TAMPILKAN GAMBAR 2D
            models3DContainer.SetActive(false);
            image2DContainer.SetActive(true);
            
            // inisialisasi slider gambar
            currentImageIndex = 0;
            ShowCurrentImage();
            
            // Tampilkan atau sembunyikan tombol navigasi slider
            bool hasMultipleImages = product.gambar != null && product.gambar.Count > 1;
            nextImageButton.gameObject.SetActive(hasMultipleImages);
            previousImageButton.gameObject.SetActive(hasMultipleImages);
            
            
            
            // // Cek apakah list gambar ada dan tidak kosong
            // if (product.gambar != null && product.gambar.Count > 0)
            // {
            //     // Ambil gambar pertama dari list untuk ditampilkan
            //     StartCoroutine(LoadImageFromURL(product.gambar[0]));
            // }
        }
    }
    
    void ShowCurrentImage()
    {
        // Pastikan list gambar valid dan index tidak di luar jangkauan
        if (currentProduct.gambar != null && currentProduct.gambar.Count > 0)
        {
            if (currentImageIndex >= 0 && currentImageIndex < currentProduct.gambar.Count)
            {
                StartCoroutine(LoadImageFromURL(currentProduct.gambar[currentImageIndex]));
            }
        }
    }
    
    // --- FUNGSI BARU UNTUK TOMBOL NEXT ---
    public void OnNextImageClicked()
    {
        currentImageIndex++;
        // Jika sudah di gambar terakhir, kembali ke gambar pertama
        if (currentImageIndex >= currentProduct.gambar.Count)
        {
            currentImageIndex = 0;
        }
        ShowCurrentImage();
    }

    // --- FUNGSI BARU UNTUK TOMBOL PREVIOUS ---
    public void OnPreviousImageClicked()
    {
        currentImageIndex--;
        // Jika sudah di gambar pertama, pergi ke gambar terakhir
        if (currentImageIndex < 0)
        {
            currentImageIndex = currentProduct.gambar.Count - 1;
        }
        ShowCurrentImage();
    } 

    void GenerateSizeButtons(List<SizeInfo> sizes)
    {
        foreach (Transform child in sizeButtonContainer) Destroy(child.gameObject);
        spawnedSizeButtons.Clear();
        selectedIdForTryOn = 0;

        foreach (SizeInfo sizeInfo in sizes)
        {
            if (string.IsNullOrEmpty(sizeInfo.size)) continue;

            GameObject buttonObj = Instantiate(sizeButtonPrefab, sizeButtonContainer);
            SizeButtonUI buttonUI = buttonObj.GetComponent<SizeButtonUI>();
            buttonUI.Setup(sizeInfo, this);
            spawnedSizeButtons.Add(buttonUI);
        }

        // Otomatis pilih ukuran pertama jika tersedia
        if (spawnedSizeButtons.Count > 0)
        {
            OnSizeSelected(spawnedSizeButtons[0]);
        }
    }

    public void OnSizeSelected(SizeButtonUI clickedButton)
    {
        SizeInfo selectedSizeInfo = clickedButton.GetSizeInfo();
        
        // Simpan ID dari ukuran yang dipilih
        selectedIdForTryOn = selectedSizeInfo.id;
        Debug.Log($"Ukuran dipilih: {selectedSizeInfo.size} dengan ID: {selectedIdForTryOn}");

        foreach (SizeButtonUI button in spawnedSizeButtons)
        {
            button.SetSelected(button == clickedButton);
        }
        
        // Perbarui model 3D yang aktif setiap kali ukuran diganti
        Activate3DModel();
    }

    void Activate3DModel()
    {
        foreach (var model in allModels)
        {
            if (model != null) model.SetActive(false);
        }

        if (selectedIdForTryOn > 0 && selectedIdForTryOn <= allModels.Count)
        {
            if (allModels[selectedIdForTryOn - 1] != null)
            {
                allModels[selectedIdForTryOn - 1].SetActive(true);
            }
        }
    }

    public void BtnTryOnClick()
    {
        ProductDataSharer.SelectedTryOnID = selectedIdForTryOn;
        Debug.Log($"Tombol Try On diklik! Mengirim ID: {selectedIdForTryOn}");
        SceneManager.LoadScene("CalibrationScene");
    }

    IEnumerator LoadImageFromURL(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                gambarProdukRawImage.texture = texture;
            }
        }
    }
}