
using UnityEngine;

public class Switch : A
{
    bool active;
    SpriteRenderer MyRend;
    public Sprite ClosedSprite,OpenSprite;
    public GameObject[] Doors;
    GameObject[] _MySwitch;
    // Start is called before the first frame update

    public enum SwitchType
    {
        Good,
        Bad,
        None,
    };
    public SwitchType _Switch;
    void Start()
    {
        MyRend = GetComponent<SpriteRenderer>();
        Doors = GameObject.FindGameObjectsWithTag("PuzzleDoor");
        _MySwitch = GameObject.FindGameObjectsWithTag("Switch");
    }
    int OpenDoor()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            if (Doors[i].GetComponent<BoxCollider2D>().enabled == true)
            {
                Doors[i].GetComponent<BoxCollider2D>().enabled = false;
                Doors[i].GetComponent<SpriteRenderer>().sprite = OpenSprite;
                active = true;
                return 0;
            }

        }
        return 0;
    }
    void CloseDoor()
    {
        for(int i = 0; i < Doors.Length; i++)
        {
            Doors[i].GetComponent<BoxCollider2D>().enabled = true;
            Doors[i].GetComponent<SpriteRenderer>().sprite = ClosedSprite;
            for(int j = 0; j < _MySwitch.Length; j++)
              _MySwitch[j].GetComponent<Switch>().active = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(MyRend.flipX)
          MyRend.flipX = false;
        else
          MyRend.flipX = true;
        switch (_Switch)
        {
            case SwitchType.Good:
                {
                    if(!active)
                       OpenDoor();
                    break;
                }
            case SwitchType.Bad:
                {
                    CloseDoor();
                    break;
                }
        }
    }
}
