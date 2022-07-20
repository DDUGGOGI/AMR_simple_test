using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class JoyStickControl : MonoBehaviour
{

    // 공개
    public Transform Player;        // 플레이어.
    public Rigidbody myRigid;
    public Transform Stick;         // 조이스틱.
    public float speed;
    public float turnSensitivity;
    public GameObject Lwheel;
    public GameObject Rwheel;

    public Text joyVecXText;
    public Text joyVecYText;
    float joyVexXfloat;
    float joyVexYfloat;

    //중력 가속도의 크기
    public float gravity = -20;

    // 비공개
    private Vector3 StickFirstPos;  // 조이스틱의 처음 위치.
    private Vector3 JoyVec;         // 조이스틱의 벡터(방향)
    private float Radius;           // 조이스틱 배경의 반 지름.
    private bool MoveFlag;          // 플레이어 움직임 스위치.

    

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // 캔버스 크기에대한 반지름 조절.
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

    // 드래그
    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
        JoyVec = (Pos - StickFirstPos).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는 곳으로 이동.
        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
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

    // 드래그 끝.
    public void DragEnd()
    {
        Stick.position = StickFirstPos; // 스틱을 원래의 위치로.
        JoyVec = Vector3.zero;          // 방향을 0으로.
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
    private void JoystickForwardAMRBody()     //조이스틱 전후진 입력
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

    void JoystickRotatieAMRBody()       //조이스틱 좌우 입력
    {
        float Ymove = JoyVec.x;
        Vector3 bodyRotationY = new Vector3(0, Ymove, 0) * turnSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(bodyRotationY));

        //AMRWheelRightRotation(Ymove);
    }

    void AMRWheelForwardRotation(float Vmove)     //입력에 따른 휠 회전
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
    public void RwheelRotate(int acc)       //R휠 회전 
    {
        Rwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

    public void LwheelRotate(int acc)           //L휠 회전
    {
        Lwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

    /*
    private void CCMove()
    {
        //사용자의 입력을 받는다.
        //float h = JoyVec.x);
        //float v = JoyVec.y);
        //방향을 만든다.
        Vector3 dir = new Vector3(JoyVec.x, 0, JoyVec.y);
        //사용자가 보는 방향 dir
        dir = Camera.main.transform.TransformDirection(dir);
        //중력을 적용한 수직방향 추가
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