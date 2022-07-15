using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
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

    void MoveAMRBody()
    {
        //float Xmove = Input.GetAxisRaw("Horizontal");
        float Zmove = Input.GetAxisRaw("Vertical");

        //Vector3 moveH = transform.right * Xmove;
        Vector3 moveV = transform.forward * Zmove;

        // Vector3 rVec = (moveH + moveV).normalized * speed * Time.deltaTime;
        Vector3 rVec = (moveV).normalized * speed * Time.deltaTime;
        myRigid.MovePosition(transform.position + rVec);
    }

    void RotatieAMRBody()
    {
        float Ymove = Input.GetAxisRaw("Horizontal");
        Vector3 bodyRotationY = new Vector3(0, Ymove, 0) * turnSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(bodyRotationY));
    }

    void AMRWheelRotation()
    {
        float Zmove = Input.GetAxisRaw("Vertical");
        if (Zmove>0)
        {
            Lwheel.transform.Rotate(new Vector3(0, 10, 0));
            Rwheel.transform.Rotate(new Vector3(0, 10, 0));
        }
        else if (Zmove<0)
        {
            Lwheel.transform.Rotate(new Vector3(0, -10, 0));
            Rwheel.transform.Rotate(new Vector3(0, -10, 0));
        }
    }
}
