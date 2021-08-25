using UnityEngine;
using System.Collections;

public class ParalaxEffectScript : MonoBehaviour 
{
    public float scrollSpeed;
    private Vector2 savedOffset;
    private bool isPaused;
    PlayerLogicScript playerLogicScript;
    public float fullscreenXRatio = 1f;
    public float fullscreenYRatio = 1f;
    public float viewPortXPosition = 0.5f;
    public float viewPortYPosition = 0.5f;

	void Start () 
    {
        savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
        playerLogicScript = GameObject.FindWithTag("Player").GetComponent<PlayerLogicScript>();

        if (playerLogicScript == null)
        {
            Debug.Log("Unable to get player input script!");
        }
        else
        {
            playerLogicScript.pauseGame += PauseGame;
        }

	}
	
    float yOffset;
	void FixedUpdate () 
    {
        if (isPaused)
        {
            return;
        }

        yOffset += Time.fixedDeltaTime * scrollSpeed; //You could use that but if game got paused -> on resume value will not be correct Mathf.Repeat(Time.time * scrollSpeed, 1);
        if (yOffset >= 1f)
        {
            yOffset = 0f;
        }

        Vector2 offset = new Vector2(savedOffset.y, yOffset);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}

    void OnDisable()
    {
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", savedOffset);

        if (playerLogicScript != null)
        {
            playerLogicScript.pauseGame -= PauseGame;
        }
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
    }
}
