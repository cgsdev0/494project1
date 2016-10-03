﻿using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	//float delay = 2.0f; //This implies a delay of 2 seconds.
	//GameObject explosion;
	public GameObject explosion;

	void WaitForExplosion() {
        //Deal damage
        Collider[] hitColliders = Physics.OverlapBox(transform.position, Vector3.one*1.5f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                hitColliders[i].gameObject.GetComponent<Enemy>().Hurt();
            }
            i++;
        }

        //Make pretty explosion
        Destroy (this.gameObject);
		GameObject go = Instantiate(explosion, this.transform.position, Quaternion.identity) as GameObject;
	}

	// Use this for initialization
	void Start () {

		Invoke("WaitForExplosion", 1.5f);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
