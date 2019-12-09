using UnityEngine;
public class LightDoor: A {
public int TotalTiles;
public BoxCollider2D Door;
public GameObject SpriteHolder;
public Sprite Open, Closed;
Sprite[] ClosedSprites;
public int LitTiles;
void Start() {
ClosedSprites = new Sprite[SpriteHolder.transform.childCount];
for (int i = 0; i < SpriteHolder.transform.childCount; i++)
{
ClosedSprites[i] = SpriteHolder.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
};}
void Update() {
if (LitTiles >= TotalTiles) {
Door.enabled = false;
for (int i = 0; i < SpriteHolder.transform.childCount; i++)
{
SpriteHolder.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = Open;
};
} else {
Door.enabled = true;
for (int i = 0; i < SpriteHolder.transform.childCount; i++){
SpriteHolder.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = ClosedSprites[i];
};}}}