using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int tileSelected;
    private string nameObject;

    public void getNameObject()
    {
        nameObject = EventSystem.current.currentSelectedGameObject.name;
    }

    public void setTileSelected()
    {
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
