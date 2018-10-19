using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float shootingInterval = 3f;
    public float shootingSpeed = 2f;
    public GameObject enemyMissile;
    public AudioSource shootsound;
    float shootingTimer;

	// Use this for initialization
	void Start () {
        shootingTimer = shootingInterval;
        shootsound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0)
        {
            shootingTimer = shootingInterval;

            EnemyController [] enemies = GetComponentsInChildren<EnemyController>(); //cogo todos los enemigos
            for (int i = 0; i < 3;i++)
            {
                EnemyController randomEnemy = enemies[Random.Range(0, enemies.Length)];
                GameObject enemyMissileIns = Instantiate(enemyMissile);
                enemyMissileIns.transform.SetParent(transform.parent);
                enemyMissileIns.transform.position = randomEnemy.transform.position;
                enemyMissileIns.GetComponent<Rigidbody2D>().velocity = Vector2.down * shootingSpeed;
                shootsound.Play();
                Destroy(enemyMissileIns, 3f);
            }
            
        }
	}
}
