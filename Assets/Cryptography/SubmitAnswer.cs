using TMPro;
using UnityEngine;

public class SubmitAnswer : MonoBehaviour
{
    //public TextMeshProUGUI letter1;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
    public TMP_InputField inputField5;
    public TMP_InputField inputField6;
    public TMP_InputField inputField7;
    public TMP_InputField inputField8;

    //check if answer is right or wrong, and change color accordingly
    public void ButtonPressed()
    {
        checkAnswer(inputField1, "X");
        checkAnswer(inputField2, "Y");
        checkAnswer(inputField3, "Z");
        checkAnswer(inputField4, "A");
        checkAnswer(inputField5, "B");
        checkAnswer(inputField6, "C");
        checkAnswer(inputField7, "D");
        checkAnswer(inputField8, "E");

    }

    void checkAnswer(TMP_InputField inputField, string answer)
    {
        //get text, make uppercase
        string input = inputField.GetComponent<TMP_InputField>().text.ToUpper();

        if (input.Equals(answer))
            markAnswerRight(inputField);
        else
            markAnswerWrong(inputField);
    }

    void markAnswerRight(TMP_InputField inputField)
    {
        //make green
        inputField.GetComponent<TMP_InputField>().image.color = new Color(0f, 1f, 0f, 1f);
        //fix answer
        inputField.GetComponent<TMP_InputField>().interactable = false;
    }

    void markAnswerWrong(TMP_InputField inputField)
    {
        //make red
        inputField.GetComponent<TMP_InputField>().image.color = new Color(1f, 0f, 0f, 1f);
    }

}
 