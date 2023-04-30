using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Countdown : MonoBehaviour
{
	public float timeStart = 10;
	public TextMeshProUGUI textBox;

	// Use this for initialization
	void Start()
	{
		textBox.text = timeStart.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		timeStart -= Time.deltaTime;
		textBox.text = Mathf.Round(timeStart).ToString();


		if (timeStart <= 0)
		{
			timeStart = 0;
			textBox.text = "0";
			GetComponent<QuizManager>().Submitbtn();
			enabled = false;
		}
	}
}