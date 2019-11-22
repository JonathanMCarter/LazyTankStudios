public class DoNotDes: A {
 public static bool Created;
 void Awake() {
  if (!Created) DontDestroyOnLoad(this);
  else D(gameObject);
 }
 void Start() {
  Created = true;
 }
}