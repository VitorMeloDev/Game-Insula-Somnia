using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfigRef : MonoBehaviour
{
    [SerializeField] private GameObject camB;

    public void EnableCam()
    {
        camB.SetActive(true);
    }

    public void DisableCam()
    {
        camB.SetActive(false);
    }
}
