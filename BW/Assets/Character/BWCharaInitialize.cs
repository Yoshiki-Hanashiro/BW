using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BWCharaInitialize : MonoBehaviour
{
    //振袖部分の布表現
    private float ArmClothDamping = 0f;
    private float ArmClothFrequency = 1.5f;

    //private Vector2 upArmAnchor = new Vector2(-0.2993939f, -1.763251f);
    private Vector2 handAnchor = new Vector2(0.1634932f, -0.5892799f);

    public GameObject[] rightArmCloth;
    public GameObject rightUpArm;
    public GameObject rightArmClothSP;
    public GameObject rightHand;

    public GameObject[] leftArmCloth;
    public GameObject leftUpArm;
    public GameObject leftArmClothSP;
    public GameObject leftHand;

    //裾部分の布表現
    private float HemDamping = 0.5f;
    private float HemFrequency = 1.5f;
    private float backHemDamping = 1f;
    private float backHemFrequency = 4f;

    public GameObject[] frontHem;
    public GameObject[] backHem;
    public GameObject bottomBody;
    private Vector2 frontHemAnchor = new Vector2(4.78f, 0f);
    private Vector2 backHemAnchor = new Vector2(2f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        //腕の布のボーン
        for(int armCount = 0; armCount < rightArmCloth.Length; armCount++)
        {
            //先端以外にfixedJointをつけていく
            if(armCount == rightArmCloth.Length - 1)
            {
                //先端だけRigidbodyをつける
                rightArmCloth[armCount].AddComponent<Rigidbody2D>();
                leftArmCloth[armCount].AddComponent<Rigidbody2D>();
                for (int i = rightArmCloth.Length - 2; i >= 0; i--)
                {
                    //それぞれのパラメータを設定。
                    //ボーン同士をつなげる、減衰比、バネの強さ、アンカー座標、自動アンカー調整機能オフを設定
                    FixedJoint2D fixedjoint = rightArmCloth[i].GetComponent<FixedJoint2D>();
                    fixedjoint.connectedBody = rightArmCloth[i + 1].GetComponent<Rigidbody2D>();
                    fixedjoint.dampingRatio = ArmClothDamping;
                    fixedjoint.frequency = ArmClothFrequency;
                    fixedjoint.anchor = new Vector2(Vector2.Distance(rightArmCloth[i + 1].transform.position, rightArmCloth[i].transform.position), 0);
                    fixedjoint.autoConfigureConnectedAnchor = false;
                    fixedjoint.enableCollision = true;

                    fixedjoint = leftArmCloth[i].GetComponent<FixedJoint2D>();
                    fixedjoint.connectedBody = leftArmCloth[i + 1].GetComponent<Rigidbody2D>();
                    fixedjoint.dampingRatio = ArmClothDamping;
                    fixedjoint.frequency = ArmClothFrequency;
                    fixedjoint.anchor = new Vector2(Vector2.Distance(leftArmCloth[i + 1].transform.position, leftArmCloth[i].transform.position), 0);
                    fixedjoint.autoConfigureConnectedAnchor = false;
                }
                break;
            }
            rightArmCloth[armCount].AddComponent<FixedJoint2D>();
            leftArmCloth[armCount].AddComponent<FixedJoint2D>();
        }
        //腕の布のボーンを吊り下げる
        FixedJoint2D rightArmfixed = rightArmClothSP.AddComponent<FixedJoint2D>();
        rightArmfixed.connectedBody = rightArmCloth[0].GetComponent<Rigidbody2D>();
        rightArmfixed.dampingRatio = ArmClothDamping;
        rightArmfixed.frequency = ArmClothFrequency;
        rightArmfixed.anchor = new Vector2(Vector2.Distance(rightArmCloth[0].transform.position, rightArmClothSP.transform.position), 0);
        rightArmfixed.autoConfigureConnectedAnchor = false;
        rightArmClothSP.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D rightHandfixed = rightHand.AddComponent<FixedJoint2D>();
        rightHandfixed.connectedBody = rightArmCloth[rightArmCloth.Length-1].GetComponent<Rigidbody2D>();
        rightHandfixed.dampingRatio = ArmClothDamping;
        rightHandfixed.frequency = 0.0001f;
        rightHandfixed.anchor = handAnchor;
        rightHandfixed.autoConfigureConnectedAnchor = false;
        rightHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D leftArmfixed = leftArmClothSP.AddComponent<FixedJoint2D>();
        leftArmfixed.connectedBody = leftArmCloth[0].GetComponent<Rigidbody2D>();
        leftArmfixed.dampingRatio = ArmClothDamping;
        leftArmfixed.frequency = ArmClothFrequency;
        leftArmfixed.anchor = new Vector2(Vector2.Distance(leftArmCloth[0].transform.position, leftArmClothSP.transform.position), 0);
        leftArmfixed.autoConfigureConnectedAnchor = false;
        leftArmClothSP.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D leftHandfixed = leftHand.AddComponent<FixedJoint2D>();
        leftHandfixed.connectedBody = leftArmCloth[rightArmCloth.Length - 1].GetComponent<Rigidbody2D>();
        leftHandfixed.dampingRatio = ArmClothDamping;
        leftHandfixed.frequency = 0.0001f;
        leftHandfixed.anchor = handAnchor;
        leftHandfixed.autoConfigureConnectedAnchor = false;
        leftHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;


        //裾のボーン
        for(int backHemCount = 0; backHemCount < backHem.Length; backHemCount++)
        {
            //先端以外にfixedJointをつけていく
            if (backHemCount == backHem.Length - 1)
            {
                //先端だけRigidbodyをつける
                backHem[backHemCount].AddComponent<Rigidbody2D>();
                for (int i = backHem.Length - 2; i >= 0; i--)
                {
                    //それぞれのパラメータを設定。
                    //ボーン同士をつなげる、減衰比、バネの強さ、アンカー座標、自動アンカー調整機能オフを設定
                    FixedJoint2D fixedjoint = backHem[i].GetComponent<FixedJoint2D>();
                    fixedjoint.connectedBody = backHem[i + 1].GetComponent<Rigidbody2D>();
                    fixedjoint.dampingRatio = HemDamping;
                    fixedjoint.frequency = HemFrequency;
                    fixedjoint.anchor = new Vector2(Vector2.Distance(backHem[i + 1].transform.position, backHem[i].transform.position), 0);
                    fixedjoint.autoConfigureConnectedAnchor = false;
                }
                break;
            }
            backHem[backHemCount].AddComponent<FixedJoint2D>();
        }
        //裾のボーンを吊り下げる
        FixedJoint2D bottomBodyfixed = bottomBody.AddComponent<FixedJoint2D>();
        bottomBodyfixed.connectedBody = backHem[0].GetComponent<Rigidbody2D>();
        bottomBodyfixed.dampingRatio = backHemDamping;
        bottomBodyfixed.frequency = backHemFrequency;
        bottomBodyfixed.anchor = backHemAnchor;
        bottomBodyfixed.autoConfigureConnectedAnchor = false;
        bottomBody.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        FixedJoint2D frohtHemfixed = frontHem[frontHem.Length-1].AddComponent<FixedJoint2D>();
        frohtHemfixed.connectedBody = backHem[backHem.Length - 1].GetComponent<Rigidbody2D>();
        frohtHemfixed.dampingRatio = HemDamping;
        frohtHemfixed.frequency = HemFrequency;
        frohtHemfixed.anchor = frontHemAnchor;
        frohtHemfixed.autoConfigureConnectedAnchor = false;
        frontHem[frontHem.Length - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }

    void Update()
    {

    }
}
