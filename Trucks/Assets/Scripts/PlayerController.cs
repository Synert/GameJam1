using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Renderer rend = new Renderer();
    public float MaxSpeed = 10;
    public float Acceleration = 35;
    public float jumpSpeed = 8;
    public float jumpDuration;

    public bool enableDoubleJump = true;
    public bool wallHitJump = true;

    bool canDoubleJump = true;
    float jmpDuration;

    bool keyPressDown = false;
    bool canJumpVariable = false; 

    void Start()
    {

        rend = GetComponent<Renderer>();

    }

	// Update is called once per frame
	void Update ()
    {
        
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal < -0.1f)
        {
            if(GetComponent<Rigidbody2D>().velocity.x > -this.MaxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.Acceleration, 0.0f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-this.MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if(horizontal > 0.1f)
        {
            if (GetComponent<Rigidbody2D>().velocity.x < this.MaxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(this.Acceleration, 0.0f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(this.MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        bool isOnGround = onGround();

        float vertical = Input.GetAxis("Vertical");

        if (isOnGround)
        {
            canDoubleJump = true;
        }

        if (vertical > 0.1f)
        {
            if (!keyPressDown)
            {
                keyPressDown = true;
                if (isOnGround || (canDoubleJump && enableDoubleJump || wallHitJump))
                {
                    bool wallHit = false;
                    int wallHitDirection = 0;

                    bool leftWallHit = onLeftWall();
                    bool rightWallHit = onRightWall();

                    if (horizontal != 0)
                    {
                        if (leftWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = 1;
                        }
                        else if (rightWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = -1;
                        }
                    }

                    if (!wallHit)
                    {
                        if (isOnGround || (canDoubleJump && enableDoubleJump))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.jumpSpeed);
                            jumpDuration = 0.0f;
                            canDoubleJump = true;
                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeed * wallHitDirection, this.jumpSpeed);

                        jmpDuration = 0.0f;
                        canJumpVariable = true;
                    }

                    if (!isOnGround && !wallHit)
                    {
                        canDoubleJump = false;
                    }
                }
            }
            else if (canJumpVariable)
            {
                jmpDuration += Time.deltaTime;

                if (jmpDuration < this.jumpDuration / 1000)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.jumpSpeed);
                }
            }
        }
        else
        {
            keyPressDown = false;
            canJumpVariable = false;
        }

    }

    private bool onGround()
    {
        float checkLength = 0.1f;
        float colliderThreshold = 0.001f;

        Vector2 lineStart = new Vector2(this.transform.position.x, this.transform.position.y - rend.bounds.extents.y - colliderThreshold);

        Vector2 searchVector = new Vector2(this.transform.position.x, lineStart.y - checkLength);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, searchVector);

        return hit;
    }

    private bool onLeftWall()
    {

        float checkLength = 0.1f;
        float colliderThreshold = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x - rend.bounds.extents.x - colliderThreshold, this.transform.position.y);

        Vector2 searchVector = new Vector2(lineStart.x - checkLength, this.transform.position.y);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, searchVector);

        return hit;
    }

    private bool onRightWall()
    {
       
        float checkLength = 0.1f;
        float colliderThreshold = 0.01f;

        Vector2 lineStart = new Vector2(this.transform.position.x + rend.bounds.extents.x + colliderThreshold, this.transform.position.y);

        Vector2 searchVector = new Vector2(lineStart.x + checkLength, this.transform.position.y);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, searchVector);

        return hit;

    }

}
