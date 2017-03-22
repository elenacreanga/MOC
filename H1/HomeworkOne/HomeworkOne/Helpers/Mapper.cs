using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace HomeworkOne.Helpers
{
    public static class Mapper
    {
        public static BitArray GetRandomSolution(Interval interval, int precission)
        {
            Constants.NrOfBitsInComponent = NumberBits(interval.LowerBound, interval.UpperBound, precission);

            var randomBitArraySolution = new BitArray(Constants.NumberOfComponentsInSolution * Constants.NrOfBitsInComponent);
            var index = 0;
            var rand = new Random();
            for (var i = 0; i < Constants.NumberOfComponentsInSolution; i++)
            {
                for (var j = 0; j < Constants.NrOfBitsInComponent; j++)
                {
                    Thread.Sleep(2);
                    randomBitArraySolution[index++] = (rand.Next(100) % 2 == 0);
                }
            }
            return randomBitArraySolution;
        }

        private static int NumberBits(double a, double b, int precission)
        {
            return (int)Math.Ceiling(Math.Log((b - a) * Math.Pow(10, precission)) / Math.Log(2));
        }

        public static BitArray AlterSolution(BitArray solutionToAlter, int index)
        {
            var alteredBitArray = solutionToAlter;

            if (alteredBitArray[index])
                alteredBitArray[index] = false;
            else
                alteredBitArray[index] = true;
            return alteredBitArray;
        }

        public static double[] GetRealRepresentationOfBits(BitArray solution, Interval interval)
        {
            var values = new double[Constants.NumberOfComponentsInSolution];
            var poz = 0;
            for (var i = 0; i < Constants.NumberOfComponentsInSolution; i++)
            {
                var value = BitArrayToDouble(solution, poz, interval.LowerBound, interval.UpperBound);
                poz += Constants.NrOfBitsInComponent;
                values[i] = value;
            }
            return values;
        }

        private static double BitArrayToDouble(BitArray solutionBitArray, int poz, double a, double b)
        {
            long longValue = 0;
            long mask = 1;
            for (var i = poz + Constants.NrOfBitsInComponent - 1; i >= poz; i--, mask <<= 1)
                if (solutionBitArray[i]) longValue |= mask;

            long max = (int)(Math.Pow(2, Constants.NrOfBitsInComponent) - 1);
            var result = (a + (longValue * (b - a) / max));
            return result;
        }

        public static void PrintBitArray(BitArray solution)
        {
            foreach (var element in solution)
            {
                Console.Write(element.Equals(true) ? 1 : 0);
            }
            Console.WriteLine();
        }

        public static void PrintBitArrayList(List<BitArray> solution)
        {
            foreach (var bitArray in solution)
            {
                foreach (var element in bitArray)
                {
                    Console.Write(element.Equals(true) ? 1 : 0);
                }
                Console.WriteLine();
            }
        }
    }
}
