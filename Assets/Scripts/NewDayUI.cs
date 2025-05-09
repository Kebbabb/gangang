using UnityEngine;
using TMPro;

public class NewDayUI : MonoBehaviour
{
    public TMP_Text dayText;
    public GameObject newDayUI;
    public GameObject continueText;
    private bool canContinue = false;
    public TMP_Text gatheredPointsText;
    public TMP_Text highestStreakText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newDayUI.SetActive(false);
        continueText.SetActive(false);
    }
    public void Display(int dayNumber, int gatheredPoints, int highestStreak)
    {
        dayText.text = "Dag " + dayNumber.ToString();
        gatheredPointsText.text = "+" + gatheredPoints.ToString();
        float gatheredPointsTextWidth = gatheredPointsText.preferredWidth;
        gatheredPointsText.rectTransform.anchoredPosition = new Vector2(-gatheredPointsTextWidth / 2 + 25, gatheredPointsText.rectTransform.anchoredPosition.y);
        highestStreakText.text = "Højeste Streak: " + highestStreak.ToString();
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
