using UnityEngine;
using UnityEngine.UI;

public class FieldPrefabObject
{
    private int _row;
    private int _column;
    private GameObject _prefab;
    public FieldPrefabObject(GameObject instance, int row, int column)
    {
        _prefab = instance;
        Row = row;
        Column = column;
    }

    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }

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
        _prefab.GetComponentInChildren<Text>().text = number.ToString();
    }
}
