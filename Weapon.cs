using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip collectClip;
    public float collectSoundVol = 1.0f;
    float horizontal;
    Vector2 move;
    Vector2 lookDirection;
    public bool isGet = false;
    GameObject location1;
    GameObject location2;
    Transform currentLocation;
    bool lookRight1 = true;
    bool lookLeft1 = false;
    bool lookRight2 = false;
    bool lookLeft2 = true;
    public Vector3 rate;
    public float rotationTime = 1f;
    float rTimer;
    bool rdirection = true;
    public float weaponForce = 500;
    Rigidbody2D rigidbody2d;
    Collider2D mycollider2D;
    Collider2D planecollider2D;
    Collider2D lastcollider2D = null;
    float leaveTimer = 0.3f;
    bool isBack1 = false;
    bool isBack2 = false;
    Animator animator;
    bool resetRotation = true;
    bool isPlayer1 = false;
    bool isPlayer2 = false;
    string lastgameobjectName;
    // Start is called before the first frame update
    void Start()
    {
        location1 = GameObject.Find("location1");
        location2 = GameObject.Find("location2");
        rTimer = rotationTime / 2;
        rigidbody2d = GetComponent<Rigidbody2D>();
        mycollider2D = GetComponent<Collider2D>();
        GameObject PLANE = GameObject.FindWithTag("Plane");
        planecollider2D = PLANE.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGet)
        {
            if(isPlayer1)
            {
                horizontal = Input.GetAxis("Horizontal1");
                move = new Vector2(horizontal, 0);
                if (!Mathf.Approximately(move.x, 0.0f))
                {
                    lookDirection.Set(move.x, 0);
                    lookDirection.Normalize();
                }
                animator.SetFloat("direction", lookDirection.x);
                if (resetRotation)
                {
                    if (isBack1 && move.x > 0 || !isBack1 && move.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
                    }
                    if (!isBack1 && move.x > 0 || isBack1 && move.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    resetRotation = false;
                }
                if (move.x > 0 && lookLeft1 && Move1.enableJump)
                {
                    lookLeft1 = false;
                    transform.Rotate(0, 180, 0, Space.World);
                    lookRight1 = true;
                }
                if (move.x < 0 && lookRight1 && Move1.enableJump)
                {
                    lookRight1 = false;
                    transform.Rotate(0, 180, 0, Space.World);
                    lookLeft1 = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rdirection = !rdirection;
                    transform.Rotate(180, 0, 180);
                    isBack1 = !isBack1;
                }
                if (Input.GetKeyDown(KeyCode.S) && isBack1)
                {
                    if (lookRight1)
                    {
                        if (transform.eulerAngles.z < 180)
                        {
                            rigidbody2d.AddForce(new Vector2(weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        if (transform.eulerAngles.z >= 180)
                        {
                            rigidbody2d.AddForce(new Vector2(weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), -weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        animator.SetBool("weaponfire", true);
                    }
                    if (lookLeft1)
                    {
                        if (transform.eulerAngles.z < 180)
                        {
                            rigidbody2d.AddForce(new Vector2(-weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        if (transform.eulerAngles.z >= 180)
                        {
                            rigidbody2d.AddForce(new Vector2(-weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), -weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        animator.SetBool("weaponfire", true);
                    }
                    Physics2D.IgnoreCollision(planecollider2D, mycollider2D, false);
                    isGet = false;
                    if (isPlayer1)
                    {
                        isPlayer1 = false;
                    }
                    if (isPlayer2)
                    {
                        isPlayer2 = false;
                    }
                }
            }
            if (isPlayer2)
            {
                horizontal = Input.GetAxis("Horizontal2");
                move = new Vector2(horizontal, 0);
                if (!Mathf.Approximately(move.x, 0.0f))
                {
                    lookDirection.Set(move.x, 0);
                    lookDirection.Normalize();
                }
                animator.SetFloat("direction", lookDirection.x);
                if (resetRotation)
                {
                    if ( isBack2 && move.x > 0 || !isBack2 && move.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
                    }
                    if (!isBack2 && move.x > 0 || isBack2 && move.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    resetRotation = false;
                }
                if (move.x > 0 && lookLeft2 && Move2.enableJump)
                {
                    lookLeft2 = false;
                    transform.Rotate(0, 180, 0, Space.World);
                    lookRight2 = true;
                }
                if (move.x < 0 && lookRight2 && Move2.enableJump)
                {
                    lookRight2 = false;
                    transform.Rotate(0, 180, 0, Space.World);
                    lookLeft2 = true;
                }
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    rdirection = !rdirection;
                    transform.Rotate(180, 0, 180);
                    isBack2 = !isBack2;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && isBack2)
                {
                    if (lookRight2)
                    {
                        if (transform.eulerAngles.z < 180)
                        {
                            rigidbody2d.AddForce(new Vector2(weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        if (transform.eulerAngles.z >= 180)
                        {
                            rigidbody2d.AddForce(new Vector2(weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), -weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        animator.SetBool("weaponfire", true);
                    }
                    if (lookLeft2)
                    {
                        if (transform.eulerAngles.z < 180)
                        {
                            rigidbody2d.AddForce(new Vector2(-weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        if (transform.eulerAngles.z >= 180)
                        {
                            rigidbody2d.AddForce(new Vector2(-weaponForce * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), -weaponForce * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)));
                        }
                        animator.SetBool("weaponfire", true);
                    }
                    Physics2D.IgnoreCollision(planecollider2D, mycollider2D, false);
                    isGet = false;
                    if (isPlayer1)
                    {
                        isPlayer1 = false;
                    }
                    if (isPlayer2)
                    {
                        isPlayer2 = false;
                    }
                }
            }
            
            if (isPlayer1)
            {
                transform.position = location1.transform.position;
            }
            if (isPlayer2)
            {
                transform.position = location2.transform.position;
            }

            if (rTimer > 0 && rdirection )
            {
                rTimer -= Time.deltaTime;
                transform.Rotate(rate * Time.deltaTime);
            }
            if (rTimer > 0 && !rdirection)
            {
                rTimer -= Time.deltaTime;
                transform.Rotate(-rate * Time.deltaTime);
            }
            if (rTimer <= 0)
            {
                rdirection = !rdirection;
                rTimer = rotationTime;
            }
            
               
        }
        if (lastcollider2D != null && ((transform.position != location1.transform.position && lastgameobjectName == "player1")|| transform.position != location2.transform.position && lastgameobjectName == "player2"))
        {
            leaveTimer -= Time.deltaTime;
            if(leaveTimer <= 0)
            {
                Physics2D.IgnoreLayerCollision(6, 7, false);
                animator.SetBool("weaponfire", false);
                rTimer = rotationTime / 2;
                leaveTimer = 0.3f;
                lastcollider2D = null;
                resetRotation = true;
                //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
            } 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision .gameObject.name == "player1"&&! isGet && Mathf.Abs (rigidbody2d.GetComponent <Rigidbody2D >().velocity.x )<= 1 )
        {
            if (!Move1.die)
            {
                PlaySound(collectClip, collectSoundVol);
                lastgameobjectName = "player1";
                lastcollider2D = collision.collider;
                Physics2D.IgnoreLayerCollision(6, 7, true);
                Physics2D.IgnoreCollision(planecollider2D, mycollider2D, true);
                transform.position = location1.transform.position;
                isGet = true;
                Move1.getWeapon = true;
                isPlayer1 = true;
            }
            
        }
        if (collision.gameObject.name == "player2" && !isGet && Mathf.Abs (rigidbody2d.GetComponent<Rigidbody2D>().velocity.x )<= 1 )
        {
            if(!Move2.die)
            {
                PlaySound(collectClip, collectSoundVol);
                lastgameobjectName = "player2";
                lastcollider2D = collision.collider;
                Physics2D.IgnoreLayerCollision(6, 7, true);
                Physics2D.IgnoreCollision(planecollider2D, mycollider2D, true);
                transform.position = location2.transform.position;
                isGet = true;
                Move2.getWeapon = true;
                isPlayer2 = true;
            }
            
        }
        if (collision.gameObject.tag == "area")
        {
            transform.position = new Vector3(0, 4, 0);
        }

    }
    public void PlaySound(AudioClip audioClip, float soundVol)
    {
        audioSource.PlayOneShot(audioClip, soundVol);
    }
}
