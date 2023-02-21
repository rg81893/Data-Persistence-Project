using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        // Désactiver le bouton au début
        if (submitButton != null)
        {
            submitButton.interactable = false;

            // Attacher un événement à la modification du champ de texte
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }
        GameManager.instance.LoadDataFromFile();
        if (bestScoreText != null)
            bestScoreText.text = "Best Score : " + GameManager.instance.bestScore;
    }
    public void StartNew()
    {
        GameManager.instance.playerName = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.instance.SaveDataToFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnInputFieldValueChanged(string newValue)
    {
        // Activer le bouton si le champ de texte n'est pas vide
        submitButton.interactable = !string.IsNullOrEmpty(newValue);
    }
}
