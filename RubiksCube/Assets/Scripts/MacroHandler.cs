using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MacroHandler : MonoBehaviour
{
    MoveHandler moveHandler;
    string[] defaultMacros = new string[] { "R U Rp Up", "Lp Up L U", "U R Up Rp", "Up Lp U L" };
    public List<string> allMacros = new List<string>();

    public List<GameObject> buttonObjects = new List<GameObject>();
    List<TextMeshProUGUI> buttonMacroTexts;

    public GameObject editMacroPanel;

    int currentMacroPage = 0;
    int macroPageCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        moveHandler = FindObjectOfType<MoveHandler>();
        buttonMacroTexts = buttonObjects.Select(x => x.GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "Macro Text")).ToList();

        LoadMacros();

        UpdateActiveMacros();
    }

    // Update is called once per frame
    void Update()
    {
        //Check macro key press
        for (int i = 0; i < allMacros.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
                moveHandler.AddMoves(allMacros[i]);
        }
    }

    private void LoadMacros()
    {
        if (!PlayerPrefs.HasKey("Macro1"))
        {
            for (int i = 1; i <= defaultMacros.Length; i++)
            {
                PlayerPrefs.SetString($"Macro{i}", defaultMacros[i - 1]);
            }
        }

        int currentMacro = 1;
        while (PlayerPrefs.HasKey($"Macro{currentMacro}"))
        {
            allMacros.Add(PlayerPrefs.GetString($"Macro{currentMacro++}"));
        }

        macroPageCount = Mathf.CeilToInt(allMacros.Count() / 9.0f);
    }

    private void UpdateActiveMacros()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i < allMacros.Count)
            {
                buttonObjects[i].SetActive(true);

                if (allMacros[i].Length <= 45)
                {
                    buttonMacroTexts[i].text = allMacros[i];
                }
                else
                {
                    buttonMacroTexts[i].text = allMacros[i].Substring(0, 40) + "...";
                }
            }
            else
            {
                buttonObjects[i].SetActive(false);
            }
        }
    }

    public void RunMacro(int macroNumber)
    {
        moveHandler.AddMoves(allMacros[macroNumber]);
    }

    public void OpenEditMacrosPanel()
    {
        editMacroPanel.SetActive(true);
        editMacroPanel.GetComponent<EditMacrosHandler>().UpdateMacroElements();
        //Call stuff to update the panel
    }
}
