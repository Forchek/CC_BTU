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
    private float PosCaida = 0f;

    public bool AttackT = false;
    public bool AttackY = false;
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

        if (((transform.position.x >= (CameraPos.position.x + 8.3)) && Mov.x > 0) || ((transform.position.x <= (CameraPos.position.x - 8.3)) && Mov.x < 0))
        {
            Mov.x = 0;
        }
        if (((transform.position.y >= (CameraPos.position.y + 1.5)) && Mov.y > 0) || ((transform.position.y <= (CameraPos.position.y - 4)) && Mov.y < 0))
        {
            Mov.y = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
            GoingUp = true;
            AlturaSalto = transform.position.y + 2f;
            PosCaida = transform.position.y;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            AttackT = true;
        }
    }

    private void Moving()
    {
        if(jumping == false)
        {
            transform.Translate(Mov.normalized * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(Mov.x,0) * Speed * Time.deltaTime);
            PosCaida += Mov.y * Time.deltaTime/2f;
        }

        //Salto
        if(jumping == true)
        {
            if(GoingUp == true)
            {
                transform.Translate(Vector2.up *Time.deltaTime * 5);
                if (transform.position.y >= AlturaSalto)
                {
                    GoingUp = false;
                }
            }
            else
            {
                transform.Translate(Vector2.down * Time.deltaTime * 5);
                if (transform.position.y <= PosCaida)
                {
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

    //Se ejecuta en el primer frame del ataque
    void EliminateAttackBool()
    {
        AttackT = false;
        AttackY = false;
    }

}
