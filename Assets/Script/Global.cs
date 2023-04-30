using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Global : MonoBehaviour
{
   // public int Coins;
    //public int Alphabets;
	[HideInInspector]
	public int currentPts ;


	void Awake()
    {
        // keeps object alive to track coins and lives through death
        DontDestroyOnLoad(gameObject);
    }
}
