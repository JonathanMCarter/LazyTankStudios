public class DoNotDes : A
{
    public static bool Created;

    int TimesFound;

    void Awake()
    {
        if(!Created) DontDestroyOnLoad(this); else Destroy(gameObject);
        //DontDestroyOnLoad(this);

    }

    void Start()
    {
       Created = true; //temp added by LC. excludes AudioManager as temp fix
    }

    //private void Update()
    //{
    //    if (SceneManager.GetActiveScene().name == "Main Menu")
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(true);
    //    }
    //}
}
