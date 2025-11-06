using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.LeetCode.Optimizate
{
    class Exercise3321_O : IExercise
    {
        /*
            You are given an array nums of n integers and two integers k and x.

            The x-sum of an array is calculated by the following procedure:

            Count the occurrences of all elements in the array.
            Keep only the occurrences of the top x most frequent elements. If two elements have the same number of occurrences, the element with the bigger value is considered more frequent.
            Calculate the sum of the resulting array.
            Note that if an array has less than x distinct elements, its x-sum is the sum of the array.

            Return an integer array answer of length n - k + 1 where answer[i] is the x-sum of the subarray nums[i..i + k - 1].



            Example 1:

            Input: nums = [1,1,2,2,3,4,2,3], k = 6, x = 2

            Output: [6,10,12]

            Explanation:

            For subarray [1, 1, 2, 2, 3, 4], only elements 1 and 2 will be kept in the resulting array. Hence, answer[0] = 1 + 1 + 2 + 2.
            For subarray [1, 2, 2, 3, 4, 2], only elements 2 and 4 will be kept in the resulting array. Hence, answer[1] = 2 + 2 + 2 + 4. Note that 4 is kept in the array since it is bigger than 3 and 1 which occur the same number of times.
            For subarray [2, 2, 3, 4, 2, 3], only elements 2 and 3 are kept in the resulting array. Hence, answer[2] = 2 + 2 + 2 + 3 + 3.
            Example 2:

            Input: nums = [3,8,7,8,7,5], k = 2, x = 2

            Output: [11,15,15,15,12]

            Explanation:

            Since k == x, answer[i] is equal to the sum of the subarray nums[i..i + k - 1].
         */

        public bool Execute()
        {
            var result = FindXSum([1, 1, 2, 2, 3, 4, 2, 3], 6, 2);

            StringBuilder sb = new StringBuilder();
            foreach (var i in result)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(i);
            }

            Console.WriteLine($"[{sb.ToString()}]");
            return true;
        }

        // essa validação somente funciona se não existir valores duplicados
        public long[] FindXSum(int[] nums, int k, int x)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < k; i++)
            {
                dict.TryGetValue(nums[i], out var cnt);
                dict[nums[i]] = cnt + 1;
            }

            var comparer = Comparer<(int, int)>.Create((a, b) =>
                a.Item2 == b.Item2
                    ? b.Item1.CompareTo(a.Item1)
                    : b.Item2.CompareTo(a.Item2)
            );
            SortedSet<(int, int)> 
                set1 = new SortedSet<(int, int)>(comparer),
                set2 = new SortedSet<(int, int)>(comparer);
            foreach (var pair in dict)
            {
                set2.Add((pair.Key, pair.Value));
            }

            var sum = 0L;
            for (int i = 0; i < x; i++)
            {
                if (set2.Count == 0)
                    break;
                var t = set2.Min;
                set1.Add(t);
                set2.Remove(t);
                sum += (long)t.Item1 * t.Item2;
            }

            var n = nums.Length;
            var ret = new long[n - k + 1];
            ret[0] = sum;
            for (int i = 1; i < n - k + 1; i++)
            {
                if (nums[i - 1] == nums[i + k - 1])
                {
                    ret[i] = sum;
                    continue;
                }

                var cnt1 = dict[nums[i - 1]];
                var t1 = (nums[i - 1], cnt1);
                if (set1.Contains(t1))
                {
                    sum -= (long)t1.Item1 * t1.Item2;
                    set1.Remove(t1);
                }
                else
                {
                    set2.Remove(t1);
                }

                if (cnt1 == 1)
                {
                    dict.Remove(nums[i - 1]);
                }
                else
                {
                    set2.Add((nums[i - 1], cnt1 - 1));
                    dict[nums[i - 1]] = cnt1 - 1;
                }

                dict.TryGetValue(nums[i + k - 1], out var cnt2);
                dict[nums[i + k - 1]] = cnt2 + 1;
                if (cnt2 == 0)
                {
                    set2.Add((nums[i + k - 1], 1));
                }
                else
                {
                    var t2 = (nums[i + k - 1], cnt2);
                    if (set1.Contains(t2))
                    {
                        set1.Remove(t2);
                        set1.Add((nums[i + k - 1], cnt2 + 1));
                        sum += nums[i + k - 1];
                    }
                    else
                    {
                        set2.Remove(t2);
                        set2.Add((nums[i + k - 1], cnt2 + 1));
                    }
                }

                while (set1.Count < x && set2.Count > 0)
                {
                    var t = set2.Min;
                    set1.Add(t);
                    set2.Remove(t);
                    sum += (long)t.Item1 * t.Item2;
                }

                if (set2.Count > 0)
                {
                    var s1 = set1.Max;
                    var s2 = set2.Min;
                    if (comparer.Compare(s1, s2) > 0)
                    {
                        set2.Add(s1);
                        set1.Remove(s1);
                        sum -= (long)s1.Item1 * s1.Item2;
                        set1.Add(s2);
                        set2.Remove(s2);
                        sum += (long)s2.Item1 * s2.Item2;
                    }
                }

                ret[i] = sum;
            }

            return ret;
        }
    }
}