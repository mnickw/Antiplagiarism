using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            int[,] opt = new int[first.Count + 1, second.Count + 1];
            for (int i = 0; i < first.Count; i++)
                opt[i, 0] = 0;
            for (int j = 0; j < second.Count; j++)
                opt[0, j] = 0;
            for (int i = 1; i <= first.Count; i++)
                for (int j = 1; j <= second.Count; j++)
                {
                    if (first[i - 1] == second[j - 1])
                        opt[i, j] = opt[i - 1, j - 1] + 1;
                    else
                        opt[i, j] = Math.Max(opt[i, j - 1], opt[i - 1, j]);
                }
            return BacktrackOpt(opt, first, second, first.Count, second.Count);
        }

        private static List<string> BacktrackOpt(int[,] opt, List<string> first, List<string> second, int x, int y)
        {
            if (x == 0 | y == 0)
                return new List<string>();
            if (first[x - 1] == second[y - 1])
            {
                // Здесь используется рекурсия, которую не тривиально заменить на цикл
                var result = BacktrackOpt(opt, first, second, x - 1, y - 1);
                result.Add(first[x - 1]);
                return result;
            }
            if (opt[x, y - 1] > opt[x - 1, y])
                return BacktrackOpt(opt, first, second, x, y - 1);
            return BacktrackOpt(opt, first, second, x - 1, y);
        }
    }
}