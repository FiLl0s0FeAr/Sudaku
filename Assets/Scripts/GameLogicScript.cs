using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{
    public GameObject fieldPanel;   // Panel where the fields will be displayed
    public GameObject field;        // Field prefab

    public GameObject controlPanel;
    public GameObject controlField;

    public GameObject informationButton;

    private FieldPrefabObject _currentFieldPrefabObject;
    private SudokuObject _gameObject;
    private SudokuObject _finalObject;

    public Button backButton;

    private bool isActiveInformationButton = false;

    private Dictionary<Tuple<int, int>, FieldPrefabObject> _fields = new(); // store the row, column for each field and the field

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSudokuField();
        CreateControlPanel();
        CreateSudokuObject();
    }

    private void CreateSudokuField()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                GameObject instance = Instantiate(field, fieldPanel.transform);

                FieldPrefabObject fieldPrefabObject = new FieldPrefabObject(instance, row, column);
                _fields.Add(new Tuple<int, int>(row, column), fieldPrefabObject);

                instance.GetComponent<Button>().onClick.AddListener(() => OnClickField(fieldPrefabObject));

                //Debug.Log("Sudoku Field created");
            }
        }
    }

    private void CreateControlPanel()
    {
        for (int i = 1; i <= 9; i++)
        {
            
            GameObject instance = Instantiate(controlField, controlPanel.transform);
            instance.GetComponentInChildren<Text>().text = i.ToString();

            ControlFieldPrefabObject controlPrefabObject = new();

            controlPrefabObject.Number = i;

            instance.GetComponent<Button>().onClick.AddListener(() => OnClickControlField(controlPrefabObject));

            //Debug.Log("Control Panel created");
        }
    }

    private void CreateSudokuObject()
    {
        SudokuGenerator.CreateSudokuObject(out SudokuObject finalObject, out SudokuObject gameObject);
        _gameObject = gameObject;
        _finalObject = finalObject;

        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                var currentValue = _gameObject.Values[row, column];
                if (currentValue != 0)
                {
                    FieldPrefabObject fieldObject = _fields[new Tuple<int, int>(row, column)];
                    fieldObject.SetNumber(currentValue);
                    fieldObject.IsChangeAble = false;

                }
            }
        }

        //Debug.Log("Sudoku Object created");
    }

    private void OnClickField(FieldPrefabObject fieldPrefabObject)
    {
        if (fieldPrefabObject.IsChangeAble)
        {
            _currentFieldPrefabObject?.UnsetHover();

            _currentFieldPrefabObject = fieldPrefabObject;

            fieldPrefabObject.SetHover();
            Debug.Log("Clicked on Field");
        }
    }

    private void OnClickControlField(ControlFieldPrefabObject controlFieldPrefabObject)
    {
        if (_currentFieldPrefabObject != null)
        {
            if (isActiveInformationButton)
            {
                _currentFieldPrefabObject.SetSmallNumber(controlFieldPrefabObject.Number);
            }
            else if (!isActiveInformationButton)
            {
                _currentFieldPrefabObject.SetNumber(controlFieldPrefabObject.Number);
            }
            Debug.Log("Clicked on Control Field");
        }

    }

    public void OnClickInformationButton()
    {
        if (!isActiveInformationButton)
        {
            isActiveInformationButton = true;
            informationButton.GetComponent<Image>().color = new Color(0.53f, 0.91f, 1f);
        }
        else if (isActiveInformationButton)
        {
            isActiveInformationButton = false;
            informationButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickFinishButton()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                FieldPrefabObject fieldObject = _fields[new Tuple<int, int>(row, column)];

                if (fieldObject.IsChangeAble)
                {
                    if (_finalObject.Values[row, column] == fieldObject.Number)
                    {
                        fieldObject.ChangeColorToGreen();
                    }
                    else
                    {
                        fieldObject.ChangeColorToRed();
                    }
                }
            }
        }
    }
}
