using UnityEngine;
using System.Collections.Generic; // Dibutuhkan untuk menggunakan List

public class CalibrationManager : MonoBehaviour
{
    // Hubungkan semua model 3D Anda ke list ini melalui Inspector
    public List<GameObject> allModels;

void Start()
    {
        foreach (GameObject model in allModels) model.SetActive(false);

        // Langsung ambil ID final dari sharer
        int tryOnID = ProductDataSharer.SelectedTryOnID;

        Debug.Log($"Calibration scene menerima ID untuk dicoba: {tryOnID}");
        
        if (tryOnID > 0 && tryOnID <= allModels.Count)
        {
            // Aktifkan model berdasarkan ID yang diterima
            allModels[tryOnID - 1].SetActive(true);
            Debug.Log($"Model untuk ID {tryOnID} telah diaktifkan.");
        }
        else
        {
            Debug.LogWarning($"ID {tryOnID} tidak valid atau tidak ada model yang cocok.");
        }
    }

    //button back to detail product dengan id saat ini
    public void CloseBtnOnClick()
    {
        // unplay CalibrationScene lalu kembali ke DetailProductScene
        UnityEngine.SceneManagement.SceneManager.LoadScene("DetailProductScene");
        Debug.Log("btn Close diklik, kembali ke scene DetailProductScene.");
    }
}