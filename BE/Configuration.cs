using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static int MinAgeTester = 40;
        public static int MinAgeTrainee = 18;
        public static int MinTestLesson = 20;
        public static int MinRangeTest = 7;

        private static int numtest = 0;
        public static int Numtest
        {
            get
            {
                return numtest++;
            }
        }


}
}
