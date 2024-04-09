using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRabbitMoving : MonoBehaviour
{
    // basic attribute
    [SerializeField]
    private float speed = 12f;
    private Rigidbody rb;

    //
    // Direction || Animation control
    enum MovementState { idle, run, dead };
    private float idleTime;
    private float runningTime;
    MovementState state;
    private Animator anim;
    private bool checkAnimationChange;
    private float rotateY;
    //

    // Time control
    private float timeSaveForUpdate;
    private float realTime;
    //

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        checkAnimationChange = false;
        timeSaveForUpdate = 0;
        realTime = 0;
        idleTime = Random.Range(5, 9);
        runningTime = Random.Range(5, 9);
        InvokeRepeating("chooseDirection", 5, 5);
        state = MovementState.idle;
    }


    private void Update()
    {
        handleAnimation();
        handleRotation();
    }


    private void handleAnimation()
    {
        realTime += Time.deltaTime;
        if (state == MovementState.idle)
        {
            if (realTime - idleTime > timeSaveForUpdate)
            {
                timeSaveForUpdate = realTime;
                idleTime = Random.Range(5, 9);
                state = MovementState.run;
                checkAnimationChange = true;
            }
        }
        else if (state == MovementState.run)
        {
            if (realTime - runningTime > timeSaveForUpdate)
            {
                timeSaveForUpdate = realTime;
                runningTime = Random.Range(5, 9);
                state = MovementState.idle;
                checkAnimationChange = true;

            }
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (checkAnimationChange)
        {
            anim.SetInteger("State", (int)state);
            checkAnimationChange = false;
        }
    }

    private void handleRotation()
    {
        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotateY, 0), Time.deltaTime);
        transform.rotation = rotation;
    }


    private void chooseDirection()
    {
        rotateY = Random.Range(-90f, 90f);
    }
}
