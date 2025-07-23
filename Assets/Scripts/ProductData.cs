using UnityEngine;

// [System.Serializable] membuat class ini bisa dilihat dan di-edit di Inspector Unity.
[System.Serializable]
public class ProductData
{
    public string namaProduk;
    public string deskripsi;
    public int harga;
    // public Sprite gambarProduk; // Tipe data Sprite untuk gambar di UI
    public string urlGambar;
    // public string size;
    public string[] size; // Assuming size is an array of strings
    public int id; // Assuming you have an ID field in your Product class
}