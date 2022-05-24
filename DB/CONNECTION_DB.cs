using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    static class CONNECTION_DB
    {
        private static DB dB = new DB();
        private static DataTable Con = dB.CONNECTION;
        public static bool CheckConnection(string point1, string point2)
        {
            if(Con.Rows[SearchRowNameToIndex(point1)]
                        [point2.ToString().Trim()]
                        .ToString().Trim() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int SearchRowNameToIndex(string name)
        {
            for(int i=0; i< Con.Rows.Count; i++)
            {
                if(Con.Rows[i]["X"].ToString().Trim() == name.Trim())
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
