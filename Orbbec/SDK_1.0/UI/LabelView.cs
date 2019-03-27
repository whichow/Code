using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelView : MonoBehaviour
{
	public RawImage labelImage;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		labelImage.texture = LabelManager.Instance.labelTexture;
    }
}
