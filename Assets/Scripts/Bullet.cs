using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private GameController gc;
    private AudioSource aus;
    public AudioClip hitS;
    private ObjectPool bulletPool;
    private ObjectPool enemyPool;
    private ObjectPool vfxPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = FindObjectOfType<GameController>();
        aus = FindObjectOfType<AudioSource>();

        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();
        vfxPool = GameObject.Find("HitVFXPool").GetComponent<ObjectPool>();
    }

    void Update()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Limit"))
        {
            bulletPool.ReturnToPool(gameObject); // Trả đạn về pool
            return;
        }

        if (col.CompareTag("Enemy"))
        {
            if (aus && hitS)
            {
                aus.PlayOneShot(hitS);
            }

            if (vfxPool != null)
            {
                GameObject vfx = vfxPool.GetFromPool(col.transform.position, Quaternion.identity);
                StartCoroutine(ReturnVFXToPool(vfx, 1f)); // Trả VFX về pool sau 1s
            }

            gc.InceasePoint();

            // Trả cả bullet và enemy về pool
            bulletPool.ReturnToPool(gameObject);
            enemyPool.ReturnToPool(col.gameObject);
        }
    }

    private IEnumerator ReturnVFXToPool(GameObject vfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        vfxPool.ReturnToPool(vfx);
    }
}
