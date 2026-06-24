using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2; // 箭飞行时间
    public float speed; // 箭速度

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;

    public int damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;



    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn); // 等待一段时间后销毁   
    }

    private void RotateArrow()
    {
        // 面向右侧时 逆时针的弧度，* Mathf.Rad2Deg变成角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 击中敌人层级
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<EnemyKnockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite; // 扎入状态精灵图
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(target);
    }


}
