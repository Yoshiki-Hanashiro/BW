using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleLimit : MonoBehaviour
{
    public float min;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var normalMin= Mathf.Repeat(min + 180, 360) - 180;
        var normalMax = Mathf.Repeat(max + 180, 360) - 180;
        float rotateZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        float angleZ = Mathf.Clamp(rotateZ, normalMin, normalMax);
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
        transform.localRotation = Quaternion.Euler(0, 0, angleZ);
        //�p�x�����𒴂��Ĉ�������Ⴄ�o�O�͐����͈͂��щz���邱�Ƃ������Ȃ̂ŁA�ړ��\��ʒu��␳���邩�A�{�[���̊p�x�����[�v���Ȃ��悤�ɂ��āi-500�x�Ƃ����\�Ȃ悤�ɂ��āj������������B
    }
}
