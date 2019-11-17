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

public class GameManager : A
{
    PlayerMovement Player;
     GameObject Menu;
    public bool isPaused=false;

     void Start()
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
        DoNotDes [] Gos = FindObjectsOfType<DoNotDes>();
        DoNotDes.Created = false;
        foreach (DoNotDes go in Gos) if (go.gameObject != gameObject) Destroy(go.gameObject);
    }

    /// <summary>
    /// Pauses/UnPauses the Game
    /// </summary>
    //public void TogglePauseEnemies()
    //{
    //    isPaused=!isPaused;
    //}


    /// <summary>
    /// Toggles the player movement script on and off
    /// </summary>
    public void TogglePlayerMovement()
    {
        Player.enabled = !Player.enabled;
    }



}
