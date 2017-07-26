﻿namespace Task_3
{
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var dimensions = Console.ReadLine().Split('x').Select(int.Parse).ToArray();
            var row = dimensions[0];
            var col = dimensions[1];
            var denMap = new string[row,col];
            //TODO: NOTE: denMap - might be better to be jagged-array!

            for (int r = 0; r < row; r++)
            {
                var imprintRow = Console.ReadLine();
                for (int c = 0; c < col; c++)
                {
                    denMap[r, c] = imprintRow[c].ToString();
                }
            }

            var directions = Console.ReadLine().Split(',');

////            Utility.ConsolePrint(directions,denMap);

            int[] startPosition = { 0, 0 };
            for (int c = 0; c < col; c++)
            {
                if (denMap[0, c] == "s") startPosition[1] = c;
            }

            var message = string.Empty;
            var snakeLength = 3;
            var moveCount = 0;
            var snakeCurrentPosition = startPosition;
            var shouldContinue = true;
            do
            {
                snakeCurrentPosition = ExecuteMove(snakeCurrentPosition, directions[moveCount]);

                if ((moveCount + 1) % 5 == 0) 
                {
                    snakeLength--;
                }

                if (snakeLength <= 0)
                {
                    message = string.Format("Snacky will starve at [{0},{1}]",snakeCurrentPosition[0],snakeCurrentPosition[1]);
                    shouldContinue = false;
                    break;
                }
                if (snakeCurrentPosition[0] >= denMap.GetLength(0))
                {
                    message = string.Format("Snacky will be lost into the depths with length {0}", snakeLength);
                    shouldContinue = false;
                    break;
                }

                // left to right COL switchup => %
//                snakeCurrentPosition = CheckForColLoop(snakeCurrentPosition,denMap);
                snakeCurrentPosition = CheckForRightLeftLoop(snakeCurrentPosition, denMap);
                // in den moving & den-morphing
                switch (denMap[snakeCurrentPosition[0],snakeCurrentPosition[1]]) // out of range - 5
                {
                    case ".": break;
                    case "*": snakeLength++;
                        denMap[snakeCurrentPosition[0], snakeCurrentPosition[1]] = ".";
                        break;
                    case "#": message = string.Format("Snacky will hit a rock at [{0},{1}]",snakeCurrentPosition[0],snakeCurrentPosition[1]);
                        shouldContinue = false;
                        break;
                    case "s": message = string.Format("Snacky will get out with length {0}",snakeLength);
                        shouldContinue = false;
                        break;

                }
            }
            while ((directions.Length > ++moveCount) && shouldContinue); // movecount out of bound

            if (string.IsNullOrEmpty(message))
            {
                message = string.Format("Snacky will be stuck in the den at [{0},{1}]", snakeCurrentPosition[0], snakeCurrentPosition[1]);
            }

            Console.WriteLine(message);
        }

        private static int[] ExecuteMove(int[] position,string direction)
        {
            switch (direction)
            {
                case "d": position[0]++; break;
                case "u": position[0]--; break;
                case "r": position[1]++; break; // Math.Abs(position[1] % denMap.GetLength(1))
                case "l": position[1]--; break; // left to right COL switchup => %
            }
            return position;
        }

        private static int[] CheckForColLoop(int[] snakeCurrentPosition, string[,] denMap)
        {
            snakeCurrentPosition[1] = Math.Abs(snakeCurrentPosition[1] % denMap.GetLength(1));
            return snakeCurrentPosition;
        }

        private static int[] CheckForRightLeftLoop(int[] snakeCurrentPosition, string[,] denMap)
        {
            if (snakeCurrentPosition[1] < 0)
            {
                snakeCurrentPosition[1] = denMap.GetLength(1) - 1;
            }
            else if (snakeCurrentPosition[1] >= denMap.GetLength(1))
            {
                snakeCurrentPosition[1] = 0;
            }
            return snakeCurrentPosition;
        }
    }
}