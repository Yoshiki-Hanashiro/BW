using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMotion : MonoBehaviour
{
    public GameObject target;
    [SerializeField]
    private GameObject body;
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

    int scaleFlag = 0;
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
        if (this.gameObject.transform.localScale.x > 0)
        {
            scaleFlag = 1;
        }
        else
        {
            scaleFlag = -1;
        }
        //�p���ێ�
        if (Input.GetKey(KeyCode.A))
        {
            bodyRigid.AddRelativeForce(new Vector2(-200f * Time.deltaTime, 0f));
            this.gameObject.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            bodyRigid.AddRelativeForce(new Vector2(200f * Time.deltaTime, 0f));
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
            //�������Ԏ��A�n�ʂɐڂ��Ă���Ƃ��Ƀu���[�L
            //�����Ă���ʂɑ΂��ĕ��s�ɗ͂�������
            //�͂͒n�ʂɂ������Ă���͂ɔ�Ⴗ��


            //���C��F=���C�W���ʁ~�����R��N
            float friction=1* Physics.gravity.y * bodyRigid.gravityScale;
            //bodyRigid.AddRelativeForce(new Vector2(scaleFlag * 10 * Time.deltaTime, 0f));
            //���Ȃ�����Ɋ֌W�Ȃ��E�ɍs��
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bodyRigid.AddRelativeForce(new Vector2(0, 7), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {

        float legLength = 1.57f;
        float legForce = 200f;
        Vector3 origin = body.transform.position; // ���_
        Vector3 direction = body.transform.TransformDirection(new Vector3(0, -1, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, legLength+0.1f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            float force = 0;
            //�n�ʂɗ���
            if (setedIK)//IK�͍ŏ��͂Ȃ��̂ł���̐ݒ肪����������B
            {
                target.transform.position = hit.point + hit.normal;

                rightLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                leftLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                float distance = Vector3.Distance(hit.point, origin);
                if (distance < legLength+0.1f)
                {
                    float gravity = Physics.gravity.y * bodyRigid.gravityScale;
                    float v0 = 2 * gravity * (legLength - distance);//�ڕW���x
                    if (v0 > 0)
                    {
                        v0 = 0;
                    }
                    v0 = Mathf.Sqrt(-v0);
                    force = ((v0 - bodyRigid.velocity.y) / (1/legForce))*Time.deltaTime;
                    force = force - gravity*0.9f;
                    bodyRigid.AddRelativeForce(new Vector2(0, force)); //��
                    //�����グ��
                    /*
                        y=v0^2/2g �����グ�̍ō��_
                        v0=sqrt(2gy) �w��̈ʒu���ō��_�ɂ���Ƃ��ɕK�v�ȑ��x
                        (�ڕW���x)=(����)+at �������x�����^���̌����@�ڕW���x�ɂ���ɂ͂ǂꂭ�炢�̉����x��������Ηǂ��̂�
                        a=(�ڕW���x-����)/t�@���݂̑��x����ڕW���x�ɓ��B���邽�߂ɕK�v�ȉ����x

                        distance�̂Ƃ���ŏd�͉����x���̉����x���m�ۂ�����
                    */
                }
            }
            //������藎����
            /*�����Ă����̊p�x��m��
             �d�͂���̊p�x�ƍ�ɑ΂��Đ��������ɕ���
            ��̊p�x�����̗͂����藎�����
            ��ɑ΂��Đ��������̗͂���ɂ����Ă����*/
            float groundAngle = Mathf.Atan2(hit.normal.x, hit.normal.y);//�����Ă���n�ʂ̊p�x��m��
            float slipForce = Mathf.Sin(groundAngle)*force;//���藎����͂����߂�
            Debug.Log(Mathf.Abs(Mathf.Sin(groundAngle))* slipForce);
            bodyRigid.AddRelativeForce(new Vector2(Mathf.Abs(Mathf.Sin(groundAngle)) * slipForce, Mathf.Cos(groundAngle)*slipForce));
        }
        Debug.DrawRay(footRay.origin, footRay.direction * (legLength+0.1f), Color.red);
    }

    void Walk(float speed)
    {

    }

    // Update is called once per frame
    void MotionUpdate()
    {
        
    }
}
