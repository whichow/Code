using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISDKPlugin
{
    void Init();
    void Release();
	void OnOrbbecDeviceInit();

    string Version
    {
        get;
    }
    string Description
    {
        get;
    }
}
