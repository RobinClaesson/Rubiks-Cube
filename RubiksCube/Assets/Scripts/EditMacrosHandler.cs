using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMacrosHandler : MonoBehaviour
{
    public GameObject macroInfoPrefab;
    public GameObject macrosPanel;
    int startY = 350, startX = -22, distance = 130;

    List<GameObject> macroElements = new List<GameObject>();

    public void UpdateMacroElements()
    {
        MacroHandler macroHandler = FindObjectOfType<MacroHandler>();

        for (int i = 0; i < macroHandler.allMacros.Count; i++)
        {
            GameObject macroElement = Instantiate(macroInfoPrefab, macrosPanel.transform);
            macroElement.transform.localPosition = new Vector2(startX, startY - distance * i);
            EditMacroElementData data = macroElement.GetComponent<EditMacroElementData>();
            data.MacroNumber = i + 1;
            data.MacroText = macroHandler.allMacros[i];
            macroElements.Add(macroElement);
        }
    }

    public void CancelEdit()
    {
        foreach (GameObject element in macroElements)
        {
            Destroy(element);
        }
        macroElements.Clear();

        gameObject.SetActive(false);
    }

    public void ApplyEdit()
    {
        gameObject.SetActive(false);
    }

    public void MoveElementUp(int macroNumber)
    {
        int index = macroNumber - 1;
        if (index > 0)
        {
            Vector2 tmpPos = macroElements[index].transform.position;
            macroElements[index].transform.position = macroElements[index - 1].transform.position;
            macroElements[index - 1].transform.position = tmpPos;

            macroElements[index].GetComponent<EditMacroElementData>().MacroNumber--;
            macroElements[index - 1].GetComponent<EditMacroElementData>().MacroNumber++;

            GameObject element = macroElements[index];
            macroElements.RemoveAt(index);
            macroElements.Insert(index - 1, element);
        }
    }

    public void MoveElementDown(int macroNumber)
    {
        int index = macroNumber - 1;

        if (index < macroElements.Count - 1)
        {
            Vector2 tmpPos = macroElements[index].transform.position;
            macroElements[index].transform.position = macroElements[index + 1].transform.position;
            macroElements[index + 1].transform.position = tmpPos;

            macroElements[index].GetComponent<EditMacroElementData>().MacroNumber++;
            macroElements[index + 1].GetComponent<EditMacroElementData>().MacroNumber--;

            GameObject element = macroElements[index + 1];
            macroElements.RemoveAt(index + 1);
            macroElements.Insert(index, element);
        }
    }

    public void DeleteElement(int macroNumber)
    {

    }
}
