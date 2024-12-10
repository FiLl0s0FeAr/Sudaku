using System.Collections.Generic;
using UnityEngine;

public class SudokuGenerator
{
    public static SudokuObject CreateSudokuObject()
    {
        SudokuObject sudokuObject = new SudokuObject();
        CreateRandomGroups(sudokuObject);
        return sudokuObject;
    }

    public static void CreateRandomGroups(SudokuObject SudokuObject)
    {
        List<int> values = new List<int>() { 0, 1, 2 };

        int index = Random.Range(0, values.Count);
        InsertRandomGroup(SudokuObject, 1 + values[index]);
        values.RemoveAt(index);

        index = Random.Range(0, values.Count);
        InsertRandomGroup(SudokuObject, 4 + values[index]);
        values.RemoveAt(index);

        index = Random.Range(0, values.Count);
        InsertRandomGroup(SudokuObject, 7 + values[index]);
    }

    public static void InsertRandomGroup(SudokuObject SudokuObject, int group)
    {
        SudokuObject.GetGroupIndex(group, out int startRow, out int startColumn);
        List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int row = startRow; row < startRow + 3; row++)
        {
            for (int column = startColumn; column < startColumn + 3; column++)
            {
                int index = Random.Range(0, values.Count);
                SudokuObject.Values[row, column] = values[index];
                values.RemoveAt(index);
            }
        }
    }
}
