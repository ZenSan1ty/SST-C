using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private Vector3 position;

    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Paper;
    [SerializeField] private GameObject Rock;
    [SerializeField] private GameObject attackSpawnPos;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireTime = 0f;
    [SerializeField] private bool canFire = true;

    //Health info
    [SerializeField] private int maxHealth = 10 ;
    [SerializeField] private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        Movement();
        Boundary();

        transform.position = position;

        if (Time.time > fireTime)
        {
            canFire = true;
        }

        Attack();
    }

    // Player input controls
    private void Movement()
    {
        //Forward Movement
        if(Input.GetKey("d") || Input.GetKey("right"))
            position.x += moveSpeed * Time.deltaTime;
        
        //Backwards Movement
        if(Input.GetKey("a") || Input.GetKey("left"))
            position.x -= moveSpeed * Time.deltaTime;

        //Upwards Movement
        if (Input.GetKey("w") || Input.GetKey("up"))
            position.y += moveSpeed * Time.deltaTime;

        //Downwards Movement
        if (Input.GetKey("s") || Input.GetKey("down"))
            position.y -= moveSpeed * Time.deltaTime;
    }

    private void Boundary()
    {
        //X Boundary Checks
        if (position.x > GameManager.instance.xBoundary)
            position.x = GameManager.instance.xBoundary;
        else if (position.x < -GameManager.instance.xBoundary)
            position.x = -GameManager.instance.xBoundary;

        //Y boundary checks
        if (position.y > GameManager.instance.yBoundary)
            position.y = GameManager.instance.yBoundary;
        else if (position.y < -GameManager.instance.yBoundary)
            position.y = -GameManager.instance.yBoundary;
    }

    private void Attack()
    {
        if (canFire == true)
        {
            //Sword Attack - c/p
            if (Input.GetKeyDown("c") || Input.GetKeyDown("p"))
                ATK_Sword();

            //Paper Attack - x/o
            if (Input.GetKeyDown("x") || Input.GetKeyDown("o"))
                ATK_Paper();

            //Rock Attack - z/i
            if (Input.GetKeyDown("z") || Input.GetKeyDown("i"))
                ATK_Rock();
        }
    }

    private void ATK_Sword()
    {
        Instantiate(Sword, attackSpawnPos.transform.position, transform.rotation);

        canFire = false;
        fireTime = Time.time + fireRate;
    }

    private void ATK_Paper()
    {
        Instantiate(Paper, attackSpawnPos.transform.position, transform.rotation);

        canFire = false;
        fireTime = Time.time + fireRate;
    }

    private void ATK_Rock()
    {
        Instantiate(Rock, attackSpawnPos.transform.position, transform.rotation);

        canFire = false;
        fireTime = Time.time + fireRate;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
