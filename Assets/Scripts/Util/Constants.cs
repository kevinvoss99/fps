using System.Diagnostics;
using System.IO;
using System.Text;
using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Assets.Scripts.Model;
using System.Linq;

namespace Assets.Scripts.Util
{

    /**
     * Diese Klasse stellt einige statische Methoden zur Verfügung, die für den Programmablauf unerlässlich sind.
     */
    public class Constants
    {
        /**
         * Die (fixe) Adresse des Hosts, auf den verbunden werden soll.
         */
        public const string HOST_ADDRESS = "127.0.0.1";
        /**
         * Der (fixe) Port, auf dem verbunden werden soll.
         */
        public const int PORT = 5556;
        /**
         * Name des Haupt-Portals.
         */
        public const string MAIN_PORTAL_NAME = "MainPortal";
        /**
         * Name des Sub-Portals.
         */
        public const string PORTAL_NAME = "Portal";
        /**
         * Name bzw. Pfad der JAR Datei des CBR-Systems.
         */
        public const string SERVER_PATH = "CBRSystem.jar";
        /**
         * Der Name des Kommunikationsagenten.
         */
        public const string COMMUNICATION_AGENT_NAME = "CommunicationAgent";
        /**
         * Der Name des Planungsagenten.
         */
        public const string PLANNING_AGENT_NAME = "PlanningAgent";

        /**
         * Der Prozess, der für den Start und die Terminierung des CBR-Systems (als JAR) benötigt wird.
         */
        public static Process proc;

        /**
         * Konstanter Pfad zur Log-Datei.
         */
        private const string TEMP_PATH = @"log.txt";
        /**
         * Der aktuelle Pfad als String.
         */
        public static string PATH = Directory.GetCurrentDirectory();
        /**
         * Pfad zum 'Saves'-Ordner, der für die Speicherung der Statistiken benötigt wird.
         */
        public static string SAVES_PATH = PATH + PATH_DELIMITER + "Saves";
        /**
         * File Ending als String.
         */
        private const string FILE_ENDING = @".csv";
        /**
         * Trennzeichen für die csv-Datei als char.
         */
        private const char DELIMITER = ';';
        /**
         * Unterstrich als char.
         */
        private const char UNDERSCORE = '_';
        /**
         * Trennzeichen für einen Pfad als String.
         */
        private const string PATH_DELIMITER = @"\";
        /**
         * Der Pfad zum Ordner des CBR-Spielers - erstmal None, wird später korrekt befüllt.
         */
        private static string CBR_PLAYER_PATH = "NONE";
        /**
         * Der Pfad zum Ordner des Non-CBR-Spielers - erstmal None, wird später korrekt befüllt.
         */
        private static string NON_CBR_PLAYER_PATH = "NONE";
        /**
         * Der Name des CBR-Spielers, wird für den Ordnernamen benötigt.
         */
        private static string cbrPlayerName = null;


        /**
         * Diese Methode erstellt einen Ordner am gegebenen Pfad, insofern dort kein Ordner mit dem Namen existiert.
         */
        public static void CreateFolderIfDoesNotExistYet(string path)
        {
            Directory.CreateDirectory(path);
            GrantAccess(path);
        }
        /**
         * Diese Methode verwaltet die Zugriffsrechte für den gegebenen Pfad.
         */
        private static void GrantAccess(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }
        /**
         * Diese Methode speichert den Abschuss eines Spielers in seiner csv-Datei und bestimmt das Kill/Death Verhaeltnis zur Laufzeit.
         * 
         */
        public static void SaveFrag(Player fragger, DateTime gameStart)
        {

            Frag frag = fragger.mStatistics.GetLatestFrag();

            string path = "";
            if (fragger.mCBR || fragger.mIsHumanControlled)
            {
                path = CBR_PLAYER_PATH;
                KDScript.frag += 1.0;
            }
            else
            {
                path = NON_CBR_PLAYER_PATH;
                KDScript.death += 1.0;
           
            }

            string text = "";

            var diffInSeconds = (int)(frag.mTimestamp - gameStart).TotalSeconds;

            string line = File.ReadLines(path).Last();

            if (line.Contains("Deaths"))
            {
                text += Environment.NewLine;
            }
            else
            {
                int pos = line.IndexOf(DELIMITER);

                if (pos == line.Length - 1)
                {
                    text += Environment.NewLine;
                }
            }

            text += diffInSeconds.ToString() + DELIMITER;

            FileStream fileStream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None);

