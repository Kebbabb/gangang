using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhonePostsManager : MonoBehaviour
{
    public int currentDay = 1;
    public List<Day> days = new List<Day>();
    public Phone phone;
    private Day currentDayObject;
    private int pointsPerPost;
    private int streakMultiplier = 1;
    private int streak = 0;
    private int highestStreak = 0;
    private int gatheredPoints = 0;
    public GameObject uiToHideOnScroll;
    public NewDayUI newDayUI;
    public TMP_Text newPointsText;
    public AudioSource correctAnswerSound;
    public AudioSource wrongAnswerSound;
    public CamSwitch camSwitch;


    // Viral count down
    public AudioSource countDownSound;
    private bool currentPostViral = false;
    private bool wrongAnswer = false;
    private float viralCountDown = 0;
    private float timeToViral = 0;
    public TMP_Text viralCountDownText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        viralCountDownText.gameObject.SetActive(false);
        newPointsText.gameObject.SetActive(false);
        // Get first day posts  
        if (days.Count > 0)
        {
            currentDayObject = days[currentDay - 1];
            pointsPerPost = currentDayObject.pointsPerPost;
            streakMultiplier = pointsPerPost/5;
            Debug.Log(streakMultiplier);
            Texture2D[] images = currentDayObject.getSourceImages();
            phone.sourceImages = images;
            phone.SetImagesToTexture();
            // Assuming you have a method to set the images to the phone screen
            // SetImagesToTexture(images);
        }
        else
        {
            Debug.LogError("No days available!");
        }
    }
    void Update()
    {
        newPointsText.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        if(currentPostViral){
            viralCountDown += Time.deltaTime;
            if(viralCountDown >= timeToViral){
                VerifyAnswer(wrongAnswer);
                // Handle viral post logic here
            } else {
                int secondsLeft = Mathf.CeilToInt(timeToViral - viralCountDown);
                viralCountDownText.text = secondsLeft.ToString() + " sekunder til viral!";
            }
        }
    }
    public void VerifyAnswer(bool answer){
        if(currentPostViral){
            currentPostViral = false;
            countDownSound.Stop();
            viralCountDown = 0;
            timeToViral = 0;
            viralCountDownText.gameObject.SetActive(false);
        }
        if(currentDayObject.posts[phone.scrollIndex].fakeNews == !answer) {
            Debug.Log("Correct Answer!");
            newPointsText.gameObject.SetActive(true);
            correctAnswerSound.Play();
            gatheredPoints += pointsPerPost + streak * streakMultiplier;
            streak++;
            if(streak > highestStreak) highestStreak = streak;
            newPointsText.text = "+" + (pointsPerPost + streak * streakMultiplier).ToString();
            // Handle correct answer logic here
        } else {
            Debug.Log("Incorrect Answer!");
            wrongAnswerSound.Play();
            streak = 0;
            // Handle incorrect answer logic here
        }
        uiToHideOnScroll.SetActive(false);
        if(phone.scrollIndex < currentDayObject.posts.Length - 1) {
            phone.Scroll(() => {
                if(currentDayObject.posts[phone.scrollIndex].viral) {
                    countDownSound.Play();
                    currentPostViral = true;
                    timeToViral = currentDayObject.posts[phone.scrollIndex].timeToViral;
                    wrongAnswer = currentDayObject.posts[phone.scrollIndex].fakeNews;
                    viralCountDown = 0;
                    currentPostViral = true;
                    viralCountDownText.gameObject.SetActive(true);
                }
                uiToHideOnScroll.SetActive(true);
                newPointsText.gameObject.SetActive(false);
        });
        } else {
            Debug.Log("End of posts for the day!");
            NewDay();
        }
    }
    void NewDay(){
        if (currentDay + 1 > days.Count) return; // No more days available

        Debug.Log("New Day: " + currentDay);
        currentDay++;
        currentDayObject = days[currentDay - 1];
        pointsPerPost = currentDayObject.pointsPerPost;
        streakMultiplier = pointsPerPost/5;
        streak = 0;
        int displayStreak = highestStreak;
        highestStreak = 0;
        camSwitch.SwitchToTable();
        Texture2D[] images = currentDayObject.getSourceImages();
        phone.sourceImages = images;
        StartCoroutine(WaitAndExecute(0.7f, () => {
            newDayUI.Display(currentDay, gatheredPoints, displayStreak);
            newPointsText.gameObject.SetActive(false);
            StartCoroutine(WaitFrame(() => {
                phone.SetImagesToTexture();
                newDayUI.CanContinue();
                uiToHideOnScroll.SetActive(true);
                
            }));
        }));
    }
    private IEnumerator WaitFrame(System.Action callback)
    {
        yield return null; // Waits one frame
        callback?.Invoke();
    }
    private IEnumerator WaitAndExecute(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);

        // Execute the callback after the delay
        callback?.Invoke();
    }

    // Update is called once per frame
}

[System.Serializable]
public class Day {
    public Post[] posts;
    public int pointsPerPost = 1;
    public Texture2D[] getSourceImages() {
        Texture2D[] images = new Texture2D[posts.Length];
        for (int i = 0; i < posts.Length; i++) {
            images[i] = posts[i].image;
        }
        return images;
    } // Assign in Inspector
}
[System.Serializable]
public class Post {
    public Texture2D image;
    public bool fakeNews;
    public bool viral;
    public float timeToViral = 20f; // Time to viral in seconds
}
