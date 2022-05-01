using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneControl : MonoBehaviour
{
    public float min;
    public float max;
    public GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var normalMin = Mathf.Repeat(min + 180, 360) - 180;//180~-180に丸める
        var normalMax = Mathf.Repeat(max + 180, 360) - 180;
        if (parentObject == null)
        {
            float rotateZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
            float angleZ = Mathf.Clamp(rotateZ, normalMin, normalMax);
            angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
            transform.localRotation = Quaternion.Euler(0, 0, angleZ);
        }
        else
        {

            float sub = parentObject.transform.eulerAngles.z - transform.eulerAngles.z;
            sub -= Mathf.Floor(sub / 360f) * 360f;
            if (sub > 180) sub -= 360;
            Debug.Log(sub);
        }

        //角度制限を超えて一周しちゃうバグは制限範囲を飛び越えることが原因なので、移動予定位置を補正するか、ボーンの角度をループしないようにして（-500度とかも可能なようにして）制限をかける。
    }
}
