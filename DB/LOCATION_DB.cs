using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    static class LOCATION_DB
    {
        private static DB dB = new DB();
        private static DataTable Location = dB.LOCATION;
        public static double GetCost(string point1, string point2)
        {
            double x1, y1, x2, y2;
            x1 = Convert.ToDouble(Location.Rows[SearchRowNameToIndex(point1)][1].ToString().Trim());
            x2 = Convert.ToDouble(Location.Rows[SearchRowNameToIndex(point2)][1].ToString().Trim());
            y1 = Convert.ToDouble(Location.Rows[SearchRowNameToIndex(point1)][2].ToString().Trim());
            y2 = Convert.ToDouble(Location.Rows[SearchRowNameToIndex(point2)][2].ToString().Trim());
            double Result;
            Result = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return Result;
        }

        public static POINT GetPOINT(string a)
        {
            int index = SearchRowNameToIndex(a);
            POINT pOINT = new POINT(Location.Rows[index][0].ToString().Trim(),
                                    Convert.ToInt32(Location.Rows[index][1].ToString().Trim()),
                                    Convert.ToInt32(Location.Rows[index][2].ToString().Trim()));
            return pOINT;
        }

        private static int SearchRowNameToIndex(string name)
        {
            for (int i = 0; i < Location.Rows.Count; i++)
            {
                if (Location.Rows[i][0].ToString().Trim() == name.Trim())
                {
                    return i;
                }
            }
            return -1;
        }

        public static Stack<POINT> GetAllPoint()
        {
            Stack<POINT> pOINTs = new Stack<POINT>();
            for(int i=0; i< Location.Rows.Count; i++)
            {
                pOINTs.Push(new POINT(Location.Rows[i][0].ToString().Trim(),
                                        Convert.ToInt32(Location.Rows[i][1]),
                                        Convert.ToInt32(Location.Rows[i][2])   ) );
            }
            return pOINTs;
        }
    }
}
