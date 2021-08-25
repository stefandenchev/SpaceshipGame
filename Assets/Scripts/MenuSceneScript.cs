using UnityEngine;
using System.Collections;

public class MenuSceneScript : MonoBehaviour 
{

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnPlayClicked()
    {
        Application.LoadLevel("GameScene");
    }
}
