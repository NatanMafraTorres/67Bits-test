using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform player, playerObj, orientation;
    public Rigidbody rb;
    public FloatingJoystick moveJts;
    public GameObject EB1, EB2, EB3, EB4, E1, E2, E3, E4;
    public Text texto;
    public float rotationSpeed, moveSpeed;
    public int back = 0, bMax = 1, money = 0;
    float horizontalInput;
    float verticalInput;
    public bool punch, enemy;
    public Animator anim;
    Vector3 inputDir;

    void Start()
    {
        rb.freezeRotation = true;
        EB1.GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void FixedUpdate()
    {
        move();
        InertiaMove();
        InertiaRotate();
    }

    void Update()
    {
        SpeedControl();

        horizontalInput = moveJts.Horizontal;
        verticalInput = moveJts.Vertical;
        
        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            if (punch == false && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
            {
                anim.Play("Run");
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (punch == false && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {
            anim.Play("Stop");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
        {
            punch = false;
        }
    }

    private void move()
    {
        rb.AddForce(inputDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }
//-----------------------------Control max speed------------------------------------------
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if (inputDir == Vector3.zero)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            punch = true;
            anim.Play("Punch");   
            //-----------------------------Get Enemy------------------------------------------
            if (back < bMax)
            {
                back = back + 1;  
                Destroy(collision.gameObject.GetComponent<Enemy>().self);
                switch (back)
                {
                    case 1:
                        EB1.SetActive(true);
                        break;
                    case 2:
                        EB2.SetActive(true);
                        break;
                    case 3:
                        EB3.SetActive(true);
                        break;
                    case 4:
                        EB4.SetActive(true);
                        break;
                }
            }
        }
        //-----------------------------Drop Enemy------------------------------------------
        if (collision.gameObject.CompareTag("RedArea"))
        {
            switch (back)
                {
                    case 1:
                        money = money + 1;
                        texto.text = "$: " + money;
                        E1.SetActive(true);
                        break;
                    case 2:
                        money = money + 2;
                        texto.text = "$: " + money;
                        E1.SetActive(true);
                        E2.SetActive(true);
                        break;
                    case 3:
                        money = money + 3;
                        texto.text = "$: " + money;
                        E1.SetActive(true);
                        E2.SetActive(true);
                        E3.SetActive(true);
                        break;
                    case 4:
                        money = money + 4;
                        texto.text = "$: " + money;
                        E1.SetActive(true);
                        E2.SetActive(true);
                        E3.SetActive(true);
                        E4.SetActive(true);
                        break;
                }
            back = 0;
            EB1.SetActive(false);
            EB2.SetActive(false);
            EB3.SetActive(false);
            EB4.SetActive(false);
        }
    }
    private void OnTriggerExit (Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            punch = false;
        }
    }

//-----------------------------Area to inertia------------------------------------------
    private void InertiaMove()
    {
        if(inputDir != Vector3.zero && EB2.transform.localPosition.z > -0.5f)
        {
            EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x, EB2.transform.localPosition.y, EB2.transform.localPosition.z - 0.01f);
        }
        if(inputDir == Vector3.zero && EB2.transform.localPosition.z < -0.2f)
        {
            EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x, EB2.transform.localPosition.y, EB2.transform.localPosition.z + 0.02f);
        }

        if(inputDir != Vector3.zero && EB3.transform.localPosition.z > -0.5f)
        {
            EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x, EB3.transform.localPosition.y, EB3.transform.localPosition.z - 0.01f);
        }
        if(inputDir == Vector3.zero && EB3.transform.localPosition.z < -0.2f)
        {
            EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x, EB3.transform.localPosition.y, EB3.transform.localPosition.z + 0.02f);
        }

        if(inputDir != Vector3.zero && EB4.transform.localPosition.z > -0.5f)
        {
            EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x, EB4.transform.localPosition.y, EB4.transform.localPosition.z - 0.01f);
        }
        if(inputDir == Vector3.zero && EB4.transform.localPosition.z < -0.2f)
        {
            EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x, EB4.transform.localPosition.y, EB4.transform.localPosition.z + 0.02f);
        }
    }

    private void InertiaRotate()
    {
        if(inputDir != Vector3.zero && EB1.transform.localRotation.x > -30f)
        {
            EB1.transform.localRotation = Quaternion.Euler(EB1.transform.localRotation.x - 1f, EB1.transform.localRotation.y, EB1.transform.localRotation.z);
        }
        if(inputDir == Vector3.zero && EB1.transform.localRotation.x < 30f)
        {
            EB1.transform.localRotation = Quaternion.Euler(EB1.transform.localRotation.x + 1f, EB1.transform.localRotation.y, EB1.transform.localRotation.z);
        }

        if(inputDir != Vector3.zero && EB2.transform.localRotation.x > -30f)
        {
            EB2.transform.localRotation = Quaternion.Euler(EB2.transform.localRotation.x - 5f, EB2.transform.localRotation.y, EB2.transform.localRotation.z);
        }
        if(inputDir == Vector3.zero && EB2.transform.localRotation.x < 30f)
        {
            EB2.transform.localRotation = Quaternion.Euler(EB2.transform.localRotation.x + 5f, EB2.transform.localRotation.y, EB2.transform.localRotation.z);
        }

        if(inputDir != Vector3.zero && EB3.transform.localRotation.x > -30f)
        {
            EB3.transform.localRotation = Quaternion.Euler(EB3.transform.localRotation.x - 5f, EB3.transform.localRotation.y, EB3.transform.localRotation.z);
        }
        if(inputDir == Vector3.zero && EB3.transform.localRotation.x < 30f)
        {
            EB3.transform.localRotation = Quaternion.Euler(EB3.transform.localRotation.x + 5f, EB3.transform.localRotation.y, EB3.transform.localRotation.z);
        }

        if (inputDir.x == -1 && playerObj.forward.x < -0.8f)
        {
            if(EB2.transform.localPosition.x > -0.5f && back >= 2)
            {
                EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x - 0.02f, EB2.transform.localPosition.y, EB2.transform.localPosition.z);
            }
            if(EB3.transform.localPosition.x > -0.5f && back >= 3)
            {
                EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x - 0.02f, EB3.transform.localPosition.y, EB3.transform.localPosition.z);
            }
            if(EB4.transform.localPosition.x > -0.5f && back == 4)
            {
                EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x - 0.02f, EB4.transform.localPosition.y, EB4.transform.localPosition.z);
            }
        }
        if (inputDir.x == 1 && playerObj.forward.x > 0.8f)
        {
            if(EB2.transform.localPosition.x < 0.5f && back >= 2)
            {
                EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x - 0.02f, EB2.transform.localPosition.y, EB2.transform.localPosition.z);
            }
            if(EB3.transform.localPosition.x < 0.5f && back >= 3)
            {
                EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x - 0.02f, EB3.transform.localPosition.y, EB3.transform.localPosition.z);
            }
            if(EB4.transform.localPosition.x < 0.5f && back == 4)
            {
                EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x - 0.02f, EB4.transform.localPosition.y, EB4.transform.localPosition.z);
            }
        }

        if(inputDir == Vector3.zero && EB1.transform.localPosition.x < 0)
        {
            EB1.transform.localPosition = new Vector3(EB1.transform.localPosition.x + 0.02f, EB1.transform.localPosition.y, EB1.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB2.transform.localPosition.x < 0)
        {
            EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x + 0.02f, EB2.transform.localPosition.y, EB2.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB3.transform.localPosition.x < 0)
        {
            EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x + 0.02f, EB3.transform.localPosition.y, EB3.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB4.transform.localPosition.x < 0)
        {
            EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x + 0.02f, EB4.transform.localPosition.y, EB4.transform.localPosition.z);
        }

        if(inputDir == Vector3.zero && EB1.transform.localPosition.x > 0.1)
        {
            EB1.transform.localPosition = new Vector3(EB1.transform.localPosition.x - 0.02f, EB1.transform.localPosition.y, EB1.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB2.transform.localPosition.x > 0.1)
        {
            EB2.transform.localPosition = new Vector3(EB2.transform.localPosition.x - 0.02f, EB2.transform.localPosition.y, EB2.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB3.transform.localPosition.x > 0.1)
        {
            EB3.transform.localPosition = new Vector3(EB3.transform.localPosition.x - 0.02f, EB3.transform.localPosition.y, EB3.transform.localPosition.z);
        }
        if(inputDir == Vector3.zero && EB4.transform.localPosition.x > 0.1)
        {
            EB4.transform.localPosition = new Vector3(EB4.transform.localPosition.x - 0.02f, EB4.transform.localPosition.y, EB4.transform.localPosition.z);
        }
    }
}
