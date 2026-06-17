using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockedBack;

    // Update is called 50x frame
    void FixedUpdate()
    {

        if (isKnockedBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 玩家实际横向操作方向 与 角色面朝方向相反时，需要翻转角色面朝方向
            if (horizontal * transform.localScale.x < 0) {
                facingDirection *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            // 是否移动状态处理
            bool isMoving = (horizontal != 0 || vertical != 0);
            anim.SetBool("isMoving", isMoving);

            rb.velocity = new Vector2(horizontal, vertical) * speed;
        }
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    // 枚举击退计数器
    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
