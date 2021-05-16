using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dino_Mov : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [SerializeField] private Slider vidaDino;
    [SerializeField] private float speedX, life;
    private bool inD, inE;

    public Player_Mov playerPos;

    private void Awake()
    {
        playerPos = GameObject.Find("Human").GetComponent<Player_Mov>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        vidaDino.maxValue = 1000;
        vidaDino.minValue = 0;
        vidaDino.value = vidaDino.maxValue;
    }
    private void Update()
    {
        float distance = playerPos.transform.position.x - this.transform.position.x;

        int direction = 0;
        if (distance > 0)
        {
            direction = 1;
            Flip();
            inD = false;
        }
        else if(distance < 0)
        {
            direction = -1;
            Flip();
            inD = true;
        }

        rb.velocity = new Vector2(speedX * direction, rb.velocity.y);
        anim.SetBool("mov", true);

        if(vidaDino.value <= 0)
        {
            DestroyObject(GameObject.Find("Chefão"));
            DestroyObject(this.gameObject);
            SceneManager.LoadScene("Credits");
        }
    }

    public void NewLife(int lifea)
    {
        vidaDino.value -= lifea;
    }

    private void Flip()
    {
        if ((inD && !inE) || (!inD && inE))
        {
            inE = !inE;
            Vector2 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
    }
}