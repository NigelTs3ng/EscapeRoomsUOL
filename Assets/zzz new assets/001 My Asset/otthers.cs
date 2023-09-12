using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class otthers : MonoBehaviour
{
	public GameObject _fail;
	public Text timertext;
	public float _time;
	public bool stopTimer;

	void Start()
	{
		
	}

	public IEnumerator StartCoundownTimer()
	{
		if (!stopTimer)
		{
			_time -= Time.deltaTime;
			yield return new WaitForSeconds(0.01f);
			string minutes = Mathf.Floor(_time / 60).ToString("00");
			string seconds = (_time % 60).ToString("00");
			string fraction = ((_time * 100) % 100).ToString("000");
			timertext.text = minutes + ":" + seconds;
			if (_time < 10.0f)
			{
				timertext.color = Color.red;
			}
			StartCoroutine(StartCoundownTimer());
		}
	}


	public Enemy_Behaviour[] _ais;

	void LateUpdate()
	{
		if (_time <= 0.5f && !stopTimer)
		{
			stopTimer = true;
			_fail.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "You run out of Time".ToString();
			_fail.SetActive(true);
			PlayerController._instance.enabled = false;
			PlayerController._instance.gameObject.GetComponent<PlayerInput>().enabled = false;
			GetComponent<AudioSource>().enabled = false;
			_ais = GameObject.FindObjectsOfType<Enemy_Behaviour>();
            for (int i = 0; i < _ais.Length; i++)
            {
				_ais[i]._canChase = false;
            }
			// here fail

		}
	}


	public void Start_Timer()
    {

		StartCoroutine(StartCoundownTimer());

	}



	public void Assig(float temp)
    {
		_time = temp;

	}



	public void Restart()
    {

		SceneManager.LoadScene(0);


    }



	public void Spinner_Set(bool temp)
    {
		stopTimer = temp;

	}


}
