using static System.Console;

var numberOfChildren = Random.Shared.Next(2, 7);
var totalItems = Random.Shared.Next(2, 100);

WriteLine($"K-ary Heap ({numberOfChildren}) - {totalItems} items");

var heap = new KAryHeap(
    numberOfChildren,
    totalItems);

for(var i = 0; i < totalItems; i++)
{
    heap.Push(Random.Shared.Next(100));
}
while(heap.Count > 0)
{
    WriteLine(heap.Pop());
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
            // Find smallest child, will iterate over the children
            for(var i = 2; i <= _arity; i++)
            {
                var currentChild = _child(current, i);

                smallestChild = currentChild < Count && _isSmaller(currentChild, smallestChild) ?
                    currentChild :
                    smallestChild;
            }

            // When does the current node is smaller tham the smallest child, that means we have a valid heap
            if(_isSmaller(current, smallestChild))
            {
                break;
            }

            // If the code hits here, that means we found a smaller child smaller than the current node, so we swap them and prepare for the next iteration (level down)
            //     The path we should take is the node of the smallest child was swapped with the current node
            _swap(current, smallestChild);
            current = smallestChild;
            smallestChild = _child(current, 1);
        }

        return result;
    }
}
