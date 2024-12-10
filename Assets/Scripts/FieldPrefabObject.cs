using UnityEngine;
using UnityEngine.UI;

public class FieldPrefabObject
{
    private int _row;
    private int _column;
    private GameObject _prefab;
    public bool IsChangeAble = true;
    public FieldPrefabObject(GameObject instance, int row, int column)
    {
        _prefab = instance;
        Row = row;
        Column = column;
    }

    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }

    public bool TryGetTextByName(string name, out Text text)
    {
        text = null;
        Text[] texts = _prefab.GetComponentsInChildren<Text>();
        foreach (var currentText in texts)
        {
            if (currentText.name.Equals(name))
            {
                text = currentText;
                return true;
            }
        }

        return false;
    }

    public void SetHover()
    {
        _prefab.GetComponent<Image>().color = new Color(0.53f, 0.91f, 1f);
    }

    public void UnsetHover()
    {
        _prefab.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public void SetNumber(int number)
    {
        if (TryGetTextByName("FieldText", out Text text))
        {
            text.text = number.ToString();
            for (int i = 1; i < 10; i++)
            {
                if (TryGetTextByName($"Number_{i}", out Text textNumber))
                {
                    textNumber.text = "";
                }
            }
        }
    }

    public void SetSmallNumber(int number)
    {
        for (int i = 1; i < 10; i++)
        {
            if (TryGetTextByName($"Number_{number}", out Text text))
            {
                text.text = number.ToString();
                if (TryGetTextByName("FieldText", out Text textNumber))
                {
                    textNumber.text = "";
                }
            }
        }
    }
}
