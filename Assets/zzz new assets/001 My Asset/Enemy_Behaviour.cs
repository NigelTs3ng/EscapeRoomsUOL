using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Enemy_Behaviour : MonoBehaviour
{



    public GameObject _target,_failPane;
    public bool _canChase;
    public float _speed;
    public Vector2 _playerPos, _enemyPos;
    public AudioSource _walk, _attack;
    public AudioClip _walkaudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _playerPos = new Vector2(_target.transform.position.x, _target.transform.position.y);
        _enemyPos = new Vector2(transform.position.x,transform.position.y);

        if (_canChase)
        {
            transform.position = Vector2.MoveTowards(_enemyPos, _playerPos, _speed * Time.deltaTime);

            PlayWalkSound();
        }
        else
            _walk.Stop();


        if(_enemyPos.x > _playerPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (_enemyPos.x < _playerPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }



    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.tag == "Player" )
        {

            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            PlayerController._instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerController._instance.enabled = false;

            PlayerController._instance.gameObject.GetComponent<PlayerInput>().enabled = false;
            _attack.Play();
            _canChase = false;
            _failPane.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "You were CAUGHT".ToString();
            CameraController._instance.gameObject.GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().enabled = false;
            otthers._instace.Spinner_Set(true);
            Invoke("Fail_Panel", 1f);
        }
        
    }


    void Fail_Panel()
    {
        _failPane.SetActive(true);
    }
    #region Audio

    float _delayer;
    void PlayWalkSound()
    {
        if (_canChase)
        {
            if (_delayer < 0)
            {
                _walk.Play();
                _delayer = .5f;
            }
            else
                _delayer -= .5f*Time.deltaTime;

            
        }

    }
    #endregion
}
