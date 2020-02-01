using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite activeStraightArrow;
    [SerializeField] private Sprite activeDiagonalArrow;
    [SerializeField] private Sprite pressedStraightArrow;
    [SerializeField] private Sprite pressedDiagonalArrow;
    [SerializeField] private SpriteRenderer[] diagonalArrows;
    [SerializeField] private SpriteRenderer[] straightArrows;
    [SerializeField] private float minActiveTime;
    [SerializeField] private float maxActiveTime;
    [SerializeField] private float minIntervalTime;
    [SerializeField] private float maxIntervalTime;
    [SerializeField] private Transform playerTransform;

    private List<Transform> _arrowPositions;
    private List<SpriteRenderer> _activeStraightArrows;
    private List<SpriteRenderer> _activeDiagonalArrows;
    private float activeTime;
    private float intervalTime;
    private float aciveTimer;
    private float intervalTimer;
    private float timer;
    private int activeArrowsAmount;
    private bool spwaned;
    private List<Vector3> selectedPositions;
    private void Start()
    {
        _arrowPositions = new List<Transform>(diagonalArrows.Length + straightArrows.Length);

        for (int i = 0; i < diagonalArrows.Length; i++)
        {
            _arrowPositions.Add(diagonalArrows[i].transform);
        }

        for (int i = 0; i < straightArrows.Length; i++)
        {
            _arrowPositions.Add(straightArrows[i].transform);
        }

        RandomizeSpawn();
    }

    private void RandomizeSpawn()
    {
        intervalTime = Random.Range(minIntervalTime, maxIntervalTime);
        activeArrowsAmount = Random.Range(3, 7);
        activeTime = Random.Range(minActiveTime, maxActiveTime) * activeArrowsAmount;
    }

    private void Update()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
        {
            timer += Time.deltaTime;

            if (timer > intervalTime)
            {
                Vector3 currentPlayerPosition = playerTransform.position;

                if (!spwaned)
                {
                    Spawn(currentPlayerPosition);
                    selectedPositions = new List<Vector3>();
                    spwaned = true;
                }
                else
                {
                    CheckPlayerPressedArrow(currentPlayerPosition);

                    if (selectedPositions.Count == activeArrowsAmount)
                    {
                        for (int i = 0; i < GameManager.Instance.ShieldMap.Length; i++)
                        {
                            GameManager.Instance.ShieldMap[i].ResetShield();
                        }

                        aciveTimer = activeTime;
                    }

                    aciveTimer += Time.deltaTime;

                    if (aciveTimer > activeTime)
                    {
                        Reset();
                    }
                }
            }
        }
        else
        {
            if (spwaned)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        for (int i = 0; i < _activeDiagonalArrows.Count; i++)
        {
            _activeDiagonalArrows[i].enabled = false;
        }

        for (int i = 0; i < _activeStraightArrows.Count; i++)
        {
            _activeStraightArrows[i].enabled = false;
        }

        spwaned = false;
        timer = 0;
        aciveTimer = 0;
        RandomizeSpawn();
    }

    private void CheckPlayerPressedArrow(Vector3 currentPlayerPosition)
    {
        if (!selectedPositions.Contains(currentPlayerPosition))
        {
            for (int i = _activeDiagonalArrows.Count - 1; i >= 0; --i)
            {
                if (currentPlayerPosition == _activeDiagonalArrows[i].transform.position)
                {
                    _activeDiagonalArrows[i].sprite = pressedDiagonalArrow;
                    selectedPositions.Add(currentPlayerPosition);
                }
            }

            for (int i = _activeStraightArrows.Count - 1; i >= 0; --i)
            {
                if (currentPlayerPosition == _activeStraightArrows[i].transform.position
                )
                {
                    _activeStraightArrows[i].sprite = pressedStraightArrow;
                    selectedPositions.Add(currentPlayerPosition);
                }
            }
        }
    }

    private void Spawn(Vector3 currentPlayerPosition)
    {
        List<int> straightArrowUsedIndexes = new List<int>(activeArrowsAmount);
        List<int> diagonalArrowUsedIndexes = new List<int>(activeArrowsAmount);
        _activeStraightArrows = new List<SpriteRenderer>(activeArrowsAmount);
        _activeDiagonalArrows = new List<SpriteRenderer>(activeArrowsAmount);

        for (int i = 0; i < activeArrowsAmount; i++)
        {
            if (Random.value > 0.5f && _activeStraightArrows.Count < straightArrows.Length - 1)
            {
                int index = Random.Range(0, straightArrows.Length);

                while (straightArrowUsedIndexes.Contains(index) ||
                       straightArrows[index].transform.position == currentPlayerPosition)
                {
                    index = Random.Range(0, straightArrows.Length);
                }

                straightArrowUsedIndexes.Add(index);
                _activeStraightArrows.Add(straightArrows[index]);
            }
            else if (_activeDiagonalArrows.Count < diagonalArrows.Length - 1)
            {
                int index = Random.Range(0, diagonalArrows.Length);

                while (diagonalArrowUsedIndexes.Contains(index) ||
                       diagonalArrows[index].transform.position == currentPlayerPosition)
                {
                    index = Random.Range(0, diagonalArrows.Length);
                }

                diagonalArrowUsedIndexes.Add(index);
                _activeDiagonalArrows.Add(diagonalArrows[index]);
            }
        }

        for (int i = 0; i < _activeDiagonalArrows.Count; i++)
        {
            _activeDiagonalArrows[i].sprite = activeDiagonalArrow;
            _activeDiagonalArrows[i].enabled = true;
        }

        for (int i = 0; i < _activeStraightArrows.Count; i++)
        {
            _activeStraightArrows[i].sprite = activeStraightArrow;
            _activeStraightArrows[i].enabled = true;
        }
    }
}