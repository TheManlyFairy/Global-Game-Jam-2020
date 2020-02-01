using System.Collections;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem teleportOut;
    [SerializeField] ParticleSystem teleportIn;
    [SerializeField] ParticleSystem sprayHeal;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Transform[] defencePoses;

    private DancePadKey lastKeyPressed;
    
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
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft) || Input.GetKeyDown(KeyCode.Q))
        {
            if (lastKeyPressed != DancePadKey.TopLeft)
            {
                StartCoroutine(Move(0));
                lastKeyPressed = DancePadKey.TopLeft;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, -45);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[0].Repair();
        }

        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle) || Input.GetKeyDown(KeyCode.W))
        {
            if (lastKeyPressed != DancePadKey.TopMiddle)
            {
                StartCoroutine(Move(1));
                lastKeyPressed = DancePadKey.TopMiddle;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, -90);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[1].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight) || Input.GetKeyDown(KeyCode.E))
        {
            if (lastKeyPressed != DancePadKey.TopRight)
            {
                StartCoroutine(Move(2));
                lastKeyPressed = DancePadKey.TopRight;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, -45);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[2].Repair();
        }

        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            if (lastKeyPressed != DancePadKey.MiddleLeft)
            {
                StartCoroutine(Move(3));
                lastKeyPressed = DancePadKey.MiddleLeft;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, 0);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[3].Repair();

        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            if (lastKeyPressed != DancePadKey.MiddleRight)
            {
                StartCoroutine(Move(4));
                lastKeyPressed = DancePadKey.MiddleRight;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, 0);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[4].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft) || Input.GetKeyDown(KeyCode.Z))
        {
            if (lastKeyPressed != DancePadKey.BottomLeft)
            {
                StartCoroutine(Move(5));
                lastKeyPressed = DancePadKey.BottomLeft;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, 45);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[5].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle) || Input.GetKeyDown(KeyCode.X))
        {
            if (lastKeyPressed != DancePadKey.BottomMiddle)
            {
                StartCoroutine(Move(6));
                lastKeyPressed = DancePadKey.BottomMiddle;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, 90);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[6].Repair();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight) || Input.GetKeyDown(KeyCode.C))
        {
            if (lastKeyPressed != DancePadKey.BottomRight)
            {
                StartCoroutine(Move(7));
                lastKeyPressed = DancePadKey.BottomRight;
            }
            sprayHeal.transform.localEulerAngles = new Vector3(0, 0, 45);
            sprayHeal.Emit(5);
            GameManager.Instance.ShieldMap[7].Repair();
        }
    }

    IEnumerator Move(int index)
    {
        teleportOut.Play();
        yield return new WaitForSeconds(0.2f);
        spriteRend.enabled = false;
        yield return null;
        transform.position = defencePoses[index].position;
        transform.rotation = transform.position.x > 0f ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        teleportIn.Play();
        yield return new WaitForSeconds(0.2f);
        spriteRend.enabled = true;
    }
}
