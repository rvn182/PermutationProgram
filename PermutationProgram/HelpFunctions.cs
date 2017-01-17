using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    static class HelpFunctions
    {
        static internal bool checkOnePermutation(int[] permutation)
        {
            if (permutation.Length < 2) return false;
            bool not = false;

            for (int i = 1; i <= permutation.Length; i++)
            {
                for (int j = 0; j < permutation.Length; j++)
                {
                    if (j == 0)
                        not = false;
                    if (permutation[j] == i)
                        not = true;
                    if (j == (permutation.Length - 1) && !not)
                        return false;
                }
            }
            return true;
        }

        internal static void check(int[] permutation)
        {
            if (!HelpFunctions.checkOnePermutation(permutation)) throw new Exception("Bad format of permutation.");
        }

        internal static int countElements(int[][] permutation)
        {
            int count = 0;
            for (int i = 0; i < permutation.Length; i++)
                for (int j = 0; j < permutation[i].Length; j++)
                    count++;
            return count;
        }
        internal static int[] vectorFromNumber(int number, int n) //wektor inwersji z numeru i liczebności zbioru
        {
            if ((number > MathFunctions.factorial(n)) || number < 0 || n < 2) throw new Exception("Bad value of typed data.");

            int[] vectorOfInversion = new int[n];
            int factorial;
            n--;
            for (int i = 0; i < vectorOfInversion.Length; i++)
            {
                factorial = MathFunctions.factorial(n);
                vectorOfInversion[i] = number / factorial;
                number = number % factorial;
                n--;
            }

            return vectorOfInversion;
        }

        internal static int[] vectorToPermutation(int[] vectorOfInversions)
        {
            if (!checkVectorOfInversion(vectorOfInversions)) throw new Exception("Bad format of vector.");

            int[] indentityOfPermutation = createIdentityPermutation(vectorOfInversions.Length);
            bool[] logicArray = new bool[vectorOfInversions.Length]; // false-nieużyta, true-użyta
            int[] permutation = new int[vectorOfInversions.Length];

            for (int i = 0; i < permutation.Length; i++)
            {
                int number = vectorOfInversions[i], index = 0;

                while (number != 0)
                {
                    if (logicArray[index])
                        index++;
                    else
                    {
                        index++;
                        number--;
                    }
                }
                while (logicArray[index])
                    index++;
                permutation[i] = indentityOfPermutation[index];
                logicArray[index] = true;
            }

            return permutation;
        }

        internal static bool checkTwoPermutations(int[] permutation1, int[] permutation2)
        {
            if (permutation1.Length == permutation2.Length)
                if (HelpFunctions.checkOnePermutation(permutation1))
                    if (HelpFunctions.checkOnePermutation(permutation2)) return true;
            return false;
        }

        internal static bool checkCyclePermutation(int[][] permutation)
        {
            int length = countElements(permutation);
            bool[] tab = new bool[length];
            for (int i = 0; i < length; i++)
                tab[i] = false;
            try
            {
                for (int i = 0; i < permutation.Length; i++)
                    for (int j = 0; j < permutation[i].Length; j++)
                        tab[permutation[i][j] - 1] = true;
            }
            catch (Exception e)
            {
                return false;
            }

            for (int i = 0; i < tab.Length; i++)
            {
                if (!tab[i]) return false;
            }

            return true;
        }

        internal static int inversionsCount(int[] permutation)
        {
            check(permutation);
            int count = 0;
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i + 1; j < permutation.Length; j++)
                {
                    if (permutation[j] < permutation[i]) count++;
                }
            }

            return count;
        }

        internal static  int[] createIdentityPermutation(int n)
        {
            if (n < 2) throw new Exception("Bad number elements of permutation.");

            int[] identityPermutation = new int[n];

            for (int i = 0; i < n; i++)
                identityPermutation[i] = i + 1;

            return identityPermutation;
        }
        internal static  bool checkVectorOfInversion(int[] vector)
        {
            int max = vector.Length - 1;

            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > max) return false;
                max--;
            }

            return true;
        }
        internal static int[] nextElementLexicographical(int[] previousPermutation)
        {
            int[] returnedPermutation = new int[previousPermutation.Length];
            int index = ArrayFunctions.decreasingSequence(previousPermutation); //index przechowuje indeks pierwszego elementu ciągu malejącego w tablicy

            if (index == 0)
            {
                returnedPermutation = ArrayFunctions.reverseArray(createIdentityPermutation(previousPermutation.Length));
            }
            else
            {
                int indexMin;
                List<int> list = new List<int>();
                for (int j = previousPermutation.Length - 1; ; j--)
                    if (previousPermutation[j] > previousPermutation[index - 1])
                    {
                        indexMin = j;
                        break;
                    }


                for (int j = 0; j < index - 1; j++)
                    list.Add(previousPermutation[j]);
                list.Add(previousPermutation[indexMin]);

                List<int> toReverse = new List<int>();

                for (int j = index; j < previousPermutation.Length; j++)
                {

                    if (j != indexMin)
                        toReverse.Add(previousPermutation[j]);
                    else
                        toReverse.Add(previousPermutation[index - 1]);
                }

                List<int> reversedList = ArrayFunctions.reversedList(toReverse);

                foreach (int j in reversedList)
                    list.Add(j);

                returnedPermutation = list.ToArray();


            }

            return returnedPermutation;
        }
        internal static int[] nextElementAntilexicographical(int[] previousPermutation)
        {
            int[] tempPermutation = new int[previousPermutation.Length];
            for (int i = 0; i < previousPermutation.Length; i++)
                tempPermutation[i] = previousPermutation[i];
            int[] returnedPermutation = new int[tempPermutation.Length];
            int index = 0;
            for (int i = 0; i < tempPermutation.Length - 1; i++)
            {
                if (tempPermutation[i] < tempPermutation[i + 1])
                {
                    index = i + 1;
                    break;
                }
            }
            int indexOfMin = 0;
            for (int i = 0; i < index; i++)
            {
                if (tempPermutation[index] > tempPermutation[i])
                {
                    indexOfMin = i;
                    break;
                }
            }

            int temp = tempPermutation[indexOfMin];
            tempPermutation[indexOfMin] = tempPermutation[index];
            tempPermutation[index] = temp;
            int counter = 0;
            for (int i = index - 1; i >= 0; i--)
            {
                returnedPermutation[counter] = tempPermutation[i];
                counter++;
            }
            for (int i = index; i < tempPermutation.Length; i++)
            {
                returnedPermutation[counter] = tempPermutation[i];
                counter++;
            }
            return returnedPermutation;
        }
        internal static bool isAnyIdentity(int[][] permutations)
        {
            int[] e = createIdentityPermutation(permutations[0].Length);
            for (int i = 0; i < permutations.Length; i++)
                if (ArrayFunctions.compareIntArrays(permutations[i], e))
                    return true;
            return false;
        }
        internal static bool isAnyReverse(int[][] permutations)
        {
            for (int i = 0; i < permutations.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < permutations.Length; j++)
                {
                    if (!flag)
                    {
                        int[] reverse = WithoutRepetition.ReversePermutation(permutations[j]);
                        if (ArrayFunctions.compareIntArrays(permutations[i], reverse))
                            flag = true;
                    }
                }
                if (!flag)
                    return false;
            }
            return true;
        }
        internal static bool isAnyComposition(int[][] permutations)
        {
            for (int i = 0; i < permutations.Length; i++)
            {
                for (int j = 0; j < permutations.Length; j++)
                {
                    int[] composition = WithoutRepetition.CompositionOfPermutation(permutations[i], permutations[j]);
                    bool flag = false;
                    for (int k = 0; k < permutations.Length; k++)
                    {
                        if (!flag)
                        {
                            if (ArrayFunctions.compareIntArrays(composition, permutations[k]))
                            {
                                flag = true;
                            }
                        }
                    }
                    if (!flag)
                        return false;
                }
            }
            return true;
        }
        internal static int[] antiInversionVector(int[] permutation)
        {
            check(permutation);
            int[] vector = new int[permutation.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = 0;
            }
            for (int i = 0; i < permutation.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (permutation[j] > permutation[i]) vector[i]++;
                }
            }

            return vector;
        }
        internal static int[] antiVectorFromNumber(int number, int n) //antywektor inwersji z numeru i liczebności zbioru
        {
            int[] inversionVector = vectorFromNumber(number, n);
            int[] antiInversionVector = new int[n];
            int index = n - 1;
            for (int i = 0; i < antiInversionVector.Length; i++)
            {
                antiInversionVector[i] = inversionVector[index];
                index--;
            }
            return antiInversionVector;
        }
        internal static int[] antiVectorToPermutation(int[] antiVectorOfInversions)
        {
            int[] indentityOfPermutation = createIdentityPermutation(antiVectorOfInversions.Length);
            bool[] logicArray = new bool[antiVectorOfInversions.Length]; // false-nieużyta, true-użyta
            int[] permutation = new int[antiVectorOfInversions.Length];

            for (int i = permutation.Length - 1; i >= 0; i--)
            {
                int number = antiVectorOfInversions[i], index = permutation.Length - 1;

                while (number != 0)
                {
                    if (logicArray[index])
                        index--;
                    else
                    {
                        index--;
                        number--;
                    }
                }
                while (logicArray[index])
                    index--;
                permutation[i] = indentityOfPermutation[index];
                logicArray[index] = true;
            }

            return permutation;
        }
        internal static int[] makeTransposition(int a, int b, int length)
        {
            int[] permutation = createIdentityPermutation(length);
            permutation[a - 1] = b;
            permutation[b - 1] = a;
            return permutation;
        }
        internal static bool isIdentity(int[] permutation)
        {
            int[] identity = createIdentityPermutation(permutation.Length);
            if (ArrayFunctions.compareIntArrays(identity, permutation))
                return true;
            return false;
        }
    }
}
