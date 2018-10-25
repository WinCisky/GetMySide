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
                //imbuto che porta al centro <basso-sx,alto-dx>
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
                //imbuto che porta al centro <alto-sx,basso-dx>
                AddNumbers(result, new[] { new Vector2(20, 0), new Vector2(0, 20) });
                AddNumbers(result, new[] { new Vector2(19, 1), new Vector2(1, 19) });
                AddNumbers(result, new[] { new Vector2(18, 2), new Vector2(2, 18) });
                AddNumbers(result, new[] { new Vector2(17, 3), new Vector2(3, 17) });
                AddNumbers(result, new[] { new Vector2(16, 4), new Vector2(4, 16) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(14, 6), new Vector2(6, 14) });
                AddNumbers(result, new[] { new Vector2(13, 7), new Vector2(7, 13) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                break;
            case 2:
                //due strade <>
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
            case 3:
                //due strade <>
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(13, 7), new Vector2(7, 13) });
                AddNumbers(result, new[] { new Vector2(14, 6), new Vector2(6, 14),
            new Vector2(10, 10)});
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9)});
                AddNumbers(result, new[] { new Vector2(16, 4), new Vector2(4, 16),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9), new Vector2(8, 12), new Vector2(12, 8)});
                AddNumbers(result, new[] { new Vector2(17, 3), new Vector2(3, 17),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9), new Vector2(8, 12), new Vector2(12, 8), new Vector2(7, 13), new Vector2(13, 7) });
                AddNumbers(result, new[] { new Vector2(17, 3), new Vector2(3, 17),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9), new Vector2(8, 12), new Vector2(12, 8), new Vector2(7, 13), new Vector2(13, 7) });
                AddNumbers(result, new[] { new Vector2(16, 4), new Vector2(4, 16),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9), new Vector2(8, 12), new Vector2(12, 8)});
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15),
            new Vector2(10, 10), new Vector2(9, 11), new Vector2(11, 9)});
                AddNumbers(result, new[] { new Vector2(14, 6), new Vector2(6, 14),
            new Vector2(10, 10)});
                AddNumbers(result, new[] { new Vector2(13, 7), new Vector2(7, 13) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                break;
            case 4:
                //zig zag e cambio dim.
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(10, 10), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(12, 12) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(10, 10) });
                AddNumbers(result, new[] { new Vector2(8, 8), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(11, 11) });
                AddNumbers(result, new[] { new Vector2(9, 9), new Vector2(10, 9), new Vector2(10, 11), new Vector2(11, 11) });
                break;
            case 5:
                //zig zag e cambio dim.
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(10, 10), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(8, 12) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(10, 10) });
                AddNumbers(result, new[] { new Vector2(12, 8), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 11) });
                AddNumbers(result, new[] { new Vector2(11, 9), new Vector2(9, 10), new Vector2(11, 10), new Vector2(9, 11) });
                break;
            case 6:
                //bordi ed oggetti sparsi
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
                new Vector2(6,6), new Vector2(6,7), new Vector2(7,6), new Vector2(7,7),
                new Vector2(9,9), new Vector2(9,10), new Vector2(10,9), new Vector2(10,10),
                new Vector2(12,12), new Vector2(13,12), new Vector2(12,13), new Vector2(13,13)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
                new Vector2(6,6), new Vector2(6,7), new Vector2(7,6), new Vector2(7,7),
                new Vector2(9,9), new Vector2(9,10), new Vector2(10,9), new Vector2(10,10),
                new Vector2(12,12), new Vector2(13,12), new Vector2(12,13), new Vector2(13,13)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
                new Vector2(7,7), new Vector2(8,7), new Vector2(7,8), new Vector2(8,8),
                new Vector2(10,10), new Vector2(11,10), new Vector2(10,11), new Vector2(11,11),
                new Vector2(13,13), new Vector2(14,13), new Vector2(13,14), new Vector2(14,14)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
                new Vector2(7,7), new Vector2(8,7), new Vector2(7,8), new Vector2(8,8),
                new Vector2(10,10), new Vector2(11,10), new Vector2(10,11), new Vector2(11,11),
                new Vector2(13,13), new Vector2(14,13), new Vector2(13,14), new Vector2(14,14)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15) });
                break;
            case 7:
                //bordi ed oggetti sparsi
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15),
                new Vector2(14,6), new Vector2(14,7), new Vector2(13,6), new Vector2(13,7),
                new Vector2(11,9), new Vector2(11,10), new Vector2(10,9), new Vector2(10,10),
                new Vector2(8,12), new Vector2(7,12), new Vector2(8,13), new Vector2(7,13)});
                AddNumbers(result, new[] { new Vector2(5, 5), new Vector2(15, 15),
                new Vector2(14,6), new Vector2(14,7), new Vector2(13,6), new Vector2(13,7),
                new Vector2(11,9), new Vector2(11,10), new Vector2(10,9), new Vector2(10,10),
                new Vector2(8,12), new Vector2(7,12), new Vector2(8,13), new Vector2(7,13)});
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15),
                new Vector2(13,7), new Vector2(12,7), new Vector2(13,8), new Vector2(12,8),
                new Vector2(10,10), new Vector2(9,10), new Vector2(10,11), new Vector2(9,11),
                new Vector2(7,13), new Vector2(6,13), new Vector2(7,14), new Vector2(6,14)});
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15),
                new Vector2(13,7), new Vector2(12,7), new Vector2(13,8), new Vector2(12,8),
                new Vector2(10,10), new Vector2(9,10), new Vector2(10,11), new Vector2(9,11),
                new Vector2(7,13), new Vector2(6,13), new Vector2(7,14), new Vector2(6,14)});
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                AddNumbers(result, new[] { new Vector2(15, 5), new Vector2(5, 15) });
                break;
            default:
                break;
        }
        return result;
    }

    public List<List<List<Vector2>>> CreateLevel(List<List<List<Vector2>>> list)
    {
        list = new List<List<List<Vector2>>>();
        list.Add(AddSector(0));
        list.Add(AddSector(1));
        list.Add(AddSector(2));
        list.Add(AddSector(3));
        list.Add(AddSector(4));
        list.Add(AddSector(5));
        list.Add(AddSector(6));

        return list;
    }
}
