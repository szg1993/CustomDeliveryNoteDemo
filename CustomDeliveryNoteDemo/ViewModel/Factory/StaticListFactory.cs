using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Util;

namespace ViewModel.Factory
{
    public static class StaticListFactory
    {
        /// <summary>
        /// Returns the values what can apply as a delivery note category
        /// </summary>
        /// <returns></returns>
        public static AsyncObservableCollection<string> GetCategoryList()
        {
            var categoryList = new AsyncObservableCollection<string>
            {
                "",
                "Other",
                "Quality complaint",
                "Return cargo",
                "Sample",
                "Tool test"
            };

            return categoryList;
        }

        /// <summary>
        /// Returns the values what can apply as a takeover place.
        /// </summary>
        /// <returns></returns>
        public static AsyncObservableCollection<string> GetTakeoverPlaceList()
        {
            var takeoverPlaceList = new AsyncObservableCollection<string>
            {
                "",
                "Logistics office",
                "Special storage",
                "Tooling storage"
            };

            return takeoverPlaceList;
        }

        /// <summary>
        /// Returns the values what can apply as a package scale unit.
        /// </summary>
        /// <returns></returns>
        public static AsyncObservableCollection<string> GetPkgScaleList()
        {
            var scaleList = new AsyncObservableCollection<string>
            {
                "Box",
                "Envelope",
                "Pallet"
            };

            return scaleList;
        }

        /// <summary>
        /// Returns the values what can apply as a package size unit.
        /// </summary>
        /// <returns></returns>
        public static AsyncObservableCollection<string> GetSizeUnitList()
        {
            var sizeUnitList = new AsyncObservableCollection<string>
            {
                "cm",
                "mm",
                "m"
            };

            return sizeUnitList;
        }

        /// <summary>
        /// Returns the values what can apply as a package weight unit.
        /// </summary>
        /// <returns></returns>
        public static AsyncObservableCollection<string> GetWeightUnitList()
        {
            var sizeWeightList = new AsyncObservableCollection<string>
            {
                "kg",
                "t",
            };

            return sizeWeightList;
        }
    }
}
