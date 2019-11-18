
public class MusicChanger : A
{
    public string newMusic;
    // Start is called before the first frame update
    void Start()
    {

        FindObjectOfType<SoundPlayer>().PlayMusic(newMusic);
        
    }

}
