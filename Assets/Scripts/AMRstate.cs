using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMRstate : MonoBehaviour
{
    enum Part
    {
        LEFT_WHEEL,
        RIGHT_WHEEL,
        TABLE,
        LIFT,
        BATTERY
    }

    enum HwError
    {
        MOTOR_ERROR,
        BATTERY_NO_COMM,
        LOW_BATTRY,
        POWER_FAIL
    }

    enum SafetyError
    {
        EMERG_STOP,
        BUMPING
    }

    enum Active
    {
        motor_ready,
        driving,
        lifting,
        charging
    }

    enum Switch
    {
        ON = 1,
        OFF = 0
    }

    enum TableAng
    {
        ANG_0 = 0,
        ANG_90 = 90,
        ANG_180 = 180,
        ANG_270 = 270
    }


    enum Motor
    {
        HwError,
    }
    /*
        def __init__(self,
                     error:HwError
                     ):
            self.__error = error
    */
    enum LeftWheel
    {
        Motor,
        speed = 0  // 0~1 값만 허용하도록 해야함
    }

    enum RightWheel
    {
        motor,
        speed = 0 // 0~1 값만 허용하도록 해야함
    }

    enum Lift
    {
        motor,
        _part = Part.LIFT,
        _motor = motor
    }

    enum Table
    {
        motor,
        ang,
        _part = Part.TABLE,
        _motor = motor,
        _ang = ang
    }

    enum Battry
    {
        temp,
        level,
        volt,
        _part = Part.BATTERY,
        _temp = temp,
        _level = level,
        _volt = volt
    }

    enum InfoAmr
    {
        // RX
        // AmrRxMotorStatusDetail 
        // AmrRxStatusDetail
        // AmrRxInputDetail
        power_sw = Switch.ON,
        emrg_sw = Switch.OFF
    }
}
