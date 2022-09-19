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
        //姿勢維持
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
            //無操作状態時、地面に接しているときにブレーキ
            //立っている面に対して並行に力をかける
            //力は地面にかかっている力に比例する


            //摩擦力F=摩擦係数μ×垂直抗力N
            float friction=1* Physics.gravity.y * bodyRigid.gravityScale;
            //bodyRigid.AddRelativeForce(new Vector2(scaleFlag * 10 * Time.deltaTime, 0f));
            //正なら向きに関係なく右に行く
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
        Vector3 origin = body.transform.position; // 原点
        Vector3 direction = body.transform.TransformDirection(new Vector3(0, -1, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, legLength+0.1f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            float force = 0;
            //地面に立つ
            if (setedIK)//IKは最初はないのでそれの設定が完了したら。
            {
                target.transform.position = hit.point + hit.normal;

                rightLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                leftLegik.transform.position = hit.point - new Vector2(direction.x, direction.y) * 0.15f;
                float distance = Vector3.Distance(hit.point, origin);
                if (distance < legLength+0.1f)
                {
                    float gravity = Physics.gravity.y * bodyRigid.gravityScale;
                    float v0 = 2 * gravity * (legLength - distance);//目標速度
                    if (v0 > 0)
                    {
                        v0 = 0;
                    }
                    v0 = Mathf.Sqrt(-v0);
                    force = ((v0 - bodyRigid.velocity.y) / (1/legForce))*Time.deltaTime;
                    force = force - gravity*0.9f;
                    bodyRigid.AddRelativeForce(new Vector2(0, force)); //正
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
            //坂を滑り落ちる
            /*立っている坂の角度を知る
             重力を坂の角度と坂に対して垂直方向に分解
            坂の角度方向の力が滑り落ちる力
            坂に対して垂直方向の力が坂にかけている力*/
            float groundAngle = Mathf.Atan2(hit.normal.x, hit.normal.y);//立っている地面の角度を知る
            float slipForce = Mathf.Sin(groundAngle)*force;//滑り落ちる力を求める
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
