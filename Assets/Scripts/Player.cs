using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]Transform[] playerPositions;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            transform.position = playerPositions[0].position;
            GameManager.instance.ShieldMap[0].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            transform.position = playerPositions[1].position;
            GameManager.instance.ShieldMap[1].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            transform.position = playerPositions[2].position;
            GameManager.instance.ShieldMap[2].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            transform.position = playerPositions[3].position;
            GameManager.instance.ShieldMap[3].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            transform.position = playerPositions[4].position;
            GameManager.instance.ShieldMap[4].Repair();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            transform.position = playerPositions[5].position;
            GameManager.instance.ShieldMap[5].Repair();
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
