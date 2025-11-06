using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.LeetCode
{
    class Exercise3321 : IExercise
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
            var result = FindXSum([1000000000,1000000000,1000000000,1000000000,1000000000,1000000000], 6, 1);
            
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
            // gerar o novo indece do array de sainda
            var newIndex = nums.Length - k + 1;
            var result = new long[newIndex];
            for (int i = 0; i < result.Length; i++)
            {
                //vamos buscar os numero que mais repeti
                Dictionary<long, long> arrCount = new Dictionary<long, long>();
                for (int j = i; j < k+i; j++)
                {
                    if (!arrCount.ContainsKey(nums[j]))
                        arrCount[nums[j]] = 0;
                    arrCount[nums[j]]++;
                }
                
                // Problema de mémoria
                var sum = arrCount
                    .OrderByDescending(i => i.Value)
                    .ThenByDescending(i => i.Key)
                    .Take(x)
                    .Sum(i => i.Key *  i.Value);
                
                result[i] = sum;
            }
            return result;
        }
    }
}
