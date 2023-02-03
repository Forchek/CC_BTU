using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_BTU : MonoBehaviour
{
    private int Speed = 6;
    private Vector2 Mov = new Vector2(0, 0);
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator Anim;
    private bool jumping = false;
    private bool GoingUp = false;
    private float AlturaSalto = 0f;
    private float PosCaida = 0f;
    private Vector3 LocScale;
    private bool FacingRight = true;
    private Vector3 EndDashPos;

    public bool AttackT = false;
    public bool AttackY = false;
    public bool Hability = false;
    public bool Inmunity = false;
    public Transform CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
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
        if(Hability == false)
        {
            Mov.x = Input.GetAxis("Horizontal");
            Mov.y = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump") && (jumping == false))
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
            else if (Input.GetKeyDown(KeyCode.U) && (Hability == false))
            {
                Anim.SetTrigger("Dash");
                Hability = true;
                Inmunity = true;
                if (FacingRight == true)
                {
                    EndDashPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                }
                else
                {
                    EndDashPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            Mov = new Vector2(0, 0);
        }
    }

    private void Moving()
    {
        if (Hability == false)
        {
            if (jumping == false)
            {
                transform.Translate(Mov.normalized * Speed * Time.deltaTime);
                //Girar el Pj
                if ((Mov.x > 0) && (FacingRight == false))
                {
                    LocScale = transform.localScale;
                    LocScale.x *= -1;
                    transform.localScale = LocScale;
                    FacingRight = true;
                }
                else if ((Mov.x < 0) && (FacingRight == true))
                {
                    LocScale = transform.localScale;
                    LocScale.x *= -1;
                    transform.localScale = LocScale;
                    FacingRight = false;
                }
            }
            else
            {
                transform.Translate(new Vector2(Mov.x, 0) * Speed * Time.deltaTime);
                PosCaida += Mov.y * Time.deltaTime / 2f;
            }

            //Salto
            if (jumping == true)
            {
                if (GoingUp == true)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * 5);
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

        //Dash
        if(Hability == true)
        {
            transform.position = Vector3.Lerp(transform.position, EndDashPos, Time.deltaTime*10);
            if(Mathf.Abs(transform.position.x - EndDashPos.x) <= 0.01 )
            {
                //Estos dos tendran que ir en nuna funcion con la animacion
                Inmunity = false;
                Hability = false;
                if(jumping == true)
                {
                    GoingUp = false;
                }
            }

        }

        //Comprobar que no pase los limites
        if (transform.position.x > (CameraPos.position.x + 8.3))
        {
            transform.position = new Vector3(CameraPos.position.x + 8.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= (CameraPos.position.x - 8.3))
        {
            transform.position = new Vector3(CameraPos.position.x - 8.3f, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= (CameraPos.position.y + 1.5) && (jumping == false))
        {
            transform.position = new Vector3(transform.position.x, CameraPos.position.y + 1.5f, transform.position.z);
        }
        else if (transform.position.y <= (CameraPos.position.y - 4))
        {
            transform.position = new Vector3(transform.position.x, CameraPos.position.y - 4f, transform.position.z);
        }

    }

    private void ChangeRenderOrder()
    {
        if (jumping == false && Hability == false)
        {
            if (transform.position.y >= CameraPos.position.y)
            {
                sr.sortingOrder = 1;
            }
            else if ((transform.position.y >= CameraPos.position.y - 2) && (transform.position.y < CameraPos.position.y))
            {
                sr.sortingOrder = 2;
            }
            else
            {
                sr.sortingOrder = 3;
            }
        }
    }

    //Se ejecuta en el primer frame del ataque
    void EliminateAttackBool()
    {
        AttackT = false;
        AttackY = false;
    }

    void EndDash()
    {
        Inmunity = false;
        Hability = false;
    }

}
