using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    static class A
    {
        private static DB dB = new DB();
        private static Stack<NODE> Open = new Stack<NODE>();
        private static List<string> Close = new List<string>();
        public static Queue<POINT> Run_A(string StartPoint, string EndPoint)
        {
            Close.Clear();
            Open.Clear();
            //Them node dau tien vao tap open
            Open.Push(new NODE(StartPoint, 0, LOCATION_DB.GetCost(StartPoint, EndPoint)));
            ShowOpen(Open);
            while(Open.Count > 0)
            {
                Console.WriteLine("---------------");
                NODE tmp = Open.Pop(); //Diem nho nhat trong open
                Close.Add(tmp.Name); //bo vao close
                if(tmp.Name == EndPoint || tmp.Heuristic == 0)
                {
                    tmp.way.Enqueue(LOCATION_DB.GetPOINT(EndPoint));
                    return tmp.way;
                }
                for(int i=0; i < dB.CONNECTION.Rows.Count; i++) //Check cac dinh lien ke
                {
                    string tmp_point_name = dB.CONNECTION.Rows[i][0].ToString().Trim(); //Điểm xét
                    if (CONNECTION_DB.CheckConnection(tmp.Name, tmp_point_name) && !Close.Contains(tmp_point_name)) // neu co ket noi se add vao stack open
                    {
                        //string p = dB.CONNECTION.Rows[i][0].ToString().Trim();  //Diem lien ket voi diem nho nhat trong open

                        string name = LOCATION_DB.GetPOINT(tmp_point_name).Name;  //Ten dien lien ket voi diem nho nhat trong open
                        int index = CheckNodeExist(Open, name); //Check xem đã tồn tại trong Open chưa
                        if (index != -1) //Check trung
                        {
                            double tmp_total = tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name) + LOCATION_DB.GetCost(name, EndPoint) * 2;
                            if(tmp_total < (Open.ElementAt(index).Cost + Open.ElementAt(index).Heuristic * 2)) //Neu điểm mới bé hơn điểm cũ
                            {
                                Open = RemoveAt(Open, index); //Loai bo
                                Open.Push(new NODE(name,                                        //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            LOCATION_DB.GetCost(name, EndPoint) * 2,            //Gia tri heuristic
                                            LOCATION_DB.GetPOINT(tmp.Name),                     //Diem moi cap nhan trong path dang xet
                                            tmp.way));
                            }
                        }
                        else
                        {
                            Open.Push(new NODE(name,                                            //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            LOCATION_DB.GetCost(name, EndPoint) * 2,            //Gia tri heuristic
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
            while(input.Count != 0)
            {
                NODE tmp = input.Pop();
                nODEs.Add(tmp);
            }
            Stack<NODE> Result = new Stack<NODE>();
            while (nODEs.Count != 0)
            {
                NODE tmp1 = nODEs.First<NODE>();
                for(int i=1; i< nODEs.Count; i++)
                {
                    if(tmp1.Cost + tmp1.Heuristic*2 < nODEs[i].Heuristic*2 + nODEs[i].Cost)
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
            for(int i = 0; i < Open.Count; i++)
            {
                double sum = (Open.ElementAt(i).Cost + Open.ElementAt(i).Heuristic*2);
                Console.WriteLine(Open.ElementAt(i).Name + 
                                " || \tG: " + Open.ElementAt(i).Cost + 
                                " \tH: " + Open.ElementAt(i).Heuristic +
                                " \tSUM: " + sum);
                foreach(POINT a in Open.ElementAt(i).way)
                {
                    Console.Write(a.Name + " ");
                }
                Console.WriteLine("\n");
            }
        }

        private static int CheckNodeExist(Stack<NODE> Open, string a) //Trùng thì trả về index k thì trả về -1
        {
            for(int i = 0; i< Open.Count; i++)
            {
                if (Open.ElementAt(i).Name == a)
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
            while(Open.Count != 0)
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
            for(int t = tmp.Count-1; t >= 0; t--)
            {
                tmp2.Push(tmp[t]);
            }
            return tmp2;
        }
    }
}
