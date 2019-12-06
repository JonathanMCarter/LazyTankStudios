using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{

    public List<int> ID;
    public List<string> QuestTag;
    public List<Quest.Status> Status;
    public List<int> Collectables;
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
    public bool Check(int QId, string Qtag)
    {
        for (int i = 0; i < ID.Count; i++)
        {
            {
                if (ID[i] == QId)
                {
                    if (QuestTag[i] == Qtag) //added tags as ID could be the same, not unique
                    {
                        if (Status[i] == Quest.Status.Completed)
                        {
                            return true;
                        }
                    }
                }
            }

        }
        return false;
    }
        public bool Collect(int QId, string Qtag)
    {

        for (int i = 0; i < ID.Count; i++)
        {
            {
                if (ID[i] == QId)
                {
                    if (QuestTag[i] == Qtag) //added tags as ID could be the same, not unique
                    {
                        if (Collectables[i] > 0)
                        {
                            Collectables[i]--;
                            SetQuest();
                            return true;
                        }
                    }
                }
            }

        }
            return false;
    }

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
                        q.ItemToCollect = Collectables[i];
                        print(q.ItemToCollect + " " + Collectables[i] + " " + q.gameObject.name);
                        
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

    public void SaveQuest(int id, string questtag, Quest.Status status, int collect)
    {
        for (int i = 0; i < ID.Count; i++)
        {
            if (ID[i] == id)
            {
                if (QuestTag[i] == questtag) //added tags as ID could be the same, not unique
                {
                    if (Status[i] != status) {
                        Status[i] = status;
                        GetComponent<SaveSys>().Save();
                    }
                    //Collectables[i] = collect;
                    return;
                }
            }

        }

        ID.Add(id);
        QuestTag.Add(questtag);
        Status.Add(status);
        Collectables.Add(collect);
        GetComponent<SaveSys>().Save();
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


    private void Awake()
    {
        GetComponent<SaveSys>().Load();
    }
}
