using UnityEngine;
using UnityEditor;
using SpeedrunPractice.Extensions;
using System;

public class FreeCam : MonoBehaviour
{
    float mainSpeed = 25;
    const float shiftAdd = 50;
    const float maxShift = 100;
    const float camSens = 0.25f;
    Vector3 lastMouse = new Vector3(255, 255, 255);
    float totalRun = 1.0f;
    const int maxSpeed = 100;
    void Update()
    {
        if (MainManager_Ext.toggleFreeCam && Application.isFocused)
        {
            mainSpeed += Input.mouseScrollDelta.y * 5f;
            mainSpeed = Mathf.Clamp(mainSpeed, 0, maxSpeed);

            
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;  

            Vector3 p = GetBaseInput();
            if (p.sqrMagnitude > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    totalRun += Time.deltaTime;
                    p = p * totalRun * shiftAdd;
                    p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                    p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                    p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
                }
                else
                {
                    totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                    p *= mainSpeed;
                }

                p *= Time.deltaTime;
                transform.Translate(p);
            }
        }
    }

    private Vector3 GetBaseInput()
    { 
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
            p_Velocity += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.S))
            p_Velocity += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.A))
            p_Velocity += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.D))
            p_Velocity += new Vector3(1, 0, 0);
        return p_Velocity;
    }
}