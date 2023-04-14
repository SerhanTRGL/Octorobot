using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float currentSize;
    // Start is called before the first frame update
    void Start()
    {
        currentSize = virtualCamera.m_Lens.OrthographicSize;
        MountCollector.OnWeaponMounted += MountCollector_OnWeaponMounted;
    }

    private void MountCollector_OnWeaponMounted() {
       
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentSize, currentSize+0.02f, 0.1f);
        currentSize += 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
