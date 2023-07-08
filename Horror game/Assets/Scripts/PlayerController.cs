using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float gravity = -22;
    public float groundDistance = 0.4f;

    public Inventory inventory;
    public Messager messager;

    private bool doorIsOpen;

    public Transform groundCheck;
    public LayerMask groundMask;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    CharacterController characterController;
    Animator animator;

    Vector3 move;
    Vector3 velocity;

    bool isGrounded;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Win")
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.gameObject.GetComponent<Door>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.gameObject.GetComponent<Door>().key is null || inventory.HasItem(hit.collider.gameObject.GetComponent<Door>().key.itemID))
                        ChangeDoorState(hit.collider.gameObject.GetComponent<Animator>());
                    else
                    {
                        messager.ShowMessage($"Требуется {hit.collider.gameObject.GetComponent<Door>().key.color} ключ", 2);
                    }
                }
            }
        }





        if (GameStates.inventoryIsOpen)
            return;


        animator.SetBool("Move", false);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            animator.SetBool("Move", true);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        if (isGrounded == true)
        {
            move = transform.right * horizontal + transform.forward * vertical;
            if (Input.GetKey(KeyCode.LeftShift))
                characterController.Move(move * speed * 1.5f * Time.deltaTime);
            else
                characterController.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void ChangeDoorState(Animator doorAnimator)
    {
        if (!doorIsOpen)
        {
            OpenDoor(doorAnimator);
            doorIsOpen = true;
        }
        else
        {
            CloseDoor(doorAnimator);
            doorIsOpen = false;
        }
    }

    private void OpenDoor(Animator doorAnimator)
    {
        doorAnimator.SetBool("Open", true);
    }

    private void CloseDoor(Animator doorAnimator)
    {
        doorAnimator.SetBool("Open", false);
    }
}
