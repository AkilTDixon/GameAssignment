using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveCharacter : MonoBehaviour
{

    [SerializeField] private float DashCooldown = 0.25f;
    private float moveSpeed = 5.0f;
    private Rigidbody body;
    public Animator anim;
    public Vector3 facing;
    private float DashStart;
    private string Mode;

    void Start()
    {
        Mode = Camera.main.transform.Find("HUD").Find("Mode").GetComponent<TextMeshProUGUI>().text;
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        facing = Vector3.right;
        DashStart = 0f;
        
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")

        
    }*/


    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        PlayerJumping();



    }

    void PlayerMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool moving = !Mathf.Approximately(horizontal, 0f);
        if (moving)
        {


            Vector3 direction = new Vector3(0,0,0);
            if (horizontal < 0)
                direction.x = -1;
            else
                direction.x = 1;
            transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, 0);

            if (transform.rotation != Quaternion.LookRotation(-direction) && Mode == "Multiplayer")
                transform.rotation = Quaternion.LookRotation(-direction);
            
            if (Input.GetKeyDown(KeyCode.E) && (Time.time > DashStart + DashCooldown || DashStart == 0))
            {
                body.AddForce(new Vector3((horizontal * 15f) , 0, 0), ForceMode.Impulse);
                DashStart = Time.time;
            }

        }

    }
    void PlayerJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            
        }
    }
    void PlayerMoving2D()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //Positive horizontal = right
        //Negative horizontal = left

        bool moving = !Mathf.Approximately(horizontal, 0f);
        if (moving)
        {

            transform.Translate(horizontal * moveSpeed * Time.deltaTime, 0, 0);
            print(horizontal);
            FaceDirection2D(horizontal < 0f ? Vector3.left : Vector3.right);
        }
        anim.SetBool("playerMoving", moving);
    }
    private void FaceDirection2D(Vector3 direction)
    {
        facing = direction;
        GetComponent<SpriteRenderer>().flipX = direction != Vector3.right;
    }
}
