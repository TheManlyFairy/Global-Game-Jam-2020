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
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft))
        {
            transform.position = playerPositions[0].position;
            GameManager.Instance.ShieldMap[0].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle))
        {
            transform.position = playerPositions[1].position;
            GameManager.Instance.ShieldMap[1].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle))
        {
            transform.position = playerPositions[2].position;
            GameManager.Instance.ShieldMap[2].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight))
        {
            transform.position = playerPositions[3].position;
            GameManager.Instance.ShieldMap[3].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft))
        {
            transform.position = playerPositions[4].position;
            GameManager.Instance.ShieldMap[4].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight))
        {
            transform.position = playerPositions[5].position;
            GameManager.Instance.ShieldMap[5].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft))
        {
            transform.position = playerPositions[6].position;
            GameManager.Instance.ShieldMap[6].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight))
        {
            transform.position = playerPositions[7].position;
            GameManager.Instance.ShieldMap[7].Repair();
        }
    }

    void DrainMana()
    {

    }
}
