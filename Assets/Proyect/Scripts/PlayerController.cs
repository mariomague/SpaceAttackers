﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3f;
    float limit;
    public GameObject missilePrefab;
    bool fire;//para que si mantienes pulsado no clickes 1023023 veces+
    public float fireSpeed= 1f;

	// Use this for initialization
	void Start () {
        limit = (((Screen.width / 100f) / 2f) / Camera.main.aspect);
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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; //pongo la velocidad a 0 para que el jugador pueda facilmente cambiar de direccion
        }

        //disparar misil
        if (Input.GetAxis("Fire1")!= 0)
        {
            if (!fire)
            {
                GameObject missileInstance = Instantiate(missilePrefab);
                missileInstance.transform.SetParent(transform.parent);
                missileInstance.transform.position = transform.position;
                missileInstance.GetComponent<Rigidbody2D>().velocity = Vector2.up * fireSpeed;
                Destroy(missileInstance, 2f); //destruyelo en 2 segundos
                fire = true;
            }
        } else
        {
            fire = false;
        }
	}
}
