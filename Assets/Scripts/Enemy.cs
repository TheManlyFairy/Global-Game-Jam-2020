using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public Transform enemyTarget;
    public float moveSpeed = 5;
    Rigidbody2D rigBody;
    Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.position;
        rigBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigBody.MovePosition(rigBody.position + ((Vector2)(enemyTarget.position)- (Vector2)(transform.position)) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = startPos;
    }
}
