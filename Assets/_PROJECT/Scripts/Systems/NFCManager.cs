using System.Collections;
using System.Collections.Generic;
using distriqt.plugins.nfc;
using Managers;
using UnityEngine;

public class NFCManager : MonoBehaviourSingletonPersistent<NFCManager>
{
    void Start()
    {
        if (NFC.isSupported == false)
        {
            Debug.LogError("NFC IS NOT SUPPORTED ON THE DEVICE !!!");
            return;
        }

        if (NFC.Instance.IsEnabled() == false)
        {
            NFC.Instance.OpenDeviceSettings();
        }
        
    }

}
