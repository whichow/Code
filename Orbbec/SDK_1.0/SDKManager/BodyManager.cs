using System;
using Orbbec;
using UnityEngine;
using System.Collections.Generic;

public class Joint
{
    private Body _body;

    public SkeletonType JointType;
    public Vector3 Position
    {
        get
        {

            int jointIndex = OrbbecManager.Instance.GetJointToIntDict()[JointType];
            return _body.OrbbecUser.BoneWorldPos[jointIndex];
        }
    }
    public Quaternion Rotation
    {
        get
        {
            int jointIndex = OrbbecManager.Instance.GetJointToIntDict()[JointType];
            return _body.OrbbecUser.BoneRotator[jointIndex];
        }
    }

    public Joint(Body body)
    {
        _body = body;
    }
}

public class Body
{
    public Dictionary<SkeletonType, Joint> Joints;
    public OrbbecUser OrbbecUser
    {
        get;
        set;
    }

    public Body()
    {
        Joints = new Dictionary<SkeletonType, Joint>();

        Joints[SkeletonType.Head] = new Joint(this) { JointType = SkeletonType.Head };
        Joints[SkeletonType.Neck] = new Joint(this) { JointType = SkeletonType.Neck };
        Joints[SkeletonType.Torso] = new Joint(this) { JointType = SkeletonType.Torso };
        Joints[SkeletonType.Waist] = new Joint(this) { JointType = SkeletonType.Waist };
        Joints[SkeletonType.LeftCollar] = new Joint(this) { JointType = SkeletonType.LeftCollar };
        Joints[SkeletonType.LeftShoulder] = new Joint(this) { JointType = SkeletonType.LeftShoulder };
        Joints[SkeletonType.LeftElbow] = new Joint(this) { JointType = SkeletonType.LeftElbow };
        Joints[SkeletonType.LeftWrist] = new Joint(this) { JointType = SkeletonType.LeftWrist };
        Joints[SkeletonType.LeftHand] = new Joint(this) { JointType = SkeletonType.LeftHand };
        Joints[SkeletonType.LeftFingertip] = new Joint(this) { JointType = SkeletonType.LeftFingertip };
        Joints[SkeletonType.RightCollar] = new Joint(this) { JointType = SkeletonType.RightCollar };
        Joints[SkeletonType.RightShoulder] = new Joint(this) { JointType = SkeletonType.RightShoulder };
        Joints[SkeletonType.RightElbow] = new Joint(this) { JointType = SkeletonType.RightElbow };
        Joints[SkeletonType.RightWrist] = new Joint(this) { JointType = SkeletonType.RightWrist };
        Joints[SkeletonType.RightHand] = new Joint(this) { JointType = SkeletonType.RightHand };
        Joints[SkeletonType.RightFingertip] = new Joint(this) { JointType = SkeletonType.RightFingertip };
        Joints[SkeletonType.LeftHip] = new Joint(this) { JointType = SkeletonType.LeftHip };
        Joints[SkeletonType.LeftKnee] = new Joint(this) { JointType = SkeletonType.LeftKnee };
        Joints[SkeletonType.LeftAnkle] = new Joint(this) { JointType = SkeletonType.LeftAnkle };
        Joints[SkeletonType.LeftFoot] = new Joint(this) { JointType = SkeletonType.LeftFoot };
        Joints[SkeletonType.RightHip] = new Joint(this) { JointType = SkeletonType.RightHip };
        Joints[SkeletonType.RightKnee] = new Joint(this) { JointType = SkeletonType.RightKnee };
        Joints[SkeletonType.RightAnkle] = new Joint(this) { JointType = SkeletonType.RightAnkle };
        Joints[SkeletonType.RightFoot] = new Joint(this) { JointType = SkeletonType.RightFoot };
    }
}

public class BodyManager : MonoBehaviour
{
    public Body CurrentBody
    {
        get
        {
            return _body.OrbbecUser == null ? null : _body;
        }
    }

    private OrbbecManager _obMgr;
    private OrbbecUser _obUser;
    private int _userId;
    private Body _body;
    private OrbbecDeviceManager _devMgr;

    void Start()
    {
        _devMgr = OrbbecDeviceManager.Instance;
        _body = new Body();

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
        // _obMgr.SetStreamFlag(BodyStream, true);
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

        var users = _obMgr.TrackingUsers;

        var etor = users.GetEnumerator();

        if (_obUser == null)
        {
            while (etor.MoveNext())
            {
                if (etor.Current.Value != null && etor.Current.Key != 0)
                {
                    _obUser = etor.Current.Value;
                    _userId = etor.Current.Key;
                    _body.OrbbecUser = _obUser;
                    Debug.Log("检测到玩家，ID:" + _userId);
                    return;
                }
            }
        }
        else
        {
            while (etor.MoveNext())
            {
                if (etor.Current.Value == _obUser)
                {
                    return;
                }
            }
        }

        _obUser = null;
    }
}