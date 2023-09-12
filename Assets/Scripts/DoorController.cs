using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public bool isDoorOpen = false;
    public float interactionDistance = 2f;
    private bool isToggling = false;
    public bool requiresKey = true;

    public void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isToggling) return;

            if (IsPlayerNearby())
            {
                ToggleDoor();
            }
            else
            {
                Debug.Log("Too far to interact with the door!");
            }
        }
    }

    //checking for distance between player and the door
    private bool IsPlayerNearby()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= interactionDistance;
        }

        return false;
    }

    public bool RequiresKey()
    {
        return requiresKey;
    }

    public void ToggleDoor()
    {
        if (PlayerController._instance._hasKey == true)
            isDoorOpen = true;
        if (requiresKey && !isDoorOpen)
        {
            // Check if the player has the key to open the door.
            Debug.Log("You need a key to open this door.");
            
        }
        else
        {
            
            if (!isDoorOpen)
            {
                //play door open animation 
                StartCoroutine(PlayDoorAnimation(true));
                doorAnimator.SetBool("isDoorOpen", isDoorOpen);
                isDoorOpen = true;
                Debug.Log("Opening the door.");
                PlayerController._instance._hasKey = false;


            }
            else
            {
                //play door close animation 
                StartCoroutine(PlayDoorAnimation(false));
                doorAnimator.SetBool("isDoorOpen", isDoorOpen);
                isDoorOpen = false;
                Debug.Log("Closing the door.");

            }
        }
           
        
    }
    void PlaySound(AudioClip clip)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }

    private IEnumerator PlayDoorAnimation(bool open)
    {
        isToggling = true;
        doorAnimator.SetBool("isDoorOpen", open);
        this.GetComponent<Collider2D>().enabled = false;
        // Wait for the animation to finish.
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);

        isToggling = false;
        isDoorOpen = open;
    }
}
