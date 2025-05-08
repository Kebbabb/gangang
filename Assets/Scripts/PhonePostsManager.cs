using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonePostsManager : MonoBehaviour
{
    public int currentDay = 1;
    public List<Day> days = new List<Day>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Day {
    public Post[] posts;
}
[System.Serializable]
public class Post {
    public Texture2D image;
    public bool fakeNews;
}
