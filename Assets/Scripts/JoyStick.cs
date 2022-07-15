using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{

    // ����
    public Transform Player;        // �÷��̾�.
    public Rigidbody myRigid;
    public Transform Stick;         // ���̽�ƽ.
    public int speed;
    public GameObject Lwheel;
    public GameObject Rwheel;

    public CharacterController cc;
    //�߷� ���ӵ��� ũ��
    public float gravity = -20;
    //���� �ӵ�
    float yVelocity = 0;

    // �����
    private Vector3 StickFirstPos;  // ���̽�ƽ�� ó�� ��ġ.
    private Vector3 JoyVec;         // ���̽�ƽ�� ����(����)
    private float Radius;           // ���̽�ƽ ����� �� ����.
    private bool MoveFlag;          // �÷��̾� ������ ����ġ.

    

    void Start()
    {
        cc = GetComponent<CharacterController>();

        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // ĵ���� ũ�⿡���� ������ ����.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    void Update()
    {
        //JoystickMoveWorld();
        Move();
        //CCMove();
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
        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        //cc.Move(dir * speed * Time.deltaTime);
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
            Lwheel.transform.Rotate(new Vector3(0, 10, 0));
            Rwheel.transform.Rotate(new Vector3(0, 10, 0));
        }
    }

    private void Move()
    {
        //float Xmove = JoyVec.x;
        //float Zmove = JoyVec.y;

        float Xmove = Input.GetAxisRaw("Horizontal");
        float Zmove = Input.GetAxisRaw("Vertical");

        Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = transform.forward * Zmove;

        Vector3 rVec = (moveH + moveV).normalized * speed * Time.deltaTime;

        myRigid.MovePosition(transform.position + rVec);
    }

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
        /*
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }
        */
        dir.y = yVelocity;

        //�̵��Ѵ�.
        cc.Move(dir * speed * Time.deltaTime);
    }
}