using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Encrypt : MonoBehaviour
{
    public GameObject hint;
    public GameObject info;
    public GameObject grid;
    public GameObject tile;
    public GameObject emptyTile;

    public static string message = "hello!";
    public static char key = 'm';

    // Start is called before the first frame update
    void Start()
    {
        updateText(char.ToUpper(key));

        string code = cesarCipher(message.ToUpper(), char.ToUpper(key));
        displayMessage(code);
    }

    public static string getMessage()
    {
        return message;
    }

    void updateText(char k)
    {
        //hint hint: A = key
        hint.GetComponent<TextMeshProUGUI>().text += "A = " + k;
        //info
        int keyIndex = (int)k - 65; //key's index in alphabet ('A' has ASKII value 65)
        info.GetComponent<TextMeshProUGUI>().text
            += "The secret message is encoded using the cesar cipher. " +
            "Each letter in the message is replaced with a new letter based on the hint. " +
            "So wherever you see " + k + ", the real letter is A. This means each letter is " + keyIndex +
            " positions to the right in the alphabet. So " + (char)((keyIndex + 1) % 26 + 65) + " means B, and so on." +
            "Type the real letter underneath, then press Enter to check your answer.";
    }


    string cesarCipher(string m, char k)
    {
        string encodedM = "";
        int keyNum = (int)k - 65; //key's index in alphabet ('A' has ASKII value 65)

        foreach (char c in m) //iterate through chars in message
        {
            if (!char.IsLetter(c)) //if punctuation, don't change
            {
                encodedM += c;
            }
            else
            {
                int cIndex = (int)c - 65; //current letter's index
                int newIndex = (cIndex + keyNum) % 26; //shift based on key
                char newC = (char)(newIndex + 65); //65 = ASKII 'A'
                encodedM += newC;
            }
        }

        return encodedM;
    }

    void displayMessage(string code)
    {
        foreach (char c in code)
        {
            //if space, add empty tile
            if (char.IsWhiteSpace(c))
            {
                Instantiate(emptyTile, grid.transform);
            }
            else //otherwise, create tile
            {
                GameObject currentLetter = Instantiate(tile, grid.transform); //add tile
                Transform letter = currentLetter.transform.GetChild(0); //get text
                letter.GetComponent<TextMeshProUGUI>().text += c; //change text to current letter in code

                if (!char.IsLetter(c)) //if punctuation, add tile without input field
                {
                    Destroy(currentLetter.transform.GetChild(1).gameObject);
                }
            }

        }
    }
}






