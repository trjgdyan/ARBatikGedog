        using UnityEngine;
        using UnityEngine.UI; // Diperlukan untuk mengakses komponen RawImage

        public class WebcamBackground : MonoBehaviour
        {
        // Buat slot di Inspector untuk memasukkan RawImage dari scene
        public RawImage background;
        
        private WebCamTexture webCamTexture;

        void Start()
        {
                // Inisialisasi WebCamTexture
                webCamTexture = new WebCamTexture();

                // Cek jika RawImage sudah di-assign
                if (background != null)
                {
                        // Terapkan tekstur dari webcam ke RawImage
                        background.texture = webCamTexture;
                        background.material.mainTexture = webCamTexture; // Pastikan material juga diupdate

                        // mengatur agar kamera mirror
                        // background.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                        Debug.LogError("RawImage background belum di-assign di Inspector!");
                        return;
                }

                // Mulai jalankan kamera
                webCamTexture.Play();
                Debug.Log("Webcam started!");
        }
        
        public void StopWebcam()
        {
                if (webCamTexture != null && webCamTexture.isPlaying)
                {
                        webCamTexture.Stop();
                        Debug.Log("Webcam manually stopped.");
                }
        }

        }