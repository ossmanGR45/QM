using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Models
{
    public class Enums
    {
        public enum ActionType
        {
            Avoidance = 0,
            Reduction = 1
        }

        public enum RequestStatus
        {
            Accepted = 0,
            InProgress = 1,
            Rejected = 2,
        }

        public enum Likelihood
        {
            veryLow = 1,
            low = 2,
            Medium = 3,
            High = 4,
            Critical = 5
        }

        public enum Impact
        {
            veryLow = 1,
            low = 2,
            Medium = 3,
            High = 4,
            Critical = 5
        }
    }
}
