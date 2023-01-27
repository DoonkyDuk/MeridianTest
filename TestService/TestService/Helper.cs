using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestService
{
    static class Helper
    {
        public static string BuildOutputData(List<string> dataList)
        {
            List<string> firstDataList = new List<string>();
            List<string> secondDataList = new List<string>();

            int firstDataStartIndex;
            int firstDataEndIndex;
            int secondDataStartIndex;
            int secondDataEndIndex;

            foreach (var data in dataList)
            {
                firstDataStartIndex = data.IndexOf("#27") + 3;
                firstDataEndIndex = data.IndexOf(';');

                secondDataStartIndex = firstDataEndIndex + 1;
                secondDataEndIndex = data.IndexOf("#91");

                firstDataList.Add(data.Substring(firstDataStartIndex, firstDataEndIndex - firstDataStartIndex));
                secondDataList.Add(data.Substring(secondDataStartIndex, secondDataEndIndex - secondDataStartIndex));
            }

            StringBuilder result = new StringBuilder("#90#010102#27");

            if (firstDataList.Any(x => x != firstDataList.First()))
            {
                result.Append("NoRead;");
            }
            else
            {
                result.Append($"{firstDataList.First()};");
            }

            if (secondDataList.Any(x => x != secondDataList.First()))
            {
                result.Append("NoRead;");
            }
            else
            {
                result.Append($"{secondDataList.First()}#91");
            }

            return result.ToString();
        }
    }
}
