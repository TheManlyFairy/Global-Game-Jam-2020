using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimator : MonoBehaviour
{
    [SerializeField] Image titleImage;
    [SerializeField] float titleAnimationTime=5;
    [SerializeField] float holdTitleTime=3;

    private void Start()
    {
        StartCoroutine(AnimateTitleScreen());
    }

    IEnumerator AnimateTitleScreen()
    {
        float timer = 0;
        while(timer<titleAnimationTime/2)
        {
            float colorValue = Mathf.Clamp01(Mathf.Sin(timer / (titleAnimationTime / 2)));
            titleImage.color = new Color(colorValue, colorValue, colorValue);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(holdTitleTime);

        timer = 0;
        while (timer < titleAnimationTime/2)
        {
            float colorValue = 1- Mathf.Clamp01(Mathf.Sin(timer / (titleAnimationTime / 2)));
            titleImage.color = new Color(colorValue, colorValue, colorValue);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.ReadyGame();
        titleImage.gameObject.SetActive(false);
    }
}
