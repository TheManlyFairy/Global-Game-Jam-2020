using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Player : MonoBehaviour
{
    [SerializeField]Transform[] playerPositions;

    void Update()
    {
        if (GameManager.CurrentGameMode == GameManager.GameMode.Play)
            MoveAndRepair();
    }

    void MoveAndRepair()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft))
        {
            transform.position = playerPositions[0].position;
            GameManager.instance.ShieldMap[0].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle))
        {
            transform.position = playerPositions[1].position;
            GameManager.instance.ShieldMap[1].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle))
        {
            transform.position = playerPositions[2].position;
            GameManager.instance.ShieldMap[2].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight))
        {
            transform.position = playerPositions[3].position;
            GameManager.instance.ShieldMap[3].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft))
        {
            transform.position = playerPositions[4].position;
            GameManager.instance.ShieldMap[4].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight))
        {
            transform.position = playerPositions[5].position;
            GameManager.instance.ShieldMap[5].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft))
        {
            transform.position = playerPositions[6].position;
            GameManager.instance.ShieldMap[6].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight))
        {
            transform.position = playerPositions[7].position;
            GameManager.instance.ShieldMap[7].Repair();
        }
    }
}
