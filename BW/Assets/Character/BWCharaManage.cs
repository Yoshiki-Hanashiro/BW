using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//キャラの初期設定と、ボーンルールの設定
public class BWCharaManage : MonoBehaviour
{
    //振袖部分の布表現
    private float ArmClothDamping = 0.5f;
    private float ArmClothFrequency = 1.5f;

    private Vector2 handAnchor = new Vector2(0.1634932f, -0.5892799f);

    //ボーン
    public GameObject root;
    public GameObject head;
    public GameObject upBody;
    public GameObject bottomBody;
    public GameObject frontUpHood;
    public GameObject frontBottomHood;
    public GameObject backUpHood;
    public GameObject backBottomHood;

    public GameObject rightUpLeg;
    public GameObject rightBottomLeg;
    public GameObject rightFoot;
    public GameObject rightToe;
    public GameObject leftUpLeg;
    public GameObject leftBottomLeg;
    public GameObject leftFoot;
    public GameObject leftToe;

    public GameObject rightUpArm;
    public GameObject rightBottomArm;
    public GameObject rightArmClothSP;
    public GameObject[] rightArmCloth;
    public GameObject rightHand;
    public GameObject rightFinger;
    public GameObject rightThumb;

    public GameObject leftUpArm;
    public GameObject leftBottomArm;
    public GameObject leftArmClothSP;
    public GameObject[] leftArmCloth;
    public GameObject leftHand;
    public GameObject leftFinger;
    public GameObject leftThumb;

    public GameObject[] frontHem;
    public GameObject[] backHem;



    //裾部分の布表現
    private float HemDamping = 0.5f;
    private float HemFrequency = 1.5f;
    private float backHemDamping = 1f;
    private float backHemFrequency = 4f;


    private Vector2 frontHemAnchor = new Vector2(4.78f, 0f);
    private Vector2 backHemAnchor = new Vector2(2f, 0f);

    [SerializeField, Range(0.096f,1f)]
    public float frontHemShrink;
    private float FRONT_HEM_RANGE = -13.14225f;


