using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float upSpeed;
    public GameObject bullet;
    public int bulletRemain = 0;
    public Transform bulletSpawn;
    private bool isSpawn = false;
    private ObjectPool bulletPool;
    GameController gc;
    public AudioSource aus;
    public AudioClip shootingS;
     void Start()
    {
        gc = FindObjectOfType<GameController>();
        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
    }
    void Update()
    {
        if (gc.IsGameOver)
        {
            return;
        }
        float xDirec = Input.GetAxisRaw("Horizontal");
        if ((xDirec < 0 && transform.position.x <= -8.53) || (xDirec > 0 && transform.position.x >= 7.87))
        {
            return;
        }
        transform.position += Vector3.right * moveSpeed * Time.deltaTime * xDirec;

        float xDirecUp = Input.GetAxisRaw("Vertical");
        if ((xDirecUp < 0 && transform.position.y <= -4.13) || (xDirecUp > 0 && transform.position.y >= 4.3))
        {
            return;
        }
        transform.position += Vector3.up * upSpeed * Time.deltaTime * xDirecUp;

        if (Input.GetKeyDown(KeyCode.Space) && bulletRemain > 0)
        {
            GameObject bullet = bulletPool.GetFromPool(transform.position, Quaternion.identity);
            bullet.SetActive(true);
            bulletRemain--;
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
            Vector2 bulletPosition = new Vector2(bulletSpawn.position.x, bulletSpawn.position.y);
            GameObject obj;
            if (bulletPool != null)
            {
                obj = bulletPool.GetFromPool(bulletPosition, Quaternion.identity);
            }
            else
            {
                obj = Instantiate(bullet, bulletPosition, Quaternion.identity);
            }

            obj.SetActive(true); // Đảm bảo object hiển thị nếu dùng object pool
        }
    }
 /*   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            gc.IsGameOver = true;
        }
    }*/
}