using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void PauseGame();

public class PlayerLogicScript : MonoBehaviour
{
    public Slider healthBar;
    public Text score;
    public GameObject deadLabel;
    public event PauseGame pauseGame;
    int _health;
    int _score;
    bool isAlive = true;
    string scoreKey = "Score";

    void Start()
    {
        Health = 100;
        Score = PlayerPrefs.GetInt(scoreKey, 0);
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthBar.value = value;
            if(_health <= 0)
            {
                DestroyMe();
                OnPauseClicked();
            }
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            if (isAlive)
            {
                _score = value;
                score.text = value.ToString();
                PlayerPrefs.SetInt(scoreKey, value);
            }
        }
    }

    public void OnPauseClicked()
    {
        pauseGame.Invoke();
    }

    void DestroyMe()
    {
        if (!isAlive)
        {
            return;
        }
        PlayerPrefs.DeleteKey(scoreKey);
        isAlive = false;
        deadLabel.SetActive(true);
        isAlive = false;
        Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(90f, 0f, 0f));
        GetComponent<MeshRenderer>().enabled = false;
    }
}