public class Solution
{
    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums); // sort the array first 

        int left = 0;
        int right = nums.Length - 1;
        int operations = 0;

        while (left < right)
        {
            int sum = nums[left] + nums[right];

            if (sum > k)
            {
                right--;
            }
            else if (sum < k)
            {
                left++;
            }
            else
            {
                operations++;
                left++;
                right--;
            }
        }

        return operations;
    }
}