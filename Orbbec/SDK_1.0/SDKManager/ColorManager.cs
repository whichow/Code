using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;

public class ColorManager : MonoSingleton<ColorManager>
{
    public Texture2D colorTexture;

    private OrbbecManager _obMgr;
    private OrbbecDeviceManager _devMgr;

    void Start()
    {
        _devMgr = OrbbecDeviceManager.Instance;

        if (_devMgr.HasInit)
        {
            _OnDeviceInit();
        }
        else
        {
            _devMgr.onDeviceInit += _OnDeviceInit;
        }
    }

    private void _OnDeviceInit()
    {
        _obMgr = OrbbecManager.Instance;
        // _obMgr.SetStreamFlag(EnumTextureType.NTT_IMAGE, true);
    }

    void Update()
    {
        if (_obMgr == null || !_obMgr.IsInited)
        {
            return;
        }

        colorTexture = (Texture2D)_obMgr.GetImageMap();
    }
}
