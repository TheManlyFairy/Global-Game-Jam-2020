using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Shield : MonoBehaviour
{ 
    public delegate void ShieldBreak();
    public static event ShieldBreak OnShieldBreak;
    
    [SerializeField] int maxHealth = 300;
    static int repairPerPressValue = 0;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private EdgeCollider2D edgeCollider2D;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        repairPerPressValue = GameManager.Instance.shieldRepairPerPress;
        ResetShield();
    }

    public void ResetShield()
    {
        currentHealth = maxHealth;
        ColorShield();
    }

    public void Repair()
    {
        currentHealth += repairPerPressValue;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        ColorShield();
    }
    private void ColorShield()
    {
        Color color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, (float) currentHealth / maxHealth);
        spriteRenderer.color = color; 
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyCollided = collision.collider.GetComponent<Enemy>();

        if (enemyCollided)
        {
            currentHealth -= enemyCollided.DamageValue;

            if (currentHealth <= 0)
            {
                edgeCollider2D.enabled = false;
                OnShieldBreak?.Invoke();
            }

            ColorShield();
        }
    }
}
