// Dan Glaubitz
// 
// Testing GetUniqueElement - straightforward method utilizing Dictionary 
// against
// GetUniqueElementBySort - created out of curiosity

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace UniqueElement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\nTwo methods - each will run 30 times against random values and the average time it takes in milliseconds for each iteration " +
                              "will be measured.\nKeep in mind there is a lot of unmeasured time spent compiling each random list that is to be tested against.\nThe numbers don't lie.\n\n\n");
            int[] containsOneUnique;
            int n = 100000;
            decimal timeSum = 0m;
            decimal time;

            int x = 1;
            // sorting array then finding the first value that is non-duplicate adjacent
            while (x <= 30)
            {
                containsOneUnique = GetPairsOneUnique(out int unique, n);
                Stopwatch watch = new Stopwatch();

                watch.Start();
                var answer = GetUniqueElementBySort(containsOneUnique, n);
                watch.Stop();
                time = (decimal)watch.Elapsed.TotalMilliseconds;
                Console.WriteLine("\n");
                Console.WriteLine(x + ": SORT METHOD - GetUniqueElementBySort()");
                Console.WriteLine("\tUnique Value: {0}", unique);
                Console.WriteLine("\tSort Method Out: {0}", answer);
                Console.WriteLine("\tElapsed Time (MS): " + time);
                if (unique != answer)
                {
                    Console.WriteLine("****************************************************");
                    Console.ReadLine();
                }
                timeSum += time;
                x += 1;
            }
            Console.WriteLine("\n");
            decimal sortMethodTimeAve = timeSum / 30;
            Console.WriteLine("ave time: " + sortMethodTimeAve);
            
            Console.WriteLine("\n\n\n");

            timeSum = 0m;
            x = 1;
            // using Dictionary
            while (x <= 30)
            {
                containsOneUnique = GetPairsOneUnique(out int unique2, n);
                Stopwatch watch = new Stopwatch();

                watch.Start();
                var answer = GetUniqueElement(containsOneUnique, n);
                watch.Stop();
                time = (decimal)watch.Elapsed.TotalMilliseconds;

                Console.WriteLine("\n");
                Console.WriteLine(x + ": DICTIONARY METHOD - GetUniqueElement()");
                Console.WriteLine("\tUnique Value: {0}", unique2);
                Console.WriteLine("\tDictionary Method Out: {0}", answer);
                Console.WriteLine("\tElapsed Time (MS): " + time);
                if (unique2 != answer)
                {
                    Console.WriteLine("****************************************************");
                    Console.ReadLine();
                }
                timeSum += time;
                x += 1;
            }
            Console.WriteLine("\n");
            decimal dictMethodTimeAve = timeSum / 30;
            Console.WriteLine("ave: " + dictMethodTimeAve);
            
            Console.WriteLine("\n\n\nSORT METHOD - GetUniqueElementBySort() average time: {0}ms", sortMethodTimeAve);
            Console.WriteLine("DICTIONARY METHOD - GetUniqueElement() average time: {0}ms", dictMethodTimeAve);
        }

        // dictionary method
        static int GetUniqueElement(int[] containsOneUnique, int n)
        {
            Dictionary<int,int> findUnique = new Dictionary<int,int>();
            int nFormula = n * 2 + 1;

            for (int x = 0; x < nFormula; x++)
            {
                int key = containsOneUnique[x];
                if (findUnique.ContainsKey(key))
                {
                    findUnique[key] += 1;
                }
                else
                {
                    findUnique.Add(key, 1);
                }
            }
            var uniqueKeyValue = findUnique.First(x => x.Value == 1);
            return uniqueKeyValue.Key;
        }

        // Array sort duplicate non-adjacent method
        static int GetUniqueElementBySort(int[] containsOneUnique, int n)
        {
            Array.Sort(containsOneUnique);
            int nFormula = n * 2 + 1;
            
            if(containsOneUnique[0] != containsOneUnique[1])
            {
                return containsOneUnique[0];
            }
            for (int x = 2; x < (nFormula - 1); x++)
            {
                if(containsOneUnique[x] != containsOneUnique[x + 1] && containsOneUnique[x] != containsOneUnique[x - 1])
                {
                    return containsOneUnique[x];
                }
            }
            return containsOneUnique[nFormula - 1];
        }

        // seeding test by creating array of random numbers then randomizing their order
        static int[] GetPairsOneUnique(out int unique, int n)
        {
            int ranRange = (int)Math.Pow(10, 9) + 1;
            int pairsOneUniqueLength = n * 2 + 1;

            Random ranNum = new Random();

            int[] containsOneUnique = new int[n * 2 + 1];
            int rando;

            for (int x = 0; x < pairsOneUniqueLength - 1; x += 2)
            {
                do
                {
                    rando = ranNum.Next(ranRange);
                } while (containsOneUnique.Contains(rando));
                containsOneUnique[x] = rando;
                containsOneUnique[x + 1] = rando;
            }

            do
            {
                rando = ranNum.Next(ranRange);
            } while (containsOneUnique.Contains(rando));
            containsOneUnique[pairsOneUniqueLength - 1] = rando;

            unique = containsOneUnique[pairsOneUniqueLength - 1];

            MakeRandom(containsOneUnique);

            return containsOneUnique;
        }

        // shuffle the array order
        public static void MakeRandom(int[] toRandom)
        {
            Random rand = new Random();

            for (int x = 0; x < toRandom.Length - 1; x++)
            {
                int y = rand.Next(x, toRandom.Length);
                int temp = toRandom[x];
                toRandom[x] = toRandom[y];
                toRandom[y] = temp;
            }
        }
    }
}
