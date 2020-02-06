using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Source of Images, all images are free.


//Dracola, https://www.pngfuel.com/free-png/afima 
//saw, https://svgsilh.com/image/2022676.html 
//Mr.Bean, https://www.seekpng.com/ipng/u2q8i1y3y3a9e6a9_mr-bean-cake-bean-cakes-cute-characters-novelty/
//Man with mask, https://dlpng.com/png/6820244 


public class gamecontrol : MonoBehaviour
{
    
    [SerializeField]
    private GameObject spawnPoint ;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform stompCheck;


    [SerializeField]
    private float stompRadius =0.2f;



    private int NoOfEnemeis = 0;
    private float maxSpeed = 10f;
    private float move;
    private float move2;
    private Rigidbody2D rb;
    private float jump = 70f;
    private bool grounded = false;
    private bool doubleJump = false;
    private bool spacePressed ;
    private int health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        NoOfEnemeis = 0;
        health = 0;
    }

    // Update is called once per frame
    void Update()

    {
        move = Input.GetAxis("Horizontal");
        move2 = Input.GetAxis("Vertical");
        spacePressed = Input.GetKeyDown("space");
       

    }


    private void checkStomp()
    {
        Collider2D stomped = Physics2D.OverlapCircle
            (
               stompCheck.position,
               stompRadius,
               enemyLayer);
        if(stomped != null)
        {
            Destroy(stomped.gameObject);
            Debug.Log("stomped");
             


        }
    }

    

    private void FixedUpdate()


    {

      grounded = Physics2D.OverlapCircle(
            
               stompCheck.position,
               stompRadius,
               groundLayer);
        rb.velocity = new Vector2(move * maxSpeed, move2 * maxSpeed);


       
        
         checkStomp();
        if (grounded)
        {
            
            if (spacePressed || doubleJump)
            {
                spacePressed = false;
                rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jump);
            }
        }

        
    }




   private void OnCollisionEnter2D(Collision2D collision)

   {

    string collideTag = collision.gameObject.tag;
    if (collideTag == "enemy") // if Mr Bean hits enemies, they will be destroyed and number of killed enemies increased by 1.
    {
            //  this.gameObject.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
            //rb.velocity = Vector2.zero;
            //grounded = false;
            //NoOfEnemeis += 1;

            decreaseHealth();

        }


  
    if (collideTag == "hazard")// if Mr Bean hits any saw, he will return to start point which has the same cordinate of
                               //starting position of Mr.Bean
    {
        this.gameObject.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);

    }

    if (collideTag == "floor")
    {
       // grounded = true;
        doubleJump = false;

    }

   }


    private void decreaseHealth()
    {

        health -= 1;
        if (health > 0) { 

            float direction = rb.velocity.x / Mathf.Abs(rb.velocity.x);
            float bounceBack = -10.0f;
            rb.AddForce(new Vector2(direction * bounceBack, 7));
    }

    else
    {

            this.gameObject.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
            rb.velocity = Vector2.zero;
            grounded = false;
            health = 2;
        }



        }
    
   

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            grounded = false;
        }
    }


    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (NoOfEnemeis == 4) ;
        {
            Debug.Log(" you win");
        }

    }
}
