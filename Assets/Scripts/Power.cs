using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Power : Enemy
{
    int bulletCount = 3;
    private ObjectPool powerPool;
    private Player player;
    void Start()
    {
        base.Start(); // Gọi Start() của Enemy để kế thừa rb, gc
        powerPool = GameObject.Find("PowerPool").GetComponent<ObjectPool>();
        player = FindObjectOfType<Player>(); // Lấy Player
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.down * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Deathzone"))
        {
            powerPool.ReturnToPool(gameObject);        }
        if (col.CompareTag("Player"))
        {
            powerPool.ReturnToPool(gameObject);
            player.bulletRemain += bulletCount;
            gc.InceaseBullet(bulletCount);
        }
    }
}
