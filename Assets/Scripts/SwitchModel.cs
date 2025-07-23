using UnityEngine;

public class SwitchModel : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToEnable;

    public void SwitchActiveObjects()
    {
        // Nonaktifkan objectToDisable (batik1)
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
            Debug.Log(objectToDisable.name + " dinonaktifkan.");
        }
        else
        {
            Debug.LogWarning("Object untuk dinonaktifkan belum di-assign!");
        }

        // Aktifkan objectToEnable (Yglobal)
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log(objectToEnable.name + " diaktifkan.");
        }
        else
        {
            Debug.LogWarning("Object untuk diaktifkan belum di-assign!");
        }
    }
}