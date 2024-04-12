using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum ActionState { noActive ,idle, attack };
    public static Animator anim;

    private ActionState state;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        controlIdleAnim();
    }

    private void controlIdleAnim()
    {
        if (!HandlePlayerMovement.isMoving)
        {
            state = ActionState.idle;
        }
        else
        {
            state = ActionState.noActive;
        }
        anim.SetInteger("state", (int)state);
    }


}
