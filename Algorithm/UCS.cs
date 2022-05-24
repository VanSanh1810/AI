using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    static class UCS
    {
        private static DB dB = new DB();
        private static Stack<NODE> Open = new Stack<NODE>();
        private static Stack<POINT> AllpOINTs = new Stack<POINT>();

        public static Queue<POINT> Run_UCS(string StartPoint, string EndPoint)
        {
            AllpOINTs = LOCATION_DB.GetAllPoint();
            Open.Push(new NODE(StartPoint, 0, 0, LOCATION_DB.GetPOINT(StartPoint)));//Them node dau tien vao tap open
            while (Open.Count > 0)
            {
                Console.WriteLine("---------------");
                NODE tmp = Open.Pop(); //Diem nho nhat trong open
                if (tmp.Name == EndPoint)
                {
                    tmp.way.Enqueue(LOCATION_DB.GetPOINT(EndPoint));
                    return tmp.way;
                }
                for (int i = 0; i < dB.CONNECTION.Rows.Count; i++) //Check cac dinh lien ke
                {
                    if (CONNECTION_DB.CheckConnection(tmp.Name, dB.CONNECTION.Rows[i][0].ToString().Trim())) // neu co ket noi se add vao stack open
                    {
                        string p = dB.CONNECTION.Rows[i][0].ToString().Trim();  //Diem lien ket voi diem nho nhat trong open

                        string name = LOCATION_DB.GetPOINT(p).Name;  //Ten dien lien ket voi diem nho nhat trong open
                        int index = CheckNodeExist(Open, new NODE(name,                         //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            0,                                                  //Gia tri heuristic
                                            LOCATION_DB.GetPOINT(tmp.Name),                     //Diem moi cap nhan trong path dang xet
                                            tmp.way));
                        if (index != -1) //Check trung
                        {
                            double tmp_total = tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name) ;
                            if (tmp_total < Open.ElementAt(index).Cost) //Nếu điểm mới bé hơn điểm cũ
                            {
                                Open = RemoveAt(Open, index);
                                Open.Push(new NODE(name,                                        //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            0,                                                  //Gia tri heuristic
                                            LOCATION_DB.GetPOINT(tmp.Name),                     //Diem moi cap nhan trong path dang xet
                                            tmp.way));
                            }
                        }
                        else
                        {
                            Open.Push(new NODE(name,                                            //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            0,                                                  //Gia tri heuristic
                                            LOCATION_DB.GetPOINT(tmp.Name),                     //Diem moi cap nhan trong path dang xet
                                            tmp.way));
                        }
                    }
                }
                Open = SortStack(Open); //Xap sep stack theo gia tri giam dan cua tong cost va heuristic
                ShowOpen(Open);
            }
            return null;
        }
        private static Stack<NODE> SortStack(Stack<NODE> input) //sap xem giam dan trong stack
        {
            List<NODE> nODEs = new List<NODE>();
            while (input.Count != 0)
            {
                NODE tmp = input.Pop();
                nODEs.Add(tmp);
            }
            Stack<NODE> Result = new Stack<NODE>();
            while (nODEs.Count != 0)
            {
                NODE tmp1 = nODEs.First<NODE>();
                for (int i = 1; i < nODEs.Count; i++)
                {
                    if (tmp1.Cost  < nODEs[i].Cost)
                    {
                        tmp1 = nODEs[i];
                    }
                }
                nODEs.Remove(tmp1);
                Result.Push(tmp1);
            }
            return Result;
        }
        private static void ShowOpen(Stack<NODE> Open)
        {
            for (int i = 0; i < Open.Count; i++)
            {

                Console.WriteLine(Open.ElementAt(i).Name + " G: " + Open.ElementAt(i).Cost + " H: " + Open.ElementAt(i).Heuristic);
            }
        }

        private static int CheckNodeExist(Stack<NODE> Open, NODE a) //Trùng thì trả về index k thì trả về -1
        {
            for (int i = 0; i < Open.Count; i++)
            {
                if (Open.ElementAt(i).Name == a.Name)
                {
                    return i;
                }
            }
            return -1;
        }

        private static Stack<NODE> RemoveAt(Stack<NODE> Open, int i)
        {
            List<NODE> tmp = new List<NODE>();
            Stack<NODE> tmp2 = new Stack<NODE>();
            while (Open.Count != 0)
            {
                if (Open.Count == i + 1)
                {
                    Open.Pop();
                }
                else
                {
                    tmp.Add(Open.Pop());
                }
            }
            for (int t = tmp.Count - 1; t >= 0; t--)
            {
                tmp2.Push(tmp[t]);
            }
            return tmp2;
        }
    }
}
