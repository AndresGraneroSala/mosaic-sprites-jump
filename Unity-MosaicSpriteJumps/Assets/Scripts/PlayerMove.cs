using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public enum StatePlayer
    {
        Idle,
        Walk,
        Run,
        Jump,
        Die
    }

    [SerializeField] private float speedMove;
    [SerializeField] private float speedRun;
    [SerializeField] private float speedJump;
    
    [SerializeField] private float forceJump;
    [SerializeField] private float timeWalking;

    [SerializeField]
    private float timeStartRun()
    {
        return ConfigurationSprites.SharedInstance.ConfigurationSpritesData.timeStartRun;
    }

    [SerializeField] private PlayerAnim anim;

    [SerializeField] private bool isMoving;

    [SerializeField] private StatePlayer statePlayer;
    [SerializeField] private StatePlayer previousStatePlayer = StatePlayer.Run;

    [SerializeField] private GameObject colliderFeets;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isCrashingWithWall;

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] public float rayLengthFloor = 1f;
    [SerializeField] public float rayLengthWall = 1f;
    [SerializeField] private Vector3 sidesRay;
    [SerializeField] private Vector3 sidesRayWall;
    
    [SerializeField] private LayerMask layersToDetect;


    [SerializeField] private Vector3 positionRespawn;
    [SerializeField] private float timeRespawn;
    
    private float moveInXToChangeDirection()
    {

        return ConfigurationSprites.SharedInstance.ConfigurationSpritesData.xPlayerMove;
    }

    private void Start()
    {
        Application.targetFrameRate = ConfigurationSprites.SharedInstance.ConfigurationSpritesData.maxFPS;

        //aprox furula
        transform.position += new Vector3(ConfigurationSprites.SharedInstance.ConfigurationSpritesData.xPlayerMove / 2,
            0, 0);
        anim.AnimIdle();

        colliderFeets.transform.position -= new Vector3(moveInXToChangeDirection() / 2, 0, 0);

        positionRespawn.x += moveInXToChangeDirection()/2;



    }


    private int numStateJump=0;
    
    // Update is called once per frame
    void Update()
    {

        if (!anim.isSpritesLoadedWeb)
        {
            return;
        }





        DetectFloor();
        DetectWall();




        float inputX = Input.GetAxisRaw("Horizontal");


        if (!isCrashingWithWall)
        {
            if (statePlayer == StatePlayer.Walk)
            {
                transform.position += new Vector3(inputX * speedMove * Time.deltaTime, 0, 0);
            }

            if (statePlayer == StatePlayer.Run)
            {
                transform.position += new Vector3(inputX * speedRun * Time.deltaTime, 0, 0);
            }

            if (statePlayer == StatePlayer.Jump)
            {
                transform.position += new Vector3(inputX * speedJump * Time.deltaTime, 0, 0);
            }
        }



        if (statePlayer != StatePlayer.Die)
        {



            if (inputX == 1)
            {


                if (transform.localScale.x < 0)
                {
                    transform.position += new Vector3(moveInXToChangeDirection(), 0, 0);
                }

                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y,
                    transform.localScale.z);


            }
            else if (inputX == -1)
            {


                if (transform.localScale.x > 0)
                {
                    transform.position -= new Vector3(moveInXToChangeDirection(), 0, 0);
                }

                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y,
                    transform.localScale.z);

            }


        }

        isMoving = inputX != 0;


        if (statePlayer != StatePlayer.Die)
        {



            if ((Input.GetAxisRaw("Jump") > 0 || Input.GetAxisRaw("Vertical") > 0) && isGrounded)
            {
                numStateJump++;
                //print(numStateJump);

                if (numStateJump == 2 && isGrounded)
                {
                    Jump();
                }

                statePlayer = StatePlayer.Jump;
            }
            else
            {
                numStateJump = 0;

                if (isGrounded)
                {
                    if (isMoving && isGrounded)
                    {
                        timeWalking += Time.deltaTime;
                        if (timeWalking >= timeStartRun())
                        {
                            statePlayer = StatePlayer.Run;
                        }
                        else
                        {
                            statePlayer = StatePlayer.Walk;
                        }
                    }
                    else
                    {
                        timeWalking = 0;
                        statePlayer = StatePlayer.Idle;
                    }
                }
            }
        }












        if (statePlayer != previousStatePlayer)
        {
            if (statePlayer == StatePlayer.Idle)
            {
                anim.AnimIdle();
            }
            else if (statePlayer == StatePlayer.Walk)
            {
                    anim.AnimWalk();
                
            }
            else if (statePlayer == StatePlayer.Run)
            {
                anim.AnimRun();
            }
            else if (statePlayer == StatePlayer.Jump)
            {
                //Jump();
            }else if (statePlayer==StatePlayer.Die)
            {
                Die();
            }
        }



        previousStatePlayer = statePlayer;




    }


    public void DetectFloor()
    {
        var positionStart = colliderFeets.transform.position;

        var distanceTotal = transform.localScale.y*rayLengthFloor;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(positionStart, Vector2.down, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart, Vector2.down*distanceTotal, Color.red);
        
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(positionStart+sidesRay, Vector2.down, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart+sidesRay, Vector2.down*distanceTotal, Color.red);
        
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(positionStart-sidesRay, Vector2.down, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart-sidesRay, Vector2.down*distanceTotal, Color.red);
        
        isGrounded = raycastHit2D.collider|| raycastHit2DRight.collider|| raycastHit2DLeft.collider;
        
    }
    
    public void DetectWall()
    {
        var positionStart = colliderFeets.transform.position;

        var distanceTotal = rayLengthWall;

        var direction = Vector2.right;

        if (transform.localScale.x <0)
        {
            direction = Vector2.left;
        }
        
        
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(positionStart, direction, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart, direction*rayLengthWall, Color.yellow);
        
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(positionStart+sidesRayWall, direction, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart+sidesRayWall, direction*rayLengthWall, Color.yellow);
        
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(positionStart-sidesRayWall, direction, distanceTotal,layersToDetect);
        Debug.DrawRay(positionStart-sidesRayWall, direction*rayLengthWall, Color.yellow);
        
        isCrashingWithWall = raycastHit2D.collider|| raycastHit2DRight.collider|| raycastHit2DLeft.collider;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("dieZone"))
        {
            statePlayer = StatePlayer.Die;
        }
    }

    public void Die()
    {

        
        /*if (isCrashingWithWall)
        {
            transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x),transform.localScale.y,transform.localScale.z );
            transform.position += new Vector3(rayLengthWall,0,0);

        }*/

        
        if (transform.localScale.x > 0)
        {
            print("dere");

            print(transform.position.x);
            transform.position -= new Vector3(ConfigurationSprites.SharedInstance.ConfigurationSpritesData.xPlayerDead,0,0);
            print(transform.position.x);
        }
        else
        {           
            print("izq");

            print(transform.position.x);

            transform.position += new Vector3(ConfigurationSprites.SharedInstance.ConfigurationSpritesData.xPlayerDead,0,0);
            print(transform.position.x);

        }
        anim.AnimDead();


        //transform.position += new Vector3(0,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.yPlayerDead,0);
        
        
        
        
        rb.velocity = Vector2.zero;
        rb.mass = 0.1f;
        rb.gravityScale = 0.1f;

        StartCoroutine(Respawn());

    }
    
    public void Jump()
    {
        timeWalking = 0;

        //print("jump");
        anim.AnimJump();

        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * forceJump);


    }

    public IEnumerator Respawn()
    {
        
        
        yield return new WaitForSeconds(timeRespawn);
        rb.velocity = Vector2.zero;
        rb.mass = 1f;
        rb.gravityScale = 1f;
        transform.position = positionRespawn;
        statePlayer = StatePlayer.Idle;

        transform.localScale = new Vector3(Mathf.Abs( transform.localScale.x),transform.localScale.y,transform.localScale.z );

    }




    
}
