using Automation.PageObjects.Apartments.Parts.BedsBaths;
using Automation.PageObjects.Apartments.Parts.RentRange;
using Automation.PageObjects.Apartments.Parts.Typeahead;

namespace Automation.PageObjects.Apartments.Pages.Home.Parts
{
    public class QuickSearch
    {
        public Typeahead Typeahead { get; set; }
        public RentRangeSelector RentRange { get; set; }
        public BedsBathsSelector BedsBaths { get; set; }
    }
}