using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3f;
    private float limit;


	// Use this for initialization
	void Start () {
        limit = (((Screen.width / 100f) / 2f) / Camera.main.aspect);
        Debug.Log(limit);
	}
	
	// Update is called once per frame
	void Update () {
        //move player
        GetComponent<Rigidbody2D>().velocity = Vector2.right * Input.GetAxis("Horizontal") * speed;
        //bounds
        if(transform.position.x > limit)
        {
            transform.position = new Vector3(limit,transform.position.y,transform.position.z);
        }
        else if(transform.position.x < -limit)
        {
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
	}
}
