using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public float speed = 5f;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    public GetherInput gatherInput;
    public new Rigidbody2D rigidbody2D;
    public Animator animator;

    public int direction= 1;

    public float jumpForce =5f ;  

    public float rayLength=1f;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform right ;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GetherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void SetAnimatorValue(){
        animator.SetFloat("speed",Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed",rigidbody2D.velocity.y);
        animator.SetBool("Grounded",grounded);
    }
    void Update()
    {
        SetAnimatorValue();
        CheckStatus();
        Move();
       // JumpPlayer();
    }
    private void CheckStatus() {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;
        
        RaycastHit2D rightCheckHit = Physics2D.Raycast(right.position, Vector2.down, rayLength, groundLayer);
        grounded = rightCheckHit;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //CheckStatus();
        //Move();
        JumpPlayer();
        Flip();
    }
    private void Move()
    {
        //Flip();
        rigidbody2D.velocity = new Vector2(
            speed * gatherInput.valueX, rigidbody2D.velocity.y);
    }
    private void Flip()
    {
        if(gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(
                -transform.localScale.x, 1,1);
            direction *= -1;
        }
    }
    private void JumpPlayer() 
    {
        if (gatherInput.jumpInput && grounded)
        {
            
            rigidbody2D.velocity = new Vector2(
                gatherInput.valueX * speed, jumpForce
                );

       
            gatherInput.jumpInput = false;
        }
    }
    
   
}

