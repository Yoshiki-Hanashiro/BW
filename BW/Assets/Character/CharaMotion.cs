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
        //épê®à€éù
        if (Input.GetKey(KeyCode.A))
        {
            bodyRigid.AddRelativeForce(new Vector2(2,0));
        }

        Vector3 origin = body.transform.position; // å¥ì_
        Vector3 direction = body.transform.TransformDirection(new Vector3(-1, 0, 0));
        footRay = new Ray2D(origin, direction);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction);
        if (hit.collider != null && hit.collider.tag=="Ground")
        {
            if (setedIK)
            {
                rightLegik.transform.position = hit.point;
                leftLegik.transform.position = hit.point;
                Debug.Log("ínÇ…ë´ÇÇ¬ÇØÇÈ");
            }

        }
        Debug.DrawRay(footRay.origin, footRay.direction*10,Color.red);
    }

    void Walk(float speed)
    {

    }

    // Update is called once per frame
    void MotionUpdate()
    {
        
    }
}
