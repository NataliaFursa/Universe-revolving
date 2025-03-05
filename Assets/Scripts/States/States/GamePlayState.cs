using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayState : IState
{
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;
    [SerializeField] Player player;

    public Action onGamePlay;

    private void Awake()
    {
        player = playerController.player;
    }
    override protected void OnEnter()
    {
        cameraController.ToogleCameraState(true);

        if (inputActions != null)
        {
            inputActions.FindActionMap("Player").Enable();
        }
        //onGamePlay?.Invoke();
    }

    override protected void OnExit()
    {
        cameraController.ToogleCameraState(false);
        if (inputActions != null)
        {
            inputActions.FindActionMap("Player").Disable();
        }
    }
    public override void Activate()
    {
        base.Activate();
        Time.timeScale = 1f;
        cameraController.ToogleCameraState(true);
        onGamePlay?.Invoke();
    }
    public override void Deactivate()
    {
        base.Deactivate();
        Time.timeScale = 0f;
        cameraController.ToogleCameraState(false);
    }
}
