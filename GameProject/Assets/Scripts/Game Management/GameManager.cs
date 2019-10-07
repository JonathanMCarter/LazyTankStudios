using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Game Manager Script
 * 
 *  Controls the state of the game 
 *
 * Owner: Andreas Kraemer
 * Last Edit : 5/10/19
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Lock player movement when game is not active
 * 
 * */

public class GameManager : MonoBehaviour
{
    private PlayerMovement Player;
    private GameObject Menu;


    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>(); // Finds the player movement script. Added by LC
        Menu = GameObject.Find("MainMenu"); // Finds the main menu
    }



    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Launches the game and closes menu
    /// </summary>
    public void StartGame()
    {
        TogglePlayerMovement();
        Menu.SetActive(false); //hides the menu
    }

    /// <summary>
    /// Toggles the player movement script on and off
    /// </summary>
    public void TogglePlayerMovement()
    {
        Player.enabled = !Player.enabled;
    }



}
