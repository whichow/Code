using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Orbbec;

public class SDKPluginManager : MonoSingleton<SDKPluginManager>
{
    private Dictionary<Type, ISDKPlugin> plugins = new Dictionary<Type, ISDKPlugin>();

    private bool deviceInit = false;

    public void Init(){}

    void Awake()
    {
        LoadPlugins();
    }

    void Start()
    {
        InitPlugins();
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        ReleasePlugins();
    }

	void Update()
	{
		if(!deviceInit)
        {
            if(OrbbecManager.Instance != null && OrbbecManager.Instance.IsInited)
            {
                deviceInit = true;
                OnOrbbecDeviceInit();
            }
        }
	}

    private void LoadPlugins()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes()
                            .Where(t => t.GetInterfaces()
                            .Contains(typeof(ISDKPlugin))))
                            .ToArray();
        foreach (var t in types)
        {
            var instance = (ISDKPlugin)Activator.CreateInstance(t);
            plugins.Add(t, instance);

            Debug.Log("Load Plugin: " + t.FullName);
        }
    }

    private void InitPlugins()
    {
        foreach (var p in plugins)
        {
            p.Value.Init();
        }
    }

    private void ReleasePlugins()
    {
        foreach (var p in plugins)
        {
            p.Value.Release();
        }
    }

    private void OnOrbbecDeviceInit()
    {
        foreach (var p in plugins)
        {
            p.Value.OnOrbbecDeviceInit();
        }
    }
}
