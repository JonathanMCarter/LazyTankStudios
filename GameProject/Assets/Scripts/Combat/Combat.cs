using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Forgive me, for I was the tireds and the things kept a changin' and 1st it was nones then it was states and now it r the butts 
//Now it r around the 1 o'clocks...Tony tired
//I planned for more and better, but I'll only implement what makes sense given what I have...which includes time I can stay awake

//Time finished = 2:45AM 
//Scripts that do combat written = 3...1 that started with nothing. 1 that was using a state machine and 1 where I was given a UI...tbh this is the most visually appealing

//Combat can be activated by opening the menu(esc) and clicking left mouse button whilst selecting items
//...coz thats the easiest way to demonstrate it at the moment


public class Combat : MonoBehaviour
{
    private InputManager IM;

//-----------------------------STUFF I HAVE TO USE THAT WILL PROBABLY CHANGE...AGAIN----------------//    
    //get the texts boxes that do the stats to change what's in them
    public Text PlayerHealth, EnemyHealth;
    //know when teh foes r bean selectatroned
    //get the enemy...words...target...ugh...let thing know when attack...coz logic????
    public GameObject targetPanel;
    public Button EnemyButton;
    public Button DefendButton;
//-------------------------------------------------------------------------------------------------//
    //STUFF THAT WILL STAY THE SAME...there's no reason why it wouldn't
    public int HP;
    public int EnemyHP;
    //stop me if I'm going too fast
    private bool selectingEnemy, didTheAttacks,justTheOnce, didTheDefends;
    public Text CombatText;

    void Start()
    {
        IM = FindObjectOfType<InputManager>();
        selectingEnemy = false;didTheAttacks = false;didTheDefends = false;justTheOnce = false;
        CombatText.text = " ";

    }

    void Update()
    {
        //back out of combat back into game land
        if (IM.Button_Menu() && targetPanel.activeSelf == false)
            GetRidofTheCombatStuff();
        //There button that brings up the panel does this in the editor...so I left it that way
        if (targetPanel.gameObject.activeSelf == true)
            selectingEnemy = true;
        //so yeah now it backs out without leaving combat :thumbsUp:
        if (selectingEnemy && IM.Button_Menu())
            targetPanel.SetActive(false);selectingEnemy = false;
        //do the attack on the click of the enemy
        if(!didTheAttacks)
            EnemyButton.onClick.AddListener(HitEnemy);
        //defend when defend is pressed
        if(!didTheDefends)
            DefendButton.onClick.AddListener(Defended);
        //wait for player to click coz read the stuff, then make the enemy (whatever it is) do a thing...

        if (didTheAttacks && !justTheOnce)
        {
            //I moved this from the function coz the enemy was dying too hard
            --EnemyHP;
            CombatText.text = "You did a damage";
            EnemyHealth.text = "Enemy1 HP:" + EnemyHP + "/10";
            justTheOnce = true;
        }
        else if (didTheAttacks && IM.Button_A())
        { CombatText.text = "The enemy hit you"; --HP; didTheAttacks = false; PlayerHealth.text = "Hero1 HP:" + HP + "/10";justTheOnce = false;}

        if (didTheDefends)
        {
            if (IM.Button_A())
            { CombatText.text = "The enemy hit you...it was super ineffective"; didTheDefends = false;}
        }
    }

    //Reset everything and take away the overlay
    void GetRidofTheCombatStuff() // name pending
    {
        EnemyHP = 10;
        HP = 10;
        EnemyHealth.text = "Enemy1 HP:10/10";
        PlayerHealth.text = "Hero1 HP:10/10";
        gameObject.SetActive(false);
    }

    void HitEnemy()
    {
        selectingEnemy = true;
        didTheAttacks = true;
    }
    
    void Defended()
    {
        CombatText.text = "You raise your guard";
        didTheDefends = true;
    }

}
