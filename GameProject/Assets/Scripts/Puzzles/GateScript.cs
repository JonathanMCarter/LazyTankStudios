public class GateScript : A
{
    public int BouldersPushed;
    void Start()
    {
        BouldersPushed = 0;
    }

    public void AddBoulder()
    {
        BouldersPushed++;
    }
    void Update()
    {
        if(BouldersPushed == 2) gameObject.SetActive(false);

             
    }
}
