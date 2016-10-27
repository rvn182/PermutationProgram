using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PermutationProgram
{
    static class Functions
    {
        static public void ClearLog()
        {
            File.Create("log.txt");
        }

        static public int MaxOfArrays(int[][] array)
        {
            int max = 0;
            for (int i = 0; i < array.Length; i++)
                if(array[i].Length>0)
                if (array[i].Max() > max) max = array[i].Max();
            return max;
        }

        static public int LenghtArrayWithMax(int[][] array)
        {
            int max = 0, index=0;
            for (int i = 0; i < array.Length; i++)
                if (array[i].Length > 0)
                    if (array[i].Max() > max)
                    {
                        max = array[i].Max();
                        index = i;
                    }
            return index;
        }
    }
}
