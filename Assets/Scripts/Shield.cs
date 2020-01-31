using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int repairPerPressValue = 25;
    int currentHealth;
    Image image;
    void Start()
    {
        currentHealth = maxHealth / 2;
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, (float)currentHealth / (float)maxHealth);
    }

    void Update()
    {

    }

    public void Repair()
    {
        currentHealth += repairPerPressValue;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, (float)currentHealth / (float)maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyCollided = collision.collider.GetComponent<Enemy>();

        if (enemyCollided)
        {
            currentHealth -= enemyCollided.DamageValue;
            image.color = new Color(image.color.r, image.color.g, image.color.b, (float)currentHealth / (float)maxHealth);
        }
    }
}
