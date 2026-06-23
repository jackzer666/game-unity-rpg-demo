using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime,  float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime); // 先等待击退持续时间结束
        rb.velocity = Vector2.zero;                     // 让敌人在击退移动中停下来
        yield return new WaitForSeconds(stunTime);      // 等待眩晕时间
        enemyMovement.ChangeState(EnemyState.Idle);     // 恢复敌人状态
    }
}
