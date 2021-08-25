using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{
    bool isGamePaused;
    public float speed = 0.4f;
    float outOfScreenValueMax = 2.4f;
    float outOfScreenValueMin = -12f;
    [HideInInspector]
    public PlayerLogicScript playerLogicScript;

    void Start()
    {
        if (this.tag == "EnemyBullet")
        {
            speed *= -1;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (isGamePaused)
        {
            return;
        }

        transform.position += Vector3.forward * speed;

        if (transform.position.z > outOfScreenValueMax || transform.position.z < outOfScreenValueMin)
        {
            RemoveBullet();
        }
	}

    void RemoveBullet()
    {
        UnsubscribeToPauseEvent();
        Destroy(this.gameObject);
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
    }

    public void SubscribeToPauseEvent()
    {
        if (playerLogicScript != null)
        {
            playerLogicScript.pauseGame += PauseGame;
        }
    }

    void UnsubscribeToPauseEvent()
    {
        if (playerLogicScript != null)
        {
            playerLogicScript.pauseGame -= PauseGame;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && this.tag == "EnemyBullet")
        {
            playerLogicScript.Health--;
            RemoveBullet();
        }

        if (collision.collider.tag == "Enemy" && this.tag == "PlayerBullet")
        {
            collision.transform.GetComponent<EnemyScript>().EnemyHealth--;
            RemoveBullet();
        }

    }

    void OnTriggerEnder(Collider col)
    {
        if (GetComponent<Collider>().tag == "Player" && this.tag == "EnemyBullet")
        {
            playerLogicScript.Health--;
            RemoveBullet();
        }

        if (GetComponent<Collider>().tag == "Enemy" && this.tag == "PlayerBullet")
        {
            transform.GetComponent<EnemyScript>().EnemyHealth--;
            RemoveBullet();
        }

    }

}
