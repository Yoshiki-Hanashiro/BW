using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneControl : MonoBehaviour
{
    public float min;
    public float max;
    public GameObject parentObject;
    public bool reverse;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        float rotateZ = UnityEditor.TransformUtils.GetInspectorRotation(transform).z;
        float angleZ = Mathf.Clamp(rotateZ, min,max);
        Debug.Log(rotateZ + " : " + angleZ);
        transform.localRotation = Quaternion.Euler(0, 0, angleZ);
    }
}
