using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transitor : IInteractable
{
    public int targetInd;
    public Room roomSO;
    public Action<int> onActivate;
    public Action onFinalActivate;
    private Canvas iconCanvas;
    private DoorsAnimationController doorController;
    private ParticlePortalAnimationController portalController;
    private bool isActive = false;
    private Icons levelIcons;

    public void Initiate(Room room, int roomInd, Icons levelIcons)
    {
        this.levelIcons = levelIcons;
        if (doorController == null)
        {
            doorController = GetComponent<DoorsAnimationController>();
        }
        if (portalController == null)
        {
            portalController = GetComponent<ParticlePortalAnimationController>();
        }
        iconCanvas = GetComponentInChildren<Canvas>();
        roomSO = room;
        targetInd = roomInd;
    }

    public void FinalInitiate(Icons levelIcons)
    {
        this.levelIcons = levelIcons;
        doorController = GetComponent<DoorsAnimationController>();
        iconCanvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if (iconCanvas != null && Camera.main != null)
        {
            iconCanvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void AddIcon(Texture icon)
    {
        RawImage iconImage = iconCanvas.GetComponentInChildren<RawImage>();
        iconImage.texture = icon;
        Color col = iconImage.color;
        col.a = 1;
        iconImage.color = col;

    }

    public void Enable()
    {
        if (roomSO != null)
        {
            switch (this.roomSO.GetType().Name)
            {
                case nameof(BattleRoom): AddIcon(levelIcons.battleRoom); break;
                case nameof(RewardRoom): AddIcon(levelIcons.rewardRoom); break;
                case nameof(RestRoom): AddIcon(levelIcons.restRoom); break;
                case nameof(BossRoom): AddIcon(levelIcons.bossRoom); break;
                case nameof(ShopRoom): AddIcon(levelIcons.shopRoom); break;
                default: AddIcon(levelIcons.restRoom); break;
            }
        }
        else
        {
            AddIcon(levelIcons.nextLevel);
        }
        if (doorController != null)
        {
            doorController.Activate();
        }
        if (portalController != null)
        {
            portalController.ActivatePortal();
        }
        isActive = true;
    }

    override public void Interact()
    {
        if (isActive) Activate();
    }

    private void Activate()
    {
        if (onActivate != null)
        {
            onActivate.Invoke(targetInd);
        }
        else
        {
            if (onFinalActivate != null)
            {
                onFinalActivate.Invoke();
            }
        }
    }
}
