using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    bool playerDetected = false;
    private Animator animator;
    [SerializeField] Transform posToGo;
    GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                playerGO.transform.position = posToGo.position;
                playerDetected = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
            animator.SetTrigger("hasTouched");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
