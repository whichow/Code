using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;

public enum BoneType
{
    HeadToNeck,
    NeckToTorso,
    TorsoToLeftShoulder,
    LeftShoulderToElbow,
    LeftElbowToHand,
    TorsoToRightShoulder,
    RightShoulderToElbow,
    RightElbowToHand,
    TorsoToLeftHip,
    LeftHipToKnee,
    LeftKneeToFoot,
    TorsoToRightHip,
    RightHipToKnee,
    RightKneeToFoot,
}

public class SkeletonView : MonoBehaviour
{
    public Material circleMat;
    public Material lineMat;
    private Dictionary<SkeletonType, Vector3> skPosMap;

    private Dictionary<SkeletonType, Mesh> circleMap;
    private Dictionary<BoneType, Mesh> lineMap;

    private Mesh circleMesh;
    private Mesh lineMesh;


    private OrbbecUserManager _obUserMgr;
    private OrbbecManager _obMgr;
    private OrbbecUser _user;
    private Dictionary<SkeletonType, int> _skMap;

    // Use this for initialization
    void Start()
    {
        _obUserMgr = OrbbecUserManager.Instance;
        _obMgr = OrbbecManager.Instance;

        GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (_obUserMgr == null)
        {
            return;
        }
        if (_obUserMgr.CurrentUser == null)
        {
            return;
        }

        _user = _obUserMgr.CurrentUser;

        if (_skMap == null)
        {
            _skMap = _obMgr.GetJointToIntDict();
            skPosMap = new Dictionary<SkeletonType, Vector3>();
            circleMap = new Dictionary<SkeletonType, Mesh>();
            lineMap = new Dictionary<BoneType, Mesh>();

            foreach (var sk in _skMap)
            {
                circleMap[sk.Key] = circleMesh;
            }

            var boneTypes = Enum.GetValues(typeof(BoneType));
            foreach (var boneType in boneTypes)
            {
                lineMap[(BoneType)boneType] = lineMesh;
            }
        }

        if (_skMap != null)
        {
            var etor = _skMap.GetEnumerator();
            while (etor.MoveNext())
            {
                var skType = etor.Current.Key;
                var skIndex = etor.Current.Value;
                var pos = _user.BoneWorldPos[skIndex];
				pos.z = 0;
				Vector4 pos4 = new Vector4(pos.x, pos.y, pos.z, 1);
                skPosMap[skType] = transform.localToWorldMatrix * pos4;
            }
        }
    }

    void OnRenderObject()
    {
        DrawLine();
        DrawCircle();
    }

    private void DrawCircle()
    {
        if (circleMap == null)
        {
            return;
        }
        circleMat.SetPass(0);
        var etor = circleMap.GetEnumerator();
        while (etor.MoveNext())
        {
            var skType = etor.Current.Key;
            var mesh = etor.Current.Value;
            var pos = skPosMap[skType];
            var rot = Quaternion.identity;
            var scale = Vector3.one * 1.5f;
            Matrix4x4 matrix = Matrix4x4.TRS(pos, rot, scale);
            Graphics.DrawMeshNow(mesh, matrix);
            Debug.Log("Draw " + skType + ":" + skPosMap[skType]);
        }
    }

    private void DrawLine()
    {
        if (lineMap == null)
        {
            return;
        }
        lineMat.SetPass(0);

        Vector3 pos;
        Quaternion rot;
        Vector3 scale;

        GetLineParams(skPosMap[SkeletonType.Head], skPosMap[SkeletonType.Neck], out pos, out rot, out scale);
        Matrix4x4 matrix = Matrix4x4.TRS(pos, rot, scale);
        var mesh = lineMap[BoneType.HeadToNeck];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.Neck], skPosMap[SkeletonType.Torso], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.NeckToTorso];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.Neck], skPosMap[SkeletonType.LeftShoulder], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.TorsoToLeftShoulder];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.LeftShoulder], skPosMap[SkeletonType.LeftElbow], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.LeftShoulderToElbow];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.LeftElbow], skPosMap[SkeletonType.LeftHand], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.LeftElbowToHand];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.Neck], skPosMap[SkeletonType.RightShoulder], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.TorsoToRightShoulder];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.RightShoulder], skPosMap[SkeletonType.RightElbow], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.RightShoulderToElbow];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.RightElbow], skPosMap[SkeletonType.RightHand], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.RightElbowToHand];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.Torso], skPosMap[SkeletonType.LeftHip], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.TorsoToLeftHip];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.LeftHip], skPosMap[SkeletonType.LeftKnee], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.LeftHipToKnee];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.LeftKnee], skPosMap[SkeletonType.LeftFoot], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.LeftKneeToFoot];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.Torso], skPosMap[SkeletonType.RightHip], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.TorsoToRightHip];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.RightHip], skPosMap[SkeletonType.RightKnee], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.RightHipToKnee];
        Graphics.DrawMeshNow(mesh, matrix);

        GetLineParams(skPosMap[SkeletonType.RightKnee], skPosMap[SkeletonType.RightFoot], out pos, out rot, out scale);
        matrix = Matrix4x4.TRS(pos, rot, scale);
        mesh = lineMap[BoneType.RightKneeToFoot];
        Graphics.DrawMeshNow(mesh, matrix);
    }

    private void GetLineParams(Vector3 beginPoint, Vector3 endPoint, out Vector3 pos, out Quaternion rot, out Vector3 scale)
    {
        pos = endPoint;
		var dv = beginPoint - endPoint;
        rot = Quaternion.FromToRotation(Vector3.right, dv.normalized);
        scale = new Vector3(dv.magnitude, 0.5f);
    }

    private void GenerateMesh()
    {
        GenerateCircle();
        GenerateLine();
    }

    private void GenerateCircle()
    {
        circleMesh = new Mesh();

        Vector3[] verts = new Vector3[51];

        verts[50] = new Vector3(0, 0, 0);
        for (int i = 0; i < verts.Length; i++)
        {
            var angle = 360f / 50f * i;
            verts[i] = new Vector3(Mathf.Sin(angle * 3.14f / 180f), Mathf.Cos(angle * 3.14f / 180f));
        }

        int[] tris = new int[50 * 3];

        for (int i = 0; i < 50; i++)
        {
            tris[i * 3] = 50;
            tris[i * 3 + 1] = i;
            tris[i * 3 + 2] = i + 1;
        }

        circleMesh.vertices = verts;
        circleMesh.triangles = tris;
    }

    private void GenerateLine()
    {
        lineMesh = new Mesh();

        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(0, -0.5f);
        verts[1] = new Vector3(0, 0.5f);
        verts[2] = new Vector3(1, 0.5f);
        verts[3] = new Vector3(1, -0.5f);

        int[] tris = new int[] { 0, 1, 2, 0, 2, 3 };

        lineMesh.vertices = verts;
        lineMesh.triangles = tris;
    }
}
