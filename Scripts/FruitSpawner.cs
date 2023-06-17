using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : Singleton<FruitSpawner>
{
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private IntEventChannelSO playerGotFruitChannel;

    private void OnEnable()
    {
        playerGotFruitChannel.OnEventRaised += (ID) => SpawnFruit(ID);
    }

    private void OnDisable()
    {
        playerGotFruitChannel.OnEventRaised += (ID) => SpawnFruit(ID);
    }

    public void SpawnFruit(int ID = GlobalConstants.NULL_PLAYER_ID){
        if(ID == GlobalConstants.NULL_PLAYER_ID) Instantiate(fruitPrefab, randomPosition(), Quaternion.identity);
    }

    private Vector3 randomPosition()
    {
        Vector3 output = this.transform.position;

        output.x += Random.Range(spawnRange.x * -1, spawnRange.x);
        output.y += Random.Range(spawnRange.y * -1, spawnRange.y);

        return output;
    }
}
