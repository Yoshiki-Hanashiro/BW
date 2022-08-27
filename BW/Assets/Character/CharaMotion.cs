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
        //姿勢維持
        if (Input.GetKey(KeyCode.A))
        {
            bodyRigid.AddRelativeForce(new Vector2(0.5f, 0f));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            bodyRigid.AddTorque(0.1f);
        }
    }

    private void FixedUpdate()
    {

        float legLength = 1.59f;
        float legForce = 100f;
        Vector3 origin = body.transform.position; // 原点
        Vector3 direction = body.transform.TransformDirection(new Vector3(-1, 0, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, legLength+1);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            if (setedIK)
            {
                rightLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                leftLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                float distance = Vector3.Distance(hit.point, origin);
                if (distance < legLength+1)
                {
                    //足が地面につく
                    //bodyRigid.AddRelativeForce(new Vector2(1.5f-distance, 0)*50);  //地面との距離に比例して力を加える
                    //下に下がっているときにより強い上昇を掛ける
                    //地面に近づくスピードに比例するとかがんだ状態で止まりそう
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
                    //bodyRigid.AddRelativeForce(new Vector2(2000*Time.deltaTime*bodyRigid.gravityScale, 0));
                    //制動距離をもとにした計算式
                    float gravity = Physics.gravity.y * bodyRigid.gravityScale;
                    float v0 = 2 * gravity * (legLength - distance);//目標速度
                    if (v0 > 0)
                    {
                        v0 = 0;
                    }
                    v0 = Mathf.Sqrt(-v0);
                    float a = ((v0 - bodyRigid.velocity.y) / (1/legForce))*Time.deltaTime;
                    a = a - gravity*0.9f;
                    bodyRigid.AddRelativeForce(new Vector2(a, 0)); //正
                    //投げ上げ式
                    /*
                        y=v0^2/2g 投げ上げの最高点
                        v0=sqrt(2gy) 指定の位置を最高点にするときに必要な速度
                        (目標速度)=(初速)+at 等加速度直線運動の公式　目標速度にするにはどれくらいの加速度をかければ良いのか
                        a=(目標速度-初速)/t　現在の速度から目標速度に到達するために必要な加速度

                        distanceのところで重力加速度分の加速度を確保したい
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
