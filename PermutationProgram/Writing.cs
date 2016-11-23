using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Permutations;
using System.IO;

namespace PermutationProgram
{
    class Writing
    {
        static public void ShowHelpMessage()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("- help - to see list of commands.");
            Console.WriteLine("- quit - to quit the program.");
            Console.WriteLine("- clear-log - to clear a log file.");
            Console.WriteLine("- generate <number of permutation> - to generate permutations in lexicographical way.");
            Console.WriteLine("- generate-anti <number of permutation> - to generate permutations in anti-lexicographical way.");
            Console.WriteLine("- generate-me <number of permutation> - like \"generate\" command but this is more effective.");
            Console.WriteLine("- reverse <permutation to reverse one-line or cycle notation>");
            Console.WriteLine("- composition <left side permutation> <right side permutation>");
            Console.WriteLine("- notations <permutation>");
            Console.WriteLine("- is-involution <permutation>");
            Console.WriteLine("- is-disorder <permutation>");
            Console.WriteLine("- is-even <permutation>");
            Console.WriteLine("- is-non-even <permutation>");
            Console.WriteLine("- sign <permutation>");
            Console.WriteLine("- sign1 <permutation> - (exponent - number of inversions)");
            Console.WriteLine("- sign2 <permutation> - (exponent - (number of element - number of cycles))");
            Console.WriteLine("- sign3 <permutation> - (exponent - (number of element + number of cycles))");
            Console.WriteLine("- sign4 <permutation> - (exponent - number of even cycles)");
            Console.WriteLine("- is-tranposition <permutation>");
            Console.WriteLine("- power <permutation> <power>");
            Console.WriteLine("- inversion-vector <permutation>");
            Console.WriteLine("- count-cycles <permutation>");
            Console.WriteLine("- count-even-cycles <permutation>");
            Console.WriteLine("- count-non-even-cycles <permutation>");
            Console.WriteLine("- count-fixed-points <permutation>");
            Console.WriteLine("- count-disorders <permutation>");
            Console.WriteLine("- count-inversions <permutation>");
            Console.WriteLine("- inversions <permutation>");
            Console.WriteLine("- index-from-perm-lo <permutation> - lo - lexicographical order");
            Console.WriteLine("- index-from-perm-lo <permutation> - alo - anti lexicographical order");
            Console.WriteLine("- permutation-from-index-lo <index of permutatation in lexicographical order> <number of elements>");
            Console.WriteLine("- permutation-from-index-alo <index of permutatation in anti lexicographical order> <number of elements>");
            Console.WriteLine("- calculate <expression> - example of command: \"calculate (f*(1 2 4))^3=(4)*(1 3)(4)\" ");
            Console.WriteLine("- generate-with-type <type> - example of command: \"generate-with-type [1^3,2^1]\"");
            Console.WriteLine("- generate-with-order <order> <length of permutation>");
            Console.WriteLine("\nExample of permutation in one-line notation: <1,2,3,7,6,5>");
            Console.WriteLine("Example of permutation in cycle notation: (1 2 3)(9)");
        }

