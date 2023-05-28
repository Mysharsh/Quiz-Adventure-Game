using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Toggle option1Toggle;
    public Toggle option2Toggle;
    public Toggle option3Toggle;
    public Toggle option4Toggle;
    public GameObject nextButton;
    public GameObject SubmitButton;
    public TextMeshProUGUI Scoretxt;
    public TextMeshProUGUI IQtxt;
    public TextMeshProUGUI Agetxt;
    public GameObject Dialoguebox;
    public GameObject Resultbox;
    [HideInInspector]
    public int quespts = 0;
    [HideInInspector]
    public int Totalpts = 0;
    [HideInInspector]
    public float IQ = 0;
    //public Global pts;
    GameObject GlobalManager;

    private int currentQuestionIndex = 0;
    private List<Question> questions = new List<Question>();

    void Start()
    {
        GlobalManager = GameObject.FindGameObjectWithTag("Global");
        LoadQuestions();
        DisplayQuestion();
    }

    void LoadQuestions()
    {
        string jsonFilePath;

        // Check the current platform and set the JSON file path accordingly
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android platform
            jsonFilePath = Path.Combine(Application.streamingAssetsPath, "quiz_data.json");
            // Read the JSON file using UnityWebRequest
            UnityWebRequest www = UnityWebRequest.Get(jsonFilePath);

            // Send the request and wait for a response
            www.SendWebRequest();

            while (!www.isDone) { }

            // Check for errors
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Retrieve the JSON text
                string jsonString = www.downloadHandler.text;

                // Parse the JSON data into the Quiz object
                Quiz quiz = JsonUtility.FromJson<Quiz>(jsonString);
                questions = quiz.questions;
                Debug.Log(questions);
            }
            else
            {
                // Error occurred while accessing the JSON file
                Debug.LogError("Failed to read JSON file: " + www.error);
            }
        }
        else
        {
            // Other platforms (e.g., Windows)
            jsonFilePath = Path.Combine(Application.streamingAssetsPath, "quiz_data.json");
            // Read the JSON file using the appropriate file path
            string jsonString = File.ReadAllText(jsonFilePath);

            Quiz quiz = JsonUtility.FromJson<Quiz>(jsonString);
            questions = quiz.questions;
            Debug.Log(questions);
        }
    }
    void DisplayQuestion()
    {
        Question question = questions[currentQuestionIndex];
        questionText.text = question.question;
        option1Toggle.GetComponentInChildren<TextMeshProUGUI>().text = question.options[0];
        option2Toggle.GetComponentInChildren<TextMeshProUGUI>().text = question.options[1];
        option3Toggle.GetComponentInChildren<TextMeshProUGUI>().text = question.options[2];
        option4Toggle.GetComponentInChildren<TextMeshProUGUI>().text = question.options[3];
    }
    void CheckAnswer()
    {
        Question question = questions[currentQuestionIndex];
        if (option1Toggle.isOn && question.answer == 0)
        {
            Debug.Log("correct 1");// Show correct feedback
            quespts += 5;
        }
        else if (option2Toggle.isOn && question.answer == 1)
        {
            Debug.Log("correct 2");
            quespts += 5;
        }
        else if (option3Toggle.isOn && question.answer == 2)
        {
            Debug.Log("correct 3");
            quespts += 5;
        }
        else if (option4Toggle.isOn && question.answer == 3)
        {
            Debug.Log("correct 4");
            quespts += 5;
        }
        else
        {
            Debug.Log("incorrect");
        }
    }
    public void NextQuestion()
    {
        CheckAnswer();
        Debug.Log(currentQuestionIndex+",,"+ questions.Count);
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion();
        }
        else
        {
            nextButton.SetActive(false);
            SubmitButton.SetActive(true);
            Debug.Log("// Quiz is over, show player's score");
        }
    }
    public void Submitbtn()
    {
        //CheckAnswer();
        Dialoguebox.SetActive(false);
        Resultbox.SetActive(true);
        Totalpts = quespts + GlobalManager.GetComponent<Global>().currentPts;
        Debug.Log(Totalpts);
        IQ = Mathf.RoundToInt(Totalpts / 48.14f)*100;
        IQtxt.SetText(IQ.ToString());
        Scoretxt.SetText(Totalpts.ToString());
        Agetxt.SetText(GlobalManager.GetComponent<Global>().Age.text.ToString());
    }
}
[System.Serializable]
public class Quiz
{
    public List<Question> questions;
}
[System.Serializable]
public class Question
{
    public string question;
    public List<string> options;
    public int answer;
}
