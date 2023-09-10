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
        }

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

            PlayerController._instance.enabled = false;
            PlayerController._instance.gameObject.GetComponent<PlayerInput>().enabled = false;


            _failPane.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "You were CAUGHT".ToString();
            _failPane.SetActive(true);
            _canChase = false;


        }
        
    }


}
