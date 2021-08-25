using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    Transform leftGun;
    Transform rightGun;
    public GameObject bulletPrefab;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public PlayerLogicScript playerLogicScript;
    Transform environmentManagerTransform;

	// Use this for initialization
    void Awake()
    {
        leftGun = transform.Find("leftGun");
        rightGun = transform.Find("rightGun");
        playerLogicScript = GameObject.FindWithTag("Player").GetComponent<PlayerLogicScript>();
        environmentManagerTransform = GameObject.Find("EnemiesManager").transform;

        if (playerLogicScript == null)
        {
            Debug.Log("Unable to get player playerLogicScript!");
        }
        else
        {
            playerLogicScript.pauseGame += PauseGame;
        }
    }

    public void Fire()
    {
        if (leftGun == null || rightGun == null)
        {
            return;
        }

        GameObject bulletLeft = GameObject.Instantiate(bulletPrefab, leftGun.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
        GameObject bulletRight = GameObject.Instantiate(bulletPrefab, rightGun.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;

        bulletLeft.transform.parent = environmentManagerTransform;
        BulletScript bulletScriptLeft = bulletLeft.GetComponent<BulletScript>();
        bulletScriptLeft.playerLogicScript = playerLogicScript;
        bulletScriptLeft.SubscribeToPauseEvent();

        bulletRight.transform.parent = environmentManagerTransform;
        BulletScript bulletScripRight = bulletRight.GetComponent<BulletScript>();
        bulletScripRight.playerLogicScript = playerLogicScript;
        bulletScripRight.SubscribeToPauseEvent();
    }

    void PauseGame()
    {
        isPaused = !isPaused;
    }

    void OnDestroy()
    {
        if (playerLogicScript != null)
        {
            playerLogicScript.pauseGame -= PauseGame;
        }
    }

    void OnDisable()
    {
        if (playerLogicScript != null)
        {
            playerLogicScript.pauseGame -= PauseGame;
        }
    }
}
