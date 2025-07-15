using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            BulletPoolManager.Instance.ReturnEnemyBullet(collision.gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            BulletPoolManager.Instance.ReturnPlayerBullet(collision.gameObject);
        }
    }

}
