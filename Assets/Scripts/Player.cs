using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveForce;
    public float jumpForce;
    public float maxMoveForce;
    public int dubleJumps = 1;
    private int currentDubleJumps = 0;
    [Space(12)]
    public int hearts = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject groundCheck;
    private bool grounded;
    [Space(12)]
    public GameObject AXE;
    private bool canAttack;
    private float attackTimer;
    [Space(12)]
    [SerializeField] private Transform rotationObject;
    [SerializeField] private Transform spawner;
    [SerializeField] private float timeToAttack;
    [Space(12)]
    private Camera cam;
    private Vector3 mousePos;

    [SerializeField]private float horizontal;
    [HideInInspector]public Rigidbody2D rb;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    private void Update()
    {
        Jump();
        Hearts();
        Attack();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (moveForce > maxMoveForce) moveForce = maxMoveForce;

        grounded = groundCheck.GetComponent<GroundCheck>().tuchGround;
        if (grounded) currentDubleJumps = 0;


        if (rb.velocity.x > 0 || rb.velocity.x < 0) animator.SetBool("move", true);
        else animator.SetBool("move", false);
        attackTimer += Time.deltaTime;

        if(attackTimer > timeToAttack)
        {
            attackTimer = 0;
            canAttack = true;
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotationObject.rotation = Quaternion.Euler(0, 0, rotZ);
        animator.speed = moveForce / 2; 
    }
    private void FixedUpdate()
    {
        if (horizontal != 0) gameManager.score+=1;
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Tree"))
        {
            collision.GetComponent<Tree>().Adios();
            collision.GetComponent<PolygonCollider2D>().enabled = false;
            GameManager gm = FindObjectOfType<GameManager>();
            gm.time -= 5f;
            ManageHearts(amount:1,heal:false);
        }
    }
    private void Move()
    {
        if (horizontal > 0.1 || horizontal < -0.1)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
            moveForce += 0.1f;
            rb.velocity = new Vector2(horizontal * moveForce,rb.velocity.y);
        }
        if(horizontal == 0)
        { 
            moveForce = 1f;
        }

        if(grounded && horizontal == 0 && rb.velocity.x > 0 || grounded && horizontal == 0 && rb.velocity.x < 0)
        {
            rb.velocity = new Vector3(0,rb.velocity.y);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded || Input.GetKeyDown(KeyCode.Space) && currentDubleJumps <dubleJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            currentDubleJumps++;
        }
    }
    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && canAttack )
        {
            if (Input.mousePosition.y < Screen.height / 2) rb.velocity = new Vector3(rb.velocity.x, 7);
            Instantiate(AXE, new Vector3(spawner.position.x, spawner.position.y, 0f),Quaternion.identity);
            canAttack=false;
        }
    }
    public void Hearts()
    { 
        if(hearts <= 0)
        {
            gameManager.GameOver();
        }
    }
    public void ManageHearts(int amount=1,bool heal = false)
    {
        if (!heal) hearts -= amount;
        else hearts += amount;
    }
}
