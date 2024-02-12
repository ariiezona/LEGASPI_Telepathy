using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public int maxHp = 100;
    public int currentHp = 100;
    public bool isGameOver;
    [SerializeField] private GameObject gameOver;


    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (currentHp <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOver.SetActive(true);

        Time.timeScale = 0;
    }
}

