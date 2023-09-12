using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUp_DoorHandler : MonoBehaviour
{


    public Text _infoText;
    public bool _hasAccess;
    public GameObject _access;
    public GameObject _otherGA;
    public bool _canPressforPick;
    public bool _canPressforOpne;
    
    public otthers _others;

    public AudioSource Ad;

    private void OnTriggerEnter2D(Collider2D other)
    {



        if(other.transform.tag == "Keyes")
        {
            if (!_hasAccess)
            {
                _otherGA = other.gameObject;

                _infoText.text = "Press 'E' to Pick up".ToString();
                _infoText.gameObject.SetActive(true);
                _canPressforPick = true;
            }
            else if (_hasAccess)
            {

                _infoText.text = "You already Pick the Key".ToString();
                _infoText.gameObject.SetActive(true);
                Invoke("Tewxt_False", 1f);


            }
        }
        


        if(other.tag == "DoorsLockes")
        {
            if (!_hasAccess)
            {
                //_otherGA = other.gameObject;

                _infoText.text = "You need a key to open".ToString();
                _infoText.gameObject.SetActive(true);
            }
            else if (_hasAccess)
            {

                _otherGA = other.gameObject;
                _infoText.text = "Press 'E' to Open".ToString();
                _infoText.gameObject.SetActive(true);

                _canPressforOpne = true;


            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Keyes")
        {

            _otherGA = null;
            _infoText.gameObject.SetActive(false);
            _canPressforPick = false;
        }
        if (collision.transform.tag == "DoorsLockes")
        {

            _otherGA = null;
            _infoText.gameObject.SetActive(false);
            _canPressforOpne = false;
        }

    }


    void PickUP()
    {

        if(_otherGA != null )
        {
            //if (!_hasAccess)
            //{


                _access = _otherGA;
                _hasAccess = true;
                _otherGA.SetActive(false);
                _otherGA = null;
                Debug.LogError("You piucked the key");
            //}
            //else if(_hasAccess)
            //{

                //_infoText.text = "You already Pick the Key".ToString();
                //_infoText.gameObject.SetActive(true);
                //Invoke("Tewxt_False",.5f);


            //}
        }

    }


    void Tewxt_False()
    {
        _infoText.gameObject.SetActive(false);
        _infoText.text = "".ToString();
    }




    private void Update()
    {
        if (_canPressforPick)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PickUP();
            }

        }
        if (_canPressforOpne)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Door_Open();
            }

        }


    }




    void Door_Open()
    {
        if(_otherGA != null)
        {
            if (_access.name == "Key")
            {
                _otherGA.transform.parent.GetComponent<Animator>().enabled = true;
                _otherGA.transform.parent.GetComponent<Collider2D>().enabled = false;
                _canPressforOpne = true;
                _hasAccess = false;
                Ad.Play();
                //Debug.LogError(transform.parent.name);
            }
            else
            {
                _others._time -= 30f;
                _hasAccess = false;
                _access = null;


                _infoText.text = "You Picked the Wroung Key".ToString();
                _infoText.gameObject.SetActive(true);
                Invoke("Tewxt_False", 1f);
                

            }
        }
    }







}
