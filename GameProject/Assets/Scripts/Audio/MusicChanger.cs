public class MusicChanger: A {
 public string newMusic;
 void Start() {
  F<SoundPlayer>().PlayMusic(newMusic);
 }
}