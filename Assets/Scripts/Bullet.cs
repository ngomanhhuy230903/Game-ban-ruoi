using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D m_rb;
    GameController m_gc;
    AudioSource aus;
    public AudioClip hitS;
    public GameObject hitVFX; // Prefab của hitVFX
    private GameObject hitVFXDes; // Tham chiếu đến đối tượng hitVFX được tạo ra

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindObjectOfType<GameController>();
        aus = FindObjectOfType<AudioSource>();
    }

    void Update()
    {
        m_rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
        if (col.CompareTag("Enemy"))
        {
            if (aus && hitS)
            {
                aus.PlayOneShot(hitS);
            }
            if (hitVFX)
            {
                hitVFXDes = Instantiate(hitVFX, col.transform.position, Quaternion.identity);
                Destroy(hitVFXDes, 1f);
            }
            m_gc.InceasePoint();
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}