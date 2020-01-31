using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int shieldRepairPerPress = 30;
    [SerializeField]Transform enemyTarget;
    [SerializeField]Shield[] shields;


    public Shield[] ShieldMap { get { return shields; } }
    public Vector3 TargetPosition { get { return enemyTarget.position; } }
    private void Start()
    {
        if(instance!=null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
