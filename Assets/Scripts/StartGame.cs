using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject GuideUI;
    public GameObject StartUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        // Hide the guide UI at the start
        GuideUI.SetActive(false);
        StartUI.SetActive(true);

    }
    public void startgame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Beny");
    }
    public void Guide()
    {
        GuideUI.SetActive(true);
        StartUI.SetActive(false);
    }


}
