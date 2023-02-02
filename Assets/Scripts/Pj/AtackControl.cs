using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackControl : MonoBehaviour
{
    private Mov_BTU MovPj;
    private char NextAttack = 'N';

    public CircleCollider2D RadAtck;
    // Start is called before the first frame update
    void Start()
    {
        MovPj = GetComponent<Mov_BTU>();
        RadAtck.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((RadAtck.enabled == false) && (MovPj.AttackT || MovPj.AttackY))
        {
            RadAtck.enabled = true;
            if (MovPj.AttackT)
            {
                //animator
            }
            else
            {
                //animator
            }
        }
        else if ((RadAtck.enabled == true) && (MovPj.AttackT || MovPj.AttackY) && (NextAttack == 'N'))
        {
            if(MovPj.AttackT)
            {
                NextAttack = 'T';
                //animator
            }
            else
            {
                NextAttack = 'Y';
                //animator
            }
        }
        else if ((RadAtck.enabled == false) && (NextAttack != 'N'))
        {
            RadAtck.enabled = true;
            if (NextAttack == 'T')
            {
                //animator
            }
            else
            {
                //animator
            }
            NextAttack = 'N';
        }
    }

    void EndAttack()
    {
        RadAtck.enabled = false;
    }
}
