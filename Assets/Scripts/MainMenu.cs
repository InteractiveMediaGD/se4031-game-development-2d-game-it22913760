using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.ResetRunData();
        SceneManager.LoadScene("Level1_Outpost");
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenFINALDONE()
    {
        SceneManager.LoadScene("FinallyDone");
    }
    public void Openscene2()
    {
        SceneManager.LoadScene("Level2_DeadZone");
    }
    public void Openscene3()
    {
        SceneManager.LoadScene("Level3_FinalStand");
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}