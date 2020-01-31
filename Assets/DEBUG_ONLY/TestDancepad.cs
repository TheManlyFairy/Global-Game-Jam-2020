 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDancepad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] joysticks = Input.GetJoystickNames();
        foreach (string joystick in joysticks)
            Debug.Log("Detected " + joystick);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("Pressed JoystickButton0");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("Pressed JoystickButton1");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Pressed JoystickButton2");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("Pressed JoystickButton3");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Debug.Log("Pressed JoystickButton4");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("Pressed JoystickButton5");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            Debug.Log("Pressed JoystickButton6");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Debug.Log("Pressed JoystickButton7");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8))
        {
            Debug.Log("Pressed JoystickButton8");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            Debug.Log("Pressed JoystickButton9");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            Debug.Log("Pressed JoystickButton10");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            Debug.Log("Pressed JoystickButton11");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            Debug.Log("Pressed JoystickButton12");
        }
    }
}
