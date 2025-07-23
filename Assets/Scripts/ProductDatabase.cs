using System.Collections.Generic;
using UnityEngine;

// Atribut ini akan membuat menu di Unity Editor untuk membuat asset database ini.
[CreateAssetMenu(fileName = "ProductDatabase", menuName = "Katalog/Product Database")]
public class ProductDatabase : ScriptableObject
{
    // List yang akan berisi semua data produk kita
    public List<ProductData> daftarProduk;
}