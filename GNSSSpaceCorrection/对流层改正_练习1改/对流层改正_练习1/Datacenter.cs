using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 对流层改正_练习1
{
    class Datacenter
    {
        public  List<Station> stations = new List<Station>();
        public double[] aw = { 0.00058021897, 0.00056794847, 0.00058118019, 0.00059727542,0.00061641693 };
        public double[] bw = { 0.0014275268, 0.0015138625, 0.0014572752, 0.0015007428, 0.0017599082 };
        public double[] cw = { 0.043472961, 0.046729510, 0.043908931, 0.044626982, 0.054736038 };

        public double[] ah_avg = { 0.0012769934, 0.0012683230, 0.0012465397, 0.0012196049, 0.0012045996 };
        public double[] bh_avg = { 0.0029153695, 0.0029152299, 0.0029288445, 0.0029022565, 0.0029024912 };
        public double[] ch_avg = { 0.062610505, 0.062837393, 0.063721774, 0.063824265, 0.064258455 };

        public double[] ah_amp = { 0, 0.000012709626, 0.000026523662, 0.000034000452, 0.000041202191 };
        public double[] bh_amp = { 0, 0.000021414979, 0.000030160779, 0.000072562722, 0.00011723375 };
        public double[] ch_amp = { 0, 0.000090128400, 0.000043497037, 0.00084795348, 0.0017037206 };
    }
}
