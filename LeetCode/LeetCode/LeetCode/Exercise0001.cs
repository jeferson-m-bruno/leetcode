using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.LeetCode
{
    class Exercise0001 : IExercise
    {
        /*
            Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
            You may assume that each input would have exactly one solution, and you may not use the same element twice.
            You can return the answer in any order.             

            Example 1:

            Input: nums = [2,7,11,15], target = 9
            Output: [0,1]
            Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
            Example 2:

            Input: nums = [3,2,4], target = 6
            Output: [1,2]
            Example 3:

            Input: nums = [3,3], target = 6
            Output: [0,1]
             

            Constraints:

            2 <= nums.length <= 104
            -109 <= nums[i] <= 109
            -109 <= target <= 109
            Only one valid answer exists.
             

            Follow-up: Can you come up with an algorithm that is less than O(n2) time complexity?
         */

        public bool Execute()
        {
            var result = TwoSum([2,7,11,15], 4);
            foreach (var i in result)
            {
                Console.WriteLine(i.ToString());      
            }      
            return true;
        }
        
        // essa validação somente funciona se não existir valores duplicados
        public int[] TwoSum(int[] nums, int target)
        {
            // Gerar uma array com o valor como indice e a posição como valor
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                dict.Add(nums[i], i);
            }

            for (int i = 0; i < dict.Count; i++)
            {
                // subtrai do objetivo o restante do valor que se deseja encontrar
                var complement = target - nums[i];
                // pesquise no indice o valor e verifique se a posição do valor no array original não é a mesma
                if (dict.ContainsKey(complement) && dict[complement] != i)
                {
                    // caso o valor esteja no indice do array gerado e o mesmo não ocupe a mesma posição do original
                    // retorna as duas posições
                    return [i, dict[complement]];
                }
            }
            return [-1, -1];
        }
    }
}
