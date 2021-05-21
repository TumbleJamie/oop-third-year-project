using System;

namespace assignmentOne
{
    class Program
    {

            static void Main(string[] args)
        {
            // START OF STRING REVERSAL TASK
            int size;
            Console.WriteLine("Please enter an integer to set size between 1 - 13: ");

            string input = Console.ReadLine();
            while (!Int32.TryParse(input, out size) || size < 1 || size > 13) // checks input is both an integer and in the correct range 
            {
                Console.WriteLine("Please enter a valid number: ");
                input = Console.ReadLine();
            }

            GenericStack<string> newStack = new GenericStack<string>(size); // Create stack using string as data type

            if (newStack.isEmpty())
            {
                // show stack is empty 
                Console.WriteLine("Stack is empty \n");
            }

            for (int i = 0; i < newStack.getLength(); i++) // place letters into correct index positions of the stack
            {
                Console.WriteLine("Enter a letter: ");

                string temp = Console.ReadLine();
                Console.WriteLine("\n");
                newStack.push(temp);
            }

            if (newStack.isFull())
            {
                // show stack is full 
                Console.WriteLine("Stack is full \n");
            }

            Console.WriteLine("Now showing the word in reversal: \n");

            for (int i = 0; i < newStack.getLength(); i++) // pop stack to show LIFO data structure 
            {
                Console.WriteLine(newStack.pop());
            }

            if (newStack.isEmpty())
            {
                // show stack is once again empty 
                Console.WriteLine("Stack is empty \n");
            }

            // START OF EXPRESSION REVERSAL

            int sizeTwo;
            Console.WriteLine("Please enter an integer to set size between 1 - 20: ");

            string inputTwo = Console.ReadLine();
            while (!Int32.TryParse(inputTwo, out sizeTwo) || sizeTwo < 1 || sizeTwo > 20) // checks input is both an integer and in the correct range 
            {
                Console.WriteLine("Please enter a valid number: ");
                inputTwo = Console.ReadLine();
            }

            GenericStack<string> newStackTwo = new GenericStack<string>(sizeTwo); // Create stack using string as data type to store operators 

            // ASK USER FOR EXPRESSION

            Console.WriteLine("Please enter your expression using operands and operators: ");

            string expression = Console.ReadLine();

            // ASK IF THEY WANT PRE OR POST FIX EVALUATION

            int answer;
            Console.WriteLine("Please enter 1 for prefix or 2 for postfix: ");
            string answerTwo = Console.ReadLine();

            while (!Int32.TryParse(answerTwo, out answer) || answer < 1 || answer > 2) // checks input is both an integer and in the correct range 
            {
                Console.WriteLine("Please enter either 1 or 2: ");
                answerTwo = Console.ReadLine();
            }

            switch (answer)
            {

                // PREFIX EVALUATION
                case 1:
                    {
                        string reversedInfix = ReverseString(expression);

                        // Reversal of string and replacing the brackets to match

                        string temp = "";

                        for (int i = 0; i < reversedInfix.Length; i++)
                        {
                            if (reversedInfix[i] == '(')
                            {
                                temp += ')';
                            }
                            else if (reversedInfix[i] == ')')
                            {
                                temp += '(';
                            }
                            else
                            {
                                temp += reversedInfix[i];
                            }
                        }
                        reversedInfix = temp;

                        // Start of prefix evaluation

                        string postfix = PostFix(reversedInfix, newStackTwo);

                        string prefix = ReverseString(postfix);

                        Console.WriteLine(prefix); // Print
                        break;
                    }
                // POSTFIX EVALUATION
                case 2:
                    {
                        string postfix = PostFix(expression, newStackTwo);

                        Console.WriteLine(postfix); // Print

                        break;
                    }
            }

            Console.ReadLine();
        }

        public static int GetPrecedence(char operatorSymbol)
        {
            if (operatorSymbol == '^')
                return 3;
            else if (operatorSymbol == '*' || operatorSymbol == '/')
                return 2;
            else if (operatorSymbol == '+' || operatorSymbol == '-')
                return 1;
            else
                return -1;
        }

        public static bool IsOperand(char c)
        {
            return (c != '+' && c != '-' && c != '/' && c != '*' && c != '^' && c != '(' && c != ')');
        }

        public static string ReverseString(string preFix)
        {
            string reverse = "";
            int length;

            length = preFix.Length - 1;

            while (length >= 0)
            {
                reverse = reverse + preFix[length];
                length--;
            }
            return reverse;
        }

        public static string PostFix(string expression, GenericStack<string> stack)
        {
            string postFix = "";
            for (int i = 0; i < expression.Length; i++)
            {
                if (IsOperand(expression[i])) // if i is operand then add to postFix string
                {
                    postFix += expression[i];
                }
                else if (expression[i] == '(')
                {
                    stack.push(expression[i].ToString());
                }
                else if (expression[i] == ')')
                {
                    while (!stack.isEmpty() && stack.peek() != '('.ToString())
                    {
                        postFix += stack.pop();
                    }

                    if (stack.peek() == '('.ToString())
                        stack.pop();
                }
                else if (!IsOperand(expression[i])) // Is operator
                {
                    if (stack.isEmpty())
                    {
                        stack.push(expression[i].ToString());
                    }
                    else
                    {
                        if (GetPrecedence(expression[i]) > GetPrecedence(stack.peek()[0])) // HOW TO EVALUATE ORDER PRECEDENCE 
                        {
                            stack.push(expression[i].ToString());
                        }
                        else if ((GetPrecedence(expression[i]) == GetPrecedence(stack.peek()[0]) && (expression[i] == '^')))
                        {
                            stack.push(expression[i].ToString());
                        }
                        else
                        {
                            while (!stack.isEmpty() && (GetPrecedence(expression[i]) <= GetPrecedence(stack.peek()[0])))
                            {
                                postFix += stack.pop();
                            }
                            stack.push(expression[i].ToString());
                        }
                    }
                }
            } // End for loop

            while (!stack.isEmpty())
            {
                postFix += stack.pop();
            }

            return postFix;
        }
    }
}
