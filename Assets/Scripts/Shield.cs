using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 300;
    static int repairPerPressValue = 0;
    int currentHealth;
    SpriteRenderer spRend;

    private void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        spRend.color = new Color(spRend.color.r, spRend.color.g, spRend.color.b, (float)currentHealth / (float)maxHealth);
        repairPerPressValue = GameManager.Instance.shieldRepairPerPress;
    }



    public void Repair()
    {
        currentHealth += repairPerPressValue;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        spRend.color = new Color(spRend.color.r, spRend.color.g, spRend.color.b, (float)currentHealth / (float)maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyCollided = collision.collider.GetComponent<Enemy>();

        if (enemyCollided)
        {
            currentHealth -= enemyCollided.DamageValue;
            spRend.color = new Color(spRend.color.r, spRend.color.g, spRend.color.b, currentHealth / (float)maxHealth);
        }
    }
}
