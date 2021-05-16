using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Player_Mov : MonoBehaviour
{
    [SerializeField] private float jumpForce, maxSpeed, aceleração, deslizamento, impulso;

    CapsuleCollider2D capsule;
    Animator anim;
    Rigidbody2D rb2d;

    private float speed, direction;
    private bool inD, inE, canJump, d, canDown, atack;
    private int timer = 0;

    public Dino_Mov dino;

    private void Awake()
    {
        dino = GameObject.Find("Dino").GetComponent<Dino_Mov>();
        capsule = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
        
        if (speed != 0)
        {
            if (direction == 0)
            {
                if (d == true)
                    rb2d.velocity = new Vector2(+speed, rb2d.velocity.y);
                else
                    rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            }
        }

        if (direction != 0)
        {
            anim.SetBool("mov", true);

            if(speed < maxSpeed) 
            {
                timer++;
                if (timer > aceleração)
                {
                    speed++;
                    timer = 0;
                }
            }

            if (direction > 0)
            {
                d = true;
                inD = false;
                Flip();
            }
            else
            {
                d = false;
                inD = true;
                Flip();
            }
        }
        else
        {
            anim.SetBool("mov", false);

            timer++;
            if (speed > 0 && timer > deslizamento)
            {
                speed--;
                timer = 0;
            }
        }

        float Y = Input.GetAxisRaw("Vertical");

        if (canJump == true)
        {
            if (Input.GetButton("Fire3") || Input.GetKey(KeyCode.Space))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, +jumpForce);
                capsule.isTrigger = true;
                anim.SetTrigger("jumping");
            }
            if (Y < 0 && canDown == true)
            {
                capsule.isTrigger = true;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            anim.SetTrigger("Atack");
            atack = true;
        }
        else
            atack = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Dino") && atack == true)
            dino.NewLife(10);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Ao Entrar na plataforma
    {
        if (collision.CompareTag("plataform"))
        {
            canJump = true;
            capsule.isTrigger = false;
        }
        else if (collision.CompareTag("UltPlataform"))
        {
            capsule.isTrigger = false;
            canJump = true;
            canDown = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Ao Sair da plataforma
    {
        if (collision.CompareTag("plataform"))
            canJump = false;

        else if (collision.CompareTag("UltPlataform"))
        {
            canDown = true;
            canJump = false;
        }
    }

    private void Flip()
    {
        if ((inD && !inE) || (!inD && inE))
        {
            speed = speed / 4;
            inE = !inE;
            Vector2 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
    }
}