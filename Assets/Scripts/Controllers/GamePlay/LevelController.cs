using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] Player player;
    public Player playerValue => player;
    [SerializeField] Icons levelIcons;
    [SerializeField] int nextSceneIndex;
    [SerializeField] GameObject playerDefaultPrefab;
    [SerializeField] CameraController cameraController;
    [SerializeField] PlayerController playerController;
    [SerializeField] UIConfigurator uiConfigurator;

    MapGenerator mapGenerator;

    List<List<Room>> levelMap;
    GameObject activeRoom;
    RoomController activeRoomController;
    int currentLayer = 0;

    public Action<int> onRoomChange;
    public Action onLevelFinish;
    private void Awake()
    {
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.GetComponent<Player>();
            }
            else
            {
                playerGO = Instantiate(playerDefaultPrefab);
                player = playerGO.GetComponent<Player>();
            }
            cameraController.SetTarget(playerGO);
            playerController.SetPlayer(player);
        }
        mapGenerator = GetComponent<MapGenerator>();
    }

    private void Start()
    {
        uiConfigurator.ConfigureWeaponCageManager(player);
        UpdateMap();
        LoadNextStage(0);
    }

    public void UpdateMap()
    {
        levelMap = mapGenerator.GenerateMapWithGlobalPool();
    }

    private void LoadNextStage(int ind)
    {
        ClearPickUps();
        if (activeRoom != null)
        {
            activeRoomController.onRoomChange -= OnLoadRequest;
            Destroy(activeRoom);
        }

        Room newRoom = levelMap[currentLayer][ind];
        if (newRoom.prefab != null)
        {
            activeRoom = Instantiate(newRoom.prefab);
            activeRoomController = activeRoom.GetComponentInChildren<RoomController>();
        }
        Vector3 startLocation = activeRoomController.startPosition.position;
        Debug.Log($"Warping player to {startLocation}");
        player.Warp(startLocation);

        activeRoomController.Initialize(GetNextLayerRooms(), player, levelIcons);

        activeRoomController.onRoomChange += OnLoadRequest;
        activeRoomController.onFinalRoomChange += OnLevelEnd;
    } 

    private void OnLoadRequest(int ind)
    {
        currentLayer += 1;
        LoadNextStage(ind);
    }

    private void ClearPickUps()
    {
        List<GameObject> pickUps = new List<GameObject>();
        GameObject.FindGameObjectsWithTag("PickUp", pickUps);
        foreach(GameObject item in pickUps)
        {
            Destroy(item);
        }
    }

    private List<Room> GetNextLayerRooms()
    {
        if (currentLayer == levelMap.Count - 1) { return new List<Room>(); }
        return levelMap[currentLayer + 1];
    }

    private void OnLevelEnd()
    {
        
        onLevelFinish.Invoke();
    }
    public void LoadNextLevel()
    {
        if (nextSceneIndex != 0)
        {
            DontDestroyOnLoad(player);
        }
        else
        {
            Destroy(player);
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
