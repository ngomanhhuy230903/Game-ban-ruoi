using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime;
    float m_spawnTime;
    int m_score;
    bool m_isGameOver;
    UIManager m_ui;
    // Start is called before the first frame update
    void Start()
    {
        m_spawnTime = 0;
        m_ui = FindObjectOfType<UIManager>();
        m_ui.SetScoreText("Score: " + m_score);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            m_spawnTime = 0;
            m_ui.Showgameoverpanel(true);
            return;
        }
        m_spawnTime -= Time.deltaTime;
        if (m_spawnTime <= 0)
        {
            SpawnEnemy();
            m_spawnTime = spawnTime;

        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SpawnEnemy()
    {
        float randPos = Random.Range(-8.5f, 8);
        Vector2 spawnPos = new Vector2(randPos, 5.32f);
        if (enemy)
        {
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
    public int Score
    {
        get { return m_score; }
        set { m_score = value; }
    }
    public void InceasePoint()
    {
        if (m_isGameOver)
        {
            return;
        }
        m_score++;
        m_ui.SetScoreText("Score: " + m_score);
    }
    public bool IsGameOver
    {
        get { return m_isGameOver; }
        set { m_isGameOver = value; }
    }
}
