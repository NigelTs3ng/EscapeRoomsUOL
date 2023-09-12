using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public Animator menuPanelAnimator;
    private bool isMenuOpen = false;

    public void ToggleMenu()
    {
        //if (menuPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("MenuSlideIn"))
        //    return;

        //isMenuOpen = !isMenuOpen;
        //menuPanelAnimator.SetBool("isOffScreen", !isMenuOpen);
        PlayerController._instance._cammove = !PlayerController._instance._cammove;
        //otthers._instace.Spinner_Set(_timer);
        otthers._instace.AI_Controller(PlayerController._instance._cammove);
        if (!PlayerController._instance._cammove)
        {

            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Time.timeScale = 0f;

        }
        else if(PlayerController._instance._cammove)
        {

            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Time.timeScale = 1f;
        }
        menuPanelAnimator.SetTrigger("isOffScreen");
    }

    public void ToggleMenuAnimationComplete()
    {
        if (!isMenuOpen)
            menuPanelAnimator.gameObject.SetActive(false);
    }
}
