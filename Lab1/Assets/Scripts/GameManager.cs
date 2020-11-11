using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;
    public void WinLevel()
    {
        Debug.Log("LEVEL WON!");
        PlayerPrefs.SetInt("levelReached",levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("nextLevelPoint"))
        {
            WinLevel();
        }
    }
}
