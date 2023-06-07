using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditMacroElementData : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public TMP_InputField macroText;
    EditMacrosHandler editMacrosHandler;

    int number = 0;

    private void Start()
    {
        editMacrosHandler = FindObjectOfType<EditMacrosHandler>();
    }

    public int MacroNumber
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            numberText.text = $"{value}:";
        }
    }

    public string MacroText
    {
        get
        {
            return macroText.text;
        }
        set
        {
            macroText.text = value;
        }
    }

    public void MoveElementUp()
    {
        editMacrosHandler.MoveElementUp(MacroNumber);
    }

    public void MoveElementDown()
    {
        editMacrosHandler.MoveElementDown(MacroNumber);
    }

    public void DeleteElement()
    {
        editMacrosHandler.DeleteElement(MacroNumber);
    }
}
