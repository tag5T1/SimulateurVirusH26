using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Scene0()
    {
        ControlGeneral.Pause();
        SceneManager.LoadScene("Main Menu");
    }


    public void FermerApp()
    {
        Application.Quit();
    }
}
