using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using RareGoods.RawData;

namespace RareGoods
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private RawData.RawData current = new RawData.RawData();

        private Dictionary<int, StarSystem> starSystemSet = new Dictionary<int, StarSystem>();

        public MainWindow()
        {
            InitializeComponent();

            starSystemSet = current.starSystemDataSet;

            setDistance();

            CreateView();

        }

        private void setDistance()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            double[] distanceTable = new double[starSystemSet.Count];
            double distance = 0;

            foreach (var origin in starSystemSet)
            {

                startX = starSystemSet[origin.Key].SystemX;
                startY = starSystemSet[origin.Key].SystemY;
                startZ = starSystemSet[origin.Key].SystemZ;

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                Dictionary<int, double> sortingTable = new Dictionary<int, double>();

                foreach (var destination in starSystemSet)
                {

                    endX = starSystemSet[destination.Key].SystemX;
                    endY = starSystemSet[destination.Key].SystemY;
                    endZ = starSystemSet[destination.Key].SystemZ;

                    double distanceX = 0;
                    double distanceY = 0;
                    double distanceZ = 0;

                    if (startX < 0 && endX < 0) distanceX = Math.Min(startX, endX) + Math.Abs(Math.Max(startX, endX));
                    if (startY < 0 && endY < 0) distanceY = Math.Min(startY, endY) + Math.Abs(Math.Max(startY, endY));
                    if (startZ < 0 && endZ < 0) distanceZ = Math.Min(startZ, endZ) + Math.Abs(Math.Max(startZ, endZ));

                    if ((startX < 0 && endX > 0) || (startX > 0 && endX < 0)) distanceX = Math.Abs(Math.Min(startX, endX)) + Math.Max(startX, endX);
                    if ((startY < 0 && endY > 0) || (startY > 0 && endY < 0)) distanceY = Math.Abs(Math.Min(startY, endY)) + Math.Max(startY, endY);
                    if ((startZ < 0 && endZ > 0) || (startZ > 0 && endZ < 0)) distanceZ = Math.Abs(Math.Min(startZ, endZ)) + Math.Max(startZ, endZ);

                    if (startX > 0 && endX > 0) distanceX = Math.Max(startX, endX) - Math.Min(startX, endX);
                    if (startY > 0 && endY > 0) distanceY = Math.Max(startY, endY) - Math.Min(startY, endY);
                    if (startZ > 0 && endZ > 0) distanceZ = Math.Max(startZ, endZ) - Math.Min(startZ, endZ);

                    distance = Math.Pow(distanceX, 2) + Math.Pow(distanceZ, 2);
                    distance = Math.Sqrt(distance);

                    distance = Math.Pow(distance, 2) + Math.Pow(distanceY, 2);
                    distance = Math.Sqrt(distance);

                    distanceTable[destination.Key] = distance;
                    sortingTable[destination.Key] = distance;
                }

                starSystemSet[origin.Key].DistanceTable = distanceTable;

                var sortedDistance = from pair in sortingTable orderby pair.Value select pair;

                double[] sortedDist = new double[starSystemSet.Count];
                string[] sortedSyst = new string[starSystemSet.Count];

                int sortCounter = 0;
               
                foreach (KeyValuePair<int, double> dist in sortedDistance)
                {
                    sortedSyst[sortCounter] = starSystemSet[dist.Key].SystemName + "\n" + dist.Value.ToString("F");
                    sortedDist[sortCounter] = dist.Value;

                    sortCounter += 1;
                }

                starSystemSet[origin.Key].SortedDistances = sortedDist;
                starSystemSet[origin.Key].DisplayDistances = sortedSyst;

            }

        }


        private void CreateView()
        {

            Resources["starSystemSet"] = starSystemSet;

            GridView starGridView = new GridView();
            
            GridViewColumn systemName = new GridViewColumn();
            GridViewColumn stationName = new GridViewColumn();
            GridViewColumn goodsName = new GridViewColumn();

            

            systemName.Header = "Star System";
            systemName.Width = 150;
            systemName.DisplayMemberBinding = new Binding("DisplaySystem");
            
            stationName.Header = "Station Name";
            stationName.Width = 150;
            stationName.DisplayMemberBinding = new Binding("DisplayStation");

            goodsName.Header = "Goods";
            goodsName.Width = 150;
            goodsName.DisplayMemberBinding = new Binding("DisplayGoods");

            starGridView.Columns.Add(systemName);
            starGridView.Columns.Add(stationName);
            starGridView.Columns.Add(goodsName);

            for (int col = 0; col < starSystemSet.Count; col += 1)
            {

                GridViewColumn target = new GridViewColumn();

                target.Header = "[" + col + "]";
                target.Width = 100;
                target.DisplayMemberBinding = new Binding("DisplayDistances[" + col + "]");

                if (col > 0)
                {
                    starGridView.Columns.Add(target);
                }
            }

            stars.View = starGridView;
            
            foreach (var name in starSystemSet)
            {
                starSystemSet[name.Key].DisplaySystem = starSystemSet[name.Key].SystemName + "\n" +starSystemSet[name.Key].GoodsName;
                starSystemSet[name.Key].DisplayStation = starSystemSet[name.Key].StationName + "\n" + starSystemSet[name.Key].StationDistance;
                starSystemSet[name.Key].DisplayGoods = starSystemSet[name.Key].GoodsPrices + "  ["+ starSystemSet[name.Key].MinAmount+"-"+ starSystemSet[name.Key].MaxAmount+"]\n" + "[" +
                                                       (starSystemSet[name.Key].MinAmount* starSystemSet[name.Key].GoodsPrices) + "-" +
                                                       (starSystemSet[name.Key].MaxAmount * starSystemSet[name.Key].GoodsPrices) + "]";
                                                       
                stars.Items.Add(new StarSystem()
                {
                    DisplaySystem = starSystemSet[name.Key].DisplaySystem,
                    DisplayStation = starSystemSet[name.Key].DisplayStation,
                    DisplayGoods = starSystemSet[name.Key].DisplayGoods,
                    DisplayDistances = starSystemSet[name.Key].DisplayDistances
                });
            }
        }
    }
}

