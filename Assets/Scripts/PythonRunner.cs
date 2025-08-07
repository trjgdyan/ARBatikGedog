using System.Diagnostics;
using System.IO;
using UnityEngine;

public class PythonRunner : MonoBehaviour
{
    public void RunPythonScript()
    {
        string batPath = Path.Combine(Application.streamingAssetsPath, "Running_mediapipe.bat");

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = batPath;
        psi.UseShellExecute = true;
        psi.CreateNoWindow = false;

        try
        {
            Process.Start(psi);
            UnityEngine.Debug.Log("File .bat berhasil dijalankan.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Gagal menjalankan file .bat: " + ex.Message);
        }
    }

    public void StopPythonScript()
    {
        foreach (var process in Process.GetProcessesByName("python"))
        {
            try
            {
                process.Kill();
                process.WaitForExit();                
                UnityEngine.Debug.Log("Proses python berhasil dihentikan.");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError("Gagal menghentikan proses python: " + ex.Message);
            }
        }
    }
}

