using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform[] playerPositions;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("Pressed JoystickButton0");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("Pressed JoystickButton1");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            transform.position = playerPositions[2].position;
            GameManager.instance.ShieldMap[2].Repair();
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
            transform.position = playerPositions[6].position;
            GameManager.instance.ShieldMap[6].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            transform.position = playerPositions[7].position;
            GameManager.instance.ShieldMap[7].Repair();
        }
    }
}
