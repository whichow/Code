using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;

public class OrbbecDeviceManager : MonoSingleton<OrbbecDeviceManager>
{
    public bool HasInit
    {
        get
        {
            return _hasInit;
        }
    }

    public Action onDeviceInit;

    private OrbbecManager _obMgr;
    private bool _hasInit;

    void Start()
    {
        _InitOrbbecDevice();
    }

    private void _InitOrbbecDevice()
    {
        _hasInit = false;

        OrbbecManager.CreateOrbbecManager(new OrbbecManagerParam()
        {
            IsTrackingSkeleton = true,
            IsUseUserLabel = true,
            IsUseUserImage = true,
            OrbbecInitResourceCallBack = OnDeviceInit,
            OrbbecInitFailedCallBack = OnInitFailed,
        });
    }

    private void OnDeviceInit()
    {
        _obMgr = OrbbecManager.Instance;
        _hasInit = true;
        if (onDeviceInit != null)
        {
            onDeviceInit();
        }
        Debug.Log("设备初始化成功");
    }

    private void OnInitFailed()
    {
        Debug.Log("设备初始化失败");
    }
}
