using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Toggle option1Toggle;
    public Toggle option2Toggle;
    public Toggle option3Toggle;
    public Toggle option4Toggle;
    public GameObject nextButton;
    public GameObject SubmitButton;
    public string jsonFilePath;
    public TextMeshProUGUI Scoretxt;
    public GameObject Dialoguebox;
    public GameObject Resultbox;
    [HideInInspector]
    public int quespts = 0;
    //public Global pts;


    private int currentQuestionIndex = 0;
    private List<Question> questions = new List<Question>();

    void Start()
    {
        LoadQuestions();
        DisplayQuestion();
        Scene sourcescene = SceneManager.GetSceneByName("Demo");
        GameObject[] gameObject = sourcescene.GetRootGameObjects();


        Debug.Log(GetComponent<Global>());
        //Debug.Log(pts);
       // quespts=pts.currentPts;
        Debug.Log(quespts);
        //nextButton.onClick.AddListener(NextQuestion);
    }

    void LoadQuestions()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/" + jsonFilePath);
        Quiz quiz = JsonUtility.FromJson<Quiz>(jsonString);
        questions = quiz.questions;
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
            quespts += 10;
        }
        else if (option2Toggle.isOn && question.answer == 1)
        {
            Debug.Log("correct 2");
            quespts += 10;

        }
        else if (option3Toggle.isOn && question.answer == 2)
        {
            Debug.Log("correct 3");
            quespts += 10;

        }
        else if (option4Toggle.isOn && question.answer == 3)
        {
            Debug.Log("correct 4");
            quespts += 10;

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
        Scoretxt.SetText(quespts.ToString());
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