    // Start is called before the first frame update
    void Start()
    {
        //ボーンをアタッチ
        root = transform.Find("root").gameObject;
        bottomBody = root.transform.Find("bottomBody").gameObject;
        BoneControl boneControl = bottomBody.AddComponent<BoneControl>();
        boneControl.min = -75f;
        boneControl.max = 30f;
        //backHemを取得
        GameObject tmp = bottomBody.transform.GetChild(0).gameObject;
        int count = 0;
        while (true)
        {
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {

                break;
            }
            count++;
        }
        backHem = new GameObject[count+1];
        count = 0;
        tmp = bottomBody.transform.GetChild(0).gameObject;
        while (true)
        {
            backHem[count] = tmp;
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            count++;
        }



        //frontHemを取得
        tmp = bottomBody.transform.GetChild(1).gameObject;
        count = 0;
        while (true)
        {
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            count++;
        }
        frontHem = new GameObject[count + 1];
        count = 0;
        tmp = bottomBody.transform.GetChild(1).gameObject;
        while (true)
        {
            frontHem[count] = tmp;
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            count++;
        }

        upBody = bottomBody.transform.Find("upBody").gameObject;
        boneControl = upBody.AddComponent<BoneControl>();
        boneControl.min = -40f;
        boneControl.max = 25f;
        head = upBody.transform.Find("head").gameObject;
        boneControl = head.AddComponent<BoneControl>();
        boneControl.min = -40f;
        boneControl.max = 30f;
        frontUpHood = head.transform.Find("frontUpHood").gameObject;
        boneControl = frontUpHood.AddComponent<BoneControl>();
        boneControl.min = 132f;
        boneControl.max = 231f;
        frontBottomHood = head.transform.Find("frontBottomHood").gameObject;
        boneControl = frontBottomHood.AddComponent<BoneControl>();
        boneControl.min = -50f;
        boneControl.max = 80f;
        backUpHood = head.transform.Find("backUpHood").gameObject;
        boneControl = backUpHood.AddComponent<BoneControl>();
        boneControl.min = 124f;
        boneControl.max = 226f;
        backBottomHood = head.transform.Find("backBottomHood").gameObject;
        boneControl = backBottomHood.AddComponent<BoneControl>();
        boneControl.min = -58f;
        boneControl.max = 80f;
        leftUpArm = upBody.transform.Find("leftUpArm").gameObject;
        boneControl = leftUpArm.AddComponent<BoneControl>();
        boneControl.min = -207f;
        boneControl.max = 18f;
        leftArmClothSP = leftUpArm.transform.Find("leftArmClothSP").gameObject;


        leftBottomArm = leftUpArm.transform.Find("leftBottomArm").gameObject;
        boneControl = leftBottomArm.AddComponent<BoneControl>();
        boneControl.min = 0f;
        boneControl.max = 145f;
        leftHand = leftBottomArm.transform.Find("leftHand").gameObject;
        boneControl = leftHand.AddComponent<BoneControl>();
        boneControl.min = -32f;
        boneControl.max = 90f;
        leftFinger = leftHand.transform.Find("leftFinger").gameObject;
        boneControl = leftFinger.AddComponent<BoneControl>();
        boneControl.min = -104f;
        boneControl.max = 56f;
        leftThumb = leftHand.transform.Find("leftThumb").gameObject;
        boneControl = leftThumb.AddComponent<BoneControl>();
        boneControl.min = -55f;
        boneControl.max = 0f;
        rightUpArm = upBody.transform.Find("rightUpArm").gameObject;
        boneControl = rightUpArm.AddComponent<BoneControl>();
        boneControl.min = -207f;
        boneControl.max = 18f;
        rightArmClothSP = rightUpArm.transform.Find("rightArmClothSP").gameObject;
        //rightArmClothとletArmClothを取得
        tmp = leftArmClothSP.transform.GetChild(0).gameObject;
        count = 0;
        while (true)
        {
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            count++;
        }
        leftArmCloth = new GameObject[count + 1];
        rightArmCloth = new GameObject[count + 1];
        count = 0;
        tmp = leftArmClothSP.transform.GetChild(0).gameObject;
        GameObject rightTmp = rightArmClothSP.transform.GetChild(0).gameObject;
        while (true)
        {
            leftArmCloth[count] = tmp;
            rightArmCloth[count] = rightTmp;
            try
            {
                tmp = tmp.transform.GetChild(0).gameObject;
                rightTmp = rightTmp.transform.GetChild(0).gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            count++;
        }
        rightBottomArm = rightUpArm.transform.Find("rightBottomArm").gameObject;
        boneControl = rightBottomArm.AddComponent<BoneControl>();
        boneControl.min = 0f;
        boneControl.max = 145f;
        rightHand = rightBottomArm.transform.Find("rightHand").gameObject;
        boneControl = rightHand.AddComponent<BoneControl>();
        boneControl.min = -32f;
        boneControl.max = 90f;
        rightFinger = rightHand.transform.Find("rightFinger").gameObject;
        boneControl = rightFinger.AddComponent<BoneControl>();
        boneControl.min = -104f;
        boneControl.max = 56f;
        rightThumb = rightHand.transform.Find("rightThumb").gameObject;
        boneControl = rightThumb.AddComponent<BoneControl>();
        boneControl.min = -55f;
        boneControl.max = 0f;
        leftUpLeg = root.transform.Find("leftUpLeg").gameObject;
        boneControl = leftUpLeg.AddComponent<BoneControl>();
        boneControl.min = 150f;
        boneControl.max = 300f;
        boneControl.parentObject = bottomBody;
        leftBottomLeg = leftUpLeg.transform.Find("leftBottomLeg").gameObject;
        boneControl = leftBottomLeg.AddComponent<BoneControl>();
        boneControl.min = -152f;
        boneControl.max = 0f;
        leftFoot = leftBottomLeg.transform.Find("leftFoot").gameObject;
        boneControl = leftFoot.AddComponent<BoneControl>();
        boneControl.min = 37f;
        boneControl.max = 108f;
        leftToe = leftFoot.transform.Find("leftToe").gameObject;
        boneControl = leftToe.AddComponent<BoneControl>();
        boneControl.min = 11f;
        boneControl.max = 61f;
        rightUpLeg = root.transform.Find("rightUpLeg").gameObject;
        boneControl = rightUpLeg.AddComponent<BoneControl>();
        boneControl.min = 150f;
        boneControl.max = 300f;
        boneControl.parentObject = bottomBody;
        rightBottomLeg = rightUpLeg.transform.Find("rightBottomLeg").gameObject;
        boneControl = rightBottomLeg.AddComponent<BoneControl>();
        boneControl.min = -152f;
        boneControl.max = 0f;
        rightFoot = rightBottomLeg.transform.Find("rightFoot").gameObject;
        boneControl = rightFoot.AddComponent<BoneControl>();
        boneControl.min = 37f;
        boneControl.max = 108f;
        rightToe = rightFoot.transform.Find("rightToe").gameObject;
        boneControl = rightToe.AddComponent<BoneControl>();
        boneControl.min = 11f;
        boneControl.max = 61f;

        //腕の布のボーン
        for (int armCount = 0; armCount < rightArmCloth.Length; armCount++)
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
        frontHem[0].transform.localEulerAngles = new Vector3(0, 0, -70 + FRONT_HEM_RANGE * frontHemShrink);
        for (int i = 1; i < frontHem.Length; i++)
        {
            frontHem[i].transform.localEulerAngles = new Vector3(0, 0, FRONT_HEM_RANGE * frontHemShrink);
        }

    }
}
