using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Interact : MonoBehaviour
{
    public float timeStart = 100;
    public TextMeshProUGUI TimetextBox;
    private bool isCountdownRunning =false;

    private GameObject player;
    private GameObject[] Npc;
    private GameObject closestNpc;
    private float remainingTime = 0f;
	public GameObject Playbtn;
	public GameObject AgePanel;
	public GameObject ErrorPanel;
	public GameObject ControllerPanel;


	private void Awake()
    {
		ControllerPanel.SetActive(false);
		ErrorPanel.SetActive(false);
	}
    
	//public int currentPts = 0;
	GameObject GlobalManager;
	// Start is called before the first frame update
	void Start()
    {
		//ControllerPanel.SetActive(false);
		GlobalManager = GameObject.FindGameObjectWithTag("Global");
		TimetextBox.text = timeStart.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        Npc = GameObject.FindGameObjectsWithTag("NPC");
		
	}

	// Update is called once per frame
	void Update()
    {
		
		if (isCountdownRunning)
		{
			timeStart -= Time.deltaTime;
			TimetextBox.text = Mathf.Round(timeStart).ToString();

			float minDistance = Mathf.Infinity;
			foreach (GameObject npc in Npc)
			{
				float distancebtw = Vector3.Distance(player.transform.position, npc.transform.position);
				if (distancebtw < minDistance)
				{
					minDistance = distancebtw;
					closestNpc = npc;
					Debug.Log(closestNpc);
					if (distancebtw < 2f)
					{
						// Stop countdown and calculate remaining time
						isCountdownRunning = false;
						remainingTime = timeStart;
						//currentPts = GlobalManager.GetComponent<Global>().currentPts;
						GlobalManager.GetComponent<Global>().currentPts += Mathf.RoundToInt(remainingTime / 2f);
						SceneManager.LoadScene(1);
					}
					Debug.Log(GlobalManager.GetComponent<Global>().currentPts);
				}
			}

		}

		if (timeStart <= 0)
		{
			isCountdownRunning = false;
			timeStart = 0;
			TimetextBox.text = "0";
			GlobalManager.GetComponent<Global>().currentPts += Mathf.RoundToInt(timeStart / 2f);
			SceneManager.LoadScene(1);
			enabled = false;
		}
	}
	public void onclickplay()
    {
        if (GlobalManager.GetComponent<Global>().Age.text.ToString() != string.Empty & GlobalManager.GetComponent<Global>().Name.text.ToString() != string.Empty)
        {
            AgePanel.SetActive(false);
            ControllerPanel.SetActive(true);
            isCountdownRunning = true;
		}
		else
        {
            ErrorPanel.SetActive(true);
        }

    }

}
