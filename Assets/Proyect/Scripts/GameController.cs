using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float shootingInterval = 3f;
    public float shootingSpeed = 2f;
    public GameObject enemyMissile;
    public GameObject enemyContainer;
    public float movingInterval = 0.5f;
    public float movingDistance= 0.1f;
    public float horizontalLimit = 2.5f;
    public GameObject player;

    float movingDirection = 1f;
    float movingTimer;
    float shootingTimer;
    AudioSource shootsound;

    //comentario

    // Use this for initialization
    void Start () {
        shootingTimer = shootingInterval;
        shootsound = GetComponent<AudioSource>();
        movingTimer = movingInterval;
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
        //movement logic
        movingTimer -= Time.deltaTime;
        if(movingTimer <= 0)
        {
            movingTimer = movingInterval;
            enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x+(movingDistance*movingDirection)*1.1f,enemyContainer.transform.position.y);
            if (movingDirection > 0)
            {
                float rightmostPosition = 0f;
                foreach(EnemyController enemy in GetComponentsInChildren<EnemyController>())
                {
                    if (enemy.transform.position.x > rightmostPosition)
                    {
                        rightmostPosition = enemy.transform.position.x;
                    }
                }

                if(rightmostPosition > horizontalLimit)
                {
                    movingDirection *= -1;
                    enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x , enemyContainer.transform.position.y - movingDistance );
                }
            }else
            {
                float leftmostPosition = 0f;
                foreach (EnemyController enemy in GetComponentsInChildren<EnemyController>())
                {
                    if (enemy.transform.position.x < leftmostPosition)
                    {
                        leftmostPosition = enemy.transform.position.x;
                    }
                }

                if (leftmostPosition < -horizontalLimit)
                {
                    movingDirection *= -1;
                    enemyContainer.transform.position = new Vector2(enemyContainer.transform.position.x, enemyContainer.transform.position.y - movingDistance);
                }
            }
        }
        if (GetComponentsInChildren<EnemyController>().Length <= 0 || player == null)
        {
            SceneManager.LoadScene("Game");
        }
    }
    
}
