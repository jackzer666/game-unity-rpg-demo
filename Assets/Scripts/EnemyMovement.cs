using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public float attakRange = 2;
    public float attackCooldown = 2; // 攻击冷却时间
    public float playerDetectRange = 5; // 玩家检测距离
    public Transform detectionPoint; // 检测，自定义可以放在敌人面向的前方
    public LayerMask PlayerLayer;


    private float attackCooldownTimer; // 攻击冷却倒计时，每次重制为冷却时间
    private int facingDirection = 1;
    private EnemyState enemyState;


    // 刚体对象
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 期望在运行时动态找到，但我不知道为什么
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero;
        }
    }
    // 追逐方向处理与速度处理
    void Chase()
    {
        if ((player.position.x - transform.position.x) * facingDirection < 0)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    // 翻转敌人方向
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // 检查玩家与检测点的距离，变更敌人状态
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, PlayerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            
            // 如果玩家在攻击范围内，且攻击冷却结束
            if (Vector2.Distance(player.position, transform.position) <= attakRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown; 
                ChangeState(EnemyState.Attacking);
            }
            // 如果玩家在攻击范围之外
            else if (Vector2.Distance(player.position, transform.position) > attakRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        enemyState = newState;
        anim.SetInteger("EnemyState", (int)newState);
    }

    // 画圆圈辅助线，方便查看攻击范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}

public enum EnemyState: int
{
    Idle,
    Chasing,
    Attacking,
}