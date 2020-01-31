using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName ="TutorialPage")]
public class ScriptableTutorial : ScriptableObject
{
    public Sprite sprite;
    [TextArea(10,100)] public string text;
}

