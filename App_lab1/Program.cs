using System;
using Collection_lab1;
using System.Collections.Specialized;
using System.Linq;

namespace lab1_Byshovets
{
    class Program
    {
        static NodeStack<string> stack;
        static string message;
        static void Main(string[] args)
        {
            stack = new NodeStack<string>();

            stack.CollectionChanged += Stack_CollectionChanged;

            int key = 1, answer;
            string value = "", peek, del;
            while (key != 0)
            {
                Console.Clear();
                Console.Write(
                    "           Menu:\n" +
                    "1: Add value to stack.\n" +
                    "2: Show all values from stack.\n" +
                    "3: Show top value of stack.\n" +
                    "4: Delete value from stack.\n" +
                    "Enter key: ");

                key = Convert.ToInt32(Console.ReadLine());

                switch (key)
                {
                    //добавляем элементы в стек
                    case 1:
                        Console.Write("\nHow many values do you want to insert?: ");
                        answer = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < answer; i++)
                        {
                            Console.Write("Add value: ");
                            value = Console.ReadLine();
                            stack.Push(value);

                            if (message != null)
                            {
                                Console.WriteLine($"\n{message}\n");
                            }
                        }
                        break;
                    //выводим вместимое стека
                    case 2:
                        try
                        {
                            stack.ReturnEmpty();
                            Console.WriteLine("\nStack values:\n");
                            foreach (var elem in stack)
                            {
                                Console.WriteLine(elem);
                            }
                            Console.WriteLine();
                        }
                        catch (InvalidOperationException err)
                        {
                            Console.WriteLine(err.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            stack.ReturnEmpty();
                            peek = stack.Peek();
                            Console.WriteLine($"\nTop value in stack: {peek}\n");
                        }
                        catch (InvalidOperationException err)
                        {
                            Console.WriteLine(err.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            stack.ReturnEmpty();
                            del = stack.Pop();
                            Console.WriteLine($"\nDeleted value is: {del}");

                            if (message != null)
                            {
                                Console.WriteLine($"\n{message}\n");
                            }
                        }
                        catch (InvalidOperationException err)
                        {
                            Console.WriteLine(err.Message);
                        }
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("\nWrong input! Try again.\n");
                        break;
                }
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void Stack_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            message = "What has changed in the stack? :)\n\n";

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    message += $"Action 'Push' - add items to stack:\n" +
                        $"Your new item is '{e.NewItems[0]}'\n";
                    break;
                case NotifyCollectionChangedAction.Remove:
                    message += $"Action 'Pop' - delete item from stack:\n" +
                        $"Your deleted item is '{e.OldItems[0]}'\n";
                    break;
            }
            message += $"Amount of items in stack: {((NodeStack<string>)sender).Count}";
        }
    }
}
