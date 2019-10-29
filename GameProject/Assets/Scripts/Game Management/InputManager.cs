using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Input Manager Script
 * 
 * This script gets the input using the Unity Input manager for K/M and controller input, as well as touch data and outputs it using 3 public functions. This allows other scripts to access input data from across all platforms using one function.
 * 
 * Owner: Lewis Cleminson
 * Last Edit : 5/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */


public class InputManager : MonoBehaviour
{
    //public Text DisplayTestText; //display text for which platform is active


    //enum Action {Up, Down, Left, Right, Interact, Menu, None};
    //private bool[] ButtonPressed = new bool [7]; //length of Action enums                 No longer needed.
    private float xAxis;
    private float yAxis;

    private bool TouchOneActive;
    private bool TouchTwoActive;

    private Vector2 TouchStartPos;
    private Vector2 TouchStartPosTwo;

    public float MinSwipeDistance;

    private bool fire1Clicked;
    private bool fire2Clicked;
    private bool fire3Clicked;



    void Start()
    {


#if UNITY_STANDALONE_WIN
        //DisplayTestText.text = ("Standalone");
        HideMobileUI(); //Hides the mobile action buttons
#endif


#if UNITY_ANDROID
       // DisplayTestText.text = "Android";

#endif

#if UNITY_WEBGL
        //DisplayTestText.text = "WebGL";
        HideMobileUI(); //Hides the mobile action buttons
#endif


    }

#if UNITY_ANDROID

    // Update is called once per frame
    void Update()
    {



        if (Input.touchCount >= 1) //if there is atleast one finger on the screen
        {

            if ((Input.touchCount == 1 && !TouchTwoActive) || (Input.touchCount > 1)) //Is the touch the first finger on the screen
            {
                Touch ThisTouch = Input.GetTouch(0); //get touch ID
                //ThisTouch = Input.GetTouch(0); 

                switch (ThisTouch.phase) //Which stage of the touch are we in
                {
                    case (TouchPhase.Began): //This runs during the first frame the finger is on the screen
                        TouchStartPos = ThisTouch.position; //get the starting position
                                                            //TouchOneActive = true; //First touch is now active
                        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                        //if (hit.collider != null)
                        //{
                        //    Debug.Log(hit.collider.gameObject.name);
                        //}


                        break;

                    case (TouchPhase.Ended): //this runs in the frame after the finger left the sccreen
                        xAxis = 0; //reset xaxis to 0
                        yAxis = 0; //reset yaxis to 0
                        break;
                    case (TouchPhase.Moved): //this runs each frame when the finger is moving
                   // case (TouchPhase.Ended):
                        //TouchOneActive = false;
                        float distanceswiped = Vector2.Distance(TouchStartPos, ThisTouch.position); //get the distance travelled so far
                        if (Mathf.Abs(distanceswiped) >= MinSwipeDistance)//Check if a swipe has occured using the minimum swipe distance
                        {

                            if (Mathf.Abs(TouchStartPos.x - ThisTouch.position.x) > Mathf.Abs(TouchStartPos.y - ThisTouch.position.y))//swiping left / right
                            {
                                //axis in UNITY go from -1 to 1.

                                if (TouchStartPos.x < ThisTouch.position.x) //swiping right 
                                {
                                    xAxis = 1; //set Xaxis to 1
                                }
                                else //swiping left 
                                {
                                    xAxis = -1; //set X axis to -1
                                }
                            }
                            else //swiping up / down
                            {
                                if (TouchStartPos.y < ThisTouch.position.y) //swiping up 
                                {
                                    yAxis = 1; //set y axis to 1

                                }
                                else //swiping down 
                                {
                                    yAxis = -1; //set y axis to -1
                                }
                            }


                        }
                        else //touch occured
                        {
                            xAxis = 0; //no swipe so axis should be 0
                            yAxis = 0; //no swipe so axis should be 0
                        }

                        break;
                };

            }
        }
        //repeating as above but for a second finger. Allows for swiping / movement and tapping an object with a second finger at the same time.
        if ((TouchTwoActive && Input.touchCount == 2) || (TouchTwoActive && Input.touchCount != 2) || (Input.touchCount == 2 && !TouchTwoActive)) //are we looking for a second finger touch
        {
            Touch ThisTouch = Input.GetTouch(0); //get the touch ID
            if (Input.touchCount > 1) 
            {
                ThisTouch = Input.GetTouch(1); //If the first finger is still active, get the second finger. (stops tracking wrong finger if they are released in different order to pressed)
            }


            switch (ThisTouch.phase) //which touch phase are we in
            {
                case (TouchPhase.Began): //this runs during the first phase the finger is on the screen
                    TouchStartPosTwo = ThisTouch.position; //get the position
                    TouchTwoActive = true; //set second finger as being active

                    break;


                case (TouchPhase.Ended): //this runs the frame after the finger leaves the screen
                    xAxis = 0; //reset x axis to 0
                    yAxis = 0; //reset y axis to 0
                    TouchTwoActive = false;//touch 2 is no longer active
                    break;
                case (TouchPhase.Moved): //this runs whenever the touch moves position
              //  case (TouchPhase.Ended):
                    //TouchTwoActive = false;
                    float distanceswiped = Vector2.Distance(TouchStartPosTwo, ThisTouch.position);//calculate distance touch has travelled
                    if (Mathf.Abs(distanceswiped) >= MinSwipeDistance)//has a swipe occured
                    {

                        if (Mathf.Abs(TouchStartPosTwo.x - ThisTouch.position.x) > Mathf.Abs(TouchStartPosTwo.y - ThisTouch.position.y))//user is swiping left / right if true
                        {
                            if (TouchStartPosTwo.x < ThisTouch.position.x) //swiping right
                            {
                                xAxis = 1;
                            }
                            else //swiping left 
                            {
                                xAxis = -1;
                            }
                        }
                        else //user is swiping up / down
                        {
                            if (TouchStartPosTwo.y < ThisTouch.position.y) //swiping up
                            {
                                yAxis = 1;

                            }
                            else //swiping down
                            {

                                yAxis = -1;
                            }

                        }


                    }
                    else //touch occured
                    {
                        xAxis = 0;
                        yAxis = 0;
                    }


                    break;
            };

        }

    }

#endif

