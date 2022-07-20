using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewJoystickControl : MonoBehaviour
{
    public GameObject newJoystick;

    public Transform Player;        // 플레이어.
    public Rigidbody myRigid;
    public float speed;
    public float turnSensitivity;
    public GameObject Lwheel;
    public GameObject Rwheel;


    public Text joyVecXText;
    public Text joyVecYText;
    float joyVexXfloat;
    float joyVexYfloat;


    void Start()
    {
        
    }


    void Update()
    {
        JoystickForwardAMRBody();
        JoystickRotatieAMRBody();
        ShowJoyVec();
    }

    void ShowJoyVec()
    {
        joyVexXfloat = (float)Math.Truncate(newJoystick.transform.localPosition.x );
        joyVexYfloat = (float)Math.Truncate(newJoystick.transform.localPosition.y);

        print("JoyVec.x : " + joyVexXfloat);
        print("JoyVec.y : " + joyVexYfloat);


        joyVecXText.GetComponent<Text>().text = "X : " + joyVexXfloat;
        joyVecYText.GetComponent<Text>().text = "Y : " + joyVexYfloat;
    }

    private void JoystickForwardAMRBody()     //조이스틱 전후진 입력
    {
        //float Xmove = JoyVec.x;
        float Zmove = newJoystick.transform.localPosition.y;
        //float Xmove = Input.GetAxisRaw("Horizontal");
        //float Zmove = Input.GetAxisRaw("Vertical");

        //Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = Player.transform.forward * Zmove;

        Vector3 rVec = moveV * speed * 5f * Time.deltaTime;

        myRigid.MovePosition(Player.transform.position + rVec);

        /*
        if (MoveFlag)
        {
            Player.transform.Translate(Vector3.forward * Zmove * Time.deltaTime * speed);
        }
        */

        joyVexXfloat = newJoystick.transform.localPosition.x;
        joyVexYfloat = newJoystick.transform.localPosition.y/10;
        AMRWheelForwardRotation(joyVexYfloat);
    }

    void JoystickRotatieAMRBody()       //조이스틱 좌우 입력
    {
        float Ymove = newJoystick.transform.localPosition.x;
        Vector3 bodyRotationY = new Vector3(0, Ymove, 0) * turnSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(bodyRotationY));

        //AMRWheelRightRotation(Ymove);
    }

    void AMRWheelForwardRotation(float Vmove)     //입력에 따른 휠 회전
    {

        LwheelRotate(Vmove);
        RwheelRotate(Vmove);
    }

    void AMRWheelRightRotation(float Hmove)
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
    public void RwheelRotate(float acc)       //R휠 회전 
    {
        Rwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

    public void LwheelRotate(float acc)           //L휠 회전
    {
        Lwheel.transform.Rotate(new Vector3(0, acc, 0));
    }

}
