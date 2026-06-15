using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called 50x frame
    void FixedUpdate()
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
