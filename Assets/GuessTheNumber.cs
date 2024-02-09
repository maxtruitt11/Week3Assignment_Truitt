using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessTheNumber : MonoBehaviour
{
    [SerializeField] public TMP_Text headerText;
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public Button submitButton;
    [SerializeField] public Button resetButton;

    private int targetNumber;
    private int attemptsRemaining = 3;
    private bool gameWon = false;
    private string incorrectGuessMessage = "";
    private bool showInitialMessage = true;

    void Start()
    {
        GameSetup();
    }

    public void GameSetup()
    {
        targetNumber = Random.Range(1, 10);
        attemptsRemaining = 3;
        gameWon = false;
        incorrectGuessMessage = "";
        showInitialMessage = true;

        UpdateHeaderText();
        ClearInputField();
    }

    public void SubmitGuess()
    {
        if (gameWon || attemptsRemaining <= 0)
        {
            // Player has already won or doesn't have any attempts remaining
            return;
        }

        string input = inputField.text;
        int guess;

        if (int.TryParse(input, out guess))
        {
            CheckGuess(guess);
        }
        else
        {
            // Display a message if input is invalid
            Debug.Log("Please enter a valid number");
        }
    }

    private void CheckGuess(int guess)
    {
        if (guess == targetNumber)
        {
            GameWon();
        }
        else
        {
            // Display a message for incorrect guesses
            incorrectGuessMessage = "Incorrect guess! ";
            Debug.Log(incorrectGuessMessage);

            attemptsRemaining--;

            if (attemptsRemaining <= 0)
            {
                GameLost();
            }
            else
            {
                showInitialMessage = false;  // Turn off the message after the first incorreect guess
                UpdateHeaderText();
                ClearInputField();
            }
        }
    }

    private void GameWon()
    {
        gameWon = true;
        headerText.text = "You won!";
    }

    private void GameLost()
    {
        gameWon = true;
        headerText.text = "You lose! Better luck next time.";
    }

    private void UpdateHeaderText()
    {
        if (showInitialMessage)
        {
            headerText.text = "I'm thinking of a number between 1 and 10. You have 3 attempts to guess it...";
        }
        else
        {
            headerText.text = incorrectGuessMessage + "You have " + attemptsRemaining + " attempts remaining.";
        }
    }

    private void ClearInputField()
    {
        inputField.text = "";
    }
}
