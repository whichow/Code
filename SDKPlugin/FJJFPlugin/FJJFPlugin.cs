using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Text;

public class FJJFPlugin : ISDKPlugin
{
    public string Version
    {
        get
        {
            return "0.0.1";
        }
    }

    public string Description
    {
        get
        {
            return "福建移动经分系统插件";
        }
    }

    private AndroidJavaObject jingfenStatesJO;
    private AndroidJavaObject baminApplicationJO;
    private AndroidJavaObject currentActivity;
    private Action<string> sendSuccess;
    private Action<int> sendFailure;
	private const string BASE_URL = "http://112.50.251.23:10094";

    public void Init()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jingfenStatesJO = new AndroidJavaObject("com.orbbec.fujianjingfen.JingfenStats");
            baminApplicationJO = new AndroidJavaObject("com.orbbec.u3d.baminsdk.Application");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    public void Release()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            jingfenStatesJO.Dispose();
            baminApplicationJO.Dispose();
        }
    }

    public string GetSN()
    {
        //TODO: Cache SN Number
        return Orbbec.OrbbecManager.Instance.ReadSN();
    }

    public string GetUserID()
    {
        if (Application.platform == RuntimePlatform.Android)
            //TODO: Cache UserID
            return jingfenStatesJO.CallStatic<string>("getOTTCountFJ");
        else
		    return "";
    }

    public string GetVendorID()
    {
        if (Application.platform == RuntimePlatform.Android)
            //TODO: Cache VendorID
            return jingfenStatesJO.CallStatic<string>("getVendorID");
        else
		    return "";
    }

    public string GetAppKey()
    {
        return "8788f1f58d784ec077f78dc97d328343";
    }

    public void SendToServer(string username, string vendorId, string sn, Action<string> onSuccess, Action<int> onFailure)
    {
        sendSuccess = onSuccess;
        sendFailure = onFailure;
        string url = string.Format("{0}/begin?ott_username={1}&cpcode={2}&sn={3}", BASE_URL, username, vendorId, sn);
        HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);
        request.Method = "GET";
        request.BeginGetResponse(new AsyncCallback(ResponseCallback), request);
    }

    private void ResponseCallback(IAsyncResult ar)
    {
        HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
        HttpWebResponse respoonse = (HttpWebResponse)request.EndGetResponse(ar);
        Stream stream = respoonse.GetResponseStream();
        if (respoonse.StatusCode != HttpStatusCode.OK)
        {
            if (sendFailure != null) sendFailure((int)respoonse.StatusCode);
        }
        else
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string message = reader.ReadToEnd();
            if (sendSuccess != null) sendSuccess(message);
        }
    }

    public void OnOrbbecDeviceInit()
    {
        var sn = GetSN();
        var userId = GetUserID();
        var vendorId = GetVendorID();

        Debug.Log(string.Format("SN:{0}, User ID:{1}, Vendor ID:{2}", sn, userId, vendorId));

        if (Application.platform == RuntimePlatform.Android)
            InitBaminSDK(sn, GetAppKey());

        SendToServer(userId, vendorId, sn, (message) =>
        {
            Debug.Log("Send success:" + message);
        }, (errorNo) =>
        {
            Debug.Log("Send failure with number:" + errorNo);
        });
    }

    private void InitBaminSDK(string sn, string appkey)
    {
        baminApplicationJO.CallStatic("initBamin", currentActivity, sn, appkey);
    }
}
