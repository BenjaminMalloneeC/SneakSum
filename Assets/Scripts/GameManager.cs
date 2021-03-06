﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string gameState = "Start Screen";
    public static GameManager instance;
    public GameObject titleScreen;
    public GameObject gameUI;
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject playerSpawnPoint;
    public GameObject playerDeathScreen;
    public GameObject gameOverScreen;
    public int lives = 3;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Attempted to crreate a second game manager.");
            Destroy(this.gameObject);
        }
    }

    public void ChangeState(string newState)
    {
        gameState = newState;
    }

    public void StartGame()
    {
        Debug.Log("Game has started");
        ChangeState("Initialize Game");
    }

    private void Update()
    {
        if (gameState == "Start Screen")
        {
            //Do the state behavior
            StartScreen();
            //Check for transitions
        }
        else if (gameState == "Initialize Game")
        {
            //Do the state behavior
            InitializeGame();
            //Check for transitions
            ChangeState("Spawn  Player");
        }
        else if (gameState == "Spawn Player")
        {
            //Do the state behavior
            SpawnPlayer();
            //Check for transitions
            ChangeState("Gameplay");
        }
        else if (gameState == "Gameplay")
        {
            //Do the state behavior
            Gameplay();
            //Check for transitions
            if (player == null && lives > 0)
            {
                ChangeState("Player Death");
            }
            else if (player == null && lives <=  0)
            {
                ChangeState("Game Over");
            }
        }
        else if (gameState == "Player Death")
        {
            //Do the state behavior
            PlayerDeath();
            //Check for transitions
            if (Input.anyKeyDown)
            {
                ChangeState("Spawn Player");
            }
        }
        else if (gameState == "Game Over")
        {
            //Do the state behavior
            GameOver();
            //Check for transitions
            if (Input.anyKeyDown)
            {
                ChangeState("Start Screen");
            }
        }
        else
        {
            Debug.LogWarning("Game manager tried to change to non-existent state: " + gameState);
        }
    }

    public void StartScreen()
    {
        //Show the menu
        if (!titleScreen.activeSelf)
        {
            titleScreen.SetActive(true);
        }
    }

    public void InitializeGame()
    {
        //Reset all variables
        //TODO: Reset varaibles in initializegame
        //Turn off menu
        titleScreen.SetActive(false);
        //Turn on game UI
        gameUI.SetActive(true);
    }

    public  void SpawnPlayer()
    {
        //Add the player to the world
        player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
    }

    public void Gameplay()
    {

    }

    public void PlayerDeath()
    {
        if (!playerDeathScreen.activeSelf)
        {
            playerDeathScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!gameUI.activeSelf)
        {
            gameUI.SetActive(false);
        }
        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
