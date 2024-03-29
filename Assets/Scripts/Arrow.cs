﻿using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public Sprite arrow_up;
	public Sprite arrow_down;
	public Sprite arrow_left;
	public Sprite arrow_right;
	public float speed = 10.0f;
    float roomX, roomY;

	public Vector3 forward;
	// Use this for initialization
	void Start () {

        roomX = Mathf.Floor((transform.position.x - 2) / 16f) * 16f + 2f;
        roomY = Mathf.Floor((transform.position.y - 2) / 11f) * 11f + 2f;

        if (PlayerControl.instance.current_direction == Direction.NORTH) {
			this.GetComponent<SpriteRenderer> ().sprite = arrow_up;
			forward = new Vector3 (0f, 1f);
		}
		if (PlayerControl.instance.current_direction == Direction.SOUTH) {
			this.GetComponent<SpriteRenderer> ().sprite = arrow_down;
			forward = new Vector3 (0f, -1f);
		}
		if (PlayerControl.instance.current_direction == Direction.EAST) {
			this.GetComponent<SpriteRenderer> ().sprite = arrow_right;
			forward = new Vector3 (1f, 0f);
		}
		if (PlayerControl.instance.current_direction == Direction.WEST) {
			this.GetComponent<SpriteRenderer> ().sprite = arrow_left;
			forward = new Vector3 (-1f, 0f);
		}
		this.GetComponent<Rigidbody> ().velocity = forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (CheckCollision()) Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "EnemyHurt" || coll.tag == "Enemy") {
			Debug.Log ("Hit Enemy");
			Destroy (this.gameObject);
		}
	}

    public virtual bool CheckCollision()
    {
        if (transform.position.x < roomX-1) return true;
        if (transform.position.y < roomY-1) return true;
        if (transform.position.x > roomX + 12) return true;
        if (transform.position.y > roomY + 7) return true;
        return false;
    }
    /*void OnCollisionEnter(Collision coll){
		if (coll.collider.tag == "Map") {
			Tile b = coll.gameObject.GetComponent<Tile>();
			char c = ShowMapOnCamera.S.collisionS[b.tileNum];
			if (c == 'S' || c == 'T') {
				Destroy (this.gameObject);
			}
		}
	}*/
}
