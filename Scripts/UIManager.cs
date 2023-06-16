using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomMonoBehaviorWrapper
{
    [SerializeField] private GameObject playerUIPrefab;
    [SerializeField] private GameObject playerUIHolder;
    [SerializeField] private PlayerScriptableObjectChannelSO setUpPlayerUIChannel;

    private void OnEnable()
    {
        setUpPlayerUIChannel.OnEventRaised += (player) => GeneratePlayerUI(player);
    }

    private void OnDisable()
    {
        setUpPlayerUIChannel.OnEventRaised -= (player) => GeneratePlayerUI(player);
    }

    private void GeneratePlayerUI(Player player)
    {
        GameObject playerUI = Instantiate(playerUIPrefab, playerUIHolder.transform);
        playerUI.GetComponent<PlayerUIHelper>().setID(player.ID);
    }

}
