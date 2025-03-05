using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] GameObject UpgradePanel;
    [SerializeField] GameObject TitlePanel;

    public void GoToUpgrade()
    {
        UpgradePanel.SetActive(true);
        TitlePanel.SetActive(false);
    }
    public void GoToTitle()
    {
        UpgradePanel.SetActive(false);
        TitlePanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartDemo()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}