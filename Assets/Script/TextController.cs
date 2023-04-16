using UnityEngine;

public class TextController : MonoBehaviour
{


    public string textString = "Hello World";
    public Font font;
    public int fontSize = 24;
    public Color fontColor = Color.white;

    private TextMesh textMesh;

    private void Start()
    {
        // Create a new TextMesh component
        textMesh = gameObject.AddComponent<TextMesh>();

        // Set the text properties
        textMesh.text = textString;
        textMesh.font = font;
        textMesh.fontSize = fontSize;
        textMesh.color = fontColor;

        // Position the text
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void Update()
    {
        // Update the text string dynamically
        textMesh.text = "Time: " + Time.time.ToString("F2");
    }

}
