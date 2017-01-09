﻿using UnityEngine;
using System.Collections;

public class BasicRangeEnemy : Enemy {


    //Projectile Variables
    public Transform projectileList;                        //Transform containing list of available bullets
    int ammo;                                        //Keeps track of ammo available
    public float fireRate;                                  //Rate at which enemy can fire
    public float bulletSpeed;                               //Speed at which bullet should move
    bool fireReady;                                         //Determines if able to fire bullet again


    // Use this for initialization
    void Start()
    {
        health = 100;
        fireReady = true;
        ammo = projectileList.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetVisibleState() && GetPursuitState())
        {
            if (!inAttackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, .01f);
                RunApproachAnim();
            }
            else
            {
                if(ammo != 0 && fireReady)
                {
                    FireProjectile();
                }
                //Should begin attack animation
                anim.SetInteger("x", 0);
                anim.SetInteger("y", 0);
                anim.SetBool("isIdle", true);
            }
        }
        else
            anim.SetBool("isIdle", true);
    }

    //Checks what animation to play
    void RunApproachAnim()
    {
        int x = 0;
        int y = 0;

        if (target.transform.position.y > transform.position.y)
            y = 1;
        else if (target.transform.position.y < transform.position.y)
            y = -1;
        else if (target.transform.position.x > transform.position.x)
            x = 1;
        else if (target.transform.position.x < transform.position.x)
            x = -1;

        if (y == 0 && x == 0)
        {
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }

        anim.SetInteger("x", x);
        anim.SetInteger("y", y);

    }

    void FireProjectile()
    {
        ammo--;
        foreach(Transform child in projectileList)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
                child.position = transform.position;
                child.GetComponent<Rigidbody2D>().velocity = -(transform.position - target.transform.position).normalized * bulletSpeed;
                StartCoroutine("FireRateRegulator");
                return;
            }
        }
    }

    //Regulates rate of fire
    IEnumerator FireRateRegulator()
    {
        fireReady = false;
        yield return new WaitForSeconds(fireRate);
        fireReady = true;
    }

    //Reloads ammo. Public for access by bullets to restock ammo
    public void ReloadAmmo()
    {
        ammo++;
    }
}
