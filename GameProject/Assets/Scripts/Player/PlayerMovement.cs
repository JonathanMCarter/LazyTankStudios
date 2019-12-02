using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Edited by Andreas on 29/11
//added function to swap animator
//commented out the sprite renderer variable and flipx calls as I made additional animations for the walking,facing,attacking Left 
public class PlayerMovement : A
{
    public float speed = 100;
    float bS;
    public Inventory Inv;
    public GameObject Bullet, DeathCanvas, Menu;
    Rigidbody2D myRigid;
    Animator myAnim;
    //SpriteRenderer render;
    InputManager IM;
    public bool dmgCD;
    SoundPlayer audioManager, BossKilled;
    public int health, QuestActiveID;
    GameObject aHB;
    public Transform aR;
    bool attacking, dashing, shieldUp, Shooting;
    [HideInInspector]
    public bool stopInput;
    public float RangedAttackDuration, dashSpeedMultiplier, DashDuration, blockTIme, AttackTime, SlideSpeed, BulletSpeed, BulletLifeTime;
    float countdown;
    enum ITEMS
    {
        SWORD,
        BLAZBOOTS,
        ICEBOW,
        SHIELDSHARPTON,
        TELERUNE,
        ELIXIRLIFE,
        ELIXIRSTR,
        ELIXIRHEARTS,
        WATERSKIN,
        FURCOAT,
        FORESTITEM
    }
    public int maxHealth;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart;
    public void ShowHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i <= maxHealth);
            hearts[i].sprite = i <= health ? fullHeart : emptyHeart;
        }
    }
    void Start()
    {
        stopInput = false;
        if (DeathCanvas != null) DontDestroyOnLoad(DeathCanvas.gameObject);
        myRigid = G<Rigidbody2D>();
        myAnim = G<Animator>();
        //render = G<SpriteRenderer>();
        IM = F<InputManager>();
        ShowHearts();
        Menu = FT("OptionsMenu");
        Menu.SetActive(false);
        audioManager = F<SoundPlayer>();
        aHB = C(aR,0).gameObject;
        aR.gameObject.SetActive(true);
        aHB.SetActive(false);
        bS = speed;
    }
    void FixedUpdate()
    {
        if (!stopInput)
        { if (countdown > 0 && speed == bS) myRigid.velocity = new Vector2(0, 0);
          else myRigid.velocity = (new Vector2(IM.X_Axis(), IM.Y_Axis())).normalized * speed; }
    }
    void Update()
    {
        if(!stopInput)
        { SetRotater();
        myAnim.SetFloat("SpeedX", IM.X_Axis());
        myAnim.SetFloat("SpeedY", IM.Y_Axis());
        if (IM.Button_Menu())
        {
            Menu.SetActive(true);
            enabled = false;
        }
        if (IM.Button_A()) if (Inv.items.Count > 0) useItem(Inv.items[Inv.current]);
        if (IM.Button_B()){
            Inv.change();
            audioManager.Play("Open_Inventory_1");
        }
        }
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                aHB.SetActive(false);
                C(aR,1).gameObject.SetActive(false);
                speed = bS;
                C(aR,3).gameObject.SetActive(false);
                C(aR,2).gameObject.SetActive(false);
                myAnim.SetBool("Attack", false);
                stopInput = false;
            }
        }
    }
    void useItem(int ID)
    {
        if (countdown <= 0)
        {
            switch (ID)
            {
                case 0:
                    myRigid.velocity = new Vector2(0, 0);
                    myAnim.SetBool("Attack", true);
                    countdown = AttackTime;
                    C(aR,0).gameObject.SetActive(true);
                    audioManager.Play("Attacking_1_(Sword)");
                    stopInput = true;
                    break;
                case 1:
                    C(aR,1).gameObject.SetActive(true);
                    speed *= dashSpeedMultiplier;
                    countdown = DashDuration;
                    break;
                case 2:
                    C(aR,2).gameObject.SetActive(true);
                    countdown = RangedAttackDuration;
                    countdown = 0.3f;
                    SC(FireProjectile());
                    audioManager.Play("Attacking_1_(Bow)");
                    break;
                case 3:
                    countdown = blockTIme;
                    C(aR,3).gameObject.SetActive(true);
                    break;
                case 4:
                    if (health < maxHealth) { ++health; ShowHearts(); Inv.items.Remove(ID); Inv.change(); }
                    break;
                case -1:
                    break;
            }
        }
    }
    IEnumerator FireProjectile()
    {
        GameObject Go = Instantiate(Bullet, C(aR,0).transform.position, aR.rotation * Quaternion.Euler(0, 0, -90));
        G<EnemyProjectileMove>(Go).S = BulletSpeed;
        yield return new WaitForSeconds(BulletLifeTime);
        Go.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        if (myRigid != null) myRigid.velocity = new Vector2(0, 0);
        if (myAnim != null) myAnim.SetFloat("SpeedX", 0);
        myAnim.SetFloat("SpeedY", 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") TakeDamage(1);
    }
    public void TakeDamage(int damage)
    {
        if (dmgCD) return;
        if (health <= 0) return;
        SC(DamageCooldown());
        health -= damage;
        ShowHearts();
        audioManager.Play("Player_Take_Damage_1");
        if (health <= 0)
        {
            audioManager.Play("Death_1");
            stopInput = true;
            SC(GameReset());
        }
    }
    IEnumerator GameReset()
    {
        if (DeathCanvas != null) DeathCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main Menu");
        DoNotDes[] Gos = Fs<DoNotDes>();
        DoNotDes.Created = false;
        foreach (DoNotDes go in Gos) if (go.gameObject != gameObject) D(go.gameObject);
        yield return new WaitForSeconds(0);
        D(gameObject);
    }
    IEnumerator DamageCooldown()
    {
        dmgCD = !dmgCD;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(0.5f);
        dmgCD = !dmgCD;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    public void Heal(int value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);
        audioManager.Play("Healing_1");
    }
    void SetRotater()
    {
        if (IM.X_Axis() > 0.1)
        {
            //render.flipX = false;
            aR.rotation = new Quaternion(0, 0, 0, 0);
            aR.GetChild(3).rotation = new Quaternion(0, 0, 0, 0);
        }
        if (IM.X_Axis() < -0.1)
        {
            //render.flipX = true;
            aR.rotation = new Quaternion(0, 0, 180, 0);
            aR.GetChild(3).rotation = new Quaternion(0, 0, 0, 180);
        }
        if (IM.Y_Axis() > 0.1)
        {
            aR.rotation = new Quaternion(0, 0, 0, 0);
            if (aR.rotation != new Quaternion(0, 0, 90, 0)) aR.Rotate(Vector3.forward * 90);
            aR.GetChild(3).rotation = new Quaternion(0, 0, 0, 90);
        }
        if (IM.Y_Axis() < -0.1)
        {
            aR.rotation = new Quaternion(0, 0, 180, 0);
            if (aR.rotation != new Quaternion(0, 0, -90, 0)) aR.Rotate(Vector3.forward * 90);
            aR.GetChild(3).rotation = new Quaternion(0, 0, 0, 180);
        }
    }
    public void load(string[] dest)
    {
        audioManager.Play("Doors_1_(Normal)");
        SC(waitUntilLoaded(dest));
    }
    public IEnumerator waitUntilLoaded(string[] dest)
    {
        AsyncOperation l = SceneManager.LoadSceneAsync(dest[0]);
        yield
        return new WaitUntil(() => l.isDone);
        Vector3 newPos = F(dest[1]).transform.position;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }

    ///<summary>
    ///Used to switch the animator controller for the costume changing dlc feature
    ///</summary>
    public void SwitchAnimator(RuntimeAnimatorController newAC)
    {
        myAnim.runtimeAnimatorController=newAC;
    }
}