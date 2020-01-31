using UnityEngine;
using Utilities;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] Transform[] defencePoses;

    private void Update()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
        {
            MoveAndRepair();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.Instance.GameOver();
    }

    private void MoveAndRepair()
    {
        int repairIndex = -1;
        
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            repairIndex = 0;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle) || Input.GetKeyDown(KeyCode.X))
        {
            repairIndex = 1;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle) || Input.GetKeyDown(KeyCode.W))
        {
            repairIndex = 2;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            repairIndex = 3;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft) || Input.GetKeyDown(KeyCode.Z))
        {
            repairIndex = 4;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight) || Input.GetKeyDown(KeyCode.C))
        {
            repairIndex = 5;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft) || Input.GetKeyDown(KeyCode.Q))
        {
            repairIndex = 6;
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight) || Input.GetKeyDown(KeyCode.E))
        {
            repairIndex = 7;
        }

        if (repairIndex != -1)
        {
            var cachedTransform = transform;
            cachedTransform.rotation = defencePoses[repairIndex].rotation;
            cachedTransform.position = defencePoses[repairIndex].position;
            GameManager.Instance.ShieldMap[repairIndex].Repair();   
        }
    }
}
