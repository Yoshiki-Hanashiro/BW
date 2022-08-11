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
        Vector3 origin = body.transform.position; // ���_
        Vector3 direction = body.transform.TransformDirection(new Vector3(-1, 0, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction,1.7f);
        if (hit.collider != null && hit.collider.tag=="Ground")
        {
            if (setedIK)
            {
                rightLegik.transform.position = hit.point-new Vector2(direction.x,direction.y)*0.15f;
                leftLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                float distance = Vector3.Distance(hit.point, origin);
                if (distance < 1.5f)
                {
                    //bodyRigid.AddRelativeForce(new Vector2(1.5f-distance, 0)*50);  //�n�ʂƂ̋����ɔ�Ⴕ�ė͂�������
                    //�n�ʂɋ߂Â��X�s�[�h�ɔ�Ⴗ��Ƃ����񂾏�ԂŎ~�܂肻��
                    bodyRigid.AddRelativeForce(new Vector2(-bodyRigid.velocity.y*3,0));
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
