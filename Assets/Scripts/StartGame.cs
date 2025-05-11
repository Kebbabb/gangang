using Unity.VisualScripting;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject IntroUI;
    public GameObject GuideUI;
    public GameObject StartUI;
    public GameObject G1;
    public GameObject G2;
    public GameObject G3;
    public GameObject G4;
    public GameObject G5;
    public GameObject G6;
    public GameObject G7;
    public GameObject G8;
    public GameObject G9;
    public GameObject G10;
    public GameObject G11;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        // Hide the guide UI at the start
        GuideUI.SetActive(false);
        StartUI.SetActive(true);
        IntroUI.SetActive(false);
        
        G2.SetActive(false);
        G3.SetActive(false);
        G4.SetActive(false);
        G5.SetActive(false);
        G6.SetActive(false);
        G7.SetActive(false);
        G8.SetActive(false);
            G9.SetActive(false);
        G10.SetActive(false);
        G11.SetActive(false);
    }
    public void startgame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ben ekstra");
    }
    public void Guide()
    {
        GuideUI.SetActive(true);
        IntroUI.SetActive(false);
        StartUI.SetActive(false);
    }
    public void intro()
    {
        IntroUI.SetActive(true);
        StartUI.SetActive(false);
        GuideUI.SetActive(false);
    }
    public void G1G()
    {
        G1.SetActive(true);
    }
    public void G2G()
    {
        G2.SetActive(true);
        G1.SetActive(false);
    }
    public void G3G()
    {
        G3.SetActive(true);
        G2.SetActive(false);
    }
    public void G4G()
    {
        G4.SetActive(true);
        G3.SetActive(false);
    }
    public void G5G()
    {
        G5.SetActive(true);
        G4.SetActive(false);
    }
    public void G6G()
    {
        G6.SetActive(true);
        G5.SetActive(false);
    }
    public void G7G()
    {
        G7.SetActive(true);
        G6.SetActive(false);
    }
    public void G8G()
    {
        G8.SetActive(true);
        G7.SetActive(false);
    }
    public void G9G()
    {
        G9.SetActive(true);
        G8.SetActive(false);
    }
    public void G10G()
    {
        G10.SetActive(true);
        G9.SetActive(false);
    }
    public void G11G()
    {
        G10.SetActive(false);
        G11.SetActive(true);
    }
}
