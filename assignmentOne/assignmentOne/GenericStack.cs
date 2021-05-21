using System;
using System.Text;

namespace assignmentOne
{
    class GenericStack<T> // T represents generic data type
    {
        int top = 0;
        int size = 0;
        T[] stack;
        public GenericStack(int maxSize) // Constructor
        {
            size = maxSize;
            stack = new T[size];

        }
        public int push(T data) // push element into stack
        {

            stack[top] = data;
            top = top + 1;
            return 0;

        }

        public T pop() // pop last element from array
        {
            T RemovedItem;
            T temp = default(T);
            if (!(top <= 0)) // if top is not 0 or less than 0
            {
                // take item from top
                RemovedItem = stack[top - 1];
                stack[top - 1] = default(T);

                // change top
                top--;

                // return item
                return RemovedItem;
            }
            return temp;
        }

        public T peek() // check last item without removing it 
        {
            return stack[top - 1]; // DOUBLE CHECK THIS 
        }

        public bool isFull() // check if stack is full 
        {
            if (top == size) // stack is full
            {
                return true;
            }
            else // stack is not full 
            {
                return false;
            }
        }

        public bool isEmpty() // check if stack is empty 
        {
            if (top == 0) // stack is empty 
            {
                return true;
            }
            else // there is something in the stack
            {
                return false;
            }
        }

        public int getLength()
        {
            return size;
        }

        public int getTop()
        {
            return top;
        }


    }


}
