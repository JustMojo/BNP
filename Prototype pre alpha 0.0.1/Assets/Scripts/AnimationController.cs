using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    RuntimeAnimatorController test;
    Player player;
    Controller2D control;
    Vector2 direction;
    Vector2 velocity;

    bool right = true;

    float moveSpeed = 6;


    void Start()
    {
        control = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update() {
        CalculateInfo(direction.x);
        Run(direction.x);
        if (direction.x == 0 && Input.GetKey("s")) Crouch(true);
        else Crouch(false);
        if (Input.GetKeyDown("w") || control.collisions.below == false) Jump(true);
        else Jump(false);
        Shoot();

    }

    void Run(float directionX) {
        if (control.collisions.below == false) { CalculateInfo(direction.x); directionX = 0;}
        anim.SetFloat("Speed", Mathf.Abs(directionX));
    }

    void Crouch(bool check) {
        anim.SetBool("Crouch", check);
        if (check) player.moveSpeed = 0;
        else player.moveSpeed = moveSpeed;
    }

    void Jump(bool check) {
        anim.SetBool("Jump", check);
        if (check) direction.x = 0;
        else player.directionalInput.x = direction.x;
    }

    void Shoot() {
        ShootMovment();
        ShootDirection();
    }

    void ShootMovment() {
        if (direction.x == 0) {
            anim.SetBool("Shoot", true);
            anim.SetBool("ShootRun", false);
        } else if (direction.x != 0) {
            anim.SetBool("Shoot", false);
            anim.SetBool("ShootRun", true);
        }
    }

    void ShootDirection() {
        if (right) {
            if (Input.GetKey(KeyCode.Keypad9)) { anim.SetBool("RightUpShoot", true); }
            if (Input.GetKeyUp(KeyCode.Keypad9)) { anim.SetBool("RightUpShoot", false); }
            if (Input.GetKey(KeyCode.Keypad3)) { anim.SetBool("RightDownShoot", true); }
            if (Input.GetKeyUp(KeyCode.Keypad3)) { anim.SetBool("RightDownShoot", false); }
        }
        else if (!right) {
            if (Input.GetKey(KeyCode.Keypad7)) { anim.SetBool("RightUpShoot", true); }
            if (Input.GetKeyUp(KeyCode.Keypad7)) { anim.SetBool("RightUpShoot", false); }
            if (Input.GetKey(KeyCode.Keypad1)) { anim.SetBool("RightDownShoot", true); }
            if (Input.GetKeyUp(KeyCode.Keypad1)) { anim.SetBool("RightDownShoot", false); }
        }
    }

    void CalculateInfo(float dirX) {
        direction = player.directionalInput;
        velocity = player.velocity;
        if (dirX > 0 && !right) Flip();
        if (dirX < 0 && right) Flip();
    }

    void Flip() {
        right = !right;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
