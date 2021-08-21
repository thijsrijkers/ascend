using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private enum Turned
    { left, right };
    private Turned turned;
    private Rigidbody2D rigidbody;
    private Vector3 jump;
    private bool isGrounded = true;

    public float jumpForce = 2.0f;
    public float walkingSpeed, runningSpeed, currentSpeed;
    public Transform playerTransform;
    public SpriteRenderer playerSpriteRenderer;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        ínit();
    }

    private void ínit()
    {
        turned = Turned.left;
    }

    private void Update()
    {
        moveHorizontal();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rigidbody.AddForce(jump * jumpForce, (ForceMode2D)ForceMode.Impulse);
        }
    }

    private void OnCollisionStay2D()
    {
        isGrounded = true;
    }

    private void moveHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal == 0)
            return;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        { 
            currentSpeed = runningSpeed; 
        }
        else
        { 
            currentSpeed = walkingSpeed; 
        }

        translationHorizontal(horizontal);
        flipCharacter(horizontal);
    }


    private void flipCharacter(float value)
    {
        if (value > 0 && turned == Turned.left)
        {
            toggleFlip();
            turned = Turned.right;
            return;
        }

        if (value < 0 && turned == Turned.right)
        {
            toggleFlip();
            turned = Turned.left;
        }
    }

    private void toggleFlip()
    {
        playerSpriteRenderer.flipX = !playerSpriteRenderer.flipX;
    }

    private void translationHorizontal(float value)
    {
        transform.Translate(value * currentSpeed, 0, 0);
    }
}
