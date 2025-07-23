using System.Collections.Generic;

[System.Serializable]
public class Product
{
    public int id;
    public string nama;
    public string deskripsi;
    public int harga;
    public List<SizeInfo> size; // Diubah ke List<SizeInfo>
    // public string gambar;
    public List<string> gambar;
    public string model3d;
}