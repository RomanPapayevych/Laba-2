using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laba_2
{
    internal class Program
    {
        
        public static void Task1()
        {
            /*
            В одновимірному масиві, що складається з N дійсних елементів, обчислити:
            •	суму додатних елементів масиву;
            •	добуток елементів масиву, що розташовані між максимальним за модулем і мінімальним за модулем елементами.
            Впорядкувати елементи масиву за спаданням.
            */

            Console.WriteLine("Input array size: ");
            int n = int.Parse(Console.ReadLine());
            int[] myArray = new int[n];
            for (int i = 0; i < n; i++)
            {
                //Console.WriteLine("Input number: ");
                Console.Write($"Element [{i /* +1 */}]: ");
                myArray[i] = int.Parse(Console.ReadLine());
            }
            ArraySegment<int> myArrSeg = new ArraySegment<int>(myArray);
            for (int i = myArrSeg.Offset; i < (myArrSeg.Offset + myArrSeg.Count); i++)
            {
                Console.WriteLine("   [{0}] : {1}", i, myArrSeg.Array[i]);
            }
            int Sum = 0;
            //Sum = myArray.Where(x => x > 0).Sum();
            foreach(var item in myArrSeg)
            {
                if(item > 0)
                {
                    Sum += item;
                }
            }

            int maxNumber = 0;
            int minNumber = 0;
            foreach (var item in myArrSeg)
            {
                int absModule = Math.Abs(item);
                if (absModule > maxNumber)
                {
                    maxNumber = absModule;
                    
                } 
                if (absModule < minNumber)
                {
                    
                    minNumber = absModule;
                }
                

            }

            int productBetweenMinAndMax = 1;
            bool foundMin = false;
            bool foundMax = false;
            foreach(var item in myArrSeg)
            {
                int absModule = Math.Abs(item);
                if(absModule != maxNumber && absModule != minNumber)
                {
                    productBetweenMinAndMax *= item;
                }
                
                /*
                int absModule = Math.Abs(item);
                if (!foundMax)
                {
                    if(absModule == maxNumber)
                    {
                        foundMax = true;
                        continue;
                    }
                }
                if(foundMax && !foundMin)
                {
                    if(absModule == minNumber)
                    {
                        foundMin = true;
                        continue;
                    }
                productBetweenMinAndMax *= item;
                }
                */


            }
            Array.Sort(myArray, (x, y) => y.CompareTo(x));
            
            Console.WriteLine("Sum = " + Sum);
            Console.WriteLine($"Product elements: {productBetweenMinAndMax}");
            Console.WriteLine("Sorted array: ");
            foreach (var element in myArray)
            {
                Console.WriteLine(element);
            }

        }

        public static void Task2()
        {
            /*
            Дана прямокутна цілочисельна матриця. 
            Визначити кількість стовпців, які не містять жодного нульового елемента.
            Характеристикою рядка цілочисельної матриці назвемо суму її додатних парних елементів. 
            Переставляючи рядки заданої матриці, розташувати їх у відповідності із зростанням характеристик.
            */


            //створення матриці
            Console.Write("Input rows of matrix: ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Input cols of matrix: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] matrix = new int[rows, cols];

            Console.Write("Input numbers in  matrix: ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine($"Element [{i}, {j}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine("----------- Matrix ------------");
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();    
            }
            
            //перевірка стовпців на наявність 0
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            int columnsWithoutZeros = 0;
            for (int j = 0; j < columnCount; j++)
            {

                bool hasZero = false;
                for (int i = 0; i < rowCount; i++)
                {
                    if (matrix[i, j] == 0)
                    {
                        hasZero = true;
                        break;
                    }
                    
                }
                if (!hasZero)
                {
                    columnsWithoutZeros++;
                }
            }
            Console.WriteLine($"Cols without zero = {columnsWithoutZeros}");

            //сума парних додатніх елементів 
            int sum = 0;
            foreach(var item in matrix)
            {
                if(item > 0 && item % 2 == 0)
                {
                    sum += item;
                }
            }
            Console.WriteLine($"Sum parnogo number = {sum}");

            //сортування матриці за зростанням елементів характеристик(сума рядків)
            int rowCountForSort = matrix.GetLength(0);
            int[] rowsSumForSort = new int[rowCountForSort];

            for (int i = 0; i < rowCountForSort; i++)
            {
                int sumRows = 0;
                int colsCountForSort = matrix.GetLength(1);
                for (int j = 0; j < colsCountForSort; j++)
                {
                    sumRows += matrix[i, j];
                }
                rowsSumForSort[i] = sumRows;
            }
            for (int i = 0; i < rowCountForSort - 1; i++)
            {
                for (int j = i + 1; j < rowCountForSort; j++)
                {
                    if (rowsSumForSort[i] > rowsSumForSort[j])
                    {
                        for(int k = 0; k < matrix.GetLength(1); k++)
                        {
                            int temp = matrix[i, k];
                            matrix[i, k] = matrix[j, k];
                            matrix[j, k] = temp;
                        }
                        int tempSum = rowsSumForSort[i];
                        rowsSumForSort[i] = rowsSumForSort[j];
                        rowsSumForSort[j] = tempSum;
                    }
                }
            }
            Console.WriteLine("------------- Sorted matrix: -----------");
            for (int i = 0; i < rowCountForSort; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
        public static void Task3()
        {
            /*
            З клавіатури вводиться текстовий рядок. 
            Скласти програму, яка перевіряє, чи співпадає кількість відкритих і закритих дужок у введеному рядку 
            (перевірити для круглих та квад.ратних дужок); 
            виводить на екран найдовше слово; 
            видаляє всі слова, що складаються тільки з латинських літер. 
            */
            Console.WriteLine("Input word or words: ");
            string word = Convert.ToString(Console.ReadLine());
            Console.WriteLine(word);
            int openBracketsCount = 0;
            int closeBracketsCount = 0;
            bool Equal = true;
            //перевірка на дужки
            foreach(var bracket in word)
            {
                if(bracket == '(' || bracket == '[')
                {
                    openBracketsCount++;
                }
                else if(bracket == ')' || bracket ==']')
                {
                    closeBracketsCount++;
                }
            }
            if (closeBracketsCount > openBracketsCount)
            {
                Equal = false;
                
            }
            if (openBracketsCount != closeBracketsCount)
            {
                Equal = false;
                
            }
            if (Equal)
            {
                Console.WriteLine("Brackets is equal :) ");
            }
            else
            {
                Console.WriteLine("Brackets is not equal :( ");
            }
            //вивід найдовшого слова
            string []TheLongWord = word.Split(new char[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string longestWord = "";
            foreach (string item in TheLongWord)
            { 
                if (item.Length > longestWord.Length)
                {
                    longestWord = item;
                }
            }
            Console.WriteLine($"Longest word: {longestWord}");


            //видалення латинських слів
            StringBuilder result = new StringBuilder();
            string[] words = word.Split(new char[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in words)
            {
                if (!Regex.IsMatch(item, @"^[A-Za-z]+$"))
                {
                    result.Append(item).Append(" ");
                }  
            }
            Console.WriteLine(result.ToString().Trim());
        }


        public static void Task4()
        {
            //номер квартирного телефону
            // 1 громамдянин може укласти договір на охорону кількох квартир
            // можливе помилкове спрацювання сигналізації, через яку повинні приїхати екіпаж патрульних(можливий виїзд декілька разів у одну квартиру) 

            while (true) 
            {
                Console.WriteLine("Choose option: ");
                Console.WriteLine("1. Add new contract");
                Console.WriteLine("2. Register new event");
                Console.WriteLine("3. Search information");
                Console.WriteLine("4. Exit");
                int choice = int.Parse(Console.ReadLine());
                switch(choice) 
                {
                    case 1:
                        Console.WriteLine("Name owner: ");
                        string NameOwner = Console.ReadLine();

                        Console.WriteLine("Number apartament: ");
                        string NumberApartament = Console.ReadLine();
                        
                        Console.WriteLine("Having home number? (true/false)");
                        bool HavingHomeNumber = bool.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Monthly payment: ");
                        uint MontlyPayment = uint.Parse(Console.ReadLine());

                        Console.WriteLine("The amount of the fine(штраф): ");
                        uint AmountOfFine = uint.Parse(Console.ReadLine());

                        contract.Add((NameOwner, NumberApartament, HavingHomeNumber, MontlyPayment, AmountOfFine));
                        Console.WriteLine("Contract is creted!");
                        break;

                    case 2:
                        Console.WriteLine("Number apartament: ");
                        string apartNumb = Console.ReadLine();
                       
                        Console.WriteLine("Did the alarm work?(чи спрацювала сигналізація?)");
                        bool alarmTrigger = bool.Parse(Console.ReadLine());
                        events.Add((DateTime.Now, apartNumb, alarmTrigger));

                        Console.WriteLine("Event is added");
                        break;

                    case 3:
                        Console.WriteLine("Input number apartament for searching: ");
                        string SearchAppartament = Console.ReadLine();      
                        var relevantContracts = contract.Where(c => c.NumberApartament == SearchAppartament).ToList();
                        var relevantEvents = events.Where(e => e.NumberApartament == SearchAppartament).ToList();
                        if (relevantContracts.Any())
                        {
                            Console.WriteLine("Found contracts: ");
                            foreach (var item in relevantContracts)
                            {
                                Console.WriteLine($"Owners: {item.NameOwner}, Monthly payment: {item.MontlyPayment}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not found any contracts");
                        }
                        if (relevantEvents.Any())
                        {
                            Console.WriteLine("Found events: ");
                            foreach(var item in relevantEvents)
                            {
                                Console.WriteLine($"Time of event: {item.EventTime}, Alarm activation: {item.alarmTrigger}");
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not found any events");
                        }
                        break;
                    
                    case 4:
                        Environment.Exit(0);
                        break;
                        
                    default: 
                        Console.WriteLine("Mistake operation :(");
                        break;
                }
            }
        }
        static List<(string NameOwner, string NumberApartament, bool HavingHomeNumber, uint MontlyPayment, uint AmountOfFine)> contract = new List<(string, string, bool, uint, uint)>();
        static List<(DateTime EventTime, string NumberApartament, bool alarmTrigger)> events = new List<(DateTime, string, bool)>();


        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("Choose task: 1, 2, 3, 4:");
                int task = Convert.ToInt32(Console.ReadLine());
                if(task == 1)
                {
                    Task1();
                }
                else if(task == 2)
                {
                    Task2(); 
                } else if(task == 3)
                {
                    Task3();
                }
                else if(task == 4)
                {
                    Task4();
                }
            } 
        }
    }
}
