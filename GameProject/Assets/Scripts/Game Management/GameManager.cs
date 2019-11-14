using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isPaused=false;


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
        Application.Quit(); //Need to hide the Exit button on WebGL build (and possibly mobile)
    }

    /// <summary>
    /// Launches the game and closes menu
    /// </summary>
    public void StartGame()
    {
        TogglePlayerMovement();
        Menu.SetActive(false); //hides the menu
    }

    public void ExitToMenu()
    {
       SceneManager.LoadScene("Main Menu");
        DoNotDes [] Gos = GameObject.FindObjectsOfType<DoNotDes>();
        DoNotDes.Created = false;
        foreach (DoNotDes go in Gos) if (go.gameObject != this.gameObject) Destroy(go.gameObject);
    }

    /// <summary>
    /// Pauses/UnPauses the Game
    /// </summary>
    public void PauseGame()
    {
        if(!isPaused)Time.timeScale=0;
        else Time.timeScale=1;
    }

    public void PauseGame(bool pause)
    {
        if(pause)Time.timeScale=0;
        else Time.timeScale=1;
    }

    /// <summary>
    /// Toggles the player movement script on and off
    /// </summary>
    public void TogglePlayerMovement()
    {
        Player.enabled = !Player.enabled;
    }



}
