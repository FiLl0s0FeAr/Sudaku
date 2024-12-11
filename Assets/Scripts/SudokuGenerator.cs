using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.DebugUI.Table;

public class SudokuGenerator
{
    private static SudokuObject _finalSudokuObject;
    public static SudokuObject CreateSudokuObject()
    {
        _finalSudokuObject = null;
        SudokuObject sudokuObject = new SudokuObject();
        CreateRandomGroups(sudokuObject);
        if(TryToSolve(sudokuObject))
        {
            sudokuObject = _finalSudokuObject;
        }
        else
        {
            throw new System.Exception("Something went wrong");
        }
        return RemoveSomeRandomNumbers(sudokuObject);
    }

    private static SudokuObject RemoveSomeRandomNumbers(SudokuObject sudokuObject)
    {
        SudokuObject newSudokuObject = new SudokuObject();
        newSudokuObject.Values = (int[,]) sudokuObject.Values.Clone();
        List<int> values = GetValues();

        bool isFinish = false;

        while(!isFinish)
        {
            int index = Random.Range(0, values.Count);
            int searchedIndex = values[index];

            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (i * j == searchedIndex)
                    {
                        values.RemoveAt(index);
                        SudokuObject nextSudokuObject = new SudokuObject();
                        nextSudokuObject.Values = (int[,]) newSudokuObject.Values.Clone();
                        nextSudokuObject.Values[i - 1, j - 1] = 0;
                        if (TryToSolve(nextSudokuObject, true))
                        {
                            newSudokuObject = nextSudokuObject;
                        }
                    }
                }
            }

            if (values.Count < 30)
            {
                isFinish = true;
            }
        }

        return newSudokuObject;
    }

    private static List<int> GetValues()
    {
        List<int> values = new List<int>();

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                values.Add(i * j);
            }
        }

        return values;
    }

    private static bool TryToSolve(SudokuObject sudokuObject, bool onlyOne = false)
    {
        //Find empty fields which can be filled
        if (HasEmptyFeldsToFill(sudokuObject, out int row, out int column, onlyOne))
        {
            List<int> possibleValues = GetPossibleValues(sudokuObject, row, column);

            foreach (var possibleValue in possibleValues)
            {
                SudokuObject nextSudokuObject = new SudokuObject();
                nextSudokuObject.Values = (int[,]) sudokuObject.Values.Clone();
                nextSudokuObject.Values[row, column] = possibleValue;
                if (TryToSolve(nextSudokuObject, onlyOne))
                {
                    return true;
                }
            }
        }

        //Has sudokuObject empty fields
        if (HasEmptyFields(sudokuObject))
        {
            return false;
        }

        _finalSudokuObject = sudokuObject;

        return true;

        // finish
    }

    private static bool HasEmptyFields(SudokuObject sudokuObject)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudokuObject.Values[i, j] == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static List<int> GetPossibleValues(SudokuObject sudokuObject, int row, int column)
    {
        List<int > possibleValues = new List<int>();
        for (int value = 1; value < 10; value++)
        {
            if (sudokuObject.IsPossibleNumberInPosition(value, row, column))
            {
                possibleValues.Add(value);
            }    
        }

        return possibleValues;
    }

    private static bool HasEmptyFeldsToFill(SudokuObject sudokuObject, out int row, out int column, bool onlyOne = false)
    {
        row = 0;
        column = 0;
        int amountOfPossibleValies = 10;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudokuObject.Values[i, j] == 0)
                {
                    int currentAmount = GetPossibleAmountOfValues(sudokuObject, i, j);

                    if (currentAmount != 0)
                    {
                        if (currentAmount < amountOfPossibleValies)
                        {
                            amountOfPossibleValies = currentAmount;
                            row = i;
                            column = j;
                        }
                    }
                }
            }
        }

        if (onlyOne)
        {
            if (amountOfPossibleValies == 1)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }

        if (amountOfPossibleValies == 10)
        {
            return false;
        }

        return true;
    }

    private static int GetPossibleAmountOfValues(SudokuObject sudokuObject, int row, int column)
    {
        int amount = 0;

        for (int value = 1; value < 10; value++)
        {
            if (sudokuObject.IsPossibleNumberInPosition(value, row, column))
            {
                amount++;
            }
        }

        return amount;
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
