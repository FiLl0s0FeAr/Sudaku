using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManagerScript : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
