using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class JoyStickControl : MonoBehaviour
{

    // ����
    public Transform Player;        // �÷��̾�.
    public Rigidbody myRigid;
    public Transform Stick;         // ���̽�ƽ.
    public float speed;
    public float turnSensitivity;
    public GameObject Lwheel;
    public GameObject Rwheel;

    public Text joyVecXText;
    public Text joyVecYText;
    float joyVexXfloat;
    float joyVexYfloat;

    //�߷� ���ӵ��� ũ��
    public float gravity = -20;

    // �����
    private Vector3 StickFirstPos;  // ���̽�ƽ�� ó�� ��ġ.
    private Vector3 JoyVec;         // ���̽�ƽ�� ����(����)
    private float Radius;           // ���̽�ƽ ����� �� ����.
    private bool MoveFlag;          // �÷��̾� ������ ����ġ.

    

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    void Update()
    {
        //CCMove();
        JoystickForwardAMRBody();
        JoystickRotatieAMRBody();
        ShowJoyVec();
    }

    // �巡��
    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // ���̽�ƽ�� �̵���ų ������ ����.(������,����,��,�Ʒ�)
        JoyVec = (Pos - StickFirstPos).normalized;

        // ���̽�ƽ�� ó�� ��ġ�� ���� ���� ��ġ�ϰ��ִ� ��ġ�� �Ÿ��� ���Ѵ�.
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        // �Ÿ��� ���������� ������ ���̽�ƽ�� ���� ��ġ�ϰ� �ִ� ������ �̵�.
        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        // �Ÿ��� ���������� Ŀ���� ���̽�ƽ�� �������� ũ�⸸ŭ�� �̵�.
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        //Vector3 dir = new Vector3(JoyVec.x, 0, JoyVec.y);
        //Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        //cc.Move(dir * speed * Time.deltaTime);
    }

    void ShowJoyVec()
    {
        joyVexXfloat = (float)Math.Truncate(JoyVec.x * 1000f) / 1000f;
        joyVexYfloat =(float)Math.Truncate( JoyVec.y*1000f) /1000f; 

        print("JoyVec.x : " + JoyVec.x);
        print("JoyVec.y : " + JoyVec.y);


        joyVecXText.GetComponent<Text>().text = "X : "+joyVexXfloat;
        joyVecYText.GetComponent<Text>().text = "Y : " + joyVexYfloat;
    }

    // �巡�� ��.
    public void DragEnd()
    {
        Stick.position = StickFirstPos; // ��ƽ�� ������ ��ġ��.
        JoyVec = Vector3.zero;          // ������ 0����.
        MoveFlag = false;
    }

    public void JoystickMoveWorld()
    {
        if (MoveFlag)
        {
            Player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            LwheelRotate(10);
            RwheelRotate(10);
        }
    }
    private void JoystickForwardAMRBody()     //���̽�ƽ ������ �Է�
    {
        //float Xmove = JoyVec.x;
        float Zmove = JoyVec.y;
        //float Xmove = Input.GetAxisRaw("Horizontal");
        //float Zmove = Input.GetAxisRaw("Vertical");

        //Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = Player.transform.forward * Zmove;

        Vector3 rVec = moveV * speed *5f* Time.deltaTime;

        myRigid.MovePosition(Player.transform.position+rVec);

        /*
        if (MoveFlag)
        {
            Player.transform.Translate(Vector3.forward * Zmove * Time.deltaTime * speed);
        }
        */

        AMRWheelForwardRotation(Zmove);


    }

    void JoystickRotatieAMRBody()       //���̽�ƽ �¿� �Է�
    {
        float Ymove = JoyVec.x;
        Vector3 bodyRotationY = new Vector3(0, Ymove, 0) * turnSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(bodyRotationY));

        //AMRWheelRightRotation(Ymove);
    }

    void AMRWheelForwardRotation(float Vmove)     //�Է¿� ���� �� ȸ��
    {
        if (Vmove > 0)
        {
            LwheelRotate(10);
            RwheelRotate(10);
        }
        else if (Vmove < 0)
        {
            LwheelRotate(-10);
            RwheelRotate(-10);
        }
    }

    void AMRWheelRightRotation( float Hmove)
    {
        if (Hmove > 0)
        {
            LwheelRotate(10);
            RwheelRotate(-10);
        }
        else if (Hmove < 0)
        {
            LwheelRotate(-10);
            RwheelRotate(10);
        }
    }
    public void RwheelRotate(int acc)       //R�� ȸ�� 
    {
        Rwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

    public void LwheelRotate(int acc)           //L�� ȸ��
    {
        Lwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

    /*
    private void CCMove()
    {
        //������� �Է��� �޴´�.
        //float h = JoyVec.x);
        //float v = JoyVec.y);
        //������ �����.
        Vector3 dir = new Vector3(JoyVec.x, 0, JoyVec.y);
        //����ڰ� ���� ���� dir
        dir = Camera.main.transform.TransformDirection(dir);
        //�߷��� ������ �������� �߰�
        yVelocity += gravity * Time.deltaTime;
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
    */
}