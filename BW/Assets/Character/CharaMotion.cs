using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMotion : MonoBehaviour
{
    [SerializeField]
    private Transform body;
    private Rigidbody2D bodyRigid;
    [SerializeField]
    private GameObject rightBottomLeg;
    Ray2D footRay;
    public BWCharaManage bwcharamanage;

    bool setedIK = false;
    GameObject rightArmik;
    GameObject leftArmik;
    GameObject rightLegik;
    GameObject leftLegik;
    public void setIK(GameObject  rightarm,GameObject leftarm,GameObject rightleg,GameObject leftleg)
    {
        setedIK = true;
        rightArmik = rightarm;
        leftArmik = leftarm;
        rightLegik = rightleg;
        leftLegik = leftleg;
    }
    public void test(Vector3 pos)
    {
        Debug.Log(pos);
    }
    // Start is called before the first frame update
    void Start()
    {
        bodyRigid = body.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //�p���ێ�
        if (Input.GetKey(KeyCode.A))
        {
            bodyRigid.AddRelativeForce(new Vector2(0.5f,0.5f));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            bodyRigid.AddTorque(0.1f);
        }
        float legLength = 1.7f;
        Vector3 origin = body.transform.position; // ���_
        Vector3 direction = body.transform.TransformDirection(new Vector3(-1, 0, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, legLength);
        if (hit.collider != null && hit.collider.tag=="Ground")
        {
            if (setedIK)
            {
                rightLegik.transform.position = hit.point-new Vector2(direction.x,direction.y)*0.15f;
                leftLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                float distance = Vector3.Distance(hit.point, origin);
                if (distance < legLength)
                {
                    //�����n�ʂɂ�
                    //bodyRigid.AddRelativeForce(new Vector2(1.5f-distance, 0)*50);  //�n�ʂƂ̋����ɔ�Ⴕ�ė͂�������
                    //���ɉ������Ă���Ƃ��ɂ�苭���㏸���|����
                    //�n�ʂɋ߂Â��X�s�[�h�ɔ�Ⴗ��Ƃ����񂾏�ԂŎ~�܂肻��
                    /*if (bodyRigid.velocity.y < -0.3f)
                    {
                        bodyRigid.AddRelativeForce(new Vector2(16, 0));
                    }
                    else {
                        if (distance > legLength - 0.1f)
                        {
                            bodyRigid.AddTorque(100f);
                        }
                        else
                        {
                            bodyRigid.AddRelativeForce(new Vector2(3, 0));
                        }
                        
                    }*/
                    //bodyRigid.AddRelativeForce(new Vector2(-bodyRigid.velocity.y*3, 0));
                    //�������������Ƃɂ����v�Z��
                    float v0 = 2 * (-9.81f * bodyRigid.gravityScale) * (legLength - distance);
                    float a = (-bodyRigid.velocity.y - v0) / (Time.deltaTime*40);
                    bodyRigid.AddRelativeForce(new Vector2(a, 0));
                    //�����グ��
                    /*
                        y=v0^2/2g �����グ�̍ō��_
                        v0=sqrt(2gy) �w��̈ʒu���ō��_�ɂ���Ƃ��ɕK�v�ȑ��x
                        v=v0+at �������x�����^���̌���
                        a=(v-v0)/t
                    */
                }
            }

        }
        Debug.DrawRay(footRay.origin, footRay.direction*1.7f,Color.red);
    }

    void Walk(float speed)
    {

    }

    // Update is called once per frame
    void MotionUpdate()
    {
        
    }
}
