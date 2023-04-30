using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Global : MonoBehaviour
{
    public int Coins;
    public int Alphabets;
	public float timeStart = 10;
	public TextMeshProUGUI textBox;
	private bool isCountdownRunning = true;

	private Transform player;
	private GameObject[] Npc;
	private GameObject closestNpc;

	public float npcDistanceThreshold = 31f;
	private float remainingTime = 0f;
	
	public int currentPts = 0;




	// Use this for initialization
	void Start()
	{
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
						currentPts = Coins + Alphabets;
						currentPts += Mathf.RoundToInt(remainingTime / 2f);
						SceneManager.LoadScene(1);
					}
					Debug.Log(currentPts);
				}
			}

		}

		if (timeStart <= 0)
		{
			currentPts = Coins + Alphabets;
			isCountdownRunning = false;
			timeStart = 0;
			textBox.text = "0";
			SceneManager.LoadScene(1);
			enabled = false;
		}
	}

	void Awake()
    {
        // keeps object alive to track coins and lives through death
        DontDestroyOnLoad(gameObject);
    }
}
