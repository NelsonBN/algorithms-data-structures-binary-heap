using static System.Console;

var totalItems = Random.Shared.Next(2, 100);

var items = Enumerable.Range(0, totalItems)
    .Select(_ => Random.Shared.Next(100))
    .ToArray();

Heap.Sort(ref items);

for(var i = 0; i < items.Length; i++)
{
    WriteLine(items[i]);
}

public static class Heap
{
    public static void Sort(ref int[] array)
    {
        _heapify(array);

        var sortedArray = new int[array.Length];
        for(var i = 0; i < array.Length; i++)
        {
            sortedArray[i] = _pop(array, array.Length - i);
        }

        array = sortedArray;
    }


    private static int _leftChild(int index)
        => (index * 2) + 1;

    private static int _rightChild(int index)
        => (index * 2) + 2;

    private static bool _isBigger(int[] array, int firstIndex, int secondIndex)
        => array[firstIndex] > array[secondIndex];

    private static void _swap(int[] array, int firstIndex, int secondIndex)
        => (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);

    private static void _heapify(int[] array)
    {
        // Half of the array minus one
        var half = array.Length / 2 - 1;

        // Each all parents with children
        for(var i = half; i >= 0; i--)
        {
            _moveDown(array, array.Length, i);
        }
    }


    private static void _moveDown(int[] array, int size, int startIndex)
    {
        var current = startIndex;
        var leftChild = _leftChild(current);
        var rightChild = _rightChild(current);

        while(leftChild < size)
        {
            var biggestChild = rightChild < size && _isBigger(array, rightChild, leftChild) ?
                rightChild :
                leftChild;

            if(_isBigger(array, current, biggestChild))
            {
                break;
            }

            _swap(array, current, biggestChild);
            current = biggestChild;
            leftChild = _leftChild(current);
            rightChild = _rightChild(current);
        }
    }


    private static int _pop(int[] array, int size)
    {
        var result = array[0];

        size--;
        array[0] = array[size];

        _moveDown(array, size, 0);

        return result;
    }
}
