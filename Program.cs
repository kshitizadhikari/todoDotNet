using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //get todo from user
            //add todo 
            //give options to add new or edit or just view
            bool isRunning = true;
            string todo;
            List<string> todoList = new List<string>();

            while (isRunning)
            {
                Console.WriteLine("\n1 = Add Todo\n2 = Edit Todo\n3 = View Todo\n4 = Exit\n");
                Console.Write("Enter your choice: ");
                char cho = Console.ReadKey().KeyChar;
               /* int choice = int.Parse(cho);*/

                switch(cho)
                {
                    case '1':
                        todo = getTodo();
                        storeTodo(todoList, todo);
                        break;

                    case '2':
                        Console.Clear();
                        viewTodo(todoList);
                        editTodo(todoList);

                        break;

                    case '3':
                        Console.Clear();
                        viewTodo(todoList);
                        break;

                    case '4':
                        isRunning = exit();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Input. Try again.");
                        break;

                }

            }

            string getTodo()
            {
                string data;
                Console.WriteLine("\nEnter a Todo: ");
                data = Console.ReadLine();
                return data;
                
            }

            void editTodo(List<string> list)
            {
                Console.Write("\nEnter the number of todo that you want to edit: ");
                char numChar = Console.ReadKey().KeyChar;
                int num = int.Parse(numChar.ToString());

                Console.WriteLine("\nEnter new todo: ");
                string newTodo = Console.ReadLine();
                list[num-1] = newTodo;

            }

            void storeTodo(List<string> list, string data)
            {
                list.Add(data);
            }

            void viewTodo(List<string> list)
            {
                Console.WriteLine("\nYour Todo list: \n");
                int i = 1;
                foreach(string item in list)
                {
                    Console.WriteLine(i + ": " + item);
                    i++;
                }
            }

            bool exit()
            {
                exitOrNot:
                Console.Write("\nDo you want to exit? (y/n): ");
                char val = Console.ReadKey().KeyChar;
                if (val=='y')
                {
                    return false;

                } else if(val=='n') {
                    return true;
                }else {
                    Console.WriteLine("\nInvalid Input. Try Again.");
                    goto exitOrNot;
                }
            }


            Console.ReadLine();
        }

        
    }
}
