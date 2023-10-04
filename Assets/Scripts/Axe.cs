using System.Collections;
using UnityEngine;


public class Axe : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    [SerializeField] float destroyTime;
    private GameManager gameManager;
    private AudioSource axeSound;
    private void Start()
    {
        axeSound = GetComponent<AudioSource>();
        axeSound.Play();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector3(direction.x, direction.y, direction.z) * force;

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, -20f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag ==("Tree"))
        {
            gameManager.time++;
            gameManager.score += 50;
            collision.GetComponent<Tree>().Adios();
            Destroy(this.gameObject);
        }
    }
}
