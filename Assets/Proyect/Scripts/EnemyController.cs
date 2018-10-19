using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject particulas;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile")==true)
        {
            GameObject particulasInstance = Instantiate(particulas);
            particulasInstance.transform.position = transform.position;


            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
