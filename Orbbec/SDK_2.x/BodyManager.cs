using System;
using Astra;
using Orbbec.Simple;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public Body CurrentBody
    {
        get
        {
            return _body;
        }
    }

    private OrbbecManager _obMgr;
    private Body _body;
    private byte _bodyId;
    private OrbbecDeviceManager _devMgr;

    void Start()
    {
        _devMgr = OrbbecDeviceManager.Instance;
        _obMgr = OrbbecManager.instance;

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
        _obMgr.streamManager.OpenStream(StreamType.Body);
    }

    void Update()
    {
        _UpdateBody();
    }

    private void _UpdateBody()
    {
        if (_obMgr == null || !_devMgr.HasInit)
        {
            return;
        }
        var streamData = _obMgr.streamManager.GetStreamData();
        if (streamData == null)
        {
            return;
        }
        var bodyData = streamData.bodyData;
        if (bodyData.frameIndex <= 0)
        {
            return;
        }

        var bodies = bodyData.bodies;

        if (_body == null)
        {
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i] != null && bodies[i].Id != 0)
                {
                    _body = bodies[i];
                    _bodyId = bodies[i].Id;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < bodies.Length; i++)
            {
                if(bodies[i].Id == _bodyId)
                {
                    _body = bodies[i];
                    return;
                }
            }
        }

        _body = null;
    }
}