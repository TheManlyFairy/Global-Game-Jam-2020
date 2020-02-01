using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopLeft) || Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("TopLeft");
        }

        if (Input.GetKeyDown((KeyCode)DancePadKey.TopMiddle) || Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("TopMiddle");
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.TopRight) || Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("TopLeft");
        }

        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("MiddleLeft");

        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("MiddleLeft");
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomLeft) || Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("BottomLeft");
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomMiddle) || Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("BottomMiddle");
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.BottomRight) || Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("BottomLeft");
        }
    }
}
