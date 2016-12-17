using System;
using System.Collections.Generic;
using Permutations;

namespace PermutationProgram
{
    class Parsing
    {
        static public bool CheckCommand(string command)
        {
            if (command.Length == 0) return false;
            if (command[0] == ' ' || command[command.Length - 1] == ' ') return false;
            for (int i = 1; i < command.Length - 1; i++)
                if (command[i] == ' ')
                    if (command[i + 1] == ' ')
                        return false;
            return true;
        }

        static public int CountSpaces(string command)
        {
            int spaces = 0;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == '(')
                    while (command[i] != ')'&&i<command.Length-1)
                        i++;
                if (command[i] == ' ') spaces++;   
            }
            return spaces;
        }
        /*
        static public string[] GroupByWords(string command)
        {
            string[] words;
            int count = CountSpaces(command) + 1;
            int j = 0;
            words = new string[count];
            for (int i = 0; i < count; i++)
            {
                
                if(command[j]=='(')
                {
                    
                    while(command[j]!=')'&&j<command.Length-1)
                    {
                        words[i] += command[j];
                        j++;
                    }
                    words[i] += command[j];
                    j++;
                }
                else while (command[j] != ' '&&j<command.Length)
                {
                    words[i] += command[j];
                    j++;
                }
                j++;
            }
            
            for (int i = j; i < command.Length; i++)
                words[count - 1] += command[i];
            
            return words;
        }*/

        static public string[] GroupByWords(string command)
        {
            string[] words;
            int count = CountSpaces(command) + 1;
            int j = 0;
            words = new string[count];
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == ' ')
                    j++;
                else
                {
                    if (command[i] == '(')
                    {
                        while (command[i] != ')' && i < command.Length - 1)
                        {
                            words[j] += command[i];
                            i++;
                        }
                        words[j] += command[i];
                    }
                    else
                        words[j] += command[i];
                }
            }
            
            return words;
        }

        public static int[] ParseVector(string permutation)
        {
            if (permutation[0]!='<' && permutation[permutation.Length - 1] != '>') throw new Exception("Bad format of permutation.");

            List<int> values = new List<int>();
            string contener = "";
            for (int i = 1; i < permutation.Length - 1; i++)
            {
                if (i != 1 && i != (permutation.Length - 2))
                {
                    if (permutation[i] == ',')
                    {
                        values.Add(Int32.Parse(contener));
                        contener = "";
                    }
                    else
                    {
                        contener += permutation[i];
                    }
                }
                else if (i == 1)
                {
                    string help = "" + permutation[i];
                    Int32.Parse(help);
                    contener += permutation[i];
                }
                else
                {
                    string help = "" + permutation[i];
                    Int32.Parse(help);
                    contener += permutation[i];
                    values.Add(Int32.Parse(contener));
                    contener = "";
                }
            }
            int[] returnedPermutation = values.ToArray();
            if (!WithoutRepetition.CheckOnePermutation(returnedPermutation)) throw new Exception("Bad format of permutation");


            return returnedPermutation;
        }

        static public int[][] ParseCycle(string permutation) //parsowanie zapisu cyklowego
        {
            if (permutation[0] != '(' && permutation[permutation.Length - 1] != ')') throw new Exception("Bad format of permutation.");
            int brackets = CountBrackets(permutation);
            string[] cycles = new string[brackets];
            int index = 1;
            int stringIndex = 0;
            string help = ""; // przechowuje łańcuchy
            while (index != (permutation.Length - 1))
            {
                if (permutation[index] == ')')
                {
                    if (help.Length != 0)
                    {
                        cycles[stringIndex] = help;
                        help = "";
                        stringIndex++;
                        if (permutation[index + 1] == '(')
                            index++;
                        else
                            throw new Exception("Bad format of permutation.");

                    }
                    else
                        throw new Exception("Bad format of permutation.");
                }
                else
                    help += permutation[index];
                index++;
            }
            if (help.Length != 0 && permutation[permutation.Length - 1] == ')') cycles[cycles.Length - 1] = help;
            else throw new Exception("Bad format of permutation.");



            int[][] tempArray = new int[brackets][];
            List<int> values = new List<int>();
            help = "";

            for (int i = 0; i < brackets; i++)
            {
                for (int j = 0; j < cycles[i].Length; j++)
                {
                    if (j != 0 && j != (cycles[i].Length - 1))
                    {
                        if (cycles[i][j] == ' ')
                        {
                            values.Add(Int32.Parse(help));
                            help = "";
                        }
                        else
                        {
                            help += cycles[i][j];
                        }
                    }
                    else if (j == 0)
                    {
                        string temp = "" + cycles[i][j];
                        Int32.Parse(temp);
                        help += cycles[i][j];
                        if (cycles[i].Length == 1)
                        {
                            values.Add(Int32.Parse(help));
                            help = "";
                        }
                    }
                    else
                    {
                        string temp = "" + cycles[i][j];
                        Int32.Parse(temp);
                        help += cycles[i][j];
                        values.Add(Int32.Parse(help));
                        help = "";
                    }
                }
                tempArray[i] = values.ToArray();
                values.Clear();
            }

            int maxOfArrays = Functions.MaxOfArrays(tempArray);
            bool[] tab_bool = new bool[maxOfArrays];
            for (int i = 0; i < tempArray.Length; i++)
                for (int j = 0; j < tempArray[i].Length; j++)
                    tab_bool[tempArray[i][j] - 1] = true;

            int counter = 0;
            for (int i = 0; i < tab_bool.Length; i++)
                if (!tab_bool[i]) counter++;

            int[][] returnedArray = new int[tempArray.Length + counter][];
            for (int i = 0; i < tempArray.Length; i++)
                returnedArray[i] = tempArray[i];

            int indexBool = tempArray.Length;
            for (int i = 0; i < tab_bool.Length; i++)
                if (!tab_bool[i])
                {
                    returnedArray[indexBool] = new int[1];
                    returnedArray[indexBool][0] = i + 1;
                    indexBool++;
                }

            if (!WithoutRepetition.CheckCyclePermutation(returnedArray)) throw new Exception("Bad format of permutation.");

            return returnedArray;
        }

        static public int CountBrackets(string permutation) //zliczanie nawiasów ')'
        {
            int count = 0;
            for (int i = 0; i < permutation.Length; i++)
                if (permutation[i] == ')')
                    count++;
            return count;
        }

        static public int[] ParsePermutation(string permutation)
        {
            int[] returnedPermutation= {1,2 };
            if (permutation[0] == '<')
                returnedPermutation = ParseVector(permutation);
            else if (permutation[0] == '(')
            {
                int[][] cyclePermutation = ParseCycle(permutation);
                returnedPermutation = WithoutRepetition.CycleToPermutation(cyclePermutation);
            }
            else
                throw new Exception("Bad format of permutation.");
            return returnedPermutation;
        }

        static public string CycleToString(int[][] permutation)
        {
            string stringPermutation = "";
            for (int i = 0; i < permutation.Length; i++)
            {
                //if (permutation[i].Length > 1)
               // {
                    stringPermutation += '(';
                    for (int j = 0; j < permutation[i].Length; j++)
                    {
                        if (j != (permutation[i].Length - 1))
                            stringPermutation += permutation[i][j] + " ";
                        else
                            stringPermutation += permutation[i][j];

                    }
                    stringPermutation += ')';
                //}
            }
            //int indexOfMax = Functions.LenghtArrayWithMax(permutation);
            //if (permutation[indexOfMax].Length == 1) stringPermutation += "(" + permutation[indexOfMax][0] + ")";
            return stringPermutation;
        }

        static public string CycleToTransposition(int[][] permutation)
        {
            string stringTransposition = "";
            for(int i=0; i<permutation.Length;i++)
                if(permutation[i].Length==2)
                {
                    stringTransposition = "(" + permutation[i][0] + " " + permutation[i][1] + ")";
                }
            return stringTransposition;
        }

        public static string PowerOfPermutationString(string Expression)
        {
            int index = Expression.IndexOf("^");
            int power = 0;
            string temp = Expression.Substring(index + 1); 
            power = int.Parse(temp);
            string cyclePermutationString = Expression.Remove(index);
            int[] permutation = ParsePermutation(cyclePermutationString);
            int[] resultPermutation = WithoutRepetition.PowerOfPermutation(permutation, power);
            int[][] resultPermutationCycle = WithoutRepetition.PermutationToCycle(resultPermutation);
            string resultPermutationString = CycleToString(resultPermutationCycle);

            return resultPermutationString;

        }

        static string CalculateExpression(string Expression)
        {
            while (Expression.Contains("(("))
            {
                
                int indexFirst = Expression.IndexOf("(("), indexLast = 0;
                int counter = 2;
                for (int i = indexFirst + 2; i < Expression.Length; i++)
                {
                    if (Expression[i] == '(')
                        counter++;
                    else if (Expression[i] == ')')
                    {
                        counter--;
                        if (counter == 0)
                        {
                            indexLast = i;
                            break;
                        }
                    }
                }
                string temp = Expression.Substring(indexFirst + 1, (indexLast - 1) - (indexFirst + 1) + 1);
                
                string newExpression = CalculateExpression(temp);
                
                string tempExpression = "";
                if (indexFirst != 0)
                    tempExpression += Expression.Substring(0, indexFirst);
                tempExpression += newExpression;
                if (indexLast != (Expression.Length - 1))
                    tempExpression += Expression.Substring(indexLast + 1, Expression.Length - 1 - (indexLast + 1) + 1);
                Expression = tempExpression;
                
            }
            string[] expressions = Expression.Split('*');
            
            string returnedExpression = "";
            for (int i = 0; i < expressions.Length; i++)
            {
                if (expressions[i].Contains("^"))
                    expressions[i] = PowerOfPermutationString(expressions[i]);
            }
            if (expressions.Length > 1)
            {
                int[] returnedPermutation;
                int[][] returnedPermutationCycle;
                int[] permutation1 = ParsePermutation(expressions[0]);
                int[] permutation2 = ParsePermutation(expressions[1]);
                returnedPermutation = WithoutRepetition.CompositionOfPermutation(permutation1, permutation2);
                for (int i = 2; i < expressions.Length; i++)
                {
                    int[] permutation = ParsePermutation(expressions[i]);
                    returnedPermutation = WithoutRepetition.CompositionOfPermutation(returnedPermutation, permutation);
                }
                returnedPermutationCycle = WithoutRepetition.PermutationToCycle(returnedPermutation);
                returnedExpression = CycleToString(returnedPermutationCycle);
            }
            else
                returnedExpression = expressions[0];
            return returnedExpression;
        }

        static internal int[] ParseType(string type)
        {
            if (type[0] != '[' || type[type.Length - 1] != ']')
                throw new Exception("Bad format of command.");
            type = type.Remove(0, 1);
            type = type.Remove(type.Length - 1, 1);
            string[] types = type.Split(',');
            int[,] matrixType = new int[types.Length, 2];
            for (int i = 0; i < matrixType.GetLength(0); i++)
            {
                string[] numbers = types[i].Split('^');
                if (numbers.Length != 2) throw new Exception("Bad format of command");
                matrixType[i, 0] = int.Parse(numbers[0]);
                matrixType[i, 1] = int.Parse(numbers[1]);
            }
            int length = 0;
            for (int i = 0; i < matrixType.GetLength(0); i++)
            {
                if (matrixType[i, 0] < 1 || matrixType[i, 1] < 1)
                    throw new Exception("Bad format of command.");
                length += matrixType[i, 0] * matrixType[i, 1];
            }
            int[] typeVector = new int[length + 1];
            for (int i = 0; i < matrixType.GetLength(0); i++)
            {
                if (typeVector[matrixType[i, 0]] != 0)
                    throw new Exception();
                typeVector[matrixType[i, 0]] = matrixType[i, 1];
            }
            return typeVector;
        }

        static string[] DivideExpression(string Expression)
        {
            List<int> listOfIndexes = new List<int>();
            int counter = 0;
            for (int i = 0; i < Expression.Length; i++)
            {
                if (Expression[i] == '*' && counter == 0)
                    listOfIndexes.Add(i);
                else if (Expression[i] == '(')
                    counter++;
                else if (Expression[i] == ')')
                    counter--;
            }
            int indexOfFirst = 0;
            int indexOfString = 0;
            string[] arrayOfExpressions = new string[listOfIndexes.Count + 1];
            foreach (int i in listOfIndexes)
            {
                if (indexOfFirst == i - 1)
                    arrayOfExpressions[indexOfFirst] += Expression[indexOfFirst];
                else
                    arrayOfExpressions[indexOfString] = Expression.Substring(indexOfFirst, i - indexOfFirst);
                indexOfFirst = i + 1;
                indexOfString++;
            }
            arrayOfExpressions[indexOfString] = Expression.Substring(indexOfFirst, (Expression.Length) - indexOfFirst);
            return arrayOfExpressions;
        }

        public static string ParseExpression(string Expression)
        {
            string[] expressions = Expression.Split('='); //podzielenie na strone prawą i lewą
            string rightOfExpression = "", leftOfExpression = "";
            string toReturn = "";
            Exception exception = new Exception("Bad format of expression.");
            if (expressions.Length != 2)
                throw exception;
            bool flag = false; // flaga czy już zostało znalezione f w wyrażeniu
            for (int i = 0; i < expressions[0].Length; i++)
                if (expressions[0][i] == 'f')
                {
                    if (flag)
                        throw exception;
                    else
                    {
                        leftOfExpression = expressions[0];
                        rightOfExpression = expressions[1];
                        flag = true;
                    }
                }
            for (int i = 0; i < expressions[1].Length; i++)
                if (expressions[1][i] == 'f')
                {
                    if (flag)
                        throw exception;
                    else
                    {
                        leftOfExpression = expressions[1];
                        rightOfExpression = expressions[0];
                        flag = true;
                    }
                }
            if (!flag)
                throw exception;
            while (leftOfExpression.Contains("(") || leftOfExpression.Contains(")") || leftOfExpression.Contains("*"))
            {
                string[] divided = DivideExpression(leftOfExpression);
                if (divided.Length == 1)
                {
                    if (isToPower(divided[0]))
                    {
                        int indexOfPower = SearchIndexPower(divided[0]);
                        string divideToPower = divided[0].Substring(1, indexOfPower - 2);
                        string[] toPower = DivideExpression(divideToPower);
                        int power = int.Parse(divided[0].Substring(indexOfPower + 1)); //+1 bo indexOfPower to index '^'
                        for (int i = 0; i < toPower.Length; i++)
                        {
                            if (isToPower(toPower[i]))
                            {
                                int indexOfS = SearchIndexPower(toPower[i]);
                                int powerOfS = int.Parse(toPower[i].Substring(indexOfS + 1));
                                toPower[i] = toPower[i].Substring(0, indexOfS + 1) + (powerOfS * power);
                            }
                            else
                            {
                                toPower[i] = toPower[i] + "^" + power;
                            }
                        }
                        leftOfExpression = "";
                        for (int i = 0; i < toPower.Length; i++)
                        {
                            if (i == toPower.Length - 1)
                                leftOfExpression += toPower[i];
                            else
                                leftOfExpression += toPower[i] + "*";
                        }
                    }
                    else
                        leftOfExpression = divided[0].Substring(1, divided[0].Length - 2);
                }
                else
                {
                    int indexOfF = 0;
                    bool flagF = false;
                    for (int i = 0; i < divided.Length; i++)
                        if (divided[i].Contains("f"))
                        {
                            indexOfF = i;
                            flagF = true;
                        }
                    if (!flagF)
                        throw exception;
                    string left = "", right = "";
                    for (int i = 0; i < indexOfF; i++)
                    {
                        if (i == indexOfF - 1)
                            left += "("+divided[i]+")^-1";
                        else
                            left += "(" + divided[i]+")^-1" + "*";
                    }
                    for (int i = divided.Length-1; i >= indexOfF+1; i--)
                    {
                        if (i == indexOfF+1)
                            right += "(" + divided[i] + ")^-1";
                        else
                            right += "(" + divided[i] + ")^-1" + "*";
                    }
                    
                    if (left.Length > 0)
                    {
                        //left = "(" + left + ")^-1";
                        rightOfExpression = left + "*" + rightOfExpression;
                    }
                    if (right.Length > 0)
                    {
                        //right = "(" + right + ")^-1";
                        rightOfExpression = rightOfExpression + "*" + right;
                    }
                    leftOfExpression = divided[indexOfF];
                }
            }
            //Console.WriteLine("Right: " + rightOfExpression);
            rightOfExpression = CalculateExpression(rightOfExpression);
            
            if (leftOfExpression.Contains("^"))
            {
                int i = leftOfExpression.IndexOf('^');
                string toParse = leftOfExpression.Substring(i + 1);
                int root = int.Parse(toParse);
                int[] permutationToRoot = ParsePermutation(rightOfExpression);
                int[][] Cases = WithoutRepetition.RootOfPermutation(permutationToRoot, root);
                if (Cases.Length == 0)
                    toReturn += "There isn't any solutions of expression.";
                else
                {
                    int[][] permutationCycle = WithoutRepetition.PermutationToCycle(Cases[0]);
                    toReturn += "f=" + CycleToString(permutationCycle);
                    if(Cases.Length>1)
                    {
                        for(int k=1;k<Cases.Length;k++)
                        {
                            permutationCycle = WithoutRepetition.PermutationToCycle(Cases[k]);
                            toReturn+="||f="+ CycleToString(permutationCycle);
                        }
                    }
                }
                /*
                int[][] permutationCycle = WithoutRepetition.PermutationToCycle(Cases);
                rightOfExpression = CycleToString(permutationCycle);*/
            }

            return toReturn;
            //return "f=" + rightOfExpression;
        }

        static int SearchIndexPower(string expression)
        {
            Exception exception = new Exception("Bad format of command.");
            int counter = 0;
            int indexOfPower = 0;
            bool flagPower = false;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    counter++;
                else if (expression[i] == ')')
                    counter--;
                else if (expression[i] == '^' && counter == 0)
                {
                    if (!flagPower)
                    {
                        indexOfPower = i;
                        flagPower = true;
                    }
                    else
                        throw exception;
                }
            }
            if (!flagPower)
                throw exception;
            return indexOfPower;

        }

        static bool isToPower(string expression)
        {
            int counter = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    counter++;
                else if (expression[i] == ')')
                    counter--;
                else if (expression[i] == '^' && counter == 0)
                    return true;
            }
            return false;
        }
    }
}
