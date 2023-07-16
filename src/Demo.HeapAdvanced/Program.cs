using Bogus;
using static System.Console;

var heap = new Heap<Person>();

var faker = new Faker<Person>();
for(var i = 0; i < 100; i++)
{
    heap.Push(faker
        .RuleFor(p => p.FirstName, (s, p) => s.Name.FirstName())
        .RuleFor(p => p.LastName, (s, p) => s.Name.LastName())
        .RuleFor(p => p.Age, (s, p) => s.Random.Int(18, 65))
        .Generate());
}

while(heap.Count > 0)
{
    WriteLine(heap.Pop());
}

WriteLine("END...");



internal class Heap<T> where T : IComparable<T>
{
    public int Count { get; private set; }

    private T[] _heap;
    private int _size;

    public Heap()
    {
        Count = 0;
        _size = 4;
        _heap = new T[_size];
    }

    private static int _parent(int index)
        => (index - 1) / 2;

    private static int _leftChild(int index)
        => (index << 1) + 1;

    private static int _rightChild(int index)
        => (index << 1) + 2;

    private bool _compare(int firstindex, int secondindex)
        => _heap[firstindex].CompareTo(_heap[secondindex]) < 0;

    private void _swap(int firstIndex, int secondIndex)
        => (_heap[firstIndex], _heap[secondIndex]) = (_heap[secondIndex], _heap[firstIndex]);

    private void _resize()
    {
        var newHeap = new T[_size * 2];
        Array.Copy(_heap, newHeap, _size);
        _heap = newHeap;
        _size *= 2;
    }

    public void Push(T item)
    {
        if(Count >= _size)
        {
            _resize();
        }

        _heap[Count] = item;

        var current = Count;
        var parent = _parent(current);

        while(_compare(current, parent))
        {
            _swap(current, parent);
            current = parent;
            parent = _parent(current);
        }
        Count++;
    }


    public T Pop()
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
            var smallestChild = rightChild < Count && _compare(rightChild, leftChild) ?
                rightChild :
                leftChild;

            if(_compare(current, smallestChild))
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

class Person : IComparable<Person>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person? other)
    {
        if(other is null)
        {
            return -1;
        }

        var result = Age.CompareTo(other.Age);
        if(result != 0)
        {
            return result;
        }

        result = compareTo(FirstName, other?.FirstName);
        if(result != 0)
        {
            return result;
        }

        return compareTo(LastName, other?.LastName);



        static int compareTo(string? left, string? right)
        {
            if(left is null && right is null)
            {
                return 0;
            }

            if(left is null)
            {
                return -1;
            }

            if(right is null)
            {
                return 1;
            }

            return left.CompareTo(right);
        }
    }

    public override string ToString()
        => $"{FirstName} {LastName} (Age: {Age})";
}
