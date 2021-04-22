namespace WechatMessageMerageDemo.Helpers
{
    public static class KMPHeler
    {
        public static bool Equals(string source, string target)
        {
            int sourceIndex, targetIndex;
            int[] nextTargetIndex = new int[target.Length];
            GetNextValue(target, nextTargetIndex);
            sourceIndex = 0;
            targetIndex = 0;
            while (sourceIndex < source.Length && targetIndex < target.Length)
            {
                if (targetIndex == -1 || source[sourceIndex] == target[targetIndex])
                {
                    ++sourceIndex;
                    ++targetIndex;
                }
                else
                {
                    targetIndex = nextTargetIndex[targetIndex];
                }
            }
            return targetIndex == target.Length;
        }

        static void GetNextValue(string target, int[] nextTargetIndex)
        {
            int index = 0;
            int reverseIndex = -1;
            nextTargetIndex[0] = -1;
            while (index < target.Length - 1)
            {
                if (reverseIndex == -1 || target[index] == target[reverseIndex])
                {
                    index++;
                    reverseIndex++;
                    nextTargetIndex[index] = reverseIndex;
                }
                else
                {
                    reverseIndex = nextTargetIndex[reverseIndex];
                }
            }
        }
    }
}
