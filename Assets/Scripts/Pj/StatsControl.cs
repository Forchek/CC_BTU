using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxVida;
    public int Vida;
    public int Daño;
    void Start()
    {
        MaxVida = 200;
        Vida = MaxVida;
        Daño = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "AtackEnemy")
        {
            Vida -= collision.GetComponent<StatsControl>().Daño;
        }
    }
}
