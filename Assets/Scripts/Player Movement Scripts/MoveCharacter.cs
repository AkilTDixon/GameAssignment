using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveCharacter : MonoBehaviour
{

    [SerializeField] private float DashCooldown = 0.25f;    
    public GameObject Crosshair;
    public float moveSpeed = 5.0f;
    private Rigidbody body;
    public Animator anim;
    public Vector3 facing;
    private float DashStart;
    public string Mode;

    void Start()
    {
        Mode = Camera.main.transform.Find("HUD").Find("GameplayUI").Find("Mode").GetComponent<TextMeshProUGUI>().text;
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
        if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad6)) && Mode != "Multiplayer")
        {
            GameObject HUD = Camera.main.transform.Find("HUD").gameObject;
            HUD.GetComponent<HUDInfo>().SetMultiplayerUI();
            Mode = "Multiplayer";
            HUD.GetComponent<SpawnDifficultyCurve>().IncreaseDifficulty();
            
            //HUD.transform.Find("GameplayUI").Find("Mode").GetComponent<TextMeshProUGUI>().text = Mode;
            //HUD.GetComponent<HUDInfo>().SetMultiplayerUI();
           

            PlayerEntityInfo pei = GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>();
            pei.Player2.SetActive(true);
            pei.Player2Reticle.SetActive(true);
        }

        if (gameObject.name == "Player1")
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                Player1Moving();

            Player1Jumping();
        }

        if (gameObject.name == "Player2")
        {
            if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Keypad6))
                Player2Moving();
            
            Player2Jumping();
        }      

    }

    void Player1Moving()
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
                GetComponent<Afterimages>().timeActivated = Time.time;
                GetComponent<Afterimages>().enabled = true;
                
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                
                GetComponent<BoxCollider>().enabled = false;
                
                body.AddForce(new Vector3((horizontal * 15f) , 0, 0), ForceMode.Impulse);
                DashStart = Time.time;
            }

        }

    }
    void Player2Moving()
    {

        //bool moving = !Mathf.Approximately(horizontal, 0f);
        float horizontal = 0;
        //if ()
        //{
        if (Input.GetKey(KeyCode.Keypad4))
            horizontal = -1;
        if (Input.GetKey(KeyCode.Keypad6))
            horizontal = 1;

        Vector3 direction = new Vector3(0, 0, 0);
            if (horizontal < 0)
                direction.x = -1;
            else
                direction.x = 1;
            transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, 0);

            if (transform.rotation != Quaternion.LookRotation(-direction) && Mode == "Multiplayer")
                transform.rotation = Quaternion.LookRotation(-direction);

            if (Input.GetKeyDown(KeyCode.Keypad9) && (Time.time > DashStart + DashCooldown || DashStart == 0))
            {
                GetComponent<Afterimages>().timeActivated = Time.time;
                GetComponent<Afterimages>().enabled = true;

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                GetComponent<BoxCollider>().enabled = false;


            body.AddForce(new Vector3((horizontal * 15f), 0, 0), ForceMode.Impulse);
                DashStart = Time.time;
            }

        //}

    }


    void Player2Jumping()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            body.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);

        }
    }


    void Player1Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            
        }
    }
    
}
