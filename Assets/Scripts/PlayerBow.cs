using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;
    public float shootCooldown = .5f;
    private float shootTimer;



    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // 实例化 箭预制体。预制体、初始化位置，初始化旋转 Quaternion.identity是无旋转的意思
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shootTimer = shootCooldown;
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 至少按下了一个方向键，才概念方向，避免没有按下时方向归零了
        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
        }
    }
}
