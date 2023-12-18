using System;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    [Flags]
    public enum eVoteChoice
    {
        UNDETERMINED = -1,
        NONE = 0,
        FIRST = 1 << 0,
        SECOND = 1 << 1,
        THIRD = 1 << 2,
        FIRST_SECOND = FIRST | SECOND,
        FIRST_THIRD = FIRST | THIRD,
        SECOND_THIRD = SECOND | THIRD,
        ALL = FIRST | SECOND | THIRD
    }

    public static class VoteCalculator
    {
        public static eVoteChoice CalculateVoteChoice(List<int> arr)
        {
            if (arr.Count != 3)
            {
                return eVoteChoice.UNDETERMINED;
            }

            if (arr.TrueForAll(val => val == 0))
            {
                return eVoteChoice.NONE;
            }
            else if (arr[0] == 1 && arr[1] == 0 && arr[2] == 0)
            {
                return eVoteChoice.FIRST;
            }
            else if (arr[0] == 0 && arr[1] == 1 && arr[2] == 0)
            {
                return eVoteChoice.SECOND;
            }
            else if (arr[0] == 0 && arr[1] == 0 && arr[2] == 1)
            {
                return eVoteChoice.THIRD;
            }
            else if (arr[0] == 1 && arr[1] == 1 && arr[2] == 0)
            {
                return eVoteChoice.FIRST_SECOND;
            }
            else if (arr[0] == 1 && arr[1] == 0 && arr[2] == 1)
            {
                return eVoteChoice.FIRST_THIRD;
            }
            else if (arr[0] == 0 && arr[1] == 1 && arr[2] == 1)
            {
                return eVoteChoice.SECOND_THIRD;
            }
            else if (arr.TrueForAll(val => val == 1))
            {
                return eVoteChoice.ALL;
            }
            else
            {
                return eVoteChoice.UNDETERMINED;
            }
        }
    }
}
