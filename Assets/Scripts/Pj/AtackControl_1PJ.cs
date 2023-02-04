using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackControl_1PJ : MonoBehaviour
{
    private Mov_BTU MovPj;
    private Animator Anim;
    private char NextAttack = 'N';

    public CircleCollider2D RadAtck;
    // Start is called before the first frame update
    void Start()
    {
        MovPj = GetComponent<Mov_BTU>();
        Anim = GetComponent<Animator>();
        RadAtck.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((RadAtck.enabled == false) && (MovPj.AttackT == true || MovPj.AttackY == true ) && (NextAttack == 'N'))
        {
            RadAtck.enabled = true;
            if (MovPj.AttackT)
            {
                Anim.SetTrigger("FirstAttackT");
            }
            else
            {
                Anim.SetTrigger("FirstAttackY");
            }
        }
        else if ((RadAtck.enabled == true) && (MovPj.AttackT || MovPj.AttackY) && (NextAttack == 'N'))
        {
            if(MovPj.AttackT)
            {
                NextAttack = 'T';
                Anim.SetBool("NextAttackT", true);
            }
            else
            {
                NextAttack = 'Y';
                Anim.SetBool("NextAttackY", true);
            }
        }
        else if ((RadAtck.enabled == false) && (NextAttack != 'N'))
        {
            RadAtck.enabled = true;
            NextAttack = 'N';
            Anim.SetBool("NextAttackY", false);
            Anim.SetBool("NextAttackT", false);
        }
    }

    void EndAttack()
    {
        RadAtck.enabled = false;
    }
}