    /// <summary>
    /// Searches for any game objects tagged as MobileUI and sets them to being inactive.
    /// </summary>
    private void HideMobileUI()
    {
        GameObject[] phoneUI = GameObject.FindGameObjectsWithTag("MobileUI"); //finds all game objects tagged with MobileUI
        foreach (GameObject GO in phoneUI) GO.gameObject.SetActive(false); //sets all game objects with tag to not active
    }

    /// <summary>
    /// Called from an on screen button for Android
    /// </summary>
    public void Fire1Clicked() //called from button in game world (Mobile)
    {
        fire1Clicked = true; //sets fire as being clicked
        print("Fire1");
        StartCoroutine(ClearButtons());
    }

    /// <summary>
    /// Called from an on screen button for Android
    /// </summary>
    public void Fire2Clicked() //called from button in game world (Mobile)
    {
        fire2Clicked = true; //sets fire 2 as being clicked
        print("Fire2");
        StartCoroutine(ClearButtons());
    }

    public void Fire3Clicked()
    {
        fire3Clicked = true;
        print("Fire3");
        StartCoroutine(ClearButtons());
    }

    /// <summary>
    /// waits until end of the frame, then resets buttons to not being clicked. Ensures they are only registered for 1 frame as being used.
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearButtons() 
    {
        yield return new WaitForEndOfFrame();
        //wait until end of frame
        //clear fire buttons as being pressed
        fire1Clicked = false;
        fire2Clicked = false;
        fire3Clicked = false;

    }

    private void FixedUpdate()
    {
       // xAxis = Input.GetAxisRaw("Horizontal");

    }

    /// <summary>
    /// returns the X_Axis values from the user input. For WebGL and Standalone these are defined in Settings -> Input. For Android these are currently set to swipe directions
    /// </summary>
    /// <returns></returns>
    public float X_Axis()
    {
#if UNITY_ANDROID
        return xAxis;
#endif

#if UNITY_WEBGL || UNITY_STANDALONE_WIN
        float x = Input.GetAxisRaw("Horizontal");
        if (x < 0.6f && x > -0.6f) return 0;//deadspace 
        return Input.GetAxisRaw("Horizontal");

#endif


    }

    /// <summary>
    /// returns the Y_Axis values from the user input. For WebGL and Standalone these are defined in Settings -> Input. For Android these are currently set to swipe directions
    /// </summary>
    /// <returns></returns>
    public float Y_Axis()
    {

#if UNITY_ANDROID
        return yAxis;
#endif

#if UNITY_WEBGL || UNITY_STANDALONE_WIN
        float x = Input.GetAxisRaw("Vertical");
        if (x < 0.6f && x > -0.6f) return 0;//deadspace 
        return Input.GetAxisRaw("Vertical");

#endif
    }

    /// <summary>
    /// Returns if the user has clicked Fire1 button this frame. For WebGL and Standalone this button is set in Settings -> Input. For Android the button is on screen and calls Fire1Clicked()
    /// </summary>
    /// <returns></returns>
    public bool Button_A()
    {

#if UNITY_ANDROID
        return fire1Clicked;
#endif
        
        return Input.GetButtonDown("Fire1");
    }

    /// <summary>
    /// returns if the user has clicked Fire2 button this frame. For WebGL and Standalone this button is set in Settings -> Input. For Android the button is on screen and calls Fire2Clicked()
    /// </summary>
    /// <returns></returns>
    public bool Button_B()
    {
#if UNITY_ANDROID
            return fire2Clicked;
#endif
        
        return Input.GetButtonDown("Fire2");
    }

    /// <summary>
    /// retruns if the user has clicked FIre3 button this frame. For WebGL and Standalone this button is set in settings -> Input. For android this button is on screen and calls Fire3Clicked()
    /// </summary>
    /// <returns></returns>
    public bool Button_Menu()
    {
#if UNITY_ANDROID
            return fire3Clicked;
#endif

        return Input.GetButtonDown("Fire3");
    }








}
