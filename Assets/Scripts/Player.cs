using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float upSpeed;
    public GameObject bullet;
    public Transform bulletSpawn;
    private bool isSpawn = false;
    private float shootTimer = 0f;
    public float shootDelay = 0.5f; // Thời gian trễ giữa các lần bắn
    GameController m_gc;
    public AudioSource aus;
    public AudioClip shootingS;
     void Start()
    {
        m_gc = FindObjectOfType<GameController>();
    }
    void Update()
    {
        if (m_gc.IsGameOver)
        {
            return;
        }
        float xDirec = Input.GetAxisRaw("Horizontal");
        if ((xDirec < 0 && transform.position.x <= -13) || (xDirec > 0 && transform.position.x >= 13))
        {
            return;
        }
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * xDirec;

        float xDirecUp = Input.GetAxisRaw("Vertical");
        if ((xDirecUp < 0 && transform.position.y <= -4.13) || (xDirecUp > 0 && transform.position.y >= 4.7))
        {
            return;
        }
        transform.position += Vector3.up * upSpeed * Time.deltaTime * xDirecUp;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpawn = true;
        }
        if (isSpawn)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootDelay)
            {
                Shoot();
                shootTimer = 0f;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isSpawn = false;
            }
        }
    }

    public void Shoot()
    {
        if (bullet && bulletSpawn)
        {
            if(aus && shootingS)
            {
                aus.PlayOneShot(shootingS);
            }
            Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        }
    }
 /*   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            m_gc.IsGameOver = true;
        }
    }*/
}