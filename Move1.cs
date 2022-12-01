using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move1 : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip throwCogClip;
    public float throwCogClipSoundVol = 1.0f;
    public AudioClip PlayerHitClip;
    public float PlayerHitClipSoundVol = 1.0f;
    public AudioClip jumpClip;
    public float jumpSoundVol = 1.0f;
    public AudioClip downClip;
    public float downClipSoundVol = 1.0f;
    public AudioClip dieClip;
    public float dieClipSoundVol = 1.0f;
    float horizontal;
    public float speed = 0.1f;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 move;
    public Vector2 jumpForce = new Vector2(0, 300);
    public Vector2 fireForce = new Vector2(100, 0);
    Animator animator;
    Rigidbody2D rigidbody2d;
    public static bool enableJump;
    bool hasJumped = false;
    bool enableFire;
    public float fireTimer = 1.0f;
    float countfireTimer;
    public static bool getWeapon = false;
    bool isBack = false;
    bool hit = false;
    float hittime = 0.5f;
    public int maxHealth1 = 5; 
    int currentHealth1;
    public static bool die = false;
    float dietime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        countfireTimer = fireTimer;
        currentHealth1 = maxHealth1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (!die)
        {
            if (!hit)
            {
                if (getWeapon)
                {
                    if (enableFire)
                    {
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            enableFire = false;
                            countfireTimer = fireTimer;
                            if (lookDirection.x > 0)
                            {
                                rigidbody2d.AddForce(-fireForce);
                                animator.SetBool("fire1", true);
                            }
                            else
                            {
                                rigidbody2d.AddForce(fireForce);
                                animator.SetBool("fire1", true);
                            }
                            if (isBack)
                            {
                                getWeapon = false;
                            }
                            PlaySound(throwCogClip, throwCogClipSoundVol);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isBack = !isBack;
                    }

                }
                if (!enableFire)
                {
                    countfireTimer -= Time.deltaTime;
                    if (countfireTimer <= 0)
                    {
                        enableFire = true;
                    }
                    if (countfireTimer <= 0.8)
                    {
                        animator.SetBool("fire1", false);
                    }
                }
                if (enableJump)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        enableJump = false;
                        rigidbody2d.AddForce(jumpForce);
                        animator.SetBool("jump1", true);
                        PlaySound(jumpClip, jumpSoundVol);
                        hasJumped = true;
                    }
                }
                horizontal = Input.GetAxis("Horizontal1");
                move = new Vector2(horizontal, 0);
                if (!Mathf.Approximately(move.x, 0.0f))
                {
                    lookDirection.Set(move.x, 0);
                    lookDirection.Normalize();
                }
                animator.SetFloat("Look X1", lookDirection.x);
                animator.SetFloat("speed1", move.magnitude);
                //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }

            if (hit)
            {
                hittime -= Time.deltaTime;
                if (hittime <= 0)
                {
                    hit = false;
                    hittime = 1f;
                    animator.SetBool("hit1", false);
                }
            }
        }
        if (die)
        {
            
            dietime -= Time.deltaTime;
            animator.SetBool("die1",true);
            if(dietime <= 0)
            {
                hasJumped = false;
                isBack = false;
                hit = false;
                getWeapon = false;
                die = false;
                Destroy(animator);
                
            }
        }
        if(currentHealth1 <= 0)
        {
            PlaySound(dieClip, dieClipSoundVol);
            die = true;
            Invoke("stop", 3f);
            

        }
    }

    private void FixedUpdate()
    {
        if (!die)
        {
            Vector2 position = transform.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            transform.position = position;
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision .gameObject.tag  == "Plane")
        {
            if (hasJumped)
            {
                PlaySound(downClip, downClipSoundVol);
                hasJumped = false;
            }
            animator.SetBool("jump1", false);
            enableJump = true;
        }
        if(collision .gameObject .tag == "weapon")
        {
            if(Mathf.Abs(collision .gameObject .GetComponent<Rigidbody2D>().velocity.x)> 1)
            {
                animator.SetBool("hit1", true);
                PlaySound(PlayerHitClip, PlayerHitClipSoundVol);
                ChangeHealth(-1);
            }
        }
        if (collision.gameObject.tag == "area")
        {
            transform.position = new Vector3(-6 + (6 * Random.value), 4, 0);
        }
    }
    void ChangeHealth(int amount)
    {

        if (amount < 0)
        {
            if (hit)
            {
                return;
            }
            else
            {
                hit = true;
                hittime = 1f;
            }
        }
        //
        currentHealth1 = Mathf.Clamp(currentHealth1 + amount, 0, maxHealth1);
        Debug.Log(currentHealth1 + "/" + maxHealth1);
        HealthBar1.Instance.SetValue(currentHealth1 / (float)maxHealth1);
    }
    public void PlaySound(AudioClip audioClip, float soundVol)
    {
        audioSource.PlayOneShot(audioClip, soundVol);
    }
    public void stop()
    {
        SceneManager.LoadScene("½áËã½çÃæ2");
    }
}

