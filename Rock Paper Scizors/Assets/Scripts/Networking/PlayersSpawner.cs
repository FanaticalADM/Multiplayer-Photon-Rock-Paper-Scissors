using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayersSpawner : MonoBehaviour
{
    public static PlayersSpawner Instance;
    private SpawnPoint[] spawnPoints;

    private void Awake()
    {
        Instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }

    public void SpawnPlayer(GameObject player)
    {
        do
        {
            int index = UnityEngine.Random.Range(0, spawnPoints.Length);
            if (spawnPoints[index].IsActive)
            {
                player.transform.position = spawnPoints[index].transform.position;
                break;
            }
        } while (true);
    }

    public Vector3 SpawnPlayer()
    {
        do
        {
            int index = UnityEngine.Random.Range(0, spawnPoints.Length);
            if (spawnPoints[index].IsActive)
            {
                return spawnPoints[index].transform.position;
            }
        } while (true);
    }
}
