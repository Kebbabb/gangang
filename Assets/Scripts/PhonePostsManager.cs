using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonePostsManager : MonoBehaviour
{
    public int currentDay = 1;
    public List<Day> days = new List<Day>();
    public Phone phone;
    private Day currentDayObject;
    public GameObject uiToHideOnScroll;
    public NewDayUI newDayUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get first day posts  
        if (days.Count > 0)
        {
            currentDayObject = days[currentDay - 1];
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
    public void VerifyAnswer(bool answer){
        if(currentDayObject.posts[phone.scrollIndex].fakeNews == !answer) {
            Debug.Log("Correct Answer!");
            // Handle correct answer logic here
        } else {
            Debug.Log("Incorrect Answer!");
            // Handle incorrect answer logic here
        }
        uiToHideOnScroll.SetActive(false);
        if(phone.scrollIndex < currentDayObject.posts.Length - 1) {
            phone.Scroll(() => {
            uiToHideOnScroll.SetActive(true);
        });
        } else {
            Debug.Log("End of posts for the day!");
            NewDay();
        }
    }
    void NewDay(){
        if (currentDay + 1 > days.Count) return; // No more days available

        Debug.Log("New Day: " + currentDay);
        newDayUI.Display(currentDay);
        currentDay++;
        
        currentDayObject = days[currentDay - 1];
        Texture2D[] images = currentDayObject.getSourceImages();
        phone.sourceImages = images;
        StartCoroutine(WaitFrame(() => {
            phone.SetImagesToTexture();
            newDayUI.CanContinue();
            uiToHideOnScroll.SetActive(true);
        }));
    }
    private IEnumerator WaitFrame(System.Action callback)
    {
        yield return null; // Waits one frame
        callback?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Day {
    public Post[] posts;
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
}
