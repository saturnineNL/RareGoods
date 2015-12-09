using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RareGoods.RawData;

namespace RareGoods.StarSystemData
{
    public class CalculatedData
    {

        private RawData.RawData current = new RawData.RawData();

        public Dictionary<int, StarSystem> starSystemSet = new Dictionary<int, StarSystem>();

        public Dictionary<Tuple<int, int>, double[]> markerDistance = new Dictionary<Tuple<int, int>, double[]>();
        public Dictionary<Tuple<int, int>, int[]> markerSystems = new Dictionary<Tuple<int, int>, int[]>();

        internal CalculatedData()
        {

            starSystemSet = current.starSystemDataSet;

        }


        public void CalculateDistance()
        {
            double startX = 0;
            double startY = 0;
            double startZ = 0;

            double[] distanceTable = new double[starSystemSet.Count];

            double[] distanceXTable = new double[starSystemSet.Count];
            double[] distanceYTable = new double[starSystemSet.Count];
            double[] distanceZTable = new double[starSystemSet.Count];

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

                // calculate 3d distance between 2 points

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

                    if (startX == endX) distanceX = 0;
                    if (startY == endY) distanceY = 0;
                    if (startZ == endZ) distanceZ = 0;

                    distance = Math.Pow(distanceX, 2) + Math.Pow(distanceZ, 2);
                    distance = Math.Sqrt(distance);

                    distance = Math.Pow(distance, 2) + Math.Pow(distanceY, 2);
                    distance = Math.Sqrt(distance);

                    distanceTable[destination.Key] = distance;

                    distanceXTable[destination.Key] = distanceX;
                    distanceYTable[destination.Key] = distanceY;
                    distanceZTable[destination.Key] = distanceZ;

                    sortingTable[destination.Key] = distance;
                }

                // sorting distances 

                starSystemSet[origin.Key].DistanceTable = distanceTable;

                starSystemSet[origin.Key].DistanceXTable = distanceXTable;
                starSystemSet[origin.Key].DistanceYTable = distanceYTable;
                starSystemSet[origin.Key].DistanceZTable = distanceZTable;

                var sortedDistance = from pair in sortingTable orderby pair.Value select pair;

                int[] sortedSyst = new int[starSystemSet.Count];
                double[] sortedDist = new double[starSystemSet.Count];

                string[] sortedDisp = new string[starSystemSet.Count];

                int sortCounter = 0;

                foreach (KeyValuePair<int, double> dist in sortedDistance)
                {
                    sortedSyst[sortCounter] = dist.Key;
                    sortedDist[sortCounter] = dist.Value;

                  //  sortedDistX[sortCounter] = starSystemSet[origin.Key].DistanceXTable[dist.Key];
                  //  sortedDistY[sortCounter] = starSystemSet[origin.Key].DistanceYTable[dist.Key];
                  //  sortedDistZ[sortCounter] = starSystemSet[origin.Key].DistanceZTable[dist.Key];

                    sortedDisp[sortCounter] = starSystemSet[dist.Key].SystemName + "\n" + dist.Value.ToString("F");

                    sortCounter += 1;
                }

                starSystemSet[origin.Key].SortedSystems = sortedSyst;
                starSystemSet[origin.Key].SortedDistances = sortedDist;
                starSystemSet[origin.Key].DisplayDistances = sortedDisp;

                starSystemSet[origin.Key].DistanceXTable = distanceXTable;
                starSystemSet[origin.Key].DistanceYTable = distanceYTable;
                starSystemSet[origin.Key].DistanceZTable = distanceZTable;

                // split data into 20 LY chunk markers

                double[] examineDistance = starSystemSet[origin.Key].SortedDistances;

                int[] examineSystem = starSystemSet[origin.Key].SortedSystems;

                for (int factor = 0; factor <= 12; factor += 1)
                {
                    double minDistance = 0;

                    if (factor > 0) minDistance = (factor - 1) * 20;

                    double maxDistance = factor * 20;

                    var target = examineDistance.Where(examDistance => (examDistance >= minDistance && examDistance < maxDistance));

                    double[] targetDistance = new double[target.Count()];
                    int[] targetSystem = new int[target.Count()];

                    int targetDistanceCounter = 0;
                    int targetSystemCounter = 0;

                    Tuple<int, int> targetID = new Tuple<int, int>(origin.Key, factor);

                    // fetch systemID from distances 

                    foreach (double showDistance in target)
                    {
                        targetDistance[targetDistanceCounter] = showDistance;
                        targetDistanceCounter += 1;
                    }

                    foreach (double getDistance in targetDistance)
                    {

                        int index = Array.FindIndex(examineDistance, examDistance => examDistance == getDistance);

                        targetSystem[targetSystemCounter] = examineSystem[index];
                        targetSystemCounter += 1;
                    }

                    markerDistance[targetID] = targetDistance;
                    markerSystems[targetID] = targetSystem;

                }

            }

        }
       
    }

}
