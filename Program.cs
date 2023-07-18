using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TodoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Server=DESKTOP-72AIJKL;Initial Catalog=TodoConsole;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connection established");

                bool isRunning = true;
                string todo;
                List<string> todoList = new List<string>();

                while (isRunning)
                {
                    Console.WriteLine("\n----------MENU---------\n1 = Add Todo\n2 = Edit Todo\n3 = View Todo\n4 = Exit\n");
                    Console.Write("Enter your choice: ");
                    char cho = Console.ReadKey().KeyChar;
                    /* int choice = int.Parse(cho);*/

                    switch (cho)
                    {
                        case '1':
                            todo = getTodo(connection);
                            storeTodo(todoList, todo);
                            break;

                        case '2':
                            Console.Clear();
                            viewTodo(todoList, connection);
                            editTodo(todoList, connection);

                            break;

                        case '3':
                            Console.Clear();
                            viewTodo(todoList, connection);
                            break;

                        case '4':
                            isRunning = exit();
                            break;

                        default:
                            Console.WriteLine("\nInvalid Input. Try again.");
                            break;

                    }

                }

                string getTodo(SqlConnection conn)
                {
                    string data;
                    Console.WriteLine("\nEnter a Todo: ");
                    data = Console.ReadLine();
                    string insertQuery = "INSERT INTO Todo(todoItem) values (@todoItem)";
                    SqlCommand command = new SqlCommand(insertQuery, conn);
                    command.Parameters.AddWithValue("@todoItem", data);
                    command.ExecuteNonQuery();
                    return data;
                }

                void editTodo(List<string> list, SqlConnection conn)
                {
                editAgain:
                    Console.Write("\nEnter the id of todo that you want to edit: ");
                    char numChar = Console.ReadKey().KeyChar;
                    string selectQuery = "SELECT * FROM Todo where id=@value";
                    SqlCommand command1 = new SqlCommand(selectQuery, conn);
                    command1.Parameters.AddWithValue("@value", numChar);
                    SqlDataReader reader = command1.ExecuteReader();

                    Console.WriteLine("\nThe todo that you are goiing to edit is: \n");
                    Console.WriteLine("SN Todo\t\tStatus");
                    if (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string todoItem = (string)reader["todoItem"];
                        string status = (string)reader["statuss"];
                        Console.WriteLine(id + ") " + todoItem + "\t" + status);
                    } else
                    {
                        Console.WriteLine("The todo with that id doesn't exist.");
                    }
                    reader.Close();

                    Console.WriteLine("\nEnter new status: ");
                    string newStatus = Console.ReadLine();
                    string updateQuery = "UPDATE Todo set statuss=@val where id=@id";
                    SqlCommand command2 = new SqlCommand(updateQuery, conn);
                    command2.Parameters.AddWithValue("@val", newStatus);
                    command2.Parameters.AddWithValue("@id", numChar);
                    command2.ExecuteNonQuery();
                    //string updateQuery = "UPDATE Todo SET status = @newStatus";
                    //SqlCommand command = new SqlCommand(updateQuery, conn);
                    //command.Parameters.AddWithValue("@newStatus", "")
                    //if (int.TryParse(numChar.ToString(), out num))
                    //{
                    //    num = num - 1;
                    //    for (int i = 0; i < list.Count; i++)
                    //    {
                    //        if (i == num)
                    //        {
                    //            Console.WriteLine("\nThe todo that you are going to edit is: ");
                    //            Console.WriteLine(num + 1 + ": " + list[num]);
                    //        }
                    //    }


                    //    Console.WriteLine("\nEnter new todo: ");
                    //    string newTodo = Console.ReadLine();
                    //    list[num] = newTodo;

                    //    Console.WriteLine("\nUpdated successfully.");
                    //}
                    //else if (num != 1 || num != 2 || num != 3 || num != 4)
                    //{
                    //    goto editAgain;
                    //}



                }

                void storeTodo(List<string> list, string data)
                {
                    list.Add(data);
                }

                void viewTodo(List<string> list, SqlConnection conn)
                {
                    string selectQuery = "SELECT * FROM Todo";
                    SqlCommand command = new SqlCommand(selectQuery, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("\nYour Todo list: \n");
                    Console.WriteLine("SN Todo\t\tStatus");
                    while(reader.Read())
                    {
                        int id = (int)reader["id"];
                        string todoItem = (string)reader["todoItem"];
                        string status = (string)reader["statuss"];

                        Console.WriteLine(id + ") " + todoItem + "\t" + status);
                    }
                    reader.Close();
                }

                bool exit()
                {
                exitOrNot:
                    Console.Write("\nDo you want to exit? (y/n): ");
                    char val = Console.ReadKey().KeyChar;
                    if (val == 'y')
                    {
                        return false;

                    }
                    else if (val == 'n')
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid Input. Try Again.");
                        goto exitOrNot;
                    }
                }


                Console.ReadLine();
            } 
            catch (SqlException ex)
            {
                Console.WriteLine("Error connecting to the database. \n" +  ex.Message);
            }

            finally
            {
                connection.Close();
            }
    }
    }
}
