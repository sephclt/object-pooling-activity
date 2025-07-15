using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }
    [Header("Player Bullets")]
    public GameObject PlayerBulletPrefab;
    public int playerPoolSize = 2;
    private Queue<GameObject> playerBulletPool = new();
    [Header("Enemy Bullets")]
    public GameObject EnemyBulletPrefab;
    public int enemyPoolSize = 2;
    private Queue<GameObject> enemyBulletPool = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < playerPoolSize; i++)
        {
            GameObject bullet = Instantiate(PlayerBulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);
            playerBulletPool.Enqueue(bullet);
        }

        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);
            enemyBulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetPlayerBullet()
    {
        if (playerBulletPool.Count > 0)
        {
            return playerBulletPool.Dequeue();
        }
        return null;
    }

    public void ReturnPlayerBullet(GameObject bulletObj)
    {
        bulletObj.SetActive(false);
        playerBulletPool.Enqueue(bulletObj);
    }

    public GameObject GetEnemyBullet()
    {
        if (enemyBulletPool.Count > 0)
        {
            return enemyBulletPool.Dequeue();
        }
        return null;
    }

    public void ReturnEnemyBullet(GameObject bulletObj)
    {
        bulletObj.SetActive(false);
        enemyBulletPool.Enqueue(bulletObj);
    }
}
