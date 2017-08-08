using MVCSampleUnitTestPagerView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSampleUnitTestPagerView.Data
{
    public class CricketerProfileRepo
    {
        public IQueryable<CricketerProfile> CricketerProfiles {
            get
            {
                return new List<CricketerProfile>
                    {
                        new CricketerProfile
                        {
                            Id=1,
                            Name = "Ian Botham",
                            TestRuns = 10000
                        },
                        new CricketerProfile
                        {
                            Id=2,
                            Name = "Geoff Boycott",
                            TestRuns = 9000
                        },
                        new CricketerProfile
                        {
                            Id=3,
                            Name = "Bob Willis",
                            TestRuns = 500
                        },
                        new CricketerProfile
                        {
                            Id=4,
                            Name = "Joe Root",
                            TestRuns = 6500
                        },
                        new CricketerProfile
                        {
                            Id=5,
                            Name = "Andrew Straus",
                            TestRuns = 9000
                        }
                    }.AsQueryable();
            }
        }
    }
}