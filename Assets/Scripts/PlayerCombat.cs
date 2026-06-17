using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint; // 攻击点？
    public float weaponRange = 1; // 攻击范围
    public float knockbackForce = 20; // 击退力
    public float knockbackTime = .5f; // 击退敌人的持续时间
    public float stunTime = .5f; // 击退敌人后造成敌人的眩晕时间

    public LayerMask enemyLayer;
    public int damage = 1;



    public Animator anim;
    public float cooldown = 2; // 攻击冷却时间
    private float timer;


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        // 找到以攻击起点为半径圆内的敌人
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
        if (enemies.Length >0)
        {
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-damage);
            enemies[0].GetComponent<EnemyKnockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
