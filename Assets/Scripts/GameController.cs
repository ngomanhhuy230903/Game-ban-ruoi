using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject power;
    private ObjectPool enemyPool;
    private ObjectPool powerPool;
    int bulletCount = 0;
    public float spawnTime;
    int score;
    bool m_isGameOver;
    bool powerSpawned;
    UIManager ui;

    void Start()
    {
        spawnTime = 0;
        powerSpawned = false;
        ui = FindObjectOfType<UIManager>();
        ui.SetScoreText("Score: " + score + " Bullet: " + bulletCount);
        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();
        powerPool = GameObject.Find("PowerPool").GetComponent<ObjectPool>();
    }

    void Update()
    {
        if (IsGameOver)
        {
            spawnTime = 0;
            ui.Showgameoverpanel(true);
            return;
        }

        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            SpawnEnemy();
            spawnTime = 2;
        }

        if (score > 0 && score % 10 == 0 && !powerSpawned)
        {
            SpawnPower();
            powerSpawned = true;
        }

        if (score % 10 != 0)
        {
            powerSpawned = false;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SpawnEnemy()
    {
        SpawnRandom(enemy, enemyPool);
    }

    public void SpawnPower()
    {
        SpawnRandom(power, powerPool);
    }

    public void SpawnRandom(GameObject prefab, ObjectPool pool)
    {
        if (prefab == null) return;

        float randPos = Random.Range(-8.5f, 8);
        Vector2 spawnPos = new Vector2(randPos, 5.32f);

        GameObject obj;
        if (pool != null)
        {
            obj = pool.GetFromPool(spawnPos, Quaternion.identity);
        }
        else
        {
            obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        obj.SetActive(true); // Đảm bảo object hiển thị nếu dùng object pool
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public void InceasePoint()
    {
        if (m_isGameOver)
        {
            return;
        }
        score++;
        ui.SetScoreText("Score: " + score + " Bullet: " + bulletCount);
    }
    public void InceaseBullet(int bulletIncrease)
    {
        if (m_isGameOver)
        {
            return;
        }
        bulletCount += bulletIncrease;
        ui.SetScoreText("Score: " + score + " Bullet: " + bulletCount);
    }

    public bool IsGameOver
    {
        get { return m_isGameOver; }
        set { m_isGameOver = value; }
    }
}
