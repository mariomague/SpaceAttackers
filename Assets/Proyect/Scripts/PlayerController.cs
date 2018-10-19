using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float destroyMissileTime = 2.5f;
    public float speed = 3f;
    float limit;
    public GameObject missilePrefab;
    bool fire;//para que si mantienes pulsado no clickes 1023023 veces+
    public float fireSpeed= 1f;
    public float cooldownfireDur= 1f;
    float cooldownfire;
    public GameObject explosionAnim;
    public AudioSource shootsound;

	// Use this for initialization
	void Start () {
        limit = (((Screen.width / 100f) / 2f) / Camera.main.aspect);
        cooldownfire = cooldownfireDur;
        shootsound = GetComponent<AudioSource>();
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
        cooldownfire -= Time.deltaTime;
        //disparar misil
        if  (Input.GetAxis("Fire1")!= 0)
        {
            if (cooldownfire <= 0 && !fire)
            {
                cooldownfire = cooldownfireDur;
                GameObject missileInstance = Instantiate(missilePrefab);
                missileInstance.transform.SetParent(transform.parent);
                missileInstance.transform.position = transform.position;
                missileInstance.GetComponent<Rigidbody2D>().velocity = Vector2.up * fireSpeed;
                Destroy(missileInstance, destroyMissileTime); //destruyelo en 2 segundos
                fire = true;
                shootsound.Play();
            }
        } else
        {
            fire = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyMissile"))
        {
            Destroy(gameObject);
            GameObject particulasInstance = Instantiate(explosionAnim);
            particulasInstance.transform.position = transform.position;


            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
