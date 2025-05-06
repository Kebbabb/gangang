using Unity.Cinemachine;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public CinemachineCamera mainVCam;
    public CinemachineCamera phoneVCam;

    public void SwitchToPhone()
    {
        mainVCam.Priority = 5;
        phoneVCam.Priority = 10;
    }
    private void Start()
    {
        if (mainVCam!= null && phoneVCam != null)
        {
            mainVCam.Priority = 10;
            phoneVCam.Priority = 5;
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
                    SwitchToPhone();
                }
            }
        }
    }
}
