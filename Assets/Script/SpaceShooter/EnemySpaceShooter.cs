using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShooter : MonoBehaviour
{
    public SpaceshipController SpaceShip;
    public int health;
    public float minFR, MaxFR;
    private float FireRate;
    private float storedFireRate;
    public float BulletSpeed;
    public static int ActiveObjects { get; private set; }
    public GameObject EnemyBulletPool;

    public float moveSpeed;
    public float moveInterval;

    public Vector3 InitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
        //minFR = 1
        //maxFR = 5
        FireRate = Random.Range(minFR, MaxFR);
        storedFireRate = FireRate;

        //InvokeRepeating
        InvokeRepeating("MoveEnemy", 5, moveInterval);
    }


    // Update is called once per frame
    void Update()
    {
        FireRate -= Time.deltaTime;
        if (FireRate <= 0)
        {
            SpawnBullet();
            FireRate = storedFireRate;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Bullet"))
        {
            health--;
            BulletPoolManager.Instance.ReturnPlayerBullet(collision.gameObject);
            if (health <= 0)
            {
                SpaceShip.score++;
                gameObject.SetActive(false);
            }
        }
    }

    public void SpawnBullet()
    {
        // Get a bullet from the pool
        GameObject bullet = BulletPoolManager.Instance.GetEnemyBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = new Vector2(0f, -BulletSpeed);
            ActiveObjects++;
        }
        else
        {
            Debug.LogWarning("Bullet pool is empty, cannot spawn bullet.");
        }
    }

    public void MoveEnemy()
    {
        //Moves the enemy downwards aloth the y axis.
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
