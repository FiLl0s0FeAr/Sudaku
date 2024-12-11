using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySceneManagerScript : MonoBehaviour
{
    public Button EasyButton;
    public Button MiddleButton;
    public Button HardButton;
    public void OnClickEasyButton()
    {
        GameSettings.EasyMiddleHard_Number = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickMiddleButton()
    {
        GameSettings.EasyMiddleHard_Number = 2;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickHardButton()
    {
        GameSettings.EasyMiddleHard_Number = 3;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
