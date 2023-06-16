using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private GameState shouldResetOnThisState;
    [SerializeField] private IntEventChannelSO deathChannel;
    [SerializeField] private IntEventChannelSO lostChannel;
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;

    private List<int> playersWhoLost;

    protected override void Awake()
    {
        base.Awake();

        playersWhoLost = new List<int>();
    }

    private void OnEnable()
    {
        deathChannel.OnEventRaised += (ID) => SpawnSnake(ID);
        lostChannel.OnEventRaised += (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised += (state) => {if(state == shouldResetOnThisState) Reset();};
    }

    private void OnDisable()
    {
        deathChannel.OnEventRaised -= (ID) => SpawnSnake(ID);
        lostChannel.OnEventRaised -= (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised -= (state) => {if(state == shouldResetOnThisState) Reset();};
    }

    public void Reset()
    {
        playersWhoLost.Clear();
    }
    
    private bool playerHasLostAlready(int ID)
    {
        if(playersWhoLost.Find(value => value == ID) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SpawnSnake(int ID)
    {
        if(playerHasLostAlready(ID)) return;
        
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.x += Random.Range(spawnRange.x * -1, spawnRange.x);
        spawnPosition.y += Random.Range(spawnRange.y * -1, spawnRange.y);

        GameObject snake = Instantiate(snakePrefab, spawnPosition, Quaternion.identity);
    }

    private void OnPlayerLost(int ID)
    {
        if(playerHasLostAlready(ID)) return;
        
        playersWhoLost.Add(ID);
    }
}
