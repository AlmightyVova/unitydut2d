using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelController : MonoBehaviour
{
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;
    public void WinLevel()
    {
        Debug.Log("LEVEL WON!");
        PlayerPrefs.SetInt("levelReached",levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("log smth");
        if (other.gameObject.CompareTag("nextLevelPoint"))
        {
            Debug.Log("nextLevelPoint Reached");
            WinLevel();
        }
    }
}