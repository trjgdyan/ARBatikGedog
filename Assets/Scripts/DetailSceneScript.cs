using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //btn back
    public void BackButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("KatalogScene");
    }

    //btn buy
    public void BuyButtonClick()
    {
        Application.OpenURL("https://wa.link/lzet26");
    }
}
