using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
//�L�����̏����ݒ�ƁA�{�[�����[���̐ݒ�
public class BWCharaManage : MonoBehaviour
{
    public GameObject target;
    //�U�������̕z�\��
    private float ArmClothDamping = 0.00001f;//���X0.5f
    private float ArmClothFrequency = 1.5f;

    private Vector2 handAnchor = new Vector2(0.1634932f, -0.5892799f);

    //�{�[��
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



    //�������̕z�\��
    private float HemDamping = 0.5f;
    private float HemFrequency = 1.5f;
    private float backHemDamping = 1f;
    private float backHemFrequency = 4f;


    private Vector2 frontHemAnchor = new Vector2(4.78f, 0f);
    private Vector2 backHemAnchor = new Vector2(2f, 0f);

    [SerializeField, Range(0f,1f)]
    public float frontHemShrink;
    private float FRONT_HEM_RANGE = -13.14225f;

    [SerializeField, Range(-1f, 1f)]
    public float headDirection;

    [SerializeField]
    AnimationCurve hemCurve;


    LimbSolver2D limb;

    // Start is called before the first frame update
    void Start()
    {
        //�{�[�����A�^�b�`
        root = transform.Find("root").gameObject;
        bottomBody = root.transform.Find("bottomBody").gameObject;
        BoneControl boneControl = bottomBody.AddComponent<BoneControl>();
        boneControl.min = -75f;
        boneControl.max = 30f;
        //backHem���擾
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



        //frontHem���擾
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
        frontBottomHood = head.transform.Find("frontBottomHood").gameObject;
        backUpHood = head.transform.Find("backUpHood").gameObject;
        backBottomHood = head.transform.Find("backBottomHood").gameObject;
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

        //rightArmCloth��letArmCloth���擾
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
        boneControl.max = 305.109f;
        boneControl.parentObject = bottomBody;
        leftBottomLeg = leftUpLeg.transform.Find("leftBottomLeg").gameObject;
        boneControl = leftBottomLeg.AddComponent<BoneControl>();
        boneControl.min = -161f;
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
        boneControl.max = 305.109f;
        boneControl.parentObject = bottomBody;
        rightBottomLeg = rightUpLeg.transform.Find("rightBottomLeg").gameObject;
        boneControl = rightBottomLeg.AddComponent<BoneControl>();
        boneControl.min = -161f;
        boneControl.max = 0f;
        rightFoot = rightBottomLeg.transform.Find("rightFoot").gameObject;
        boneControl = rightFoot.AddComponent<BoneControl>();
        boneControl.min = 37f;
        boneControl.max = 108f;
        rightToe = rightFoot.transform.Find("rightToe").gameObject;
        boneControl = rightToe.AddComponent<BoneControl>();
        boneControl.min = 11f;
        boneControl.max = 61f;

        //�r�̕z�̃{�[��
        for (int armCount = 0; armCount < rightArmCloth.Length; armCount++)
        {

            /*boneControl  = rightArmCloth[armCount].AddComponent<BoneControl>();
            boneControl.min = 0f;
            boneControl.max = 90f;
            boneControl = leftArmCloth[armCount].AddComponent<BoneControl>();
            boneControl.min = 0f;
            boneControl.max = 90f;*/
            
            //��[�ȊO��fixedJoint�����Ă���
            if (armCount == rightArmCloth.Length - 1)
            {
                //��[����Rigidbody������
                rightArmCloth[armCount].AddComponent<Rigidbody2D>();
                leftArmCloth[armCount].AddComponent<Rigidbody2D>();
                for (int i = rightArmCloth.Length - 2; i >= 0; i--)
                {
                    //���ꂼ��̃p�����[�^��ݒ�B
                    //�{�[�����m���Ȃ���A������A�o�l�̋����A�A���J�[���W�A�����A���J�[�����@�\�I�t��ݒ�
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
        //�����r���ђʂ��Ȃ��p�̏���    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*for(int armCount = 1; armCount < rightArmCloth.Length; armCount++)
        {
            CircleCollider2D tmpCircleCollider2D = rightArmCloth[armCount].AddComponent<CircleCollider2D>();
            Rigidbody2D tmpRigidbody2D = rightArmCloth[armCount].GetComponent<Rigidbody2D>();
        }
        BoxCollider2D tmpBoxCollider2D =  rightBottomArm.AddComponent<BoxCollider2D>();
        tmpBoxCollider2D.offset = new Vector2(1.96f, 0f);
        tmpBoxCollider2D.size = new Vector2(3.91f, 1f);
        tmpBoxCollider2D = rightUpArm.AddComponent<BoxCollider2D>();
        tmpBoxCollider2D.offset = new Vector2(2.18f, 0f);
        tmpBoxCollider2D.size = new Vector2(4.27f, 1f);*/





        //�r�̕z�̃{�[����݂艺����
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


        //���̃{�[��
        for(int backHemCount = 0; backHemCount < backHem.Length; backHemCount++)
        {
            //��[�ȊO��fixedJoint�����Ă���
            if (backHemCount == backHem.Length - 1)
            {
                //��[����Rigidbody������
                backHem[backHemCount].AddComponent<Rigidbody2D>();
                for (int i = backHem.Length - 2; i >= 0; i--)
                {
                    //���ꂼ��̃p�����[�^��ݒ�B
                    //�{�[�����m���Ȃ���A������A�o�l�̋����A�A���J�[���W�A�����A���J�[�����@�\�I�t��ݒ�
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
        //���̃{�[����݂艺����
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

        hemCurve = new AnimationCurve(
        new Keyframe(235f, 1f),
        new Keyframe(244.309f, 0.905f),
        new Keyframe(257.941f, 0.743f),
        new Keyframe(270.822f, 0.573f),
        new Keyframe(280.978f, 0.428f),
        new Keyframe(287.673f, 0.324f),
        new Keyframe(295.121f, 0.204f),
        new Keyframe(297.98f, 0.154f),
        new Keyframe(301.179f, 0.104f),
        new Keyframe(305.109f, 0.021f)
        );
        for(int i = 0; i < hemCurve.length; i++)
        {
            hemCurve.SmoothTangents(i, 0);
        }

        //IK�ݒ�
        IKManager2D ik = root.AddComponent<IKManager2D>();
        GameObject rightArmik = new GameObject("rightArmIK");
        rightArmik.transform.parent = root.transform;
        limb = rightArmik.AddComponent<LimbSolver2D>();
        //limb.UpdateIK();
        //Effector��Target����������ݒ肷����@��T��
        ik.AddSolver(limb);
        /*IKChain2D��Effector�̌��c�݂����Ȃ���������B�ł��w�肵�Ă����ɕς�炸*/
        //IKChain2D ikch = new IKChain2D();
        //ikch.effector = rightHand.transform;
        /*LimbSolver2DEditor.cs�ɃC���X�y�N�^���Effector�Ƃ������O���w�肵�Ă���̂��������B*/
        /*
         *�����炭����̓C���X�y�N�^��ɕ\�����ē���̖��O��t���邽�߂̂�ŁA�{���ł͂Ȃ��B
         *�Ȃ炻�̖{���̕ϐ��͂ǂ��H�\��������̂�LImbSolver2D�������͂��̌p������Solver2D
         *�ǂ�����ĒT�������H
         * EditorGUILayout.PropertyField(m_ChainProperty.FindPropertyRelative("m_EffectorTransform"), Contents.effectorLabel);
         * m_ChainProperty = serializedObject.FindProperty("m_Chain");�Ȃ̂ŁA�ǂ�����m_Chain�Ƃ����v���p�e�B�����݂���͂��B
         * ����Ɋ֘A�������m_EffectorTransform�H���ꂪEffector�Ƃ�Target�̖{���ȋC������B
         * ���Ⴀm_Chain��T�����B
         * LimbSolver2D.cs�ɔ����B
         * private IKChain2D m_Chain = new IKChain2D();�@IKChain2D�炵���Bm_Chain.effector�Ƃ����������Ƃ��炱�̒��ɂ��肻��
         * IKChain2D��T�����BPackages/com.unity.2d.animation/IK/Runtime/IKChain2D.cs�ɂ������B
         * IKChain2D.cs�̒���effector��T�����B
         * m_Target��m_Effector�Ɩ��O�̂���ꂽTransform m_TargetTransform��m_EffecterTransform�𔭌�
         * private����set,get�̂������̂Ŏg������
         * public Transform target���p������Ή\�H
         * IKChain2D m_Chain = new IKChain2D();
            m_Chain.effector = rightHand.transform;
            m_Chain.target = target.transform;
         * ��������Ă��ω��Ȃ��B�G���[���Ȃ��BIK�I�u�W�F�N�g�Ɋ֘A�Â��ĂȂ����炩���B
         * ���̃v���O�����Œ�`���Ă�LimbSolver2D�Ƃ�IKManager�̕ϐ��z���ɐݒ�ł��邩�H�������͂�������Ǝ��ɉ��炩�̌p�������邩
         * �܂���limb����ǂ��ɂ��A�N�Z�X�ł��邩�������B
         * 
         * limb�̒��g�͉E���AddComponent���ꂽLimbSolver2D�B
         * LimbSolver2D�ɂ�IKChain2D.cs���i�[���Ă���m_chain������B
         * IKChain2D�ɂ�effector�̖{���ł���m_EffectorTransform������
         * IKChain2D�ɂ̓Z�b�^�[�Ƃ��ā@effector�֐�������
         * LimbSolver2D����m_chain�ɃA�N�Z�X���āA��������m_chain.effector�݂����ɍs���邩
         * 
         * LimbSolver2D��m_chain�̒�`���܂܂�Ă��炸�E�E�E
         * �����炭m_Chain��private�Œ�`����Ă邩��B�Z�b�^�[����ɍ������{���邩�ȁBSerializeField���āA�֐��O����̓A�N�Z�X�ł��Ȃ����ǃC���X�y�N�^�ɂ͏o���݂����Ȃ�������B
         * �Q�b�^�[�̕�����ɍ�����牽�炩�̗͂ɂ���ď��ł����B
         * LImbSolver2D��public override IKChain2D GetChain(int index)���Ă̂��������B����return m_chain����������擾�ł��邩���B����index�������킩��Ȃ��B
         * limb.GetChain() ���@LimbSolver2D.Getchain(int)�̕K�v�ȉ��p�����[�^'index�ɑΉ��������̈���������܂���B�����h�L�������g��Description�͋󔒁B
         * �ł�GetChain����index���g���ĉ������������Ă��Ȃ��B
         * IKChain2D���I�[�o�[���C�h���Ă�Ȃ�IKChain2D.cs��GetChain�����ɍs���΂����H
         * IKChain2D��GetChain�Ȃ�ĕ�����͂Ȃ������B�K���Ɉ�U0�Ƃ��ł���Ă݂悤����
         * IKChain2D m_Chain  = limb.GetChain(0); 
         * m_Chain.effector = target.transform;�@�@�@�@�@�@�@�@�@�@�����BrightArmIK��LimbSolver2D��Effector��target(Transform�j�����܂���
         */
        IKChain2D m_Chain  = limb.GetChain(0);
        m_Chain.effector = rightHand.transform;
        m_Chain.target = target.transform;
        
    }

    void Update()
    {
        //���̐���
        frontHem[0].transform.localEulerAngles = new Vector3(0, 0, -70 + FRONT_HEM_RANGE * frontHemShrink);
        for (int i = 1; i < frontHem.Length; i++)
        {
            frontHem[i].transform.localEulerAngles = new Vector3(0, 0, FRONT_HEM_RANGE * frontHemShrink);
        }
        float leftLegAngle = UnityEditor.TransformUtils.GetInspectorRotation(leftUpLeg.transform).z;
        float rightLegAngle = UnityEditor.TransformUtils.GetInspectorRotation(rightUpLeg.transform).z;
        float legAngle = (leftLegAngle > rightLegAngle) ? leftLegAngle : rightLegAngle;
        frontHemShrink = hemCurve.Evaluate(legAngle);

        //�t�[�h�̐���
        if (headDirection < 0)
        {
            frontUpHood.transform.localEulerAngles = new Vector3(0, 0, 157 + headDirection * 25);
            frontBottomHood.transform.localEulerAngles = new Vector3(0, 0, 44 + -headDirection * 36);
            backUpHood.transform.localEulerAngles = new Vector3(0, 0, 164 + -headDirection * 40);
            backBottomHood.transform.localEulerAngles = new Vector3(0, 0, 28 + headDirection * 84);
        }
        else
        {
            frontUpHood.transform.localEulerAngles = new Vector3(0, 0, 157 + headDirection * 56);
            frontBottomHood.transform.localEulerAngles = new Vector3(0, 0, 44 + -headDirection * 101);
        }
        //�U���̕t�����̐���

        //�r�@-207��18
        //���@-20��-180
        float leftArmAngleRate = (UnityEditor.TransformUtils.GetInspectorRotation(leftUpArm.transform).z + 207) / 225f;
        leftArmClothSP.transform.localEulerAngles = new Vector3(0,0, -20 - (160 * leftArmAngleRate));
        float rightArmAngleRate = (UnityEditor.TransformUtils.GetInspectorRotation(rightUpArm.transform).z + 207) / 225f;
        rightArmClothSP.transform.localEulerAngles = new Vector3(0, 0, -20 - (160 * rightArmAngleRate));

        //�����r���ђʂ��Ȃ��悤�ɂ���
        //rightArmCloth[6]�ƁArightBottomArm����rightHand
        int changeLord = 3;//�r�̐��̐�[���炱�̌������O�r�ɓ�����悤�ɂ���
        for(int i = 0; i < rightArmCloth.Length; i++)
        {
            Vector3 bottomClothLocalPos = rightBottomArm.transform.InverseTransformPoint(rightArmCloth[i].transform.position);
            Vector3 upClothLocalPos = rightUpArm.transform.InverseTransformPoint(rightArmCloth[i].transform.position);
            if (bottomClothLocalPos.x > 0 && bottomClothLocalPos.x < rightHand.transform.localPosition.x)//�O�r�͈͓̔���
            {
                if (bottomClothLocalPos.y >= -1 && bottomClothLocalPos.y <=1)//�r���z���Ă邩
                {
                    //rightArmCloth[6].transform.Translate(new Vector3(0, -(clothLocalPos.y + 1), 0));
                    if (i >= rightArmCloth.Length - changeLord)//���̈ꕔ�i�O�r�ɋ߂������j�����������Ȃ��悤�ɂ���
                    {
                        //rightArmCloth[i].GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * (bottomClothLocalPos.y + 1) * 10, ForceMode2D.Impulse);
                    }
                }
            }
            if (upClothLocalPos.x > 0 && upClothLocalPos.x < rightHand.transform.localPosition.x)//��r�͈͓̔���
            {
                if (upClothLocalPos.y >= -1 && upClothLocalPos.y <= 1)//�r���z���Ă邩
                {
                    if (i < rightArmCloth.Length - changeLord)//���̈ꕔ�i�O�r�ɋ߂������j�����������Ȃ��悤�ɂ���
                    {
                        //rightArmCloth[i].GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * (upClothLocalPos.y + 1) * 10, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
