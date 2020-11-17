using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerLevelController : MonoBehaviour
{
    private string _currentSceneName;
    private int _currentLevel;
    private string _nextLevelName;

    private void Start()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
        _currentLevel = Int32.Parse(_currentSceneName.Substring(_currentSceneName.Length-1));
        _nextLevelName = $"Level{_currentLevel+1}";
        PlayerPrefs.SetInt("levelReached",_currentLevel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("nextLevelPoint"))
        {
            WinLevel();
        }
        else if(other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.ChangeScore(1);
        }
    }
    
    public void WinLevel()
    {
        Debug.Log($"{_currentSceneName} {_currentLevel} {_nextLevelName}");
        PlayerPrefs.SetInt("levelReached",_currentLevel);
        SceneManager.LoadScene(_nextLevelName);
    }
}