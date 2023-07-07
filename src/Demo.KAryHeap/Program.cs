using static System.Console;

var arity = 4;
var size = Random.Shared.Next(100);

var list = new int[size];
var heap = new KAryHeap(arity, size);

for(var i = 0; i < size; i++)
{
    var value = Random.Shared.Next(100);

    list[i] = value;
    heap.Push(value);
}

Array.Sort(list);

var count = 0;
while(heap.Count > 0)
{
    var value = heap.Pop();

    if(value != list[count++])
    {
        throw new Exception("Invalid value");
    }

    WriteLine(value);
}



public class KAryHeap
{
    public int Count { get; private set; }

    private int[] _heap;
    private int _size;

    private readonly int _arity;

    public KAryHeap(int arity = 4, int size = 4)
    {
        Count = 0;
        _size = size;
        _arity = arity;
        _heap = new int[_size];
    }

    private int _parent(int index)
        => (index - 1) / _arity;

    private int _child(int index, int childNumber)
        => (index * _arity) + childNumber;

    private bool _isSmaller(int firstIndex, int secondIndex)
        => _heap[firstIndex] < _heap[secondIndex];

    private void _swap(int firstIndex, int secondIndex)
        => (_heap[firstIndex], _heap[secondIndex]) = (_heap[secondIndex], _heap[firstIndex]);

    private void _resize()
    {
        if(Count >= _size)
        {
            var newHeap = new int[_size * 2];
            Array.Copy(_heap, newHeap, _size);
            _heap = newHeap;
            _size *= 2;
        }
    }

    public void Push(int element)
    {
        _resize();

        _heap[Count] = element;

        var current = Count;
        var parent = _parent(current);

        while(_isSmaller(current, parent))
        {
            _swap(current, parent);
            current = parent;
            parent = _parent(current);
        }
        Count++;
    }

    public int Peek()
        => Count > 0 ? _heap[0] : throw new InvalidOperationException("Heap is empty");

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
        var smallestChild = _child(current, 1);
        while(smallestChild < Count)
        {
            for(var i = 2; i <= _arity; i++)
            {
                var currentChild = _child(current, i);

                smallestChild = currentChild < Count && _isSmaller(currentChild, smallestChild) ?
                    currentChild :
                    smallestChild;
            }

            if(_isSmaller(current, smallestChild))
            {
                break;
            }

            _swap(current, smallestChild);
            current = smallestChild;
            smallestChild = _child(current, 1);
        }

        return result;
    }
}
