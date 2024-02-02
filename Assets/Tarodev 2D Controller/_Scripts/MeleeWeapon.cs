using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class MeleeWeapon : MonoBehaviour
{
    //How much damage the melee attack does
    [SerializeField]
    private int damageAmount = 20;
    //Reference to Character script which contains the value if the player is facing left or right; if you don't have this or it's named something different, either omit it or change the class name to what your Character script is called
    private PlayerController character;
    //Reference to the Rigidbody2D on the player
    private Rigidbody2D rb;
    //Reference to the direction the player needs to go in after melee weapon contacts something
    private Vector2 direction;
    //Bool that manages if the player should move after melee weapon colides
    private bool collided;
    //Determines if the melee strike is downwards to perform extra force to fight against gravity
    private bool downwardStrike;

    private void Start()
    {
        //Reference to the Character script on the player; if you don't have this or it's named something different, either omit it or change the class name to what your Character script is called
        character = GetComponentInParent<PlayerController>();
        //Reference to the Rigidbody2D on the player
        rb = GetComponentInParent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks to see if the GameObject the MeleeWeapon is colliding with has an EnemyHealth script
        if (collision.GetComponent<EnemyHealth>())
        {
            //Method that checks to see what force can be applied to the player when melee attacking
            HandleCollision(collision.GetComponent<EnemyHealth>());
        }
    }

    private void HandleCollision(EnemyHealth objHealth)
    {
        //Checks to see if the GameObject allows for upward force and if the strike is downward as well as grounded
        if (objHealth.giveUpwardForce && Input.GetAxis("Vertical") < 0 && !character._grounded)
        {
            //Sets the direction variable to up
            direction = Vector2.up;
            //Sets downwardStrike to true
            downwardStrike = true;
            //Sets collided to true
            collided = true;
        }
        if (Input.GetAxis("Vertical") > 0 && !character._grounded)
        {
            //Sets the direction variable to up
            direction = Vector2.down;
            //Sets collided to true
            collided = true;
        }
        //Checks to see if the melee attack is a standard melee attack
        if ((Input.GetAxis("Vertical") <= 0 && character._grounded) || Input.GetAxis("Vertical") == 0)
        {
            //Sets collided to true
            collided = true;
        }
        //Deals damage in the amount of damageAmount
        objHealth.Damage(damageAmount);
      
    }

 

    
}
