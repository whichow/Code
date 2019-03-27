using System;
using System.Collections;
using System.Collections.Generic;
using Astra;
using Orbbec.Simple;
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
        _obMgr = OrbbecManager.instance;
        // var devices = _obMgr.deviceManager.GetOrbbecDevices();
        // if (devices == null || devices.Count == 0)
        // {
        //     Debug.LogError("Orbbec device not found!");
        //     return;
        // }

        _obMgr.streamManager.SetInitFailureCallback(() =>
        {
            Debug.LogError("Orbbec device init failed");
        });
        _obMgr.streamManager.SetInitSuccessCallback(() =>
        {
            Debug.Log("Orbbec device has init");

            var imgMode = _obMgr.streamManager.GetAvailableColorModes();
            foreach (var mode in imgMode)
            {
                if (mode.Width == 640 && mode.Height == 480 && mode.FramesPerSecond == 30)
                {
                    _obMgr.streamManager.SetColorMode(mode);
                    break;
                }
            }
            var depthMode = _obMgr.streamManager.GetAvailableDepthModes();
            foreach (var mode in depthMode)
            {
                if (mode.Width == 160 && mode.Height == 120 && mode.FramesPerSecond == 30)
                {
                    _obMgr.streamManager.SetDepthMode(mode);
                    break;
                }
            }
            _obMgr.streamManager.SetDefaultBodyFeatures(BodyTrackingFeatures.Skeleton);
            _obMgr.streamManager.SetSkeletonProfile(SkeletonProfile.Full);
            _obMgr.streamManager.SetSkeletonOptimization(SkeletonOptimization.BestAccuracy);

            _hasInit = true;

            if (onDeviceInit != null)
            {
                onDeviceInit();
            }
        });

        _obMgr.streamManager.Initialize();
    }
}
