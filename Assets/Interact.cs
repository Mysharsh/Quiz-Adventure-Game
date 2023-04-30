using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Interact : MonoBehaviour
{
    public float timeStart = 10;
    public TextMeshProUGUI textBox;
    private bool isCountdownRunning = true;

    private Transform player;
    private GameObject[] Npc;
    private GameObject closestNpc;
    private float remainingTime = 0f;


	//public int currentPts = 0;
    GameObject GlobalManager;
	// Start is called before the first frame update
	void Start()
    {
		GlobalManager = GameObject.FindGameObjectWithTag("Global");
		textBox.text = timeStart.ToString();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Npc = GameObject.FindGameObjectsWithTag("NPC");
		
	}

    // Update is called once per frame
    void Update()
    {
		
		if (isCountdownRunning)
		{
			timeStart -= Time.deltaTime;
			textBox.text = Mathf.Round(timeStart).ToString();

			float minDistance = Mathf.Infinity;
			foreach (GameObject npc in Npc)
			{
				float distancebtw = Vector3.Distance(player.position, npc.transform.position);
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
			textBox.text = "0";
			GlobalManager.GetComponent<Global>().currentPts += Mathf.RoundToInt(timeStart / 2f);
			SceneManager.LoadScene(1);
			enabled = false;
		}
	}
}
