using UnityEngine;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    public GridManagerMult gridManager; // Reference to GridManager
    public TextMeshProUGUI instructionText; // UI text to show instructions

    void Start()
    {
        ShowInstruction("Create a grid to start placing ships.");
    }

    public void ShowInstruction(string message)
    {
        instructionText.text = message; // Update the instruction text on the UI
    }
}