using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermutationProgram
{
    class Stages
    {
        static public void StartProgram()
        {
            ShowMessage();
            WaitForTyping();

        }

        static void ShowMessage()
        {
            Console.WriteLine("Program for calculating on permutations.\nTo see list of command type help.");
        }

        static void WaitForTyping()
        {
            Console.Write("\n-> ");
            string command = Console.ReadLine();
            Console.WriteLine();
            try
            {
                Handle(command);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                WaitForTyping();
            }
            
        }

        static void Handle(string command)
        {
            if (!Parsing.CheckCommand(command)) throw new Exception("Bad format of command.");
            int countSpaces = Parsing.CountSpaces(command);
            switch(countSpaces)
            {
                case 0:
                    Handle1WordCommand(command);
                    break;
                case 1:
                    Handle2WordCommand(command);
                    break;
                case 2:
                    Handle3WordCommand(command);
                    break;
                default:
                    throw new Exception("Bad format of command.");
            }
            WaitForTyping();
        }

        static void Handle1WordCommand(string command)
        {
            switch(command)
            {
                case "help":
                    Writing.ShowHelpMessage();
                    break;
                case "quit":
                    Environment.Exit(0);
                    break;
                case "clear-log":
                    Functions.ClearLog();
                    Console.WriteLine("Log file has been cleared.");
                    break;
                default:
                    throw new Exception("Bad format of command.");
            }
        }

        static void Handle2WordCommand(string command)
        {
            string[] words = Parsing.GroupByWords(command);
            int[] permutation;
            switch(words[0])
            {
                case "generate":
                    Writing.GeneratePermutations(words[1]);
                    break;
                case "generate-me":
                    Writing.GeneratePermutationsME(words[1]);
                    break;
                case "reverse":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteReverse(permutation);
                    break;
                case "notations":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteNotations(permutation);
                    break;
                case "rank":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteRank(permutation);
                    break;
                case "type":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteType(permutation);
                    break;
                case "is-involution":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsInvolution(permutation);
                    break;
                case "is-disorder":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsDisorder(permutation);
                    break;
                case "is-even":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsEven(permutation);
                    break;
                case "is-non-even":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsNonEven(permutation);
                    break;
                case "sign":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteSign(permutation);
                    break;
                case "is-transposition":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsTransposition(permutation);
                    break;
                case "is-one-cycle":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteIsOneCycle(permutation);
                    break;
                case "inversion-vector":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteInversionVector(permutation);
                    break;
                case "count-cycles":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteCountCycles(permutation);
                    break;
                case "count-even-cycles":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteCountEvenCycles(permutation);
                    break;
                case "inversions":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteInversionCommand(permutation);
                    break;
                case "number-of-permutation":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteNumberOfPermutation(permutation);
                    break;
                case "calculate":
                    Writing.WriteCalculate(words[1]);
                    break;
                case "generate-with-type":
                    Writing.WriteGenerateWithType(words[1]);
                    break;
                case "order":
                    permutation = Parsing.ParsePermutation(words[1]);
                    Writing.WriteOrder(permutation);
                    break;
                case "generate-anti":
                    Writing.GenerateAntilexicographical(words[1]);
                    break;
                default:
                    throw new Exception("Bad format of command.");

            }
        }

        static void Handle3WordCommand(string command)
        {
            string[] words = Parsing.GroupByWords(command);
            int[] permutation;
            switch(words[0])
            {
                case "folding":
                    int[] leftPermutation = Parsing.ParsePermutation(words[1]);
                    int[] rightPermutation = Parsing.ParsePermutation(words[2]);
                    Writing.WriteFolding(leftPermutation, rightPermutation);
                    break;
                case "power":
                    permutation = Parsing.ParsePermutation(words[1]);
                    int power = int.Parse(words[2]);
                    Writing.WritePower(permutation, power);
                    break;
                case "permutation-by-number":
                    int number = int.Parse(words[1]);
                    int n = int.Parse(words[2]);
                    Writing.WritePermutationByNumber(number, n);
                    break;
                case "generate-with-order":
                    int order = int.Parse(words[1]);
                    int length = int.Parse(words[2]);
                    Writing.WritePermutationWithOrder(order, length);
                    break;
                default:
                    throw new Exception("Bad format of command.");
            }
        }

        

        
    }
}
