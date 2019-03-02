using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Project_2_1_
{
    class Program
    {



        static void Main()
        {
            List<SBList1> ListAdd = new List<SBList1>();
            SBList1 sbInfo;
            Console.WriteLine("Enter the location of the file Super_Bowl_Project");
            string inputPath = Console.ReadLine();
            string FileRead = inputPath + @"\Super_Bowl_Project.csv";
            const char DELIMITER = ',';
            string[] columns;
            string newFileWrite = inputPath + @"\List.txt";
            Console.WriteLine("The new File will be a text file in the same location under the name List");


            try
            {

                FileStream file = new FileStream(FileRead, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);

                reader.ReadLine();

                while (!reader.EndOfStream)
                {

                    columns = reader.ReadLine().Split(DELIMITER);
                    sbInfo = new SBList1(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5], columns[6], columns[7], columns[8], columns[9], columns[10], columns[11], columns[12], columns[13], columns[14]);

                    ListAdd.Add(sbInfo);

                }

                reader.Close();
                file.Close();


            }
            catch (Exception i)
            {
                Console.WriteLine(i.Message);
            }

            using (StreamWriter FileWrite = new StreamWriter(newFileWrite))
            {
                List1(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                Attending(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostHost(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostMVP(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostcLost(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostcWon(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostTWon(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                MostTLost(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                GPointDiff(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                AverageAtt(ref ListAdd, FileWrite);
                FileWrite.WriteLine();
                FileWrite.WriteLine("End of File");
            }


            return;
        }

        public static void List1(ref List<SBList1> ListAdd, StreamWriter FillInfo)
        {
            FillInfo.WriteLine("Winning Team of Each Super Bowl");
            FillInfo.WriteLine();
            for (var x = 0; x < ListAdd.Count; x++)
            {
                SBList1.FillInfo(ListAdd[x], FillInfo);
            }
            FillInfo.WriteLine("----- End of Winning Team List");
            FillInfo.WriteLine();
            return;
        }

        public static void Attending(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            var AttendList = (from order in ListAdd orderby order.attendance descending select order).Take(5).ToList();
            FileWrite.WriteLine("Top Attended Super Bowls");
            FileWrite.WriteLine();

            for (var x = 0; x < AttendList.Count(); x++)
            {
                FileWrite.WriteLine($"- Date: {AttendList[x].date}");
                FileWrite.WriteLine($"- Winning Team: {AttendList[x].tWinner}");
                FileWrite.WriteLine($"- Losing Team: {AttendList[x].tLoser}");
                FileWrite.WriteLine($"- City: {AttendList[x].city}");
                FileWrite.WriteLine($"- State: {AttendList[x].state}");
                FileWrite.WriteLine($"- Stadium: {AttendList[x].stadium}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine("----- End of Top Attended Super Bowls");
            FileWrite.WriteLine();
            return;
        }

        public static void MostHost(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Most Hosted Super Bowl State");
            FileWrite.WriteLine();
            var StateHosts = (from order in ListAdd group order by order.state into StateGroup orderby StateGroup.Count() descending select StateGroup.First()).ToList();
            FileWrite.WriteLine($"The Most Super Bowls have been hosted at {StateHosts[0].city}, {StateHosts[0].state} in the {StateHosts[0].stadium} Stadium");
            FileWrite.WriteLine();
            FileWrite.WriteLine("----- End of Most Hosted Super Bowl State");
            FileWrite.WriteLine();
            return;
        }

        public static void MostMVP(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Most MVP's");
            FileWrite.WriteLine();
            var mvpList = (from order in ListAdd group order by order.MVP into MVPGroup where MVPGroup.Count() > 2 select MVPGroup.First()).ToList();
            for (var x = 0; x < mvpList.Count(); x++)
            {
                FileWrite.WriteLine($"MVP: {mvpList[x].MVP}");
                FileWrite.WriteLine($"Team that won: {mvpList[x].tWinner}");
                FileWrite.WriteLine($"Team that lost: {mvpList[x].tLoser}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine("----- End of Most MVP's");
            FileWrite.WriteLine();
            return;
        }

        public static void MostcLost(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Which Coach('s) Lost the Most Super Bowls?");
            FileWrite.WriteLine();
            var cLostMost = (from order in ListAdd group order by order.cLoser into cGroup orderby cGroup.Count() descending select cGroup.First()).Take(4).ToList();
            for (var x = 0; x < cLostMost.Count(); x++)
            {
                FileWrite.WriteLine($"--- {cLostMost[x].cLoser}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine();
            return;
        }

        public static void MostcWon(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Which Coach('s) Won the Most Super Bowls?");
            FileWrite.WriteLine();
            var cWonMost = (from order in ListAdd group order by order.cWinner into cGroup orderby cGroup.Count() descending select cGroup.First()).Take(1).ToList();
            for (var x = 0; x < cWonMost.Count(); x++)
            {
                FileWrite.WriteLine($"--- {cWonMost[x].cWinner}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine();
            return;
        }

        public static void MostTWon(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Which Team(s) Won the Most Super Bowls?");
            FileWrite.WriteLine();
            var TWonMost = (from order in ListAdd group order by order.tWinner into tGroup orderby tGroup.Count() descending select tGroup.First()).Take(1).ToList();
            for (var x = 0; x < TWonMost.Count(); x++)
            {
                FileWrite.WriteLine($"--- {TWonMost[x].tWinner}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine();
            return;
        }

        public static void MostTLost(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Which Team(s) Lost the Most Super Bowls?");
            FileWrite.WriteLine();
            var TLostMost = (from order in ListAdd group order by order.tLoser into tGroup orderby tGroup.Count() descending select tGroup.First()).Take(1).ToList();
            for (var x = 0; x < TLostMost.Count(); x++)
            {
                FileWrite.WriteLine($"--- {TLostMost[x].tLoser}");
                FileWrite.WriteLine();
            }
            FileWrite.WriteLine();
            return;
        }

        public static void GPointDiff(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            FileWrite.WriteLine("Which Super Bowl had the greatest point difference?");
            FileWrite.WriteLine();
            var PointDiff = (from order in ListAdd orderby order.winPoints - order.losePoints descending select order).ToList();
            FileWrite.WriteLine($"--- Super Bowl {PointDiff[0].sbNumber} had the greatest point difference");
            FileWrite.WriteLine();
            return;
        }

        public static void AverageAtt(ref List<SBList1> ListAdd, StreamWriter FileWrite)
        {
            List<int> AvAttend = new List<int>();
            for (var x = 0; x < ListAdd.Count(); x++)
            {
                AvAttend.Add(ListAdd[x].attendance);
            }
            double average = System.Math.Round(AvAttend.Average(), 0);
            FileWrite.WriteLine($"What is the Average Attendance of all the Super Bowls?");
            FileWrite.WriteLine();
            FileWrite.WriteLine($"--- The Average Attendance is {average}");
            return;
        }
    }
    class SBList1
    {
        public string date { get; set; }
        public string sbNumber { get; set; }
        public int attendance { get; set; }
        public string qbWinner { get; set; }
        public string cWinner { get; set; }
        public string tWinner { get; set; }
        public int winPoints { get; set; }
        public string qbLoser { get; set; }
        public string cLoser { get; set; }
        public string tLoser { get; set; }
        public int losePoints { get; set; }
        public string MVP { get; set; }
        public string stadium { get; set; }
        public string city { get; set; }
        public string state { get; set; }



        public SBList1(string date, string sbNumber, string attendance, string qbWinner, string cWinner, string tWinner, string winPoints, string qbLoser, string cLoser, string tLoser, string losePoints, string MVP, string stadium, string city, string state)
        {
            this.date = date;
            this.sbNumber = sbNumber;
            this.attendance = int.Parse(attendance);
            this.qbWinner = qbWinner;
            this.cWinner = cWinner;
            this.tWinner = tWinner;
            this.winPoints = int.Parse(winPoints);
            this.qbLoser = qbLoser;
            this.cLoser = cLoser;
            this.tLoser = tLoser;
            this.losePoints = int.Parse(losePoints);
            this.MVP = MVP;
            this.stadium = stadium;
            this.city = city;
            this.state = state;


        }

        public static void FillInfo(SBList1 ListAdd, StreamWriter FillInfo)
        {
            FillInfo.WriteLine($"- Winning Team: {ListAdd.tWinner}\n");
            FillInfo.WriteLine($"- Date: {ListAdd.date}\n");
            FillInfo.WriteLine($"- Winning Quarterback: {ListAdd.qbWinner}\n");
            FillInfo.WriteLine($"- Winning Coach: {ListAdd.cWinner}\n");
            FillInfo.WriteLine($"- MVP: {ListAdd.MVP}\n");
            FillInfo.WriteLine($"- Point Difference: {ListAdd.winPoints - ListAdd.losePoints}\n");
            FillInfo.WriteLine();

            return;
        }

    }
}