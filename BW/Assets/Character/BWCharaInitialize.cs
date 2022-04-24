using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWCharaInitialize : MonoBehaviour
{
    //振袖部分の布表現
    private float ArmClothDamping = 0.5f;
    private float ArmClothFrequency = 1.5f;

    private Vector2 upArmAnchor = new Vector2(-0.2993939f, -1.763251f);
    private Vector2 handAnchor = new Vector2(0.1634932f, -0.5892799f);

    public GameObject[] rightArmCloth;
    public GameObject rightUpArm;
    public GameObject rightHand;

    public GameObject[] leftArmCloth;
    public GameObject leftUpArm;
    public GameObject leftHand;

    //裾部分の布表現
    public GameObject[] frontHem;
    public GameObject[] backHem;
    // Start is called before the first frame update
    void Start()
    {
        //腕の布のボーン
        for(int armCount = 0; armCount < rightArmCloth.Length; armCount++)
        {
            if(armCount == rightArmCloth.Length - 1)
            {
                rightArmCloth[armCount].AddComponent<Rigidbody2D>();
                leftArmCloth[armCount].AddComponent<Rigidbody2D>();
                for (int i = rightArmCloth.Length - 2; i >= 0; i--)
                {
                    FixedJoint2D fixedjoint = rightArmCloth[i].GetComponent<FixedJoint2D>();
                    fixedjoint.connectedBody = rightArmCloth[i + 1].GetComponent<Rigidbody2D>();
                    fixedjoint.dampingRatio = ArmClothDamping;
                    fixedjoint.frequency = ArmClothFrequency;
                    fixedjoint.anchor = new Vector2(Vector2.Distance(rightArmCloth[i + 1].transform.position, rightArmCloth[i].transform.position), 0);
                    fixedjoint.autoConfigureConnectedAnchor = false;

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
        FixedJoint2D rightArmfixed = rightUpArm.AddComponent<FixedJoint2D>();
        rightArmfixed.connectedBody = rightArmCloth[0].GetComponent<Rigidbody2D>();
        rightArmfixed.dampingRatio = ArmClothDamping;
        rightArmfixed.frequency = ArmClothFrequency;
        rightArmfixed.anchor = upArmAnchor;
        rightArmfixed.autoConfigureConnectedAnchor = false;
        rightUpArm.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D rightHandfixed = rightHand.AddComponent<FixedJoint2D>();
        rightHandfixed.connectedBody = rightArmCloth[rightArmCloth.Length-1].GetComponent<Rigidbody2D>();
        rightHandfixed.dampingRatio = ArmClothDamping;
        rightHandfixed.frequency = ArmClothFrequency;
        rightHandfixed.anchor = handAnchor;
        rightHandfixed.autoConfigureConnectedAnchor = false;
        rightHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D leftArmfixed = leftUpArm.AddComponent<FixedJoint2D>();
        leftArmfixed.connectedBody = leftArmCloth[0].GetComponent<Rigidbody2D>();
        leftArmfixed.dampingRatio = ArmClothDamping;
        leftArmfixed.frequency = ArmClothFrequency;
        leftArmfixed.anchor = upArmAnchor;
        leftArmfixed.autoConfigureConnectedAnchor = false;
        leftUpArm.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        FixedJoint2D leftHandfixed = leftHand.AddComponent<FixedJoint2D>();
        leftHandfixed.connectedBody = leftArmCloth[rightArmCloth.Length - 1].GetComponent<Rigidbody2D>();
        leftHandfixed.dampingRatio = ArmClothDamping;
        leftHandfixed.frequency = ArmClothFrequency;
        leftHandfixed.anchor = handAnchor;
        leftHandfixed.autoConfigureConnectedAnchor = false;
        leftHand.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}
