using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonHud : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneConstants.MenuScene);
    }
}
