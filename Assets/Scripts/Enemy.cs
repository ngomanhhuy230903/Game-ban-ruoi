using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    private ObjectPool enemyPool;
    protected Rigidbody2D rb;
    protected GameController gc;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = FindObjectOfType<GameController>();
        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
         rb.velocity = Vector3.down * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Deathzone"))
                {
            enemyPool.ReturnToPool(gameObject);
            //gc.IsGameOver = true;
            gc.InceasePoint();
        }
        if (col.CompareTag("Player"))
        {
            enemyPool.ReturnToPool(gameObject);
            gc.IsGameOver = true;
        }
    }
}
