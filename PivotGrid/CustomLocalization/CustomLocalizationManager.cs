using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace CustomLocalization
{
    public class CustomLocalizationManager : LocalizationManager
    {
        private IDictionary<string, string> dictionary;

        public CustomLocalizationManager()
        {
            this.dictionary = new Dictionary<string, string>();

            this.dictionary["Pivot_Row"] = "Row";
            this.dictionary["Pivot_Column"] = "Column";
            this.dictionary["Pivot_Value"] = "Value: {0}";

            this.dictionary["PivotFieldList_ShowItemsForWhichTheLabel"] = "Show items for which the label";
            this.dictionary["PivotFieldList_And"] = "and";
            this.dictionary["PivotFieldList_AscendingBy"] = "Ascending (A to Z) by:";
            this.dictionary["PivotFieldList_BaseField"] = "Base field:";
            this.dictionary["PivotFieldList_BaseItem"] = "Base item:";
            this.dictionary["PivotFieldList_By"] = "by:";
            this.dictionary["PivotFieldList_ChooseAggregateFunction"] = "Choose the type of calculation that you want to use to summarize data from the selected field.";
            this.dictionary["PivotFieldList_ChooseFieldsToAddToReport"] = "Choose fields to add to report:";
            this.dictionary["PivotFieldList_ColumnLabels"] = "Column Labels";
            this.dictionary["PivotFieldList_DeferLayoutUpdate"] = "Defer Layout Update";
            this.dictionary["PivotFieldList_DescendingBy"] = "Descending (Z to A) by:";
            this.dictionary["PivotFieldList_DragFieldsBetweenAreasBelow"] = "Drag fields between areas below:";
            this.dictionary["PivotFieldList_Format"] = "Format:";
            this.dictionary["PivotFieldList_GeneralFormat"] = "General Format";
            this.dictionary["PivotFieldList_IgnoreCase"] = "Ignore Case";
            this.dictionary["PivotFieldList_PleaseRefreshThePivot"] = "Please refresh the pivot.";
            this.dictionary["PivotFieldList_Refresh"] = "Refresh";
            this.dictionary["PivotFieldList_ReportFilter"] = "Report Filter";
            this.dictionary["PivotFieldList_RowLabels"] = "Row Labels";
            this.dictionary["PivotFieldList_SelectItem"] = "Select Item";
            this.dictionary["PivotFieldList_Show"] = "Show";
            this.dictionary["PivotFieldList_ShowItemsForWhich"] = "PivotFieldList_ShowItemsForWhich";
            this.dictionary["PivotFieldList_ShowValuesAs"] = "Show Values As";
            this.dictionary["PivotFieldList_SortOptions"] = "Sort options";
            this.dictionary["PivotFieldList_StringFormatDescription"] = "The format should identify the measurement type of the value. The format would be used for general computations such as Sum, Average, Min, Max and others.";
            this.dictionary["PivotFieldList_SummarizeValuesBy"] = "Summarize Values By";
            this.dictionary["PivotFieldList_TheActionRequiresMoreRecentInformation"] = "The action requires more recent information.";
            this.dictionary["PivotFieldList_Update"] = "Update";
            this.dictionary["PivotFiledList_Values"] = "Values";

            this.dictionary["Ok"] = "Ok";
            this.dictionary["Cancel"] = "Cancel";

            this.dictionary["PivotFieldList_SetSumAggregate"] = "Sum";
            this.dictionary["PivotFieldList_SetCountAggregate"] = "Count";
            this.dictionary["PivotFieldList_SetAverageAggregate"] = "Average";
            this.dictionary["PivotFieldList_SetIndexTotalFormat"] = "Index";
            this.dictionary["PivotFieldList_SetPercentOfGrandTotalFormat"] = "% of Grand Total";
            this.dictionary["PivotFieldList_SortAtoZ"] = "Sort A to Z";
            this.dictionary["PivotFieldList_SortZtoA"] = "Sort Z to A";
            this.dictionary["PivotFieldList_MoreSortingOptions"] = "More Sorting Options...";
            this.dictionary["PivotFieldList_LabelFilter"] = "Label Filter";
            this.dictionary["PivotFieldList_ValueFilter"] = "Value Filter";
            this.dictionary["PivotFieldList_TopTenFilter"] = "Top 10 Filter";
            this.dictionary["PivotFieldList_ClearFilter"] = "Clear Filter";
            this.dictionary["PivotFieldList_ShowEmptyGroups"] = "Show Empty Groups";
            this.dictionary["PivotFieldList_SelectItems"] = "Select Items";
            this.dictionary["PivotFieldList_MoreAggregateOptions"] = "More Aggregate Options...";
            this.dictionary["PivotFieldList_MoreCalculationOptions"] = "More Calculation Options...";
            this.dictionary["PivotFieldList_ClearCalculations"] = "Clear Calculations";
            this.dictionary["PivotFieldList_NumberFormat"] = "Number Format";

            this.dictionary["Pivot_GrandTotal"] = "Grand Total";
            this.dictionary["Pivot_Values"] = "Values";
            this.dictionary["Pivot_Error"] = "Error";
            this.dictionary["Pivot_TotalP0"] = "Total {0}";
            this.dictionary["Pivot_P0Total"] = "{0} Total";

            this.dictionary["Pivot_GroupP0AggregateP1"] = "{0} {1}";

            this.dictionary["Pivot_AggregateP0ofP1"] = "{0} of {1}";

            this.dictionary["Pivot_AggregateSum"] = "Sum";
            this.dictionary["Pivot_AggregateCount"] = "Count";
            this.dictionary["Pivot_AggregateAverage"] = "Average";
            this.dictionary["Pivot_AggregateMin"] = "Min";
            this.dictionary["Pivot_AggregateMax"] = "Max";
            this.dictionary["Pivot_AggregateProduct"] = "Product";
            this.dictionary["Pivot_AggregateVar"] = "Var";
            this.dictionary["Pivot_AggregateVarP"] = "VarP";
            this.dictionary["Pivot_AggregateStdDev"] = "StdDev";
            this.dictionary["Pivot_AggregateStdDevP"] = "StdDevP";

            this.dictionary["Pivot_HourGroup"] = "{0} - Hour";
            this.dictionary["Pivot_MinuteGroup"] = "{0} - Minute";
            this.dictionary["Pivot_MonthGroup"] = "{0} - Month";
            this.dictionary["Pivot_QuarterGroup"] = "{0} - Quarter";
            this.dictionary["Pivot_SecondGroup"] = "{0} - Second";
            this.dictionary["Pivot_WeekGroup"] = "{0} - Week";
            this.dictionary["Pivot_YearGroup"] = "{0} - Year";
            this.dictionary["Pivot_DayGroup"] = "{0} - Day";

            this.dictionary["PivotFieldList_PercentOfColumnTotal"] = "% of Column Total";
            this.dictionary["PivotFieldList_PercentOfRowTotal"] = "% of Row Total";
            this.dictionary["PivotFieldList_PercentOfGrandTotal"] = "% of Grand Total";
            this.dictionary["PivotFieldList_NoCalculation"] = "No Calculation";
            this.dictionary["PivotFieldList_DifferenceFrom"] = "Difference From";
            this.dictionary["PivotFieldList_PercentDifferenceFrom"] = "% Difference From";
            this.dictionary["PivotFieldList_PercentOf"] = "% Of";
            this.dictionary["PivotFieldList_RankSmallestToLargest"] = "Rank Smallest to Largest";
            this.dictionary["PivotFieldList_RankLargestToSmallest"] = "Rank Largest to Smallest";
            this.dictionary["PivotFieldList_PercentRunningTotalIn"] = "% Running Total In";
            this.dictionary["PivotFieldList_RunningTotalIn"] = "Running Total In";
            this.dictionary["PivotFieldList_Index"] = "Index";

            this.dictionary["PivotFieldList_RelativeToPrevious"] = "(previous)";
            this.dictionary["PivotFieldList_RelativeToNext"] = "(next)";

            this.dictionary["PivotFieldList_ConditionEquals"] = "equals";
            this.dictionary["PivotFieldList_DoesNotEqual"] = "does not equal";
            this.dictionary["PivotFieldList_IsGreaterThan"] = "is greater than";
            this.dictionary["PivotFieldList_IsGreaterThanOrEqualTo"] = "is greater than or equal to";
            this.dictionary["PivotFieldList_IsLessThan"] = "is less than";
            this.dictionary["PivotFieldList_IsLessThanOrEqualTo"] = "is less than or equal to";
            this.dictionary["PivotFieldList_BeginsWith"] = "begins with";
            this.dictionary["PivotFieldList_DoesNotBeginWith"] = "does not begin with";
            this.dictionary["PivotFieldList_EndsWith"] = "ends with";
            this.dictionary["PivotFieldList_DoesNotEndWith"] = "does not end with";
            this.dictionary["PivotFieldList_Contains"] = "contains";
            this.dictionary["PivotFieldList_DoesNotContains"] = "does not contain";
            this.dictionary["PivotFieldList_IsBetween"] = "is between";
            this.dictionary["PivotFieldList_IsNotBetween"] = "is not between";

            this.dictionary["PivotFieldList_SelectAll"] = "(Select All)";

            this.dictionary["PivotFieldList_Top10Items"] = "Items";
            this.dictionary["PivotFieldList_Top10Percent"] = "Percent";
            this.dictionary["PivotFieldList_Top10Sum"] = "Sum";

            this.dictionary["PivotFieldList_TopItems"] = "Top";
            this.dictionary["PivotFieldList_BottomItems"] = "Bottom";

            this.dictionary["PivotFieldList_FilterItemsP0"] = "Fitler Items ({0})";
            this.dictionary["PivotFieldList_LabelFilterP0"] = "Label Filter ({0})";
            this.dictionary["PivotFieldList_SortP0"] = "Sort ({0})";
            this.dictionary["PivotFieldList_FormatCellsP0"] = "Format Cells ({0})";
            this.dictionary["PivotFieldList_ValueSummarizationP0"] = "Value Summarization ({0})";
            this.dictionary["PivotFieldList_Top10FilterP0"] = "Top10 Filter ({0})";
            this.dictionary["PivotFieldList_ShowValuesAsP0"] = "Show Values As ({0})";
            this.dictionary["PivotFieldList_ValueFilterP0"] = "Value Filter ({0})";

            this.dictionary["Pivot_CalculatedFields"] = "Calculated Fields";
            this.dictionary["PivotFieldList_ItemFilterConditionCaption"] = "Show items with value that";
            this.dictionary["PivotFieldList_None"] = "Data source order";
            this.dictionary["PivotFieldList_Sort_BySortKeys"] = "by Sort Keys";
        }

        public override string GetStringOverride(string key)
        {
            string localized;

            if (this.dictionary.TryGetValue(key, out localized))
            {
                return localized;
            }
            else
            {
                return base.GetStringOverride(key);
            }
        }
    }
}
