using UnityEngine;

public class REALController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject targetObject;
    public GameObject targetObject2;

    public void ShowTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Enable the object
            targetObject2.SetActive(false); // Disable the second object
        }
    }
}
