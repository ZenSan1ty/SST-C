using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider healthBarSlider;
    public static GameManager instance = null;
    private GameObject parent;
    private int score = 0;
    private Text scoreText;

    public float xBoundary = 95f;
    public float yBoundary = 35f;

    public GameObject player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    /*void Start()
    {
        parent = GameObject.Find("Score Text");
        scoreText = parent.GetComponent<Text>();

        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;

        UpdateScore();
    }

    public void RemoveScore(int newScoreValue)
    {
        score -= newScoreValue;

        UpdateScore();
    }

    public void SetMaxHealth(float health)
    {
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthBarSlider.value = health;
    }*/
}
