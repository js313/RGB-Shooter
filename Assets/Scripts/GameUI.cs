using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image fadePlane;
    public GameObject gameOverUI;
    public GameObject gameUI;
    public RectTransform newWaveBanner;
    public TMP_Text newWaveTitle;
    public TMP_Text newWaveEnemyCount;
    public TMP_Text scoreUI;
    public TMP_Text gameOverScoreUI;
    public RectTransform healthBar;

    Spawner spawner;
    Player player;

    string[] numbers = { "One", "Two", "Three", "Foun", "Five" };

    void Awake()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    void Start()
    {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
        gameOverUI.SetActive(false);
        gameUI.SetActive(true);
    }

    void Update()
    {
        scoreUI.text = ScoreKeeper.Score.ToString("D6");
        float healthPercent = 0;
        if (player != null)
        {
            healthPercent = player.health / player.startingHealth;
        }
        healthBar.localScale = new(healthPercent, 1, 1);
    }

    void OnNewWave(int waveNumber)
    {
        newWaveTitle.text = $"- Wave {numbers[waveNumber - 1]} -";
        var enemyCount = spawner.waves[waveNumber - 1].infinite ?
          "Infinite" :
          spawner.waves[waveNumber - 1].enemyCount.ToString();
        newWaveEnemyCount.text = $"Enemies: {enemyCount}";

        StopCoroutine(nameof(AnimateNewWaveBanner));
        StartCoroutine(nameof(AnimateNewWaveBanner));
    }

    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, new(0, 0, 0, .9875f), 1));
        gameOverUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverScoreUI.text = scoreUI.text;
        Cursor.visible = true;
    }

    IEnumerator AnimateNewWaveBanner()
    {
        var delayTime = 1.5f;
        var speed = 3f;
        var animationPercent = 0f;
        var dir = 1;
        var endDelayTime = Time.time + 1 / speed + delayTime;

        while (animationPercent >= 0)
        {
            animationPercent += Time.deltaTime * speed * dir;

            if (animationPercent >= 1)
            {
                animationPercent = 1;
                if (Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }

            newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-170, 30, animationPercent);
            yield return null;
        }
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        var speed = 1 / time;
        var percent = 0f;

        while (percent <= 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
