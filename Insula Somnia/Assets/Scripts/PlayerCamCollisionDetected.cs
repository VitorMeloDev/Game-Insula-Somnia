using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamCollisionDetected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                other.gameObject.GetComponent<CameraConfigRef>().EnableCam();
            break;
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                other.gameObject.GetComponent<CameraConfigRef>().DisableCam();
            break;
        }    
    }
}
