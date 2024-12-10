using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{
    public GameObject fieldPanel;   // Panel where the fields will be displayed
    public GameObject field;        // Field prefab

    public GameObject controlPanel;
    public GameObject controlField;

    private FieldPrefabObject _currentFieldPrefabObject;

    private Dictionary<Tuple<int, int>, FieldPrefabObject> _fields = new Dictionary<Tuple<int, int>, FieldPrefabObject>(); // store the row, column for each field and the field

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSudokuField();
        CreateControlPanel();
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

                FieldPrefabObject fieldPrefabObject = new FieldPrefabObject(instance, row, column);
                _fields.Add(new Tuple<int, int>(row, column), fieldPrefabObject);

                instance.GetComponent<Button>().onClick.AddListener(() => OnClickField(fieldPrefabObject));

            }
        }
    }

    private void CreateControlPanel()
    {
        for (int i = 1; i <= 9; i++)
        {
            
            GameObject instance = Instantiate(controlField, controlPanel.transform);
            instance.GetComponentInChildren<Text>().text = i.ToString();

            ControlFieldPrefabObject controlPrefabObject = new ControlFieldPrefabObject();

            controlPrefabObject.Number = i;

            instance.GetComponent<Button>().onClick.AddListener(() => OnClickControlField(controlPrefabObject));

        }
    }

    private void OnClickField(FieldPrefabObject fieldPrefabObject)
    {
        if (_currentFieldPrefabObject != null)
        {
            _currentFieldPrefabObject.UnsetHover();
        }

        _currentFieldPrefabObject = fieldPrefabObject;

        fieldPrefabObject.SetHover();
        Debug.Log("Clicked on Field");
    }

    private void OnClickControlField(ControlFieldPrefabObject controlFieldPrefabObject)
    {
        if (_currentFieldPrefabObject != null)
        {
            _currentFieldPrefabObject.SetNumber(controlFieldPrefabObject.Number);
        }
        Debug.Log("Clicked on Control Field");
    }
}
