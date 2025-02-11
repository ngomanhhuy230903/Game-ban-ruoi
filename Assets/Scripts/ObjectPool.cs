using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // Prefab của object cần quản lý
    public int poolSize = 10; // Số lượng object tối đa trong pool
    private Queue<GameObject> pool = new Queue<GameObject>();
    void Start()
    {
        // Khởi tạo pool với số lượng sẵn có
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
