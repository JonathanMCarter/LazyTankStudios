using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Owner: Toby Wishart
 * Last edit: 03/11/19
 * 
 * Script for moving the player to the destination of the gate and move the player across the screen
 *
 *
 * Also edited by:Andreas Kraemer
 * Last edit: 21/10/19
 *
 * Added sound effects
 *
 */
public class PlayerZoneTransition : A
{

   // BoxCollider2D cameraBox;

   // Animator pAnim;
    AsyncOperation asyncLoad;

   // float sX, sY;

    public string Destination;
    string[] destSplit;
   // bool transitioning = false;

   // SoundPlayer audioManager;



    public void StartTransition(string Destination)
    {
       // SceneManager.LoadScene(Destination);


    destSplit = Destination.Split(':');

            asyncLoad = SceneManager.LoadSceneAsync(destSplit[0]);
            //asyncLoad.allowSceneActivation = false;
        StartCoroutine(waitUntilLoaded());
        
    }


//void FinishTransition()
//{

//   // asyncLoad.allowSceneActivation = true;
   

//}

private IEnumerator waitUntilLoaded()
{
    yield return new WaitUntil(() => asyncLoad.isDone);
    Vector3 newPos = GameObject.Find(destSplit[1]).transform.position;
    //Preserve the player's Z position
    transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

}
}
