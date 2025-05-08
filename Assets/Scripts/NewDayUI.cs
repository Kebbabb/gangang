using UnityEngine;
using TMPro;

public class NewDayUI : MonoBehaviour
{
    public TMP_Text dayText;
    public GameObject newDayUI;
    public GameObject continueText;
    private bool canContinue = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newDayUI.SetActive(false);
        continueText.SetActive(false);
    }
    public void Display(int dayNumber)
    {
        dayText.text = "Day " + dayNumber.ToString();
        newDayUI.SetActive(true);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canContinue){
            newDayUI.SetActive(false);
            continueText.SetActive(false);
        }
    }
    public void CanContinue()
    {
        canContinue = true;
        continueText.SetActive(true);
    }
}