        static public void GeneratePermutations(string number)
        {
            int numberInt = int.Parse(number);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Generating permutations...");
                int[][] permutations = WithoutRepetition.PermutationLexicographical(numberInt);
                for (int i = 0; i < permutations.Length; i++)
                {
                    Write(writer, (i + 1) + ") ");
                    WriteVector(writer, permutations[i]);
                    WriteLine(writer);
                }
            }
        }

        static public void WriteLine(StreamWriter writer, String text)
        {
            writer.WriteLine(text);
            Console.WriteLine(text);
        }

        static public void WriteLine(StreamWriter writer)
        {
            writer.WriteLine();
            Console.WriteLine();
        }

        static public void Write(StreamWriter writer, String text)
        {
            writer.Write(text);
            Console.Write(text);
        }

        static public void GeneratePermutationsME(string number)
        {
            int numberInt = int.Parse(number);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Generating permutations...");
                int[] permutation = WithoutRepetition.CreateIdentityPermutation(numberInt);
                Write(writer, "1) ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                int factorial = MathFunctions.Factorial(numberInt);
                for (int i = 2; i <= factorial; i++)
                {
                    permutation = WithoutRepetition.NextElementLexicographical(permutation);
                    Write(writer, i + ") ");
                    WriteVector(writer, permutation);
                    WriteLine(writer);
                }
            }
        }

        static void WriteVector(StreamWriter writer, int[] vector)
        {
            Write(writer, "<");
            for (int j = 0; j < vector.Length; j++)
            {
                Write(writer, vector[j]+"");
                if (j != (vector.Length - 1)) Write(writer, ","); //żeby nie wstawiało przecinka po ostatnim elemencie
            }
            Write(writer, ">");
        }
        /*
        static void WriteCycle(StreamWriter writer, int[][] permutation)
        {
            for(int i=0;i<permutation.Length;i++)
            {
                
                if(permutation[i].Length>1)
                {
                    Write(writer, "(");
                    for (int j=0;j<permutation[i].Length;j++)
                    {
                        Write(writer, permutation[i][j]);
                        if (j != (permutation[i].Length - 1)) Write(writer, " ");
                    }
                    Write(writer, ")");
                }
                
            }
            int indexOfMax = Functions.LenghtArrayWithMax(permutation);
            if (permutation[indexOfMax].Length==1) Write(writer, "(" + permutation[indexOfMax][0] + ")");
        }
        */
        static void WriteCycle(StreamWriter writer, int[][] permutation)
        {
            Write(writer, Parsing.CycleToString(permutation));
        }

        static void WriteMatrix(StreamWriter writer, int[,] matrix)
        {
            Write(writer, "   ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i < matrix.Length - 1)
                    Write(writer,(i + 1) + " ");
                else
                    Write(writer, i+"");
            }
            WriteLine(writer);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Write(writer, (i + 1) + " ");
                if (i < 9) Write(writer, " ");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j < matrix.GetLength(1) - 1)
                        Write(writer, matrix[i, j] + " ");
                    else
                        Write(writer, matrix[i, j] + "");
                    if ((j + 1) >= 10) Write(writer, " ");
                }
                WriteLine(writer);
            }
        }

        static public void WriteInversions(StreamWriter writer, int[,] inversions)
        {
            for (int i = 0; i < inversions.GetLength(0); i++)
                WriteLine(writer, "(" + inversions[i, 0] + "," + inversions[i, 1] + ")");
        }

        static public void WriteReverse(int[] permutation)
        {
            int[] reversePermutation = WithoutRepetition.ReversePermutation(permutation);
            int[][] cycleReversePermutation = WithoutRepetition.VectorToCycle(reversePermutation);
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Reverse permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Result:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, reversePermutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cycleReversePermutation);
                WriteLine(writer);
            }
        }

        static public void WriteComposition(int[] leftPermutation, int[] rightPermutation)
        {
            int[] resultPermutation = WithoutRepetition.CompositionPermutation(leftPermutation, rightPermutation);
            int[][] resultPermutationCycle = WithoutRepetition.VectorToCycle(resultPermutation);
            int[][] leftPermutationCycle = WithoutRepetition.VectorToCycle(leftPermutation);
            int[][] rightPermutationCycle = WithoutRepetition.VectorToCycle(rightPermutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Composition permutations:");
                WriteLine(writer);
                WriteLine(writer, "Left Permutation:");
                Write(writer, "One-line notation: ");
                WriteVector(writer, leftPermutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, leftPermutationCycle);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Right Permutation:");
                Write(writer, "One-line notation: ");
                WriteVector(writer, rightPermutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, rightPermutationCycle);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Result Permutation:");
                Write(writer, "One-line notation: ");
                WriteVector(writer, resultPermutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, resultPermutationCycle);
                WriteLine(writer);
            }
        }

        static public void WriteNotations(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            int[,] matrixPermutation = WithoutRepetition.VectorToMatrix(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Permutation in different notations:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Matrix notation:");
                WriteLine(writer);
                WriteMatrix(writer, matrixPermutation);
                WriteLine(writer);
            }
        }

        static public void WriteOrder(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Rank of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Rank of permutation: " + WithoutRepetition.OrderOfPermutation(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteType(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            int[] vectorType = WithoutRepetition.TypeOfPermutation(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Type of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Type of permutation: [");
                string toWrite = "";
                for (int i = 1; i < vectorType.Length; i++)
                    if (vectorType[i] != 0)
                        toWrite+=i + "^" + vectorType[i] + " ";
                toWrite = toWrite.Remove(toWrite.Length - 1, 1);
                Write(writer, toWrite);
                Write(writer, "]");
                WriteLine(writer);
            }
        }

        static public void WriteIsInvolution(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is an involution:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is an involution? - ");
                if (WithoutRepetition.IsInvolution(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WriteIsDisorder(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is a disorder:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is a disorder? - ");
                if (WithoutRepetition.IsDisorder(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WriteIsEven(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is even:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is even? - ");
                if (WithoutRepetition.IsEven(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WriteIsNonEven(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is non even:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is non even? - ");
                if (WithoutRepetition.IsNonEven(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WriteSign(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Sign of permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Value of sign: ");
                Write(writer, WithoutRepetition.SignOfPermutation(permutation) + "");
                WriteLine(writer);
            }
        }

        static public void WriteSign1(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Sign of permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Value of sign: ");
                Write(writer, WithoutRepetition.SignOfPermutation1(permutation) + "");
                WriteLine(writer);
            }
        }

        static public void WriteSign2(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Sign of permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Value of sign: ");
                Write(writer, WithoutRepetition.SignOfPermutation2(permutation) + "");
                WriteLine(writer);
            }
        }

        static public void WriteSign3(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Sign of permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Value of sign: ");
                Write(writer, WithoutRepetition.SignOfPermutation3(permutation) + "");
                WriteLine(writer);
            }
        }

        static public void WriteSign4(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Sign of permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Value of sign: ");
                Write(writer, WithoutRepetition.SignOfPermutation4(permutation) + "");
                WriteLine(writer);
            }
        }

        static public void WriteIsTransposition(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is a transposition:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is a transposition? - ");
                if (WithoutRepetition.IsTransposition(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WritePower(int[] permutation, int power)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            int[] resultPermutation = WithoutRepetition.PowerOfPermutation(permutation, power);
            int[][] resultCyclePermutation = WithoutRepetition.VectorToCycle(resultPermutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Power of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer, "Power: " + power);
                WriteLine(writer);
                WriteLine(writer, "Result permutation");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, resultPermutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, resultCyclePermutation);
                WriteLine(writer);
            }

        }

        static public void WriteIsOneCycle(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "If permutation is one cycle:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Is one cycle? - ");
                if (WithoutRepetition.IsOneCycle(permutation))
                    Write(writer, "yes");
                else
                    Write(writer, "no");
                WriteLine(writer);
            }
        }

        static public void WriteInversionVector(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            int[] inversionVector = WithoutRepetition.InversionVector(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Iversion vector of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Inversion vector: ");
                WriteVector(writer, inversionVector);
                WriteLine(writer);
            }
        }

        static public void WriteCountCycles(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count cycles of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of cycles: "+WithoutRepetition.CountCycles(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteCountEvenCycles(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count even cycles of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of even cycles: " + WithoutRepetition.CountEvenCycles(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteCountNonEvenCycles(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count non even cycles of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of non even cycles: " + WithoutRepetition.CountNonEvenCycles(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteCountFixedPoints(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count fixed points of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of fixed points " + WithoutRepetition.CountFixedPoints(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteCountDisorders(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count disorders of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of disorders " + WithoutRepetition.CountDisorders(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteCountInversions(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Count inversions of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                Write(writer, "Number of inversions " + WithoutRepetition.CountInversions(permutation));
                WriteLine(writer);
            }
        }

        static public void WriteInversionCommand(int[] permutation)
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            int[,] matrixOfInversions = WithoutRepetition.MatrixOfInversions(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Writing inversions of permutation:");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Writing inversions");
                WriteLine(writer);
                WriteInversions(writer, matrixOfInversions);
                WriteLine(writer);
            }
        }

        static public void WritePermutationIndexLO(int[] permutation) // LO- lexicographical order
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            long index = WithoutRepetition.PermutationIndexLO(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Permutation index in lexicographical order: ");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Index: "+index);
                WriteLine(writer);
            }
        }

        static public void WritePermutationIndexALO(int[] permutation) // ALO- anti lexicographical order
        {
            int[][] cyclePermutation = WithoutRepetition.VectorToCycle(permutation);
            long index = WithoutRepetition.PermutationIndexALO(permutation);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Permutation index in anti lexicographical order: ");
                WriteLine(writer);
                Write(writer, "One-line notation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
                Write(writer, "Canonical cycle notation: ");
                WriteCycle(writer, cyclePermutation);
                WriteLine(writer);
                WriteLine(writer);
                WriteLine(writer, "Index: " + index);
                WriteLine(writer);
            }
        }

        static public void WritePermutationFromIndexLO(int number, int n) //LO - lexicographical order
        {
            int[] permutation = WithoutRepetition.PermutationFromIndexLO(number, n);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Permutation from index in lexicographical order: ");
                WriteLine(writer);
                WriteLine(writer, "Index of permutation: " + number);       
                WriteLine(writer, "Number of elements: " + n);
                WriteLine(writer);
                Write(writer, "Permutation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
            }
        }

        static public void WritePermutationFromIndexALO(int number, int n) //ALO - anti lexicographical order
        {
            int[] permutation = WithoutRepetition.PermutationFromIndexALO(number, n);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Permutation from index in anti lexicographical order: ");
                WriteLine(writer);
                WriteLine(writer, "Index of permutation: " + number);
                WriteLine(writer, "Number of elements: " + n);
                WriteLine(writer);
                Write(writer, "Permutation: ");
                WriteVector(writer, permutation);
                WriteLine(writer);
            }
        }

        static public void WriteCalculate(string Expression)
        {
            string calculated = Parsing.ParseExpression(Expression);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Calculating expression: " + Expression);
                WriteLine(writer);
                WriteLine(writer, "Output: "+calculated);
                WriteLine(writer);
            }
        }

        static public void WriteGenerateWithType(string type)
        {
            int[] vectorType = Parsing.ParseType(type);
            
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Generating permutations for type: " + type);
                WriteLine(writer);
                int[][] permutations = WithoutRepetition.GeneratePermutationsOfType(vectorType);
                int counter = 1;
                foreach(int[] permutation in permutations)
                {
                    Write(writer, counter + ") ");
                    counter++;
                    WriteVector(writer, permutation);
                    WriteLine(writer);
                }
                WriteLine(writer);
            }
            
        }

        static public void WritePermutationWithOrder(int order, int length)
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Generating permutations for order: " + order + " and length: " + length);
                WriteLine(writer);
                int[][] permutations = WithoutRepetition.GenerateWithOrder(order, length);
                int counter = 1;
                foreach (int[] permutation in permutations)
                {
                    Write(writer, counter + ") ");
                    counter++;
                    WriteVector(writer, permutation);
                    WriteLine(writer);
                }
                WriteLine(writer);
            }
        }



        static void WriteDate(StreamWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(DateTime.Now.ToString());
            writer.WriteLine();
        }

        static public void GenerateAntilexicographical(string number)
        {
            int numberInt = int.Parse(number);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Generating permutations...");
                int[][] permutations = WithoutRepetition.PermutationAntylexicographical(numberInt);
                for (int i = 0; i < permutations.Length; i++)
                {
                    Write(writer, (i + 1) + ") ");
                    WriteVector(writer, permutations[i]);
                    WriteLine(writer);
                }
            }
        }

        static public void IsGroup(int[][] permutations)
        {
            bool isGroup = WithoutRepetition.IsGroup(permutations);
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                WriteDate(writer);
                WriteLine(writer, "Is permutations are a group?- " + isGroup);
            }
        }
    }
}
