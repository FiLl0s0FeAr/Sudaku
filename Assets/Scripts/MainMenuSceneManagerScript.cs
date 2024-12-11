using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManagerScript : MonoBehaviour
{
    public Text difficultyText;

    private void Update()
    {
        if (GameSettings.EasyMiddleHard_Number == 1) { difficultyText.text = "Easy"; }
        else if (GameSettings.EasyMiddleHard_Number == 2) { difficultyText.text = "Middle";}
        else if (GameSettings.EasyMiddleHard_Number == 3) { difficultyText.text = "Hard"; }
    }
    public void OnClickNewGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickDifficultyButton()
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
