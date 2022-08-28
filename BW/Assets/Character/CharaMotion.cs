using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMotion : MonoBehaviour
{
    public GameObject target;
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
            bodyRigid.AddRelativeForce(new Vector2(0f, 100f*Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            bodyRigid.AddRelativeForce(new Vector2(0f, -100f*Time.deltaTime));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bodyRigid.AddRelativeForce(new Vector2(7, 0), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {

        float legLength = 1.57f;
        float legForce = 200f;
        Vector3 origin = body.transform.position; // ���_
        Vector3 direction = body.transform.TransformDirection(new Vector3(-1, 0, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, legLength+0.1f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
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
                    float a = ((v0 - bodyRigid.velocity.y) / (1/legForce))*Time.deltaTime;
                    a = a - gravity*0.9f;
                    bodyRigid.AddRelativeForce(new Vector2(a, 0)); //��
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

        }
        Debug.DrawRay(footRay.origin, footRay.direction * (legLength+1), Color.red);
    }

    void Walk(float speed)
    {

    }

    // Update is called once per frame
    void MotionUpdate()
    {
        
    }
}
