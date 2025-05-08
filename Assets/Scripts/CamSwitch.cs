using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CamSwitch : MonoBehaviour
{
    public CinemachineCamera mainVCam;
    public CinemachineCamera tableVCam;
    public CinemachineCamera mobileVCam;
    public GameObject Button;
    public GameObject PhoneScreen;


    public void SwitchToTable()
    {
        mobileVCam.Priority = 2;
        mainVCam.Priority = 5;
        tableVCam.Priority = 10;
    }
    public void SwitchToPhone()
    {
        mobileVCam.Priority = 10;
        mainVCam.Priority = 2;
        tableVCam.Priority = 5;
        
    }
    public void SwitchToGround()
    {
        mainVCam.Priority = 10;
        tableVCam.Priority = 5;
        mobileVCam.Priority = 2;
    }
    private void Start()
    {
        if (mainVCam!= null && tableVCam != null && mobileVCam != null)
        {
            mainVCam.Priority = 10;
            tableVCam.Priority = 5;
            mobileVCam.Priority = 2;

            if (Button != null)
            {
                Button.SetActive(false);
            }
            else
            {
                Debug.LogError("Button not assigned!");
            }
            if (PhoneScreen != null)
            {
                PhoneScreen.SetActive(false);
            }
            else
            {
                Debug.LogError("PhoneScreen not assigned!");
            }
        }
        else
        {
            Debug.LogError("Cameras not assigned!");
        }
    }
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("MainDesk")) // or check tag
                {
                    if (Button != null)
                    {
                        Button.SetActive(true);
                    }
                    if (PhoneScreen != null)
                    {
                        PhoneScreen.SetActive(true);
                    }
                    else
                    {
                        Debug.LogError("PhoneScreen not assigned!");
                    }
                    SwitchToTable();
                    
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent("PhoneP")) // or check tag
                {
                    SwitchToPhone();
                }
            }
        }

        if (Input.GetKeyDown("backspace"))// Left click
        {
            SwitchToGround();
            if (Button != null)
            {
                Button.SetActive(false);
            }
            if (PhoneScreen != null)
            {
                PhoneScreen.SetActive(false);
            }
            else
            {
                Debug.LogError("PhoneScreen not assigned!");
            }
        }
    }
}
