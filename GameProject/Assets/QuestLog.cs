using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{

    public List<int> ID;
    public List<string> QuestTag;
    public List<Quest.Status> Status;
    // Start is called before the first frame update


    //public void SetQuest(int id, string questtag, Quest.Status status)
    //{
    //    for (int i = 0; i < ID.Count; i++)
    //    {
    //        if (ID[i] == id)
    //        {
    //            if (QuestTag[i] == questtag) //added tags as ID could be the same, not unique
    //            {
    //                status = Status[i];
    //            }
    //        }

    //    }
    //}

    public void SetQuest()
    {
        Quest[] Q = FindObjectsOfType<Quest>();

       // print(Quest.currQuest);
        for (int i = 0; i < ID.Count; i++)
        {
            foreach (Quest q in Q)
            {
                if (ID[i] == q.ID)
                {
                    if (QuestTag[i] == q.QuestTag) //added tags as ID could be the same, not unique
                    {
                        q.status = Status[i];
                    }
                }
            }

        }


        //foreach (Quest Q in FindObjectsOfType<Quest>())
        // {
        //     if (Q.ID == id)
        //     {
        //         if (Q.QuestTag == )
        //     }
        // }
    }

    public void SaveQuest(int id, string questtag, Quest.Status status)
    {
        for (int i = 0; i < ID.Count; i++)
        {
            if (ID[i] == id)
            {
                if (QuestTag[i] == questtag) //added tags as ID could be the same, not unique
                {
                    Status[i] = status;
                    return;
                }
            }

        }

        ID.Add(id);
        QuestTag.Add(questtag);
        Status.Add(status);
    }

    //int Search(int id, string qt)
    //{

    //    for (int i = 0; i < ID.Count; i++)
    //    {
    //        if (ID[i] == id)
    //        {
    //            if (QuestTag[i] == qt) //added tags as ID could be the same, not unique
    //            {
    //                return i;
    //            }
    //        }

    //    }
    //    return -1;
    //}

}
