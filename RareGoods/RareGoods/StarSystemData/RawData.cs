using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RareGoods.RawData
{
    public class RawData
    {
        private readonly Dictionary<int, string> starSystemNames = new Dictionary<int, string>();
        private readonly Dictionary<int, string> stationNames = new Dictionary<int, string>();
        private readonly Dictionary<int, double[]> starSystemLocations = new Dictionary<int, double[]>();
        private readonly Dictionary<int, int> stationDistances = new Dictionary<int, int>();
        private readonly Dictionary<int, string> goodsNames = new Dictionary<int, string>();
        private readonly Dictionary<int, int> goodsPrices = new Dictionary<int, int>();
        private readonly Dictionary<int,int[]> goodsAmount = new Dictionary<int, int[]>(); 

      //  public Dictionary<int, StarSystem> starSystemData = new Dictionary<int, StarSystem>();

        public RawData()
        {
            StarSystemNameData();
            StarSystemLocationData();

            StationNameData();
            StationDistanceData();

            GoodsNameData();
            GoodsPriceData();
            GoodsAmountData();

            InitData();

        }

        public void StarSystemNameData()
        {

            starSystemNames.Add(0, "39 Tauri");
            starSystemNames.Add(1, "47 Ceti");
            starSystemNames.Add(2, "47 Ceti");
            starSystemNames.Add(3, "Aegaeon");
            starSystemNames.Add(4, "Aerial");
            starSystemNames.Add(5, "Aganippe");
            starSystemNames.Add(6, "Alacarakmo");
            starSystemNames.Add(7, "Alpha Centauri");
            starSystemNames.Add(8, "Altair");
            starSystemNames.Add(9, "Alya");
            starSystemNames.Add(10, "Anduliga");
            starSystemNames.Add(11, "Any Na");
            starSystemNames.Add(12, "Arouca");
            starSystemNames.Add(13, "AZ Cancri");
            starSystemNames.Add(14, "Baltah'Sine");
            starSystemNames.Add(15, "Banki");
            starSystemNames.Add(16, "Bast");
            starSystemNames.Add(17, "Belalans");
            starSystemNames.Add(18, "CD-75 661");
            starSystemNames.Add(19, "Cherbones");
            starSystemNames.Add(20, "Chi Eridani");
            starSystemNames.Add(21, "Coquim");
            starSystemNames.Add(22, "Damna");
            starSystemNames.Add(23, "Dea Motrona");
            starSystemNames.Add(24, "Delta Phoenicis");
            starSystemNames.Add(25, "Deuringas");
            starSystemNames.Add(26, "Diso");
            starSystemNames.Add(27, "Eleu");
            starSystemNames.Add(28, "Epsilon Indi");
            starSystemNames.Add(29, "Eranin");
            starSystemNames.Add(30, "Eshu");
            starSystemNames.Add(31, "Esuseku");
            starSystemNames.Add(32, "Ethgreze");
            starSystemNames.Add(33, "Fujin");
            starSystemNames.Add(34, "Geawen");
            starSystemNames.Add(35, "George Pantazis");
            starSystemNames.Add(36, "Geras");
            starSystemNames.Add(37, "Goman");
            starSystemNames.Add(38, "Haiden");
            starSystemNames.Add(39, "Havasupai");
            starSystemNames.Add(40, "Hecate");
            starSystemNames.Add(41, "Heike");
            starSystemNames.Add(42, "Helvetitj");
            starSystemNames.Add(43, "HIP 10175");
            starSystemNames.Add(44, "HIP 41181");
            starSystemNames.Add(45, "HIP 59533");
            starSystemNames.Add(46, "HIP 80364");
            starSystemNames.Add(47, "Holva");
            starSystemNames.Add(48, "HR 7221");
            starSystemNames.Add(49, "Irukama");
            starSystemNames.Add(50, "Jaradharre");
            starSystemNames.Add(51, "Jaroua");
            starSystemNames.Add(52, "Jotun (permit)");
            starSystemNames.Add(53, "Kachirigin");
            starSystemNames.Add(54, "Kamitra");
            starSystemNames.Add(55, "Kamorin");
            starSystemNames.Add(56, "Kappa Fornacis");
            starSystemNames.Add(57, "Karetii");
            starSystemNames.Add(58, "Karsuki Ti");
            starSystemNames.Add(59, "Kinago");
            starSystemNames.Add(60, "Kongga");
            starSystemNames.Add(61, "Korro Kung");
            starSystemNames.Add(62, "Lave");
            starSystemNames.Add(63, "LDS 883");
            starSystemNames.Add(64, "Leesti");
            starSystemNames.Add(65, "Leesti");
            starSystemNames.Add(66, "LFT 1421");
            starSystemNames.Add(67, "LP 375-25");
            starSystemNames.Add(68, "Mechucos");
            starSystemNames.Add(69, "Medb");
            starSystemNames.Add(70, "Mokojing");
            starSystemNames.Add(71, "Momus Reach");
            starSystemNames.Add(72, "Mukusubii");
            starSystemNames.Add(73, "Mulachi");
            starSystemNames.Add(74, "Neritus");
            starSystemNames.Add(75, "Ngadandari");
            starSystemNames.Add(76, "Nguna");
            starSystemNames.Add(77, "Ngurii");
            starSystemNames.Add(78, "Njangari");
            starSystemNames.Add(79, "Ochoeng");
            starSystemNames.Add(80, "Orrere");
            starSystemNames.Add(81, "Phiagre");
            starSystemNames.Add(82, "Quechua");
            starSystemNames.Add(83, "Rajukru");
            starSystemNames.Add(84, "Rapa Bao");
            starSystemNames.Add(85, "Rusani");
            starSystemNames.Add(86, "Sanuma");
            starSystemNames.Add(87, "Shinrarta Dezhra");
            starSystemNames.Add(88, "Tanmark");
            starSystemNames.Add(89, "Tarach Tor");
            starSystemNames.Add(90, "Terra Mater");
            starSystemNames.Add(91, "Thrutis");
            starSystemNames.Add(92, "Tiolce");
            starSystemNames.Add(93, "Toxandji");
            starSystemNames.Add(94, "Uszaa");
            starSystemNames.Add(95, "Utgaroar");
            starSystemNames.Add(96, "Uzumoku");
            starSystemNames.Add(97, "V1090 Herculis");
            starSystemNames.Add(98, "Vanayequi");
            starSystemNames.Add(99, "Vega");
            starSystemNames.Add(100, "Vidavanta");
            starSystemNames.Add(101, "Volkhab");
            starSystemNames.Add(102, "Wheemete");
            starSystemNames.Add(103, "Witchhaul");
            starSystemNames.Add(104, "Wulpa");
            starSystemNames.Add(105, "Wuthielo Ku");
            starSystemNames.Add(106, "Xihe");
            starSystemNames.Add(107, "Yaso Kondi");
            starSystemNames.Add(108, "Zaonce");
            starSystemNames.Add(109, "Zeessze");

        }

        public void StationNameData()
        {
            stationNames.Add(0, "Porta");
            stationNames.Add(1, "Glushko station");
            stationNames.Add(2, "Kaufmanis Hub");
            stationNames.Add(3, "Schweikart station");
            stationNames.Add(4, "Andrade Legacy");
            stationNames.Add(5, "Julian Market");
            stationNames.Add(6, "Weyl Gateway");
            stationNames.Add(7, "Hutton Oribtal");
            stationNames.Add(8, "Solo Orbiter");
            stationNames.Add(9, "Malaspina Gateway");
            stationNames.Add(10, "Celsius Estate");
            stationNames.Add(11, "Libby Orbital");
            stationNames.Add(12, "Shipton Orbital");
            stationNames.Add(13, "Fisher station");
            stationNames.Add(14, "Baltha'sine station");
            stationNames.Add(15, "Antonio De Andrade Vista");
            stationNames.Add(16, "Hart station");
            stationNames.Add(17, "Boscovich Ring");
            stationNames.Add(18, "Kirk Dock");
            stationNames.Add(19, "Chalker Landing");
            stationNames.Add(20, "Steve Masters station");
            stationNames.Add(21, "Hirayama Installation");
            stationNames.Add(22, "Nemere Market");
            stationNames.Add(23, "Pinzon Dock");
            stationNames.Add(24, "Trading post");
            stationNames.Add(25, "Shukor Hub");
            stationNames.Add(26, "Shifnalport");
            stationNames.Add(27, "Finney Dock");
            stationNames.Add(28, "Mansfield Orbiter");
            stationNames.Add(29, "Azeban City");
            stationNames.Add(30, "Shajn Terminal");
            stationNames.Add(31, "Savinykh Orbital");
            stationNames.Add(32, "Bloch station");
            stationNames.Add(33, "Futen Spaceport");
            stationNames.Add(34, "Obruchev Legacy");
            stationNames.Add(35, "Zamka Platform");
            stationNames.Add(36, "Yurchikhin Port");
            stationNames.Add(37, "Gustav Sporer Port");
            stationNames.Add(38, "Searfoss Enterprise");
            stationNames.Add(39, "Lovelace Port");
            stationNames.Add(40, "RJH1972");
            stationNames.Add(41, "Brunel City");
            stationNames.Add(42, "Friend Orbital");
            stationNames.Add(43, "Stefanyshyn-Piper station");
            stationNames.Add(44, "Andersson station");
            stationNames.Add(45, "Burnham Beacon");
            stationNames.Add(46, "Stasheff Colony");
            stationNames.Add(47, "Kreutz Orbital");
            stationNames.Add(48, "Veron City");
            stationNames.Add(49, "Blaauw City");
            stationNames.Add(50, "Gohar station");
            stationNames.Add(51, "Mccool City");
            stationNames.Add(52, "Icelock");
            stationNames.Add(53, "Nowak Orbital");
            stationNames.Add(54, "Hammel Terminal");
            stationNames.Add(55, "Godwin Vision");
            stationNames.Add(56, "Harvestport");
            stationNames.Add(57, "Sinclair Platform");
            stationNames.Add(58, "West Market");
            stationNames.Add(59, "Fozard Ring");
            stationNames.Add(60, "Laplace Ring");
            stationNames.Add(61, "Lonchakov Orbital");
            stationNames.Add(62, "Lave station");
            stationNames.Add(63, "Smith Reserve");
            stationNames.Add(64, "George Lucas");
            stationNames.Add(65, "George Lucas");
            stationNames.Add(66, "Ehrlich Orbital");
            stationNames.Add(67, "King Gateway");
            stationNames.Add(68, "Brandenstein Port");
            stationNames.Add(69, "Vela dock");
            stationNames.Add(70, "Noli Terminal");
            stationNames.Add(71, "Tartarus Point");
            stationNames.Add(72, "Ledyard Dock");
            stationNames.Add(73, "Clark Terminal");
            stationNames.Add(74, "Toll Ring");
            stationNames.Add(75, "Consolmagno Horizons");
            stationNames.Add(76, "Biggle Hub");
            stationNames.Add(77, "Cheranovsky City");
            stationNames.Add(78, "Lee Hub");
            stationNames.Add(79, "Roddenberry Gateway");
            stationNames.Add(80, "Sharon Lee Free Market");
            stationNames.Add(81, "Greeboski's Outpost");
            stationNames.Add(82, "Crown Ring");
            stationNames.Add(83, "Snyder Terminal");
            stationNames.Add(84, "Flagg Gateway");
            stationNames.Add(85, "Fernandes Market");
            stationNames.Add(86, "Dunyach Gateway");
            stationNames.Add(87, "Jameson Memorial");
            stationNames.Add(88, "Cassie-L-Peia");
            stationNames.Add(89, "Tranquillity");
            stationNames.Add(90, "Gr8minds");
            stationNames.Add(91, "Kingsbury Dock");
            stationNames.Add(92, "Gordon Terminal");
            stationNames.Add(93, "Tsunenaga Orbital");
            stationNames.Add(94, "Guest Installation");
            stationNames.Add(95, "Fort Klarix");
            stationNames.Add(96, "Sevrdup Ring");
            stationNames.Add(97, "Kaku Plant");
            stationNames.Add(98, "Clauss Hub");
            stationNames.Add(99, "Taylor City");
            stationNames.Add(100, "Lee Mines");
            stationNames.Add(101, "Vernadsky Dock");
            stationNames.Add(102, "Eisinga Enterprise");
            stationNames.Add(103, "Hornby Terminal");
            stationNames.Add(104, "Williams Gateway");
            stationNames.Add(105, "Tarter Dock");
            stationNames.Add(106, "Zhen Dock");
            stationNames.Add(107, "Wheeler Market");
            stationNames.Add(108, "Ridley Scott");
            stationNames.Add(109, "Nicollier Hanger");
        }

        public void StarSystemLocationData()
        {
            starSystemLocations.Add(0, new double[] {-7.31, -20.28, -50.91});
            starSystemLocations.Add(1, new double[] {-14.13, -116.97, -32.53});
            starSystemLocations.Add(2, new double[] {-14.13, -116.97, -32.53});
            starSystemLocations.Add(3, new double[] {46.91, 23.63, -59.75});
            starSystemLocations.Add(4, new double[] {100.75, -102.63, 8.41});
            starSystemLocations.Add(5, new double[] {-11.56, 43.81, 11.63});
            starSystemLocations.Add(6, new double[] {-32.41, 169.53, -49.44});
            starSystemLocations.Add(7, new double[] {3.03, -0.09, 3.16});
            starSystemLocations.Add(8, new double[] {-12.31, -2.75, 11});
            starSystemLocations.Add(9, new double[] {-77.28, -57.437, 30.53});
            starSystemLocations.Add(10, new double[] {124.63, 2.5, 61.25});
            starSystemLocations.Add(11, new double[] {125.66, -1.72, 14.09});
            starSystemLocations.Add(12, new double[] {104.97, -6.53, -4.41});
            starSystemLocations.Add(13, new double[] {19.66, 26.84, -38.09});
            starSystemLocations.Add(14, new double[] {85.16, -56.31, 40.34});
            starSystemLocations.Add(15, new double[] {49.53, 15.75, -91.72});
            starSystemLocations.Add(16, new double[] {-36.47, 16.16, -34.94});
            starSystemLocations.Add(17, new double[] {81.63, -94.88, -58.56});
            starSystemLocations.Add(18, new double[] {67.88, -21.5, 51.16});
            starSystemLocations.Add(19, new double[] {5.19, 84.53, -16.75});
            starSystemLocations.Add(20, new double[] {26.28, -51.75, 4.63});
            starSystemLocations.Add(21, new double[] {20.75, -82.25, 33.59});
            starSystemLocations.Add(22, new double[] {-40.53, 4.18, -124.25});
            starSystemLocations.Add(23, new double[] {-12.16, 62.63, 29.72});
            starSystemLocations.Add(24, new double[] {53.91, -130.69, 14.66});
            starSystemLocations.Add(25, new double[] {137.31, 3.84, -35.91});
            starSystemLocations.Add(26, new double[] {72.16, 48.75, 70.75});
            starSystemLocations.Add(27, new double[] {-29.66, 32.69, 104.84});
            starSystemLocations.Add(28, new double[] {3.13, -8.88, 7.13});
            starSystemLocations.Add(29, new double[] {-22.84, 36.53, -1.19});
            starSystemLocations.Add(30, new double[] {120.78, -247.19, -16.44});
            starSystemLocations.Add(31, new double[] {-107.88, 29.56, -20.94});
            starSystemLocations.Add(32, new double[] {-30.03, 72.34, -23.81});
            starSystemLocations.Add(33, new double[] {-6.03, -30.38, -59.03});
            starSystemLocations.Add(34, new double[] {22.5, 23.78, 171.06});
            starSystemLocations.Add(35, new double[] {-12.09, -16, -14.22});
            starSystemLocations.Add(36, new double[] {-7.03, -12.88, -56.38});
            starSystemLocations.Add(37, new double[] {150.88, -173.78, 25.28});
            starSystemLocations.Add(38, new double[] {-142.03, -13.34, -43.91});
            starSystemLocations.Add(39, new double[] {-88.75, -76.75, -39.63});
            starSystemLocations.Add(40, new double[] {-56, -25.13, -44.28});
            starSystemLocations.Add(41, new double[] {-76.97, 71.91, 69.19});
            starSystemLocations.Add(42, new double[] {-23.19, 80.03, 61.84});
            starSystemLocations.Add(43, new double[] {-45.78, -93, -83.91});
            starSystemLocations.Add(44, new double[] {-8.16, 74.81, -105.13});
            starSystemLocations.Add(45, new double[] {122.97, -9.06, 69});
            starSystemLocations.Add(46, new double[] {-101.59, 93.78, 9.91});
            starSystemLocations.Add(47, new double[] {58.69, -170.97, -41.97});
            starSystemLocations.Add(48, new double[] {58.53, -55.81, 91.25});
            starSystemLocations.Add(49, new double[] {140.72, -96.97, 67.78});
            starSystemLocations.Add(50, new double[] {39.53, 21.28, 56.63});
            starSystemLocations.Add(51, new double[] {157.53, -110.53, 28.25});
            starSystemLocations.Add(52, new double[] {-11.03, -79.22, -92.31});
            starSystemLocations.Add(53, new double[] {-105.38, -73.34, 27.75});
            starSystemLocations.Add(54, new double[] {5.53, -183.41, 63.84});
            starSystemLocations.Add(55, new double[] {-123.88, -81.59, 45.19});
            starSystemLocations.Add(56, new double[] {12.47, -66.72, -22.91});
            starSystemLocations.Add(57, new double[] {-125.59, 44.03, 78.41});
            starSystemLocations.Add(58, new double[] {134.03, -163.59, 71.06});
            starSystemLocations.Add(59, new double[] {-67.41, -7.41, 150.06});
            starSystemLocations.Add(60, new double[] {-104.16, 82.72, -32.38});
            starSystemLocations.Add(61, new double[] {81.03, 52.84, 31.5});
            starSystemLocations.Add(62, new double[] {75.75, 48.75, 70.75});
            starSystemLocations.Add(63, new double[] {-28.72, -35.72, -61.44});
            starSystemLocations.Add(64, new double[] {72.75, 48.75, 68.25});
            starSystemLocations.Add(65, new double[] {72.75, 48.75, 68.25});
            starSystemLocations.Add(66, new double[] {-45.47, 18.56, 12.59});
            starSystemLocations.Add(67, new double[] {9.53, 59.31, -13.22});
            starSystemLocations.Add(68, new double[] {67.03, 39.59, -70.09});
            starSystemLocations.Add(69, new double[] {12.78, 4.81, 39.34});
            starSystemLocations.Add(70, new double[] {73.44, 58.34, -0.22});
            starSystemLocations.Add(71, new double[] {-34.94, -44.16, -77.34});
            starSystemLocations.Add(72, new double[] {-147.47, -64.13, 46.09});
            starSystemLocations.Add(73, new double[] {6.25, -5.78, 35.94});
            starSystemLocations.Add(74, new double[] {75.44, 2.63, -30.88});
            starSystemLocations.Add(75, new double[] {62.75, -74.19, 109.5});
            starSystemLocations.Add(76, new double[] {-122.78, -102.53, -22.56});
            starSystemLocations.Add(77, new double[] {169.5, -42.34, 87.06});
            starSystemLocations.Add(78, new double[] {8.88, -205.44, 64.38});
            starSystemLocations.Add(79, new double[] {-139.06, -2.31, -6.66});
            starSystemLocations.Add(80, new double[] {68.84, 48.75, 76.75});
            starSystemLocations.Add(81, new double[] {44.28, -82.97, 52.5});
            starSystemLocations.Add(82, new double[] {52.09, -112.09, -40.78});
            starSystemLocations.Add(83, new double[] {-96.94, 143.44, 4.63});
            starSystemLocations.Add(84, new double[] {94.16, -167.38, -23.19});
            starSystemLocations.Add(85, new double[] {-64.75, 57.22, -82.63});
            starSystemLocations.Add(86, new double[] {153.44, 61.44, 79.03});
            starSystemLocations.Add(87, new double[] {55.72, 17.59, 27.16});
            starSystemLocations.Add(88, new double[] {1.69, -77.88, -30.09});
            starSystemLocations.Add(89, new double[] {-13.13, -81, -20.28});
            starSystemLocations.Add(90, new double[] {-49.75, -19.03, -45});
            starSystemLocations.Add(91, new double[] {-90.69, 8.13, -79.53});
            starSystemLocations.Add(92, new double[] {10.5, -34.78, 25.06});
            starSystemLocations.Add(93, new double[] {155.09, -12.41, 61.06});
            starSystemLocations.Add(94, new double[] {68.84, 48.75, 74.75});
            starSystemLocations.Add(95, new double[] {-0.16, -102.75, -5.56});
            starSystemLocations.Add(96, new double[] {-95.22, 9.88, -74.13});
            starSystemLocations.Add(97, new double[] {-44.94, 36.94, 13.47});
            starSystemLocations.Add(98, new double[] {-88.5, -12.31, 98.84});
            starSystemLocations.Add(99, new double[] {-21.91, 8.13, 9});
            starSystemLocations.Add(100, new double[] {-50.94, 90.22, 65.81});
            starSystemLocations.Add(101, new double[] {36.53, 100.06, -135.72});
            starSystemLocations.Add(102, new double[] {145.13, -140.06, 131.97});
            starSystemLocations.Add(103, new double[] {-13.25, -52.06, -65.88});
            starSystemLocations.Add(104, new double[] {-126.09, -83.09, -85.56});
            starSystemLocations.Add(105, new double[] {68.31, -190.03, 12.38});
            starSystemLocations.Add(106, new double[] {-19.81, -35.38, 34});
            starSystemLocations.Add(107, new double[] {4.94, -175.91, -0.91});
            starSystemLocations.Add(108, new double[] {80.75, 48.75, 69.25});
            starSystemLocations.Add(109, new double[] {-19.81, -3.25, -14.28});

        }

        private void StationDistanceData()
        {
            stationDistances.Add(0, 991);
            stationDistances.Add(1, 600000);
            stationDistances.Add(2, 600000);
            stationDistances.Add(3, 11655);
            stationDistances.Add(4, 180);
            stationDistances.Add(5, 133);
            stationDistances.Add(6, 16);
            stationDistances.Add(7, 6942672);
            stationDistances.Add(8, 661);
            stationDistances.Add(9, 540000);
            stationDistances.Add(10, 626220);
            stationDistances.Add(11, 581);
            stationDistances.Add(12, 410);
            stationDistances.Add(13, 16);
            stationDistances.Add(14, 356);
            stationDistances.Add(15, 531869);
            stationDistances.Add(16, 202);
            stationDistances.Add(17, 78);
            stationDistances.Add(18, 339);
            stationDistances.Add(19, 576740);
            stationDistances.Add(20, 2181);
            stationDistances.Add(21, 606);
            stationDistances.Add(22, 2532);
            stationDistances.Add(23, 1700000);
            stationDistances.Add(24, 3799);
            stationDistances.Add(25, 798);
            stationDistances.Add(26, 284);
            stationDistances.Add(27, 333);
            stationDistances.Add(28, 143);
            stationDistances.Add(29, 297);
            stationDistances.Add(30, 1742);
            stationDistances.Add(31, 276);
            stationDistances.Add(32, 351);
            stationDistances.Add(33, 559);
            stationDistances.Add(34, 426261);
            stationDistances.Add(35, 45);
            stationDistances.Add(36, 1052931);
            stationDistances.Add(37, 291);
            stationDistances.Add(38, 561080);
            stationDistances.Add(39, 601314);
            stationDistances.Add(40, 570);
            stationDistances.Add(41, 406);
            stationDistances.Add(42, 444);
            stationDistances.Add(43, 4850);
            stationDistances.Add(44, 536714);
            stationDistances.Add(45, 529600);
            stationDistances.Add(46, 2632);
            stationDistances.Add(47, 22000);
            stationDistances.Add(48, 1886);
            stationDistances.Add(49, 323);
            stationDistances.Add(50, 526178);
            stationDistances.Add(51, 143);
            stationDistances.Add(52, 80);
            stationDistances.Add(53, 340);
            stationDistances.Add(54, 82);
            stationDistances.Add(55, 445);
            stationDistances.Add(56, 919);
            stationDistances.Add(57, 990);
            stationDistances.Add(58, 28);
            stationDistances.Add(59, 1723);
            stationDistances.Add(60, 254);
            stationDistances.Add(61, 622040);
            stationDistances.Add(62, 302);
            stationDistances.Add(63, 54480);
            stationDistances.Add(64, 257);
            stationDistances.Add(65, 257);
            stationDistances.Add(66, 773000);
            stationDistances.Add(67, 514700);
            stationDistances.Add(68, 534137);
            stationDistances.Add(69, 1686844);
            stationDistances.Add(70, 539400);
            stationDistances.Add(71, 413);
            stationDistances.Add(72, 505430);
            stationDistances.Add(73, 594550);
            stationDistances.Add(74, 524);
            stationDistances.Add(75, 502357);
            stationDistances.Add(76, 1870);
            stationDistances.Add(77, 1265);
            stationDistances.Add(78, 21400);
            stationDistances.Add(79, 484);
            stationDistances.Add(80, 963);
            stationDistances.Add(81, 85);
            stationDistances.Add(82, 120);
            stationDistances.Add(83, 117);
            stationDistances.Add(84, 2338);
            stationDistances.Add(85, 72);
            stationDistances.Add(86, 6000);
            stationDistances.Add(87, 347);
            stationDistances.Add(88, 414);
            stationDistances.Add(89, 359);
            stationDistances.Add(90, 504);
            stationDistances.Add(91, 288);
            stationDistances.Add(92, 158);
            stationDistances.Add(93, 7000);
            stationDistances.Add(94, 4350);
            stationDistances.Add(95, 171);
            stationDistances.Add(96, 505300);
            stationDistances.Add(97, 1233592);
            stationDistances.Add(98, 81);
            stationDistances.Add(99, 1100);
            stationDistances.Add(100, 556882);
            stationDistances.Add(101, 408);
            stationDistances.Add(102, 551628);
            stationDistances.Add(103, 219);
            stationDistances.Add(104, 28);
            stationDistances.Add(105, 145);
            stationDistances.Add(106, 1965);
            stationDistances.Add(107, 114);
            stationDistances.Add(108, 400);
            stationDistances.Add(109, 489);
        }

        private void GoodsNameData()
        {
            goodsNames.Add(0, "Tauri Chimes");
            goodsNames.Add(1, "Aepyornis Egg");
            goodsNames.Add(2, "Ceti Rabbits");
            goodsNames.Add(3, "Chateau De Aegaeon");
            goodsNames.Add(4, "Edan Apples of Aerial");
            goodsNames.Add(5, "Aganippe Rush");
            goodsNames.Add(6, "Alacarakmo Skin Art");
            goodsNames.Add(7, "Centauri Mega Gin");
            goodsNames.Add(8, "Altairian Skin");
            goodsNames.Add(9, "Alya Body Soup");
            goodsNames.Add(10, "Anduliga Fire Works");
            goodsNames.Add(11, "Any Na Coffee");
            goodsNames.Add(12, "Arouca Conventula Sweets");
            goodsNames.Add(13, "Az Cancri Formula 42");
            goodsNames.Add(14, "Baltha'sine Vacuum Krill");
            goodsNames.Add(15, "Banki Amphibious Leather");
            goodsNames.Add(16, "Bast Snake Gin");
            goodsNames.Add(17, "Belalans Ray Leather");
            goodsNames.Add(18, "CD-75 Kitten Brand Coffee");
            goodsNames.Add(19, "Cherbones Blood Crystals");
            goodsNames.Add(20, "Chi Eridani Marine Paste");
            goodsNames.Add(21, "Coquim Spongiform Victuals");
            goodsNames.Add(22, "Damna Carapaces");
            goodsNames.Add(23, "Motrona Experience Jelly");
            goodsNames.Add(24, "Delta Phoenicis Palms");
            goodsNames.Add(25, "Deuringas Truffles");
            goodsNames.Add(26, "Diso Ma Corn");
            goodsNames.Add(27, "Eleu Thermals");
            goodsNames.Add(28, "Indi Bourbon ");
            goodsNames.Add(29, "Eranin Whiskey");
            goodsNames.Add(30, "Eshu Umbrellas");
            goodsNames.Add(31, "Esuseku Caviar");
            goodsNames.Add(32, "Ethgreze Tea Buds");
            goodsNames.Add(33, "Fujin Tea");
            goodsNames.Add(34, "Geawen Dance Dust");
            goodsNames.Add(35, "Pantaa Prayer Sticks");
            goodsNames.Add(36, "Gerasian Gueuze Beer");
            goodsNames.Add(37, "Goman Yaupon Coffee");
            goodsNames.Add(38, "Haidne Black Brew");
            goodsNames.Add(39, "Havasupai Dream Catcher");
            goodsNames.Add(40, "Live Hecate Sea Worms");
            goodsNames.Add(41, "Ceremonial Heike Tea");
            goodsNames.Add(42, "Helvetitj Pearls");
            goodsNames.Add(43, "HIP 10175 Bush Meat");
            goodsNames.Add(44, "HIP Proto-Squid");
            goodsNames.Add(45, "Burnham Bile Distillate");
            goodsNames.Add(46, "HIP Organophospates");
            goodsNames.Add(47, "Holva Duelling Blades");
            goodsNames.Add(48, "HR 7221 Wheat");
            goodsNames.Add(49, "Giant Irukama Snails");
            goodsNames.Add(50, "Jaradharre Puzzle Box");
            goodsNames.Add(51, "Jaroua Rice");
            goodsNames.Add(52, "Jotun Mookah");
            goodsNames.Add(53, "Kachirigin Filter Leeches");
            goodsNames.Add(54, "Kamitra Cigars");
            goodsNames.Add(55, "Kamorin Historic Weapons");
            goodsNames.Add(56, "Onion Head");
            goodsNames.Add(57, "Karetii Couture");
            goodsNames.Add(58, "Karsuki Locusts");
            goodsNames.Add(59, "Kinago Violins");
            goodsNames.Add(60, "Kongga Ale");
            goodsNames.Add(61, "Koro Kung Pellets");
            goodsNames.Add(62, "Lavian Brandy ");
            goodsNames.Add(63, "Chameleon Cloth");
            goodsNames.Add(64, "Leestian Evil Juice");
            goodsNames.Add(65, "Azure Milk");
            goodsNames.Add(66, "Void Extract Coffee");
            goodsNames.Add(67, "Honesty Pills");
            goodsNames.Add(68, "Mechucos High Tea");
            goodsNames.Add(69, "Medb starlube");
            goodsNames.Add(70, "Mokojing Beast Feast");
            goodsNames.Add(71, "Momus Bog Spaniel");
            goodsNames.Add(72, "Mukusubii Chitin-os");
            goodsNames.Add(73, "Mulachi Giant Fungus");
            goodsNames.Add(74, "Neritus Berries");
            goodsNames.Add(75, "Ngandandari Fire Opals");
            goodsNames.Add(76, "Nguna Modern Antiques");
            goodsNames.Add(77, "Soontill Relics");
            goodsNames.Add(78, "Njangari Saddles");
            goodsNames.Add(79, "Ochoeng Chillies");
            goodsNames.Add(80, "Orrerian Vicious Brew");
            goodsNames.Add(81, "Giant Verrix");
            goodsNames.Add(82, "Albino Quechua Mammoth Meat");
            goodsNames.Add(83, "Rajukru Multi-Stoves");
            goodsNames.Add(84, "Rapa Bao Snake Skins");
            goodsNames.Add(85, "Rusani Old Smokey");
            goodsNames.Add(86, "Sanuma Decorative Meat");
            goodsNames.Add(87, "Waters of Shintara");
            goodsNames.Add(88, "Tanmark Tranquil Tea");
            goodsNames.Add(89, "Tarach Spice");
            goodsNames.Add(90, "Terra Mater Blood Bores");
            goodsNames.Add(91, "Thrutis Cream");
            goodsNames.Add(92, "Tiolce Waste 2 Paste");
            goodsNames.Add(93, "Toxandji Virocide");
            goodsNames.Add(94, "Uszaian Tree Grub");
            goodsNames.Add(95, "Utgaroar Millennial Eggs");
            goodsNames.Add(96, "Uzumoku Low-G Wings");
            goodsNames.Add(97, "V Herculis Body Rub");
            goodsNames.Add(98, "Vanayequi Ceratomorpha Fur");
            goodsNames.Add(99, "Vega Slimweed");
            goodsNames.Add(100, "Vidavantian Lace");
            goodsNames.Add(101, "Volkhab Bee Drones");
            goodsNames.Add(102, "Wheemete Wheat Cakes");
            goodsNames.Add(103, "Witchhaul Kobe Beef ");
            goodsNames.Add(104, "Wulpa Hyperbore Systems");
            goodsNames.Add(105, "Wuthielo Ku Froth");
            goodsNames.Add(106, "Xihe Biomorphic Companions");
            goodsNames.Add(107, "Yaso Kondi Leaf");
            goodsNames.Add(108, "Leathery Eggs");
            goodsNames.Add(109, "Zeessze Ant Grub Glue");
        }

        private void GoodsPriceData()
        {
            goodsPrices.Add(0, 924);
            goodsPrices.Add(1, 2654);
            goodsPrices.Add(2, 1675);
            goodsPrices.Add(3, 1283);
            goodsPrices.Add(4, 621);
            goodsPrices.Add(5, 8966);
            goodsPrices.Add(6, 1421);
            goodsPrices.Add(7, 3319);
            goodsPrices.Add(8, 489);
            goodsPrices.Add(9, 454);
            goodsPrices.Add(10, 891);
            goodsPrices.Add(11, 1790);
            goodsPrices.Add(12, 1191);
            goodsPrices.Add(13, 6442);
            goodsPrices.Add(14, 825);
            goodsPrices.Add(15, 634);
            goodsPrices.Add(16, 1089);
            goodsPrices.Add(17, 364);
            goodsPrices.Add(18, 2373);
            goodsPrices.Add(19, 12550);
            goodsPrices.Add(20, 790);
            goodsPrices.Add(21, 255);
            goodsPrices.Add(22, 315);
            goodsPrices.Add(23, 7420);
            goodsPrices.Add(24, 211);
            goodsPrices.Add(25, 1892);
            goodsPrices.Add(26, 340);
            goodsPrices.Add(27, 863);
            goodsPrices.Add(28, 978);
            goodsPrices.Add(29, 1633);
            goodsPrices.Add(30, 2050);
            goodsPrices.Add(31, 2450);
            goodsPrices.Add(32, 3308);
            goodsPrices.Add(33, 992);
            goodsPrices.Add(34, 1022);
            goodsPrices.Add(35, 1827);
            goodsPrices.Add(36, 456);
            goodsPrices.Add(37, 1451);
            goodsPrices.Add(38, 1347);
            goodsPrices.Add(39, 9636);
            goodsPrices.Add(40, 1190);
            goodsPrices.Add(41, 1920);
            goodsPrices.Add(42, 3620);
            goodsPrices.Add(43, 2105);
            goodsPrices.Add(44, 1488);
            goodsPrices.Add(45, 806);
            goodsPrices.Add(46, 385);
            goodsPrices.Add(47, 6518);
            goodsPrices.Add(48, 415);
            goodsPrices.Add(49, 1810);
            goodsPrices.Add(50, 12706);
            goodsPrices.Add(51, 385);
            goodsPrices.Add(52, 1252);
            goodsPrices.Add(53, 473);
            goodsPrices.Add(54, 5800);
            goodsPrices.Add(55, 2678);
            goodsPrices.Add(56, 765);
            goodsPrices.Add(57, 5225);
            goodsPrices.Add(58, 915);
            goodsPrices.Add(59, 7279);
            goodsPrices.Add(60, 595);
            goodsPrices.Add(61, 220);
            goodsPrices.Add(62, 3543);
            goodsPrices.Add(63, 1664);
            goodsPrices.Add(64, 462);
            goodsPrices.Add(65, 4164);
            goodsPrices.Add(66, 2357);
            goodsPrices.Add(67, 1365);
            goodsPrices.Add(68, 1345);
            goodsPrices.Add(69, 416);
            goodsPrices.Add(70, 2681);
            goodsPrices.Add(71, 1836);
            goodsPrices.Add(72, 654);
            goodsPrices.Add(73, 86);
            goodsPrices.Add(74, 850);
            goodsPrices.Add(75, 16028);
            goodsPrices.Add(76, 930);
            goodsPrices.Add(77, 17000);
            goodsPrices.Add(78, 650);
            goodsPrices.Add(79, 998);
            goodsPrices.Add(80, 533);
            goodsPrices.Add(81, 6552);
            goodsPrices.Add(82, 2538);
            goodsPrices.Add(83, 682);
            goodsPrices.Add(84, 550);
            goodsPrices.Add(85, 5810);
            goodsPrices.Add(86, 860);
            goodsPrices.Add(87, 3710);
            goodsPrices.Add(88, 1814);
            goodsPrices.Add(89, 1056);
            goodsPrices.Add(90, 7824);
            goodsPrices.Add(91, 925);
            goodsPrices.Add(92, 1153);
            goodsPrices.Add(93, 539);
            goodsPrices.Add(94, 965);
            goodsPrices.Add(95, 1795);
            goodsPrices.Add(96, 8496);
            goodsPrices.Add(97, 160);
            goodsPrices.Add(98, 615);
            goodsPrices.Add(99, 2398);
            goodsPrices.Add(100, 6771);
            goodsPrices.Add(101, 3262);
            goodsPrices.Add(102, 262);
            goodsPrices.Add(103, 4520);
            goodsPrices.Add(104, 1175);
            goodsPrices.Add(105, 420);
            goodsPrices.Add(106, 4482);
            goodsPrices.Add(107, 6060);
            goodsPrices.Add(108, 24590);
            goodsPrices.Add(109, 374);

        }

        private void GoodsAmountData()
        {
            goodsAmount.Add(0, new int[] {1, 15});
            goodsAmount.Add(1, new int[] {4, 4});
            goodsAmount.Add(2, new int[] {11, 11});
            goodsAmount.Add(3, new int[] {0, 9});
            goodsAmount.Add(4, new int[] {3, 15});
            goodsAmount.Add(5, new int[] {4, 4});
            goodsAmount.Add(6, new int[] {9, 14});
            goodsAmount.Add(7, new int[] {1, 6});
            goodsAmount.Add(8, new int[] {2, 22});
            goodsAmount.Add(9, new int[] {2, 7});
            goodsAmount.Add(10, new int[] {0, 11});
            goodsAmount.Add(11, new int[] {2, 11});
            goodsAmount.Add(12, new int[] {1, 6});
            goodsAmount.Add(13, new int[] {1, 5});
            goodsAmount.Add(14, new int[] {2, 10});
            goodsAmount.Add(15, new int[] {16, 27});
            goodsAmount.Add(16, new int[] {4, 5});
            goodsAmount.Add(17, new int[] {3, 6});
            goodsAmount.Add(18, new int[] {5, 11});
            goodsAmount.Add(19, new int[] {2, 3});
            goodsAmount.Add(20, new int[] {5, 12});
            goodsAmount.Add(21, new int[] {4, 14});
            goodsAmount.Add(22, new int[] {2, 8});
            goodsAmount.Add(23, new int[] {11, 11});
            goodsAmount.Add(24, new int[] {2, 17});
            goodsAmount.Add(25, new int[] {2, 5});
            goodsAmount.Add(26, new int[] {1, 10});
            goodsAmount.Add(27, new int[] {2, 4});
            goodsAmount.Add(28, new int[] {3, 9});
            goodsAmount.Add(29, new int[] {1, 7});
            goodsAmount.Add(30, new int[] {1, 6});
            goodsAmount.Add(31, new int[] {1, 10});
            goodsAmount.Add(32, new int[] {1, 7});
            goodsAmount.Add(33, new int[] {2, 10});
            goodsAmount.Add(34, new int[] {26, 26});
            goodsAmount.Add(35, new int[] {3, 17});
            goodsAmount.Add(36, new int[] {9, 22});
            goodsAmount.Add(37, new int[] {3, 11});
            goodsAmount.Add(38, new int[] {1, 8});
            goodsAmount.Add(39, new int[] {1, 2});
            goodsAmount.Add(40, new int[] {2, 6});
            goodsAmount.Add(41, new int[] {1, 6});
            goodsAmount.Add(42, new int[] {1, 3});
            goodsAmount.Add(43, new int[] {4, 12});
            goodsAmount.Add(44, new int[] {1, 11});
            goodsAmount.Add(45, new int[] {13, 13});
            goodsAmount.Add(46, new int[] {1, 18});
            goodsAmount.Add(47, new int[] {1, 6});
            goodsAmount.Add(48, new int[] {1, 14});
            goodsAmount.Add(49, new int[] {2, 7});
            goodsAmount.Add(50, new int[] {1, 6});
            goodsAmount.Add(51, new int[] {2, 16});
            goodsAmount.Add(52, new int[] {4, 24});
            goodsAmount.Add(53, new int[] {2, 9});
            goodsAmount.Add(54, new int[] {1, 4});
            goodsAmount.Add(55, new int[] {1, 8});
            goodsAmount.Add(56, new int[] {0, 0});
            goodsAmount.Add(57, new int[] {2, 5});
            goodsAmount.Add(58, new int[] {7, 11});
            goodsAmount.Add(59, new int[] {1, 1});
            goodsAmount.Add(60, new int[] {1, 12});
            goodsAmount.Add(61, new int[] {6, 14});
            goodsAmount.Add(62, new int[] {4, 7});
            goodsAmount.Add(63, new int[] {3, 5});
            goodsAmount.Add(64, new int[] {1, 10});
            goodsAmount.Add(65, new int[] {2, 7});
            goodsAmount.Add(66, new int[] {3, 11});
            goodsAmount.Add(67, new int[] {13, 13});
            goodsAmount.Add(68, new int[] {0, 0});
            goodsAmount.Add(69, new int[] {18, 18});
            goodsAmount.Add(70, new int[] {1, 3});
            goodsAmount.Add(71, new int[] {3, 7});
            goodsAmount.Add(72, new int[] {3, 5});
            goodsAmount.Add(73, new int[] {11, 19});
            goodsAmount.Add(74, new int[] {6, 7});
            goodsAmount.Add(75, new int[] {3, 5});
            goodsAmount.Add(76, new int[] {2, 9});
            goodsAmount.Add(77, new int[] {1, 4});
            goodsAmount.Add(78, new int[] {7, 7});
            goodsAmount.Add(79, new int[] {1, 9});
            goodsAmount.Add(80, new int[] {6, 10});
            goodsAmount.Add(81, new int[] {1, 2});
            goodsAmount.Add(82, new int[] {1, 6});
            goodsAmount.Add(83, new int[] {6, 27});
            goodsAmount.Add(84, new int[] {5, 6});
            goodsAmount.Add(85, new int[] {3, 5});
            goodsAmount.Add(86, new int[] {1, 17});
            goodsAmount.Add(87, new int[] {4, 6});
            goodsAmount.Add(88, new int[] {1, 5});
            goodsAmount.Add(89, new int[] {1, 7});
            goodsAmount.Add(90, new int[] {0, 0});
            goodsAmount.Add(91, new int[] {1, 1});
            goodsAmount.Add(92, new int[] {9, 9});
            goodsAmount.Add(93, new int[] {2, 14});
            goodsAmount.Add(94, new int[] {3, 14});
            goodsAmount.Add(95, new int[] {3, 10});
            goodsAmount.Add(96, new int[] {4, 7});
            goodsAmount.Add(97, new int[] {2, 15});
            goodsAmount.Add(98, new int[] {3, 10});
            goodsAmount.Add(99, new int[] {2, 6});
            goodsAmount.Add(100, new int[] {2, 2});
            goodsAmount.Add(101, new int[] {3, 3});
            goodsAmount.Add(102, new int[] {5, 10});
            goodsAmount.Add(103, new int[] {2, 5});
            goodsAmount.Add(104, new int[] {0, 0});
            goodsAmount.Add(105, new int[] {1, 16});
            goodsAmount.Add(106, new int[] {3, 10});
            goodsAmount.Add(107, new int[] {1, 1});
            goodsAmount.Add(108, new int[] {1, 1});
            goodsAmount.Add(109, new int[] {1, 18});

        }

        private void InitData()
        {
            
            StarSystem[] tempData = new StarSystem[starSystemNames.Count];

            foreach (KeyValuePair<int, string> system in starSystemNames)
            {
                
                tempData[system.Key]=new StarSystem();
                    
                tempData[system.Key].SystemID = system.Key;
                tempData[system.Key].SystemName = system.Value;
            }

            foreach (KeyValuePair<int, double[]> location in starSystemLocations)
            {
                tempData[location.Key].Location = location.Value;

                tempData[location.Key].SystemX = location.Value[0];
                tempData[location.Key].SystemY = location.Value[1];
                tempData[location.Key].SystemZ = location.Value[2];
            }

            foreach (KeyValuePair<int, string> station in stationNames)
            {
                tempData[station.Key].StationName = station.Value;
            }

            foreach (KeyValuePair<int, int> distance in stationDistances)
            {
                tempData[distance.Key].StationDistance = distance.Value;
            }

            foreach (KeyValuePair<int, string> goods in goodsNames)
            {
                tempData[goods.Key].GoodsName = goods.Value;
            }

            foreach (KeyValuePair<int, int> prices in goodsPrices)
            {
                tempData[prices.Key].GoodsPrices = prices.Value;
            }

            foreach (KeyValuePair<int,int[]> amount in goodsAmount)
            {
                tempData[amount.Key].AmountTable = amount.Value;

                tempData[amount.Key].MinAmount = amount.Value[0];
                tempData[amount.Key].MaxAmount = amount.Value[1];
                
            }
            starSystemDataSet = starSystemNames.ToDictionary(system => tempData[system.Key].SystemID, system => tempData[system.Key]);

            tempData = null;
        }


        public Dictionary<int, StarSystem> starSystemDataSet { get; set; }
    }

    public class StarSystem
    {
        public int SystemID { get; set; }

        public string SystemName { get; set; }
        public string StationName { get; set; }
        public string GoodsName { get; set; }

        public double[] Location { get; set; }

        public double[] DistanceTable { get; set; }
        public int[] SortedSystems { get; set; }
        public double[] SortedDistances { get; set; }

        public string[] DisplayDistances { get; set; }
        public string DisplaySystem { get; set; }
        public string DisplayStation { get; set; }
        public string DisplayGoods { get; set; }

        public double[] VectorXTable { get; set; }
        public double[] VectorYTable { get; set; }
        public double[] VectorZTable { get; set; }

        public double[] HeadingTable { get; set; }

        public double SystemX { get; set; }
        public double SystemY { get; set; }
        public double SystemZ { get; set; }

        public double StationDistance { get; set; }

        public int GoodsPrices { get; set; }
        public int[] AmountTable { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }

       
    }


}
