using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CamSwitch : MonoBehaviour
{
    public CinemachineCamera mainVCam;
    public CinemachineCamera tableVCam;
    public CinemachineCamera mobileVCam;
    public CinemachineCamera GuideVCam;
    private CinemachineBrain brain;
    public GameObject phoneUI;
    public PhonePostsManager phone;


    public void SwitchToTable()
    {
        mobileVCam.Priority = 2;
        mainVCam.Priority = 5;
        tableVCam.Priority = 10;
        DisableUI();
    }
    public void SwitchToPhone()
    {
        mobileVCam.Priority = 10;
        mainVCam.Priority = 2;
        tableVCam.Priority = 5;
        StartCoroutine(WaitUntilCameraIsActive(mobileVCam, () => {EnablePhoneUI();}));
    }
    public void SwitchToGuide()
    {
        mobileVCam.Priority = 1;
        mainVCam.Priority = 2;
        tableVCam.Priority = 5;
        GuideVCam.Priority = 10;
        StartCoroutine(WaitUntilCameraIsActive(GuideVCam, () => { EnableGuideUI(); }));
    }
    void EnablePhoneUI(){
        phoneUI.SetActive(true);
    }
    void EnableGuideUI()
    {
        if(phone.currentDayObject.paper != null){
            phone.currentDayObject.paper.SetActive(true);
        }
    }
    void DisableUI(){
        phoneUI.SetActive(false);
        if(phone.currentDayObject.paper != null){
            phone.currentDayObject.paper.SetActive(false);
        }
    }
    public void SwitchToGround()
    {
        mainVCam.Priority = 10;
        tableVCam.Priority = 5;
        mobileVCam.Priority = 2;
        DisableUI();
    }
    private void Start()
    {
        DisableUI();
        brain = Camera.main.GetComponent<CinemachineBrain>();
        if (mainVCam!= null && tableVCam != null && mobileVCam != null)
        {
            mainVCam.Priority = 10;
            tableVCam.Priority = 5;
            mobileVCam.Priority = 2;
        }
        else
        {
            Debug.LogError("Cameras not assigned!");
        }
    }
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Use Camera.main only
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.CompareTag("MainDesk"))
                {
                    SwitchToTable();
                }
                else if (hit.collider.CompareTag("Guide"))
                {
                    Debug.Log("Hit Guide");
                    SwitchToGuide();
                }
                else if (hit.collider.CompareTag("Phone"))
                {
                    Debug.Log("Hit Phone");
                    SwitchToPhone();
                }
            }
        }


        if (Input.GetKeyDown("backspace"))// Left click
        {
            SwitchToGround();
        }
    }
    private IEnumerator WaitUntilCameraIsActive(CinemachineCamera targetCam, System.Action onComplete = null)
    {
        Debug.Log($"Waiting for {targetCam.Name} to be active...");
        Debug.Log(brain.ActiveVirtualCamera);
        while (brain.IsBlending || brain.ActiveVirtualCamera != targetCam)
        {
            yield return null;
        }
        onComplete?.Invoke();
        Debug.Log($"{targetCam.Name} is now active!");
    }
}