            byte[] bytes = Encoding.ASCII.GetBytes(text);

            WriteToFile("Frag: " + text);

            fileStream.Write(bytes, 0, bytes.Length);

            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }
        /**
         * Diese Methode speichert den Tod eines Spielers in seiner csv-Datei.
         */
        public static void SaveDeath(Player deadOne, DateTime gameStart)
        {

            Death death = deadOne.mStatistics.GetLatestDeath();

            string path = "";
            if (deadOne.mCBR || deadOne.mIsHumanControlled)
            {
                path = CBR_PLAYER_PATH;
                UpTimeScript.upTimeValue = 0.00;
                WinChanceScript.winChanceValue = 00;
                HealthScript.healthValue = 00;
                UpTimeScript.pauseHit = true; 
            }
            else
            {
                path = NON_CBR_PLAYER_PATH;
            }

            string line = File.ReadLines(path).Last();


            string text = "";

            if (line.Contains("Deaths"))
            {
                text += Environment.NewLine + DELIMITER;
            }
            else
            {
                int pos = line.IndexOf(DELIMITER);
                if ((pos != 0 && pos != line.Length - 1) || pos == 0)
                {
                    text += DELIMITER;
                }
            }

            var diffInSeconds = (int)(death.mTimestamp - gameStart).TotalSeconds;

            text += diffInSeconds.ToString() + Environment.NewLine;

            FileStream fileStream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None);


            byte[] bytes = Encoding.ASCII.GetBytes(text);

            WriteToFile("Death: " + text);

            fileStream.Write(bytes, 0, bytes.Length);

            fileStream.Flush();
            fileStream.Close();
        }

        /**
         * Diese Methode wird zu Beginn des Spiels aufgerufen und erstellt alle notwendigen Ordner und Dateien für die Teilnehmer des Spiels.
         */
        public static void InitSaveFolderAndPlayerSaveFile(Player player, DateTime gameStart)
        {
            string path = SAVES_PATH;
            string time = "" + gameStart.Day + UNDERSCORE + gameStart.Month + UNDERSCORE + gameStart.Year + UNDERSCORE + gameStart.Hour + UNDERSCORE + gameStart.Minute + UNDERSCORE + gameStart.Second;
            path += PATH_DELIMITER + time;
            CreateFolderIfDoesNotExistYet(path);

            if (player.mCBR)
            {
                cbrPlayerName = player.mName;
            }

            path += PATH_DELIMITER + player.mName + (player.mCBR ? "(CBR)" : "");

            string filePath = path + PATH_DELIMITER + "Statistics" + FILE_ENDING;


            if (cbrPlayerName.Equals(player.mName))
            {
                CBR_PLAYER_PATH = filePath;
            }
            else
            {
                NON_CBR_PLAYER_PATH = filePath;
            }

            CreateFolderIfDoesNotExistYet(path);

            FileStream fileStream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None);

            byte[] bytes = Encoding.ASCII.GetBytes("Frags" + DELIMITER + "Deaths");

            fileStream.Write(bytes, 0, bytes.Length);

            fileStream.Flush();
            fileStream.Close();
        }
        /**
         * Diese Methode ermöglicht das Schreiben in eine Datei - ist derzeit nur für Logging-Zwecke vorgesehen.
         */
        public static void WriteToFile(string text)
        {
            text += Environment.NewLine;

            FileStream fileStream = File.Open(TEMP_PATH, FileMode.Append, FileAccess.Write, FileShare.None);

            byte[] bytes = Encoding.ASCII.GetBytes(text);

            fileStream.Write(bytes, 0, bytes.Length);


            fileStream.Close();
        }
        /**
         * Diese Methode startet das CBR-System mittels eines Process. Der Parameter 'window' gibt an, ob das Command-Fenster beim Start sichtbar oder unsichtbar ist.
         */
        public static void StartServer(bool window)
        {
            proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = "java";
            proc.StartInfo.Arguments = "-jar " + SERVER_PATH + " " + PORT;
            if (!window)
            {
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
            }
            proc.Start();
        }
    }

}
