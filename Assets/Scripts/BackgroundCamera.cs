using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    // Variabel untuk menampung Renderer dari Quad kita
    public Renderer backgroundPlane;

    void Start()
    {
        // Cari perangkat kamera yang tersedia
        WebCamDevice[] devices = WebCamTexture.devices;

        // Jika tidak ada kamera ditemukan, tampilkan pesan error dan berhenti
        if (devices.Length == 0)
        {
            Debug.LogError("Tidak ada kamera yang ditemukan!");
            return;
        }

        // Inisialisasi WebCamTexture dengan kamera default
        WebCamTexture webcamTexture = new WebCamTexture();

        // Terapkan tekstur dari webcam ke material Quad kita
        // Ini akan "memproyeksikan" video ke Quad
        if (backgroundPlane != null)
        {
            backgroundPlane.material.mainTexture = webcamTexture;
        }

        // Mulai streaming dari webcam
        webcamTexture.Play();
    }
}