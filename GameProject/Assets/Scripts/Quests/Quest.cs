using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest : MonoBehaviour
{
    #region Variables

    #region Public Variables
    //ID                   -   To maintain the order of the Quests
    public int ID;

    //Type                 -   To define if the player has to return to someone in order to complete the quest
    public enum Type { Return, NonReturn };
    public Type type;

    //NPC                  -   To define the GameObject the player has to return to   
    public GameObject NPCToReturnTo;

    //Dialogue             -   The dialogue that will play when the quest is given to the player
    public DialogueFile Dialogue;

    //QuestCompleted       -   The dialogue that will play when the quest is completed
    public DialogueFile QuestCompleted;

    [Space(5)]
    [Header("Quest Scope")]
    [Space(15)]

    //Booleans             -   Tick the task of the quest
    public bool KillRequest;
    public bool CollectRequest;
    public bool DeliverRequest;

    [Space(5)]
    [Header("Quest Set Up")]
    [Space(15)]
    [SerializeField]

    //Kills Array          -   Drag in the GameObjects that must be killed for the player to complete the quest
    public List<GameObject> Kills = new List<GameObject>();

    //Items To Be Collected-   Drag the sprites of the items that need to be collected
    public Sprite[] ItemsToBeCollected;

    //Items Quantities     -   The number of the corresponding items that need to be collected (
    public int[] ItemsQuantities;

    //Deliver Gold         -   The Gold that must be delivered on quest
    public int DeliverGold;

    //Reward enum          -   The type of reward playe is going to get
    public enum Reward { Items, Gold, GoldAndItems }

    [Space(5)]
    [Header("Rewards Set Up")]
    [Space(15)]

    public Reward reward;

    //Rewards Items        -   Drag in the sprites of the items the player will be rewarded with
    public Sprite[] rewardItems;

    //New Item             -   The reference of a new item prefab that will be used for instantiating the items on the ground ready to be picked up
    GameObject newItem;

    [Tooltip("X = ID , Y = Quantity ")]

    //Rewards IDs and Quant-   The pre-set numbers for correspoding ID for the item as well as the quanitity of it (X = ID , Y = Quantity)
    public Vector2[] rewardIdsAndQuantities;

    //Reward gold          -   The gold player is going to get at the end of the quest
    public int rewardGold;


    //Items Status         -   Defines if a quest is completed/Ongoing/Available or Not Available
    public enum Status { NotAvailable, Available, OnGoing, Completed };

    [Space(15)]
    public Status status;

    #endregion

    #region Private Variables

    //GameManagerTalk                   -   Accesses the Dialogue Handler 
    TalkScript GameManagerTalk;


    //Colliders & ContactFi-   Helps to catch the number of colliders for the NPC To Return To (as the NPC To Return To might not be always the one you start the quest at)
    Collider2D[] colliders = new Collider2D[10];
    ContactFilter2D contactFilter = new ContactFilter2D();

    //Quests               -   Holds all quests within the game for quests manipulation
    Quest[] quests;

    //Inventory            -   Reference to the player inventory
    Inventory inv;
    #endregion

    #endregion

    private void Awake()
    {
        //Get the reference to the new item prefab
        newItem = Resources.Load<GameObject>("New Item");

        //Get the reference to the Player inventory
        inv = GameObject.FindObjectOfType<Inventory>();

        //Catches all the quests in the game
        quests = GameObject.FindObjectsOfType<Quest>();

        //Catches the GameManager TalkScript Component
        GameManagerTalk = GameObject.Find("GameManager").GetComponent<TalkScript>();

        //Sets available just the first quest at the beginning of the game - Creating a sequence of quests
        status = this.ID == 0 ? Status.Available : Status.NotAvailable;

        //Disable all other quests within the game based on the its status 
        gameObject.transform.GetComponent<BoxCollider2D>().enabled = status == Status.Available ? true : false;
        this.enabled = this.status == Status.Available;
    }


    private void Update()
    {
        //Checks NPC gameObject availability and the type of the Quest
        if (NPCToReturnTo && type.Equals(Type.Return))
        {
            //Checks the number of colliders over the NPC (Starts from 1 because every NPC has an interaction zone with a BoxCollider2D attached)
            //Also checks for the status of the quest
            if (NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 1
                && status.Equals(Status.Completed))
            {
                //Display Quest Completed Dialogue and Enable Next Quest
                displayQuestCompletedDialogue();
                findNextQuest();

            }
        }
        //This means type is nonreturn and once it is completed display quest completed dialogue and enable next quest
        else if (status.Equals(Status.Completed))
        {
            //Display Quest Completed Dialogue and Enable Next Quest
            displayQuestCompletedDialogue();
            findNextQuest();
        }

        //Check if the quest is ongoing
        if (status == Status.OnGoing)

            //If all enemies have been killed or all items collected or all gold received 
            if (checkKilledAllEnemies() && KillRequest
                ||
                checkItemsCollected() && CollectRequest
                ||
                DeliverRequest && inv.getCoins() >= DeliverGold)
            {

                //Set status completed
                status = Status.Completed;

                //Enable NPC To Return To the BoxCollider2D component in order to be able to interact with its
                NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled ? true : true;
            }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if player has collided with NPC that holds the quest and if the quest is active
        if (collision.gameObject.name == "Hero" && this.isActiveAndEnabled)
        {
            //Gathers the local TalkScript Component and assign the public Dialogue item
            GetComponent<TalkScript>().dialogueEnglish = Dialogue;

            //Checks if it is a deliver quest and status indicates the quest has not been taken yet
            if (DeliverRequest && status == Status.Available)
                inv.addCoins(DeliverGold);

            //Set the quest as Ongoing
            status = Status.OnGoing;


        }
    }


    //Checks if all enemies have been successfully killed for the completion of the quest
    private bool checkKilledAllEnemies()
    {

        bool state = false;

        //If there are kills to be made then check their availability in Hierarchy
        if (Kills.Count > 0)
            foreach (GameObject enemy in Kills)
                state = enemy.activeInHierarchy ? false : true;

        //If all the enemies are inactive checkKilledAllEnemies will return true
        return state;
    }

    //Check for all the items needed for the completion of the quest
    private bool checkItemsCollected()
    {
        bool state = false;

        //If there are items to be collected then check the quanitites
        foreach (int elementQuantity in ItemsQuantities)
            state = elementQuantity == 0 ? true : false;

        //If quantity is 0 then all elements have been collected and will return true
        return state;
    }

    //Displays 'Quest Completed' dialogue at the end of the quest
    void displayQuestCompletedDialogue()
    {
        //If the quest is a deliver request check if player still have the money
        if (DeliverRequest)
        {
            //Positive result will substract the money and reward player
            if (inv.getCoins() >= DeliverGold)
            {
                inv.addCoins(-DeliverGold);
                GameManagerTalk.dialogueEnglish = QuestCompleted;
                GameManagerTalk.talk();
                offerReward(reward);
            }
            else
                Debug.LogError("This " + this.ID + " quest has failed because you spend all the money");
        }

        //Quest is not deliver request and has been completed, therefore offer reward and display text
        else
        {

            GameManagerTalk.dialogueEnglish = QuestCompleted;
            GameManagerTalk.talk();
            offerReward(reward);

        }


    }

    //Finds the next quest based on ID
    public void findNextQuest()
    {
        bool lastQuest = true;

        foreach (Quest component in quests)
        {
            //If there is a next quest enables it + its box collider
            if (component.ID == this.ID + 1)
            {
                component.enabled = component.gameObject.transform.GetComponent<BoxCollider2D>().enabled = true;
                component.status = Status.Available;

                //Disable the current quest
                this.enabled = false;

                //If the next quest is on the same game object this is not the last quest
                lastQuest = component.gameObject == gameObject ? false : true;

            }
            else
            {
                //Secure check to maintain the completed quests disabled
                component.enabled = component.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                component.status = Status.NotAvailable;
                this.enabled = false;

            }
        }

        //If it is the last quest on this game object disable it and its box collider
        if (lastQuest)
        {
            this.enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }


    }

    //Offers rewards depending on the set up
    public void offerReward(Quest.Reward typeOfReward)
    {
        //Create a random seed
        Random.InitState(Random.Range(0, 100));

        //If is a non return type update position to the players position instead of the NPC to return to
        Vector3 updatedPosition = type == Type.Return ? NPCToReturnTo.transform.position : GameObject.Find("Hero").transform.position;

        switch (typeOfReward)
        {
            case Reward.Items:
                {
                    //For every item set up
                    for (int i = 0; i < rewardItems.Length; i++)
                    {

                        //Create the object at the given position
                        GameObject temp = Instantiate(newItem, updatedPosition, Quaternion.identity);

                        //Add the correct sprite and set the sorting order
                        temp.GetComponent<SpriteRenderer>().sprite = rewardItems[i];
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 20;

                        //Scale the object to match the rest of the game pick up items
                        temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                        //Check ID to be between 0 and 11 in order to match the slots of the inventory
                        if ((int)rewardIdsAndQuantities[i].x >= 0 && (int)rewardIdsAndQuantities[i].x < 12)
                            temp.GetComponent<InventoryItem>().ID = (int)rewardIdsAndQuantities[i].x;

                        //Check the Quantity to be greater than 0
                        if ((int)rewardIdsAndQuantities[i].y > 0)
                            temp.GetComponent<InventoryItem>().quantity = (int)rewardIdsAndQuantities[i].y;

                        temp.AddComponent<Rigidbody2D>();

                        //AddForce creates the jumping effect of the reward Items
                        temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));

                        //Change the Rigidbody to a static one and set the gravity to 0
                        StartCoroutine(freezeItemGravity(temp.GetComponent<Rigidbody2D>()));

                    }
                    break;


                }

            case Reward.Gold:
                inv.addCoins(rewardGold);
                break;
            case Reward.GoldAndItems:
                {
                    //The first two cases combined
                    for (int i = 0; i < rewardItems.Length; i++)
                    {

                        GameObject temp = Instantiate(newItem, updatedPosition, Quaternion.identity);

                        temp.GetComponent<SpriteRenderer>().sprite = rewardItems[i];
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 20;
                        temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        if ((int)rewardIdsAndQuantities[i].x >= 0 && (int)rewardIdsAndQuantities[i].x < 12)
                            temp.GetComponent<InventoryItem>().ID = (int)rewardIdsAndQuantities[i].x;
                        if ((int)rewardIdsAndQuantities[i].y > 0)
                            temp.GetComponent<InventoryItem>().quantity = (int)rewardIdsAndQuantities[i].y;

                        temp.AddComponent<Rigidbody2D>();

                        temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));
                        StartCoroutine(freezeItemGravity(temp.GetComponent<Rigidbody2D>()));

                    }
                    inv.addCoins(rewardGold);
                    break;
                }
            default:
                break;
        }

    }

    //Change the Rigidbody to a static one and set the gravity to 0
    IEnumerator freezeItemGravity(Rigidbody2D temporaryObject)
    {
        yield return new WaitForSeconds(0.5f);
        temporaryObject.gravityScale = 0;
        temporaryObject.bodyType = RigidbodyType2D.Static;
    }








// Jonathan Added this
    public bool CheckID(int Input)
        {
            if (Input == ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    // Jonathan Also Added this - can be called to update the status of a quest!
    public void UpdateStatus(int QuestID, Status NewStatus)
    {
        if (FindObjectsOfType<Quest>().Length > 1)
        {
            for (int i = 0; i < FindObjectsOfType<Quest>().Length; i++)
            {
                if (FindObjectsOfType<Quest>()[i].ID == QuestID)
                {
                    FindObjectsOfType<Quest>()[i].status = NewStatus;
                }
            }
        }
        else
        {
            if (FindObjectOfType<Quest>().ID == QuestID)
            {
                FindObjectOfType<Quest>().status = NewStatus;
            }
        }
    }
}




