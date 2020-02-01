using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateRuneLetters : MonoBehaviour
{
    [SerializeField] float animateTime;
    [SerializeField] Sprite[] runeLetterSprites;
    float timer;
    int spriteIndex=0;
    SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer> animateTime)
        {
            timer = 0;
            spriteIndex++;
            if (spriteIndex == runeLetterSprites.Length)
                spriteIndex = 0;
            spriteRend.sprite = runeLetterSprites[spriteIndex];
        }
    }
}
