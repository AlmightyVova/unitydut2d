using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPointScript : MonoBehaviour
{
    private string nextLevel;
    public int levelToUnlock;
    private void OnTriggerEnter2D(Collider2D other)
    {
        nextLevel = $"Level{levelToUnlock}";
        if (other.gameObject.CompareTag("Player"))
        {
            WinLevel();
        }
    }
    
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached",levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }
}
