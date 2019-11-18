using UnityEngine;

public class Bullet : A
{
    public int Dmg;
    public Vector2 Sp;
    public float Lspan;
    public int SourceItem = -1;

    public void SetStats(int dmg, Vector2 speed, float lspan, int sourceItem)
    {
        Dmg = dmg;
        Sp = speed;
        Lspan = lspan;
        SourceItem = sourceItem;

        GetComponent<Rigidbody2D>().AddForce(Sp, ForceMode2D.Impulse);
        if (Lspan >= 0) Destroy(gameObject, Lspan);
    }

    public void IncreaseSpeed()
    {
        GetComponent<Rigidbody2D>().AddForce(Sp, ForceMode2D.Impulse);
        Sp = Sp * 2;
    }

    public void IncreaseDamage(int dmgChange)
    {
        Dmg = Dmg * dmgChange;
    }
}
