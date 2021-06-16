using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This will be the main player controller it does not work in the VR environment since I don't have any
 * vr equipment to test it on so what this controller will encompass is:
 * Right Click: pan the camer
 * W: move forward
 * A: move left
 * S: move back
 * D: move right
 * esc: open the menu tab to teleport
 * 
 * The main purpose of this is to demo a different way to handle the networked awacs learning enviroment
 * the main goals of this are to:
 * Make connecting to a vr awacs server computer easier
 * Present the challenge of expandability potentially to Germany, Hawaii, and Alaska
 * 
 * How I took these challenges on, whether correct or incorrect we will see but I will:
 * Create a seperate server side aspect of the game environment so it isn't included in one package
 * ^what that does is makes all of the computers on the same playing field and will make it possible
 * ^^to have a dedicated server in OKC that will make scalability will be more doable
 * The goal for me to test with this is to have one server in OKC capable of running 38 individual 'awacs' or
 * having up to all 38 computers on the same 'awacs' or any other possible awacs configurations connected to
 * the same server IP address this allows more user friendly "server addresses" to be used for example referencing an 
 * awacs as Awacs1 or Jake's Awacs to connect to it is easier than 192.168.1.11 with the current system(this also
 * could solve the issue with having to input the server's IPv4 address before loading the awacs)
 * 
 * @author A1C Jake Brower
 * @date 14 June 2021
 */
public class PlayerController : MonoBehaviour
{

    public int movementSpeed = 5;
    public int rotateSpeed = 5;
    public Canvas mainUI;
    private bool canMove = true;
    public string username;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) & canMove == true)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) & canMove == true)
        {
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) & canMove == true)
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) & canMove == true)
        {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetMouseButton(1) & canMove == true)
        {
            float x = rotateSpeed * Input.GetAxis("Mouse X");
            float y = rotateSpeed * -Input.GetAxis("Mouse Y");
            Camera.main.transform.Rotate(y, x, 0);
            float z = Camera.main.transform.eulerAngles.z;
            Camera.main.transform.Rotate(0, 0, -z);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainUI.gameObject.SetActive(!mainUI.gameObject.activeSelf);
            canMove = !canMove;
        }
    }
}
