public class Unlock : A{
public int Id;
public string Tag;
void Start(){
if (!FindObjectOfType<QuestLog>().Check(Id, Tag)) gameObject.SetActive(false);}}