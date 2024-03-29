﻿using UnityEngine;
using System.Collections;
using System;

public class Stalfos : Enemy
{

    float cooldown;

    // Use this for initialization
    void Start()
    {
        base.Start();
        fps = 6;
    }

    public override void StartMovement()
    {
        cooldown = UnityEngine.Random.Range(40f, 80f);
        SnapGrid();
        float v, h = Mathf.Round(UnityEngine.Random.Range(0f, 1f));

        if (h == 0f) v = 1f;
        else v = 0f;

        int flip = UnityEngine.Random.Range(0, 2);
        if (flip == 1)
        {
            h *= -1f;
            v *= -1f;
        }

        this.GetComponent<Rigidbody>().velocity = new Vector3(h, v) * 3;
    }

    public override void UpdateMovement()
    {
        if(cooldown > 0)
        {
            float time_delta_fraction = Time.deltaTime / (1.0f / Application.targetFrameRate);
            cooldown -= time_delta_fraction;
            if (cooldown < 0) cooldown = 0f;
        }

        if (Mathf.Round(transform.position.x * 10f) / 10f % 1.0f == 0)
            if (Mathf.Round(transform.position.y * 10f) / 10f % 1.0f == 0)
                if (cooldown == 0)
                {
                    StartMovement();
                    return;
                }
        if (CheckCollision()) StartMovement();
    }

    public override bool CheckCollision()
    {
        RaycastHit hit;
        Vector3 v = GetComponent<Rigidbody>().velocity;
        v = v / v.sqrMagnitude;
        Debug.DrawRay(transform.position, v * 2, Color.green);
        if (Physics.Raycast(transform.position, v * 2, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Map"))
            {
                if (hit.distance < 0.5f) return true;
            }
        }
        return base.CheckCollision();
    }
}
	
	// Update is called once per frame
	/*void Update () {
        base.Update();
        if (knockback > 0) return;

        //Movement
        if (cooldown > 0) --cooldown;
        if (Mathf.Round(transform.position.x * 10f) / 10f % 1.0f == 0)
            if (Mathf.Round(transform.position.y * 10f) / 10f % 1.0f == 0)
                if (cooldown == 0)
                {
                    changeDirection();
                }
        RaycastHit hit;

        Debug.DrawRay(transform.position, forward * 2, Color.green);
        if (Physics.Raycast(transform.position, forward * 2, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Map"))
            {
                if (hit.distance < 0.5f) changeDirection();
            }
        }
    }

    void knockbackEnd()
    {
        base.knockbackEnd();
        changeDirection();
    }

    void changeDirection()
    {
        this.gameObject.tag = "Enemy";
        cooldown = 100;
        //Snap to the grid
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;

        //Pick a random direction
        float v, h = Mathf.Round(Random.Range(0f, 1f));

        if (h == 0f) v = 1f;
        else v = 0f;

        int flip = Random.Range(0, 2);
        if (flip == 1)
        {
            h *= -1f;
            v *= -1f;
        }

        forward = new Vector3(h, v);
        this.GetComponent<Rigidbody>().velocity = forward * walking_velocity;
    }
}
*/