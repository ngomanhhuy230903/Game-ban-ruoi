using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D m_rb;
    GameController m_gc;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
         m_rb.velocity = Vector3.down * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Deathzone"))
                {
            Destroy(gameObject);
            m_gc.IsGameOver = true;
        }
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            m_gc.IsGameOver = true;
        }
    }
}
