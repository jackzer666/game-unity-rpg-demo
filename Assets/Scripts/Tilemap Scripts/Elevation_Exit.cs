using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                // 每一个碰撞体失效
                mountain.enabled = true;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                // 边界生效 避免玩家从山脉边缘直接走出去
                boundary.enabled = false;
            }

            // 玩家碰撞体渲染到一个新的涂层？玩家层级更高
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
