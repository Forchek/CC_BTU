using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_BTU : MonoBehaviour
{
    private int Speed = 6;
    private Vector2 Mov = new Vector2(0, 0);
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool jumping = false;
    private bool GoingUp = false;
    private float AlturaSalto = 0f;

    public Transform CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        Moving();
        ChangeRenderOrder();
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

        if(Input.GetButtonDown("Jump"))
        {
            jumping = true;
            GoingUp = true;
            AlturaSalto = transform.position.y + 2f;
        }
    }

    private void Moving()
    {
        rb.velocity = Mov.normalized * Speed;

        //Salto
        if(jumping == true)
        {
            if(GoingUp == true)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, AlturaSalto), Time.deltaTime * 5);
                if (transform.position.y >= AlturaSalto - 0.2)
                {
                    GoingUp = false;
                    AlturaSalto = transform.position.y - 2f;
                }
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, AlturaSalto), Time.deltaTime * 5);
                if (transform.position.y <= AlturaSalto + 0.2)
                {
                    Debug.Log("En Suelo: " + AlturaSalto);
                    jumping = false;
                }
            }
        }

    }

    private void ChangeRenderOrder()
    {
        if (transform.position.y >= CameraPos.position.y)
        {
            sr.sortingOrder = 1;
        }
        else if ((transform.position.y >= CameraPos.position.y -2) && (transform.position.y < CameraPos.position.y))
        {
            sr.sortingOrder = 2;
        }
        else
        {
            sr.sortingOrder = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CHOCANDO");
    }

}
