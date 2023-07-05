using static System.Console;

var minHeap = new MinHeap(10);

minHeap.Push(20);
minHeap.Push(30);
minHeap.Push(300);
minHeap.Push(1);
minHeap.Push(40);
minHeap.Push(5);
minHeap.Push(7);
minHeap.Push(9);
minHeap.Push(9);
minHeap.Push(19);

WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());
WriteLine(minHeap.Pop());


public class MinHeap
{
    private readonly int[] _heap;
    private readonly int _maxSize;
    private int _size;

    public MinHeap(int maxSize)
    {
        _maxSize = maxSize;
        _size = 0;
        _heap = new int[_maxSize];
    }

    private static int _parent(int index)
        => (index - 1) / 2;

    private static int _leftChild(int index)
        => (index * 2) + 1;

    private static int _rightChild(int index)
        => (index * 2) + 2;

    private bool _isSmaller(int firstIndex, int secondIndex)
        => _heap[firstIndex] < _heap[secondIndex];

    private void _swap(int firstIndex, int secondIndex)
        => (_heap[firstIndex], _heap[secondIndex]) = (_heap[secondIndex], _heap[firstIndex]);

    public void Push(int element)
    {
        if(_size >= _maxSize)
        {
            throw new InvalidOperationException("Heap is full");
        }

        _heap[_size] = element;

        var current = _size;
        var parent = _parent(current);

        while(_isSmaller(current, parent))
        {
            _swap(current, parent);
            current = parent;
            parent = _parent(current);
        }
        _size++;
    }

    public int Peek()
        => _size > 0 ? _heap[0] : throw new InvalidOperationException("Heap is empty");

    public int Pop()
    {
        if(_size <= 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        _size--;
        var result = _heap[0];
        _heap[0] = _heap[_size];


        var current = 0;
        var leftChild = _leftChild(current);
        var rightChild = _rightChild(current);

        while(leftChild < _size)
        {
            var smallestChild = rightChild < _size && _isSmaller(rightChild, leftChild) ? rightChild : leftChild;
            if(_isSmaller(current, smallestChild))
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
