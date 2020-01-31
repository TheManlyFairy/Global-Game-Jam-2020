using UnityEngine;
using Utilities;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public delegate void EnemyCollided(Enemy enemy);
    public event EnemyCollided OnEnemyCollision;
    
    public EnemyType enemyType;

    private Rigidbody2D _rigidBody;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int damageValue = 5;
    [SerializeField] private int scoreValue = 15;

    public int DamageValue => damageValue;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidBody.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
        {
            Vector2 moveDirection = ((Vector2) (GameManager.Instance.TargetPosition) - (Vector2) transform.position)
                .normalized;
            _rigidBody.velocity = moveDirection * (moveSpeed * Time.deltaTime);
        }
        else if (GameManager.CurrentGameMode == GameMode.Pause)
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.IncrementScore(scoreValue);
        gameObject.SetActive(false);
        OnEnemyCollision?.Invoke(this);
    }
}
