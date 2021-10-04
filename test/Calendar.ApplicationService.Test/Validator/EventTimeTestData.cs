using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Endpoints.WebAPI.Test.Validator
{
    class EventTimeTestData
    {
        public static IEnumerable<object[]> InvalidTestData
        {
            get
            {
                yield return new object[]
                {
                    DateTime.Now.AddDays(180).AddDays(1)
                };
                yield return new object[]
                {
                    DateTime.Now.AddDays(-1)
                };
            }
        }

        public static IEnumerable<object[]> ValidTestData
        {
            get
            {
                yield return new object[]
                {
                    DateTime.Now.AddYears(180)
                };
                yield return new object[]
                {
                    DateTime.Now
                };
            }
        }
    }
}
