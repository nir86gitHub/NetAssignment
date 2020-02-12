using FSC_BOL.Models.Common;
using System.Collections.Generic;

namespace FSC_Helpers
{
    public static class DTHelper
    {
        public static IEnumerable<string> GetOrderByClause(DataTablesRequest pageRequest)
        {
            var columns = pageRequest.ColumnNames.Split(',');

            for (var idx = 0; idx < pageRequest.SortingCols; ++idx)
            {
                yield return string.Format("{0} {1}", columns[pageRequest.SortCol[idx]], pageRequest.SortDir[idx]);
            }
        }
    }
}
