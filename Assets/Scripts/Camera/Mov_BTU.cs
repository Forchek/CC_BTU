using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_BTU : MonoBehaviour
{
    private int Speed = 6;
    private Vector2 Mov = new Vector2(0, 0);
    private Rigidbody2D rb;

    public Transform CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        Moving();
    }

    private void GetInputs()
    {
        Mov.x = Input.GetAxis("Horizontal");
        Mov.y = Input.GetAxis("Vertical");

        if(((transform.position.x >= (CameraPos.position.x + 8.3)) && Mov.x > 0) || ((transform.position.x <= (CameraPos.position.x - 8.3)) && Mov.x < 0))
        {
            Mov.x = 0;
        }
        if (((transform.position.y >= (CameraPos.position.y + 1.5)) && Mov.y > 0) || ((transform.position.y <= (CameraPos.position.y - 4)) && Mov.y < 0))
        {
            Mov.y = 0;
        }
    }

    private void Moving()
    {
        rb.velocity = Mov.normalized * Speed;
    }
}
