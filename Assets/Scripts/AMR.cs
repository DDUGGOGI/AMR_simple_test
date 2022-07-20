using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMR : MonoBehaviour
{
    class Map
    {
        Vector3 local_coordinate=new Vector3(0,0,0);
        Vector3 global_coordinate=new Vector3(0,0,0);
    }
    
    enum AmrState
    {
        POWER_SW=1,
        MOTOR_READY
    }

    AmrState amr_state = new AmrState();


    //AMR info 와 AMR state를 구분
    string AMRid;

    enum LeftWheel
    {
        motor_left_speed,
        motor_left_status
    }

    enum RightWheel
    {
        motor_right_speed,
        motor_right_status
    }

    enum Table
    {
        motor_table_status
    }

    enum Lift
    {
        motor_table_status
    }

    enum Battry
    {
        batteryTemp,
        batteryLevel,
        batteryVolt,
    }

    enum Motor
    {
        motorErrLeft,
        motorErrRight,
        motorErrTable,
        motorErrLift
    }
}
