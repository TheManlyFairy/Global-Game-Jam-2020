using UnityEngine;
using Utilities;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public delegate void EnemyCollided(Enemy enemy);
    public event EnemyCollided OnEnemyCollision;
    
    public EnemyType enemyType;

    private Rigidbody2D rigBody;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int damageValue = 5;
    [SerializeField] private int scoreValue = 15;

    public int DamageValue
    {
        get { return damageValue; }
    }

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        rigBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigBody.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
            rigBody.MovePosition(rigBody.position +
                                 ((Vector2) (GameManager.Instance.TargetPosition) - (Vector2) (transform.position)) *
                                 (moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnEnemyCollision != null)
        {
            GameManager.Instance.IncrementScore(scoreValue);
            gameObject.SetActive(false);
            OnEnemyCollision.Invoke(this);
        }
    }
}
