using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Player : MonoBehaviour
{
    [SerializeField]Transform[] playerPositions;
    [SerializeField] float maxMana;
    [SerializeField] float manaDrainPerPress;
    [SerializeField] float manaGainPerSecond;
    float currentMana;

    void Update()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
            MoveAndRepair();
    }

    void MoveAndRepair()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position = playerPositions[0].position;
            GameManager.Instance.ShieldMap[0].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle) || Input.GetKeyDown(KeyCode.X))
        {
            transform.position = playerPositions[1].position;
            GameManager.Instance.ShieldMap[1].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle) || Input.GetKeyDown(KeyCode.W))
        {
            transform.position = playerPositions[2].position;
            GameManager.Instance.ShieldMap[2].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position = playerPositions[3].position;
            GameManager.Instance.ShieldMap[3].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft) || Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = playerPositions[4].position;
            GameManager.Instance.ShieldMap[4].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight) || Input.GetKeyDown(KeyCode.C))
        {
            transform.position = playerPositions[5].position;
            GameManager.Instance.ShieldMap[5].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft) || Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = playerPositions[6].position;
            GameManager.Instance.ShieldMap[6].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight) || Input.GetKeyDown(KeyCode.E))
        {
            transform.position = playerPositions[7].position;
            GameManager.Instance.ShieldMap[7].Repair();
        }
    }

    void DrainMana()
    {

    }
}
