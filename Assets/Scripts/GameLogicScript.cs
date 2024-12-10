using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{
    public GameObject fieldPanel;   // Panel where the fields will be displayed
    public GameObject field;        // Field prefab

    private Dictionary<Tuple<int, int>, GameObject> fields = new Dictionary<Tuple<int, int>, GameObject>(); // store the row, column for each field and the field

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSudokuField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSudokuField()
    {
        for (int row = 1; row <= 9; row++)
        {
            for (int column = 1; column <= 9; column++)
            {
                GameObject instance = Instantiate(field, fieldPanel.transform);
                instance.name = row.ToString() + " " + column.ToString();

                instance.GetComponent<Button>().onClick.AddListener( () => OnClickField());

                fields.Add(new Tuple<int, int>(row, column), field);
            }
        }
    }

    private void OnClickField()
    {
        Debug.Log("Clicked");
    }
}
