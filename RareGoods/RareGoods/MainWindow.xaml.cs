using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


                   

       private Dictionary<int, double[]> distance = new Dictionary<int, double[]>();
        private Dictionary<int, int[]> distanceOrder = new Dictionary<int, int[]>();
        private Dictionary<int, string[]> distanceSystem = new Dictionary<int, string[]>();
        private Dictionary<int, double[]> Rotation = new Dictionary<int, double[]>();

        

        List<string> starSystemList = new List<string>();
        List<string> stationList = new List<string>();
        List<string[]> distanceSystemList = new List<string[]>();
        List<double[]> distanceList = new List<double[]>();

        private int[] jumpList = new int[120];
        private int[] soldList = new int[120];
        private int jumpListCounter = 0;
        private int currentSystemID = 0;

        public MainWindow()
        {
            InitializeComponent();

            RawData.RawData current = new RawData.RawData();

            Dictionary<int,StarSystem> starSystemData = current.starSystemDataSet;
           
            // RawData();
            int count = starSystemData[0].systemID;


            //   setRotation();

            //  setDistance();

            //   CreateView();

            //   CalculateRoute();
        }

        private void RawDatas()
        {

            
            // id
            // star system 
            // station name
            // location x,y,z
            // item name
            // item price

            

            jumpList[0] = -1;

            var starList = new List<SystemData>();
            
        }

        private void setDistance()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            Dictionary<int, double> tempRotation = new Dictionary<int, double>();

            foreach (var startLoc in location)
            {
                double[] start = startLoc.Value;

                startX = start[0];
                startY = start[1];
                startZ = start[2];

                Dictionary<int, double> tempDistance = new Dictionary<int, double>();

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                foreach (var endLoc in location)
                {
                    double[] end = endLoc.Value;

                    endX = end[0];
                    endY = end[1];
                    endZ = end[2];

                    tempDistance[endLoc.Key] = calculateDistance(startX, startY, startZ, endX, endY, endZ);
                    
                }

                var sortedDistance = from pair in tempDistance orderby pair.Value ascending select pair;

                int keyCount = 0;

                int[] sortedKeys = new int[location.Count];
                double[] sortedValues = new double[location.Count];
 
                foreach (KeyValuePair<int,double> pair in sortedDistance)
                {
                    sortedKeys[keyCount] = pair.Key;
                    sortedValues[keyCount] = pair.Value;

                    keyCount += 1;
                }

                distance[startLoc.Key] = sortedValues;
                distanceOrder[startLoc.Key] = sortedKeys;

                int[] locationKey = sortedKeys;
                double[] distanceNames = sortedValues;
                
                string[] systemNames = new string[locationKey.Length];

                for (int step=0;step<locationKey.Length;step+=1)

                {

                    int systemID = locationKey[step];

                    string systemName;
                    double distanceString = distanceNames[step];
                    
                    starSystem.TryGetValue(systemID , out systemName);
                    systemNames[step] = systemName+"\n["+distanceString.ToString("##.##")+"]";

                }
                
                distanceSystem[startLoc.Key] = systemNames;

            }

        }

        private void setRotation()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            foreach (var startLoc in location)
            {
                double[] start = startLoc.Value;

                startX = start[0];
                startY = start[1];
                startZ = start[2];

                double endX = 0;
                double endY = 0;
                double endZ = 0;

                foreach (var endLoc in location)
                {
                    double[] end = endLoc.Value;

                    endX = end[0];
                    endY = end[1];
                    endZ = end[2];

               //     Rotation[endLoc.Key] = calculateRotation(startX, startY, startZ, endX, endY, endZ);

                }

            }

        }

        private void CreateView()
        {

            List<SystemData> masterList = new List<SystemData>();

            ListViewItem masterItem = new ListViewItem();

            foreach (var systemName in starSystem)
            {
                starSystemList.Add(systemName.Value);
            }

            foreach (var stationName in station)
            {
                stationList.Add(stationName.Value);
            }

            foreach (var systemNames in distanceSystem)
            {
                distanceSystemList.Add(systemNames.Value);
            }

            foreach (var distances in distance)
            {
                distanceList.Add(distances.Value);
            }

            foreach (var distanceSys in distanceSystem)
            {
                distanceSystemList.Add(distanceSys.Value);
            }

            GridView starGridView = new GridView();
            GridViewColumn idName = new GridViewColumn();
            GridViewColumn sysName = new GridViewColumn();
            GridViewColumn statName = new GridViewColumn();

            idName.Header = "Star System";
            idName.Width = 25;
            idName.DisplayMemberBinding = new Binding("systemID");

            sysName.Header = "Star System";
            sysName.Width = 150;
            sysName.DisplayMemberBinding = new Binding("systemName");

            statName.Header = "Station Name";
            statName.Width = 150;
            statName.DisplayMemberBinding = new Binding("stationName");

            starGridView.Columns.Add(idName);
            starGridView.Columns.Add(sysName);
            starGridView.Columns.Add(statName);

            for (int col = 0; col < distanceSystem.Count; col += 1)
            {
            
                GridViewColumn target = new GridViewColumn();

                target.Header = "["+col+"]";
                target.Width = 100;
                target.DisplayMemberBinding = new Binding("distanceSystem[" + col + "]");

               if(col>0) { starGridView.Columns.Add(target);}

        }

        stars.View = starGridView;

            int count = 0;

            foreach (string system in starSystemList)
            {
                string station;
                double[] distant;
                string[] distantSys;

                station = stationList[count];
                distant = distanceList[count];
                distantSys = distanceSystemList[count];
                
                stars.Items.Add(new SystemData() {systemID=count,systemName = system, stationName = station, distanceSystem = distantSys});

                count += 1;
            }

        }

        private void CalculateRoute()
        {


            for (int i = 0; i < soldList.Length; i += 1)
            {
                soldList[i] = -1;
            }

            currentSystemID = 69;

            Jump(currentSystemID,"25");
            route.Text += "----------------\n";
            Jump(currentSystemID,"50");
            route.Text += "----------------\n";
            Jump(currentSystemID, "100");
            route.Text += "----------------\n";
            Jump(currentSystemID, "25");
            route.Text += "----------------\n";
            Jump(currentSystemID, "50");
            route.Text += "----------------\n";
            Jump(currentSystemID, "100");
            route.Text += "----------------\n";
            Jump(currentSystemID, "200");
            route.Text += "----------------\n";
            
        }

        private void Jump(int currentID,string jump)
        {
            double[] currentDistance = distanceList[currentID];
            int[] currentOrderList = distanceOrder[currentID];
            string currentStation = stationList[currentID];
            string currentSystem = starSystem[currentID];
            int currentStatDistance = stationDistance[currentID];

            int maxStep = 0;
            int jumpRange = 0;

            if (jump == "25")
            {
                maxStep = starSystem.Count;
                jumpRange = 25;
            }

            if (jump == "50")
            {
                maxStep = starSystem.Count;
                jumpRange = 50;
            }

            if (jump == "100")
            {
                maxStep = 2;
                jumpRange = 100;
            }

            if (jump == "200")
            {
                maxStep = 2;
                jumpRange = 200;
            }

            double totalDistance = 0;

            if (jumpList[0] == -1) { jumpList[0] = currentID; }

            for (int steps = 1; steps < maxStep; steps += 1)
            {

                int count = 0;

                while (count < starSystem.Count)
                {
                    int nextStopID = currentOrderList[count];

                    double nextDistance = currentDistance[count];

                    bool jumpCheck = false;

                    if (jumpRange == 25) jumpCheck = nextDistance <= 25;
                    if (jumpRange == 50) jumpCheck = nextDistance <= 50;
                    if (jumpRange == 100) jumpCheck = nextDistance >= 100;
                    if (jumpRange == 200) jumpCheck = nextDistance >= 200;

                    if (!jumpList.Contains(nextStopID) && jumpCheck && stationDistance[nextStopID] < 6000)
                    {
                        currentSystemID = nextStopID;

                        currentOrderList = distanceOrder[currentSystemID];
                        currentDistance = distanceList[currentSystemID];
                        currentSystem = starSystem[currentSystemID];
                        currentStation = stationList[currentSystemID];
                        currentStatDistance = stationDistance[currentSystemID];

                        totalDistance += nextDistance;

                        route.Text +=jumpListCounter+ ": " 
                                    + nextDistance.ToString("##.#") + "  [" 
                                    + totalDistance.ToString("##.#") + "] "                 
                                    + currentSystem + ":  " 
                                    + currentStation + " ( " 
                                    + currentStatDistance + ") -> " +sellCheck(currentSystemID) 
                                    +"\n";

                        jumpListCounter += 1;

                        jumpList[jumpListCounter] = currentSystemID;
                        soldList[jumpListCounter] = currentSystemID;

                        break;
                    }

                    count += 1;

                }

            }

        }

        private string sellCheck(int currentID)
        {
            double[] currentDistance = distanceList[currentID];
            int[] currentOrderList = distanceOrder[currentID];

            string sellItem = "";

            for (int counter = 0; counter < currentDistance.Length; counter += 1)
            {
                int fetch = currentOrderList[counter];

                if (currentDistance[counter] >= 150 && soldList.Contains(fetch))
                {
                    for (int lookup = 0; lookup < soldList.Length; lookup += 1)
                    {
                        if (soldList[lookup] == fetch)
                        {
                            double sellDistance = currentDistance[counter];

                            sellItem += starSystem[fetch] + "(" + sellDistance.ToString("##.##") + ") ,";

                           if(currentDistance[counter]>150) soldList[lookup] = -1;
                        }
                    }
                }
            }

             return sellItem;

        }

        private double calculateRotation(double startX, double startY, double startZ, double endX, double endY,double endZ)
        {
            double rotation = 0;

            double rad = 360/(Math.PI*2);

            double distanceX = 0;
            double distanceY = 0;
            double distanceZ = 0;

            if (startX < 0 && endX < 0) distanceX = Math.Min(startX, endX) + Math.Abs(Math.Max(startX, endX));
            if (startY < 0 && endY < 0) distanceY = Math.Min(startY, endY) + Math.Abs(Math.Max(startY, endY));
            if (startZ < 0 && endZ < 0) distanceZ = Math.Min(startZ, endZ) + Math.Abs(Math.Max(startZ, endZ));

            if ((startX < 0 && endX > 0) || (startX > 0 && endX < 0)) distanceX = Math.Abs(Math.Min(startX, endX)) + Math.Max(startX, endX);
            if ((startY < 0 && endY > 0) || (startY > 0 && endY < 0)) distanceY = Math.Abs(Math.Min(startY, endY)) + Math.Max(startY, endY);
            if ((startZ < 0 && endZ > 0) || (startZ > 0 && endZ < 0)) distanceZ = Math.Abs(Math.Min(startZ, endZ)) + Math.Max(startZ, endZ);

            rotation = (Math.Tan(distanceZ / distanceX)) * rad;

            return rotation;

        }

        private double calculateDistance(double startX, double startY, double startZ, double endX, double endY,double endZ)
        {
            double distance = 0;

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

          // rotation = (Math.Tan(distanceZ / distanceX)) * rad;

            return distance;
        }


        public class SystemData
        {
            public int systemID { get; set; }
            public string systemName { get; set; }
            public string stationName { get; set; }
            public double[] location { get; set; }
            public string goods { get; set; }
            public double[] distance { get; set; }
            public int[] distanceOrder { get; set; }
            public string[] distanceSystem { get; set; }
            public List<string> StarSystemList { get; set; } 
            
        }
    }
}