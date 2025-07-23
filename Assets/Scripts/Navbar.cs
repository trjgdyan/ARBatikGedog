using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navbar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void MenuCatalogButtonClick()
    {
        SceneManager.LoadScene("KatalogScene");
    }

    public void InformationButtonClick()
    {
        SceneManager.LoadScene("InformasiScene");
    }
}
