using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float rotSpeed = 70;
    public float gravity = 5;
    Vector3 moveDirection = Vector3.zero;

    CharacterController Controller;
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }

    void Movement()
    {
        if (Controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (Anim.GetBool("attacking") == false)
                {
                    Anim.SetBool("running", true);
                    Anim.SetInteger("condition", 1);
                    moveDirection = new Vector3(0, 0, 1);
                    moveDirection *= speed;
                    moveDirection = transform.TransformDirection(moveDirection);

                }
            }
            {

                if (Input.GetKeyUp(KeyCode.W))
                {
                    Anim.SetBool("running", false);
                    Anim.SetInteger("condition", 0);
                    moveDirection = new Vector3(0, 0, 0);
                }
            }

            rotSpeed += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotSpeed, 0);
            moveDirection.y -= gravity * Time.deltaTime;
            Controller.Move(moveDirection * Time.deltaTime);
        }

    }

        void GetInput()
        {
            if (Controller.isGrounded)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Anim.GetBool("running") == true)
                    {
                        Anim.SetBool("running", false);
                        Anim.SetInteger("condition", 0);
                    }

                }
                else if (Anim.GetBool("running") == false)
                {
                    Attacking();
                }
            }
        }
        void Attacking()
        {

            StartCoroutine(AttackRoutine());

        }

        IEnumerator AttackRoutine()
        {
            Anim.SetBool("attacking", true);
            Anim.SetInteger("condition", 2);
            yield return new WaitForSeconds(1);
            Anim.SetInteger("condition", 0);
            Anim.SetBool("attacking", false);
        }
}
