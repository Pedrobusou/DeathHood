using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    //Axis Player

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }

    //Axis Camera

    public static float CameraHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_CameraHorizontal");
        r += Input.GetAxis("K_CameraHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float CameraVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_CameraVertical");
        r += Input.GetAxis("K_CameraVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 CameraJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }

    //Axis Triggers

    public static float LTTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("LT");
        r += Input.GetAxis("LeftClick");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float RTTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("RT");
        r += Input.GetAxis("RightClick");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
}