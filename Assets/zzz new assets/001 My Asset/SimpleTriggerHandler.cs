using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SimpleTriggerHandler : MonoBehaviour
{
    public string ColliderTag;
    [SerializeField] GameObject otherGameObject;
    public bool  isCollider;
    public UnityEvent OnEnter,OnExit;





    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollider)
        {
            
            if (other.tag == ColliderTag)
            {
                otherGameObject = other.gameObject;
                OnEnter.Invoke();
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (!isCollider)
        {
            if (other.CompareTag(ColliderTag))
            {
                OnExit.Invoke();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isCollider)
        {
            if (collision.gameObject.CompareTag(ColliderTag))
            {
                OnEnter.Invoke();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isCollider)
        {
            if (collision.gameObject.CompareTag(ColliderTag))
            {
                OnExit.Invoke();
            }
        }
    }


    public  void DestroyingThis()
    {
        StartCoroutine(destroying(this.gameObject));
    }

    public  void DestroyingOther()
    {
        StartCoroutine(destroying(otherGameObject));

    }



    IEnumerator destroying(GameObject temp)
    {
        yield return new WaitForSeconds(.15f);
        Destroy(temp);
    }
 





}
