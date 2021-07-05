using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Extensions
{
    public class YearRangTillDat:RangeAttribute
    {
        public YearRangTillDat(int startYear):base(startYear,DateTime.Now.Year)
        {

        }
    }
}
