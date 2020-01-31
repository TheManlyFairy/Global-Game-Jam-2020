using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public delegate void EnemyCollided(Enemy enemy);
    public static event EnemyCollided onEnemyCollision;

    public Transform enemyTarget;
    Rigidbody2D rigBody;
    Vector3 startPos;

    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    int damageValue = 5;

    public int DamageValue { get { return damageValue; } }
    private void Start()
    {
        startPos = transform.position;
        rigBody = GetComponent<Rigidbody2D>();
        rigBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigBody.gravityScale = 0;

    }
    private void FixedUpdate()
    {
        rigBody.MovePosition(rigBody.position + 
                             ((Vector2)(GameManager.instance.TargetPosition) - 
                              (Vector2)(transform.position)) * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onEnemyCollision != null)
        {
            gameObject.SetActive(false);
            onEnemyCollision(this);
        }
    }
}
