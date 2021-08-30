using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject rightButtons;
    [SerializeField] public GameObject transformationButton;
    [SerializeField] public GameObject abilityButton;
    List<GameObject> rightButtonsList;

    [SerializeField] private GameObject Spawner;
    PlayersSpawner playerSpawner;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        playerSpawner = Spawner.GetComponent<PlayersSpawner>();
   //     playerSpawner.OnSpawn += AddUi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddUi()
    {
        
    }
}
