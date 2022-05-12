using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneControl : MonoBehaviour
{
    public float min;
    public float max;
    float relativeMin;
    float relativeMax;
    float adjustedMin;
    float adjustedMax;
    public GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        //İ’è‚³‚ê‚½Šp“x‚ğe‚Æ‚Ì‘Š‘ÎŠp“x‚É•ÏŠ·‚µ•Û
        //e‚Æ‚Ì‘Š‘ÎŠp“x‚ğŠî‚É’²®‚³‚ê‚½Šp“xŒÀŠE‚ğXV‚µ‘±‚¯‚é
        if(parentObject != null)
        {
            relativeMin = min - UnityEditor.TransformUtils.GetInspectorRotation(parentObject.transform).z;
            relativeMax = max - UnityEditor.TransformUtils.GetInspectorRotation(parentObject.transform).z;
        }
        else
        {
            adjustedMin = min;
            adjustedMax = max;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parentObject != null)
        {
            adjustedMin = UnityEditor.TransformUtils.GetInspectorRotation(parentObject.transform).z + relativeMin;
            adjustedMax = UnityEditor.TransformUtils.GetInspectorRotation(parentObject.transform).z + relativeMax;
        }
        float rotateZ = UnityEditor.TransformUtils.GetInspectorRotation(transform).z;
        float angleZ = Mathf.Clamp(rotateZ, adjustedMin, adjustedMax);
        Vector3 setAngle = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angleZ);
        UnityEditor.TransformUtils.SetInspectorRotation(transform, setAngle);
    }
}
