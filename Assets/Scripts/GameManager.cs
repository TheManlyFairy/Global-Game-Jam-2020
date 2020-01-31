using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int shieldRepairPerPress = 30;
    
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Shield[] shields;
    
    public Shield[] ShieldMap => shields;
    public Vector3 TargetPosition => enemyTarget.position;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
