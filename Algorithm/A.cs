﻿using System;
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
        private static Stack<POINT> AllpOINTs = new Stack<POINT>();
        private static List<string> Close = new List<string>();
        public static Queue<POINT> Run_A(string StartPoint, string EndPoint)
        {
            Close.Clear();
            AllpOINTs = LOCATION_DB.GetAllPoint();
            //Them node dau tien vao tap open
            Open.Push(new NODE(StartPoint, 0, LOCATION_DB.GetCost(StartPoint, EndPoint), LOCATION_DB.GetPOINT(StartPoint))); 
            while(Open.Count > 0)
            {
                Console.WriteLine("---------------");
                NODE tmp = Open.Pop(); //Diem nho nhat trong open
                Close.Add(tmp.Name);
                if(tmp.Name == EndPoint || tmp.Heuristic == 0)
                {
                    tmp.way.Enqueue(LOCATION_DB.GetPOINT(EndPoint));
                    return tmp.way;
                }
                for(int i=0; i< dB.CONNECTION.Rows.Count; i++) //Check cac dinh lien ke
                {
                    if (CONNECTION_DB.CheckConnection(tmp.Name, dB.CONNECTION.Rows[i][0].ToString().Trim()) && !Close.Contains(dB.CONNECTION.Rows[i][0].ToString().Trim())) // neu co ket noi se add vao stack open
                    {
                        string p = dB.CONNECTION.Rows[i][0].ToString().Trim();  //Diem lien ket voi diem nho nhat trong open

                        string name = LOCATION_DB.GetPOINT(p).Name;  //Ten dien lien ket voi diem nho nhat trong open
                        int index = CheckNodeExist(Open, new NODE(name,                         //Ten node
                                            tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name),     //Tong chi phi tu nguon toi vi tri hien tai
                                            LOCATION_DB.GetCost(name, EndPoint) * 2,            //Gia tri heuristic
                                            LOCATION_DB.GetPOINT(tmp.Name),                     //Diem moi cap nhan trong path dang xet
                                            tmp.way));
                        if (index != -1) //Check trung
                        {
                            double tmp_total = tmp.Cost + LOCATION_DB.GetCost(tmp.Name, name) + LOCATION_DB.GetCost(name, EndPoint) * 2;
                            if(tmp_total < Open.ElementAt(index).Cost + Open.ElementAt(index).Heuristic * 2) //Neu điểm mới bé hơn điểm cũ
                            {
                                Open = RemoveAt(Open, index);
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

                Console.WriteLine(Open.ElementAt(i).Name + 
                                " G: " + Open.ElementAt(i).Cost + 
                                " H: " + Open.ElementAt(i).Heuristic + 
                                " P: " + Open.ElementAt(i).way.Last().Name);
            }
        }

        private static int CheckNodeExist(Stack<NODE> Open, NODE a) //Trùng thì trả về index k thì trả về -1
        {
            for(int i = 0; i< Open.Count; i++)
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
            while(Open.Count != 0)
            {
                if (Open.Count == i+1)
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