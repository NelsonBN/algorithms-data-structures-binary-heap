using static System.Console;

var maxHeap = new MaxHeap();


for(var i = 0; i < 10; i++)
{
    maxHeap.Push(Random.Shared.Next(0, 100));
}

while(maxHeap.Count > 0)
{
    WriteLine(maxHeap.Pop());
}



public class MaxHeap
{
    public int Count { get; private set; }

    private int[] _heap;
    private int _size;

    public MaxHeap(int size = 4)
    {
        Count = 0;
        _size = size;
        _heap = new int[_size];
    }

    private static int _parent(int index)
        => (index - 1) / 2;

    private static int _leftChild(int index)
        => (index << 1) + 1;

    private static int _rightChild(int index)
        => (index << 1) + 2;

    private bool _isBigger(int firstIndex, int secondIndex)
        => _heap[firstIndex] > _heap[secondIndex];

    private void _swap(int firstIndex, int secondIndex)
        => (_heap[firstIndex], _heap[secondIndex]) = (_heap[secondIndex], _heap[firstIndex]);

    private void _resize()
    {
        var newHeap = new int[_size * 2];
        Array.Copy(_heap, newHeap, _size);
        _heap = newHeap;
        _size *= 2;
    }

    public void Push(int element)
    {
        if(Count >= _size)
        {
            _resize();
        }

        _heap[Count] = element;

        var current = Count;
        var parent = _parent(current);

        while(_isBigger(current, parent))
        {
            _swap(current, parent);
            current = parent;
            parent = _parent(current);
        }
        Count++;
    }


    public int Pop()
    {
        if(Count <= 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        Count--;
        var result = _heap[0];
        _heap[0] = _heap[Count];


        var current = 0;
        var leftChild = _leftChild(current);
        var rightChild = _rightChild(current);

        while(leftChild < Count)
        {
            var smallestChild = rightChild < Count && _isBigger(rightChild, leftChild) ?
                rightChild :
                leftChild;

            if(_isBigger(current, smallestChild))
            {
                break;
            }

            _swap(current, smallestChild);
            current = smallestChild;
            leftChild = _leftChild(current);
            rightChild = _rightChild(current);
        }

        return result;
    }
}
