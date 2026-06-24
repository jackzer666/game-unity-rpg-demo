using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    public PlayerMovement playerMovement;
    private Vector2 aimDirection = Vector2.right;
    public float shootCooldown = .5f;
    private float shootTimer;

    public Animator anim;



    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
        }
    }

    // 当前脚本开启时
    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0); // 第0层基础层权重设置为关闭（骑士关闭）
        anim.SetLayerWeight(1, 1); // 弓箭手层设置为开启（弓箭手开启）
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1); // 第0层基础层权重设置为开启（骑士开启）
        anim.SetLayerWeight(1, 0); // 弓箭手层设置为关闭（弓箭手关闭）
    }

    public void Shoot()
    {
        if (shootTimer <= 0) {
            // 实例化 箭预制体。预制体、初始化位置，初始化旋转 Quaternion.identity是无旋转的意思
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDirection;
            shootTimer = shootCooldown;
        }
        anim.SetBool("isShooting", false);
        playerMovement.isShooting = false;
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 至少按下了一个方向键，才概念方向，避免没有按下时方向归零了
        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }
}
