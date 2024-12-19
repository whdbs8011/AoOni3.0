using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rigid;
    private float h;
    private float v;
    private bool isHorizonMove;
    
    Animator ani;
    private static readonly int IsChange = Animator.StringToHash("isChange");
    private static readonly int HAxisRaw = Animator.StringToHash("hAxisRaw");
    private static readonly int VAxisRaw = Animator.StringToHash("vAxisRaw");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }
        else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }
        //anim
        
        if (ani.GetInteger("hAxisRaw") != h)
        {
            ani.SetBool("isChange", true);
            ani.SetInteger("hAxisRaw", (int)h);
        }
        else if (ani.GetInteger("vAxisRaw") != v)
        {
            ani.SetBool("isChange",true);
            ani.SetInteger("vAxisRaw",(int)v);
        }
        else
        {
            ani.SetBool("isChange", false);
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;
    }
}
