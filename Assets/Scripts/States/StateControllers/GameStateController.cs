using UnityEngine;
using UnityEngine.SceneManagement;
using static StateController;

public class GameStateController : StateController
{
    [SerializeField] PlayerController playerController;
    [SerializeField] LevelController levelController;
    [SerializeField] UIConfigurator uiConfigurator;
    private void Awake()
    {
        m_stateActivator = new StateActivator();
    }

    private void Start()
    {
        playerController.onPauseToogle += OnEscButton;
        playerController.onInventoryToogle += GoToInventory;
        playerController.player.onPlayerDeath += GoToDeath;
        levelController.onLevelFinish += GoToEndLevel;
        levelController.onRunFinish += GoToRunEnd;
        var states = GetComponentsInChildren<IState>(true);
        foreach (var state in states)
        {
            m_stateActivator.Add(state);
        }

        m_stateActivator.Activate<GamePlayState>();
    }

    public void OnEscButton()
    {
        uiConfigurator.UpdateXP();
        if (!((m_stateActivator.current is PauseState) || (m_stateActivator.current is InventoryState) || (m_stateActivator.current is SettingsState)))
        {
            m_stateActivator.Push<PauseState>();
        }
        else
        {
            m_stateActivator.Back();
        }
    }
    public void GoToGamePlay()
    {
        m_stateActivator.Activate<GamePlayState>();
    }
    public void GoToEndLevel()
    {
        uiConfigurator.UpdateXP();
        m_stateActivator.Push<LevelEndState>();
    }
    public void GoToSettings()
    {
        m_stateActivator.Push<SettingsState>();
    }
    
    public void GoToInventory()
    {
        if (!(m_stateActivator.current is InventoryState))
        {
            m_stateActivator.Push<InventoryState>();
        }
        else
        {
            m_stateActivator.Back();
        }
    }
    public void GoToDeath()
    {
        uiConfigurator.UpdateXP();
        m_stateActivator.Push<DeathState>();
    }

    public void GoToRunEnd()
    {
        uiConfigurator.UpdateXP();
        m_stateActivator.Push<RunEndState>();
    }

    public void GoToMenu()
    {
        JsonManager.SaveToJson();
        Destroy(playerController.player);
        SceneManager.LoadScene(0);
    }
}
