using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cheat : MonoBehaviour
{



    public Text _cheatText;

    public string _num;
    public bool _matchFound;

    public Actions[] _cheatsArray;





    public void CheatFTN(int temp)
    {
        _num = _cheatText.text;
        if(_num.Length < 4)
            _cheatText.text = (_cheatText.text + temp).ToString();
        _cheatText.transform.GetChild(1).gameObject.SetActive(false);

    }



    public void DeleteLAstDigit()
    {
        if(_cheatText.text.Length > 0)
            _cheatText.text = _cheatText.text.Remove(_cheatText.text.Length - 1);

        _num = _cheatText.text;
        if(_num.Length == 0)
            _cheatText.transform.GetChild(1).gameObject.SetActive(true);

    }



    public void Done()
    {

        for (int i = 0; i < _cheatsArray.Length; i++)
        {
            if (_cheatText.text == _cheatsArray[i]._cheatCode)
            {
                // Spawn required item
                Debug.Log(_cheatsArray[i]._action + "Spawned");
                _cheatsArray[i]._action.Invoke();
                _cheatText.text = null;
                _matchFound = true;
                //break;
            }
        }
        if(_matchFound)
        {
            _matchFound = false;
            return;
        }
        else if(!_matchFound)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        _cheatText.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _cheatText.transform.GetChild(0).gameObject.SetActive(false);

    }

    [System.Serializable]
    public class Actions
    {
        public string _productName;
        public string _cheatCode;
        public UnityEngine.Events.UnityEvent _action;


    }








}