//    private void CalculateRoute()
    //    {


    //        for (int i = 0; i < soldList.Length; i += 1)
    //        {
    //            soldList[i] = -1;
    //        }

    //        currentSystemID = 69;

    //        Jump(currentSystemID,"25");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID,"50");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID, "100");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID, "25");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID, "50");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID, "100");
    //        route.Text += "----------------\n";
    //        Jump(currentSystemID, "200");
    //        route.Text += "----------------\n";
            
    //    }

    //    private void Jump(int currentID,string jump)
    //    {
    //        double[] currentDistance = distanceList[currentID];
    //        int[] currentOrderList = distanceOrder[currentID];
    //        string currentStation = stationList[currentID];
    //        string currentSystem = starSystem[currentID];
    //        int currentStatDistance = stationDistance[currentID];

    //        int maxStep = 0;
    //        int jumpRange = 0;

    //        if (jump == "25")
    //        {
    //            maxStep = starSystem.Count;
    //            jumpRange = 25;
    //        }

    //        if (jump == "50")
    //        {
    //            maxStep = starSystem.Count;
    //            jumpRange = 50;
    //        }

    //        if (jump == "100")
    //        {
    //            maxStep = 2;
    //            jumpRange = 100;
    //        }

    //        if (jump == "200")
    //        {
    //            maxStep = 2;
    //            jumpRange = 200;
    //        }

    //        double totalDistance = 0;

    //        if (jumpList[0] == -1) { jumpList[0] = currentID; }

    //        for (int steps = 1; steps < maxStep; steps += 1)
    //        {

    //            int count = 0;

    //            while (count < starSystem.Count)
    //            {
    //                int nextStopID = currentOrderList[count];

    //                double nextDistance = currentDistance[count];

    //                bool jumpCheck = false;

    //                if (jumpRange == 25) jumpCheck = nextDistance <= 25;
    //                if (jumpRange == 50) jumpCheck = nextDistance <= 50;
    //                if (jumpRange == 100) jumpCheck = nextDistance >= 100;
    //                if (jumpRange == 200) jumpCheck = nextDistance >= 200;

    //                if (!jumpList.Contains(nextStopID) && jumpCheck && stationDistance[nextStopID] < 6000)
    //                {
    //                    currentSystemID = nextStopID;

    //                    currentOrderList = distanceOrder[currentSystemID];
    //                    currentDistance = distanceList[currentSystemID];
    //                    currentSystem = starSystem[currentSystemID];
    //                    currentStation = stationList[currentSystemID];
    //                    currentStatDistance = stationDistance[currentSystemID];

    //                    totalDistance += nextDistance;

    //                    route.Text +=jumpListCounter+ ": " 
    //                                + nextDistance.ToString("##.#") + "  [" 
    //                                + totalDistance.ToString("##.#") + "] "                 
    //                                + currentSystem + ":  " 
    //                                + currentStation + " ( " 
    //                                + currentStatDistance + ") -> " +sellCheck(currentSystemID) 
    //                                +"\n";

    //                    jumpListCounter += 1;

    //                    jumpList[jumpListCounter] = currentSystemID;
    //                    soldList[jumpListCounter] = currentSystemID;

    //                    break;
    //                }

    //                count += 1;

    //            }

    //        }

    //    }

    //    private string sellCheck(int currentID)
    //    {
    //        double[] currentDistance = distanceList[currentID];
    //        int[] currentOrderList = distanceOrder[currentID];

    //        string sellItem = "";

    //        for (int counter = 0; counter < currentDistance.Length; counter += 1)
    //        {
    //            int fetch = currentOrderList[counter];

    //            if (currentDistance[counter] >= 150 && soldList.Contains(fetch))
    //            {
    //                for (int lookup = 0; lookup < soldList.Length; lookup += 1)
    //                {
    //                    if (soldList[lookup] == fetch)
    //                    {
    //                        double sellDistance = currentDistance[counter];

    //                        sellItem += starSystem[fetch] + "(" + sellDistance.ToString("##.##") + ") ,";

    //                       if(currentDistance[counter]>150) soldList[lookup] = -1;
    //                    }
    //                }
    //            }
    //        }

    //         return sellItem;

    //    }

   