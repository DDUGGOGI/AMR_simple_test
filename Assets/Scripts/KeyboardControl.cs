using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl: MonoBehaviour
{
    public Rigidbody myRigid;
    public float speed;

    public float turnSensitivity;

    public GameObject player;


    public GameObject Lwheel;
    public GameObject Rwheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveAMRBody();
        RotatieAMRBody();
        AMRWheelRotation();
    }

    void MoveAMRBody()      //Ű���� ������ �Է¿� ���� ���� ���� ó��
    {
        //float Xmove = Input.GetAxisRaw("Horizontal");
        float Zmove = Input.GetAxisRaw("Vertical");

        //Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = transform.forward * Zmove;

        // Vector3 rVec = (moveH + moveV).normalized * speed * Time.deltaTime;
        Vector3 rVec = (moveV).normalized * speed * Time.deltaTime;
        myRigid.MovePosition(transform.position + rVec);
    }

    void RotatieAMRBody()       //Ű���� �¿� �Է¿� ���� ȸ�� ó��
    {
        float Ymove = Input.GetAxisRaw("Horizontal");
        Vector3 bodyRotationY = new Vector3(0, Ymove, 0) * turnSensitivity/1000;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(bodyRotationY));
    }

    void AMRWheelRotation()     //�Է¿� ���� �� ȸ��
    {
        float Zmove = Input.GetAxisRaw("Vertical");
        float Ymove = Input.GetAxisRaw("Horizontal");
        if (Zmove > 0)
        {
            LwheelRotate(10);
            RwheelRotate(10);
        }
        else if (Zmove < 0)
        {
            LwheelRotate(-10);
            RwheelRotate(-10);
        }
        else if (Ymove > 0)
        {
            LwheelRotate(10);
            RwheelRotate(-10);
        }
        else if (Ymove<0)
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
}
