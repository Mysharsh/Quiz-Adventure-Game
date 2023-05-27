using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Global : MonoBehaviour
{
   // public int Coins;
    //public int Alphabets;
	[HideInInspector]
	public int currentPts ;
	public TMP_InputField Age;
	public TMP_InputField Name;
   


	void Awake()
    {
        // keeps object alive to track coins 
        DontDestroyOnLoad(gameObject);
    }
}
