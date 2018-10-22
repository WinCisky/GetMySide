using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private void AddNumbers(List<List<Vector2>> frames, Vector2[] numbers)
    {
        List<Vector2> tmp = new List<Vector2>();
        for (int i = 0; i < numbers.Length; i++)
        {
            tmp.Add(numbers[i]);
        }
        frames.Add(tmp);
    }

    private List<List<Vector2>> AddSector(int index)
    {
        List<List<Vector2>> result = new List<List<Vector2>>();
        switch (index)
        {
            case 0:
                AddNumbers(result, new[] { new Vector2(0, 0), new Vector2(20, 20) });
                AddNumbers(result, new[] { new Vector2(1, 1), new Vector2(19, 19) });
                AddNumbers(result, new[] { new Vector2(2, 2), new Vector2(18, 18) });
                AddNumbers(result, new[] { new Vector2(3, 3), new Vector2(17, 17) });
                AddNumbers(result, new[] { new Vector2(4, 4), new Vector2(16, 16) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(6, 6), new Vector2(14, 14) });
                AddNumbers(result, new[] { new Vector2(7, 7), new Vector2(13, 13) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                break;
            case 1:
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(7, 7), new Vector2(13, 13) });
                AddNumbers(result, new[] { new Vector2(6, 6), new Vector2(14, 14),
            new Vector2(10, 10)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9)});
                AddNumbers(result, new[] { new Vector2(4, 4), new Vector2(16, 16),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8)});
                AddNumbers(result, new[] { new Vector2(3, 3), new Vector2(17, 17),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8), new Vector2(13, 13), new Vector2(7, 7) });
                //two lateral
                //not full screen distance
                AddNumbers(result, new[] { new Vector2(3, 3), new Vector2(17, 17),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8), new Vector2(13, 13), new Vector2(7, 7) });
                AddNumbers(result, new[] { new Vector2(4, 4), new Vector2(16, 16),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9), new Vector2(12, 12), new Vector2(8, 8)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
            new Vector2(10, 10), new Vector2(11, 11), new Vector2(9, 9)});
                AddNumbers(result, new[] { new Vector2(6, 6), new Vector2(14, 14),
            new Vector2(10, 10)});
                AddNumbers(result, new[] { new Vector2(7, 7), new Vector2(13, 13) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                break;
            case 2:
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(10, 10), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(10, 10), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(9, 9) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(9, 10), new Vector2(10, 9), new Vector2(11, 11) });
                break;
            default:
                break;
        }
        return result;
    }

    public List<List<List<Vector2>>> CreateLevel(List<List<List<Vector2>>> list)
    {
        list = new List<List<List<Vector2>>>();
        //list.Add(AddSector(0));
        //list.Add(AddSector(1));
        list.Add(AddSector(2));

        return list;
    }
}
