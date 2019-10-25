using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Text.RegularExpressions;

public class UIManager : MonoBehaviour
{
    private int i, j;

    public void setTileSelected()
    {
        string nom = EventSystem.current.currentSelectedGameObject.name;
        string ind = Regex.Replace(nom, "[^0-9]", "");
        i = ind[0] - 48;
        j = ind[1] - 48;
    }

    public void generateNumberSelection()
    {
        GameObject buttonReference = GameObject.Find("ButtonPrefab");
        for (int i = 1; i < 10; i++)
        {
            GameObject tile = UnityEngine.Object.Instantiate(buttonReference, buttonReference.transform.position, buttonReference.transform.rotation, GameObject.Find("ButtonManager").transform);
            tile.name = "Bouton_"+ i;
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
            tile.transform.position = new Vector3((float)(tile.transform.position.x + 1.1*(i-1)), tile.transform.position.y);
        }
        buttonReference.SetActive(false);
    }

    void Start()
    {
        generateNumberSelection();
    }
}
