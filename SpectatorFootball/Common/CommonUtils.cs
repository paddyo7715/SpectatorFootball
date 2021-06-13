﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using SpectatorFootball.Enum;
using System.Windows.Media;

namespace SpectatorFootball
{
    public class CommonUtils
    {
        private static Random random = new Random();
        public static String getSettingsDBConnectionString()
        {
          return "Data Source=" + getAppPath() + Path.DirectorySeparatorChar + "Database" + Path.DirectorySeparatorChar + "Settings.db;";
        }

        public static String getLeagueDBConnectionString(string newLeague_path)
        {
            return "Data Source=" + newLeague_path + ";";
        }

        public static string getAppPath()
        {
            string c = Environment.CurrentDirectory;

            c = c.Replace(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug", "");
            c = c.Replace(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Release", "");


            return c;
        }
        public static int ExtractTeamNumber(string s)
        {
            int i = s.IndexOf("Team");
            string r = s.Substring(i + 4);
            return int.Parse(r);
        }
        public static int ExtractDivNumber(string s)
        {
            int i = s.IndexOf("newldiv");
            string r = s.Substring(i + 7);
            return int.Parse(r);
        }
        public static bool isBlank(string s)
        {
            if (s == null || s.Trim().Length == 0)
                return true;
            else
                return false;
        }
        public static string CapitalizeFirstLetter(string s)
        {
            return s.Substring(0, 1).ToUpper() + substr(s, 1, s.Length);
        }
        public static string substr(string s, int f, int l)
        {
            string r;
            if (s == null)
                r = "";
            else if (string.IsNullOrEmpty(s.Trim()))
                r = "";
            else if (f > s.Length - 1)
                r = "";
            else
                r = s.Substring(f, Math.Min(l, s.Length - f));

            return r;
        }
        public static int getRandomNum(int iLow, int iHigh)
        {
            int r;

            r = random.Next(iLow, iHigh + 1);

            return r;
        }
        public static int getDivisionNum_from_Team_Number(int num_divs, int t_num)
        {
            return (t_num - 1) % num_divs + 1;
        }
        public static int[] getLeagueStructure(string s)
        {
            int i = 0;
            int Teams = default(int);
            int Num_Divisions = default(int);
            int Conferences = default(int);

            String[] ls = s.Split(' ');
            foreach (String l in ls)
            {
                switch (l)
                {
                    case "teams":
                        {
                            Teams = int.Parse(ls[i - 1]);
                            break;
                        }

                    case "conferences":
                        {
                            Conferences = int.Parse(ls[i - 1]);
                            break;
                        }

                    case "divisions":
                        {
                            Num_Divisions = int.Parse(ls[i - 1]);
                            break;
                        }
                }

                i += 1;
            }

            return new int[] { Teams, Num_Divisions, Conferences };
        }

        public static int getConferenceNum_from_Team_Number(int num_confs, int t_num)
        {
            int r;

            if (num_confs == 0)
                r = 0;
            else if (num_confs > t_num)
                r = 2;
            else
                r = 1;

            return r;
        }

        public static Brush getBrushfromHexString(string sHex)
        {
            var bc = new BrushConverter();
            return (Brush)bc.ConvertFrom(sHex);
        }
        public static string getHexfromColor(System.Windows.Media.Color c)
        {
            string r = null;

            r = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");

            return r;
        }
        public static System.Windows.Media.Color getColorfromHex(string s)
        {
            System.Windows.Media.Color r = new System.Windows.Media.Color();
            byte red = (byte) 0;
            byte green = (byte)0;
            byte blue = (byte)0;

            red = (byte)Convert.ToInt32(s.Substring(1, 2),16);
            green = (byte)Convert.ToInt32(s.Substring(3, 2),16);
            blue = (byte)Convert.ToInt32(s.Substring(5, 2),16);

            r = System.Windows.Media.Color.FromRgb(red, green, blue);
            return r;
        }
        public static bool isAlpha(string s, bool bAllowSpace)
        {
            bool r = true;
            string pattern = null;

            if (bAllowSpace)
                pattern = "^[a-zA-Z ]*$";
            else
                pattern = "^[a-zA-Z]*$";

            Regex Regexm = new Regex(pattern);
            if (!Regexm.IsMatch(s))
                r = false;

            return r;
        }
        public static bool isAlphaNumeric(string s, bool bAllowSpace)
        {
            bool r = true;
            string pattern = null;

            if (bAllowSpace)
                pattern = "^[a-zA-Z0-9 ]*$";
            else
                pattern = "^[a-zA-Z0-9]*$";

            Regex Regexm = new Regex(pattern);
            if (!Regexm.IsMatch(s))
                r = false;

            return r;
        }

        public static List<T> ShufleList<T>(List<T> Source)
        {
            List<T> r = null;
            Random rnd = new Random();

            r = Source.Select(x => new { value = x, order = rnd.Next() })
            .OrderBy(x => x.order).Select(x => x.value).ToList();

            return r;
        }

        public static int PercentFromRange(int value, int lowerR, int upperR)
        {
            int r = 0;

            float range = upperR - lowerR;
            float true_value = value - lowerR;

            r = (int)(((true_value / range) * 100.0) + 0.5) ;
            return r;
        }
        public static void SetFullAccess(string DIRPath_League)
        {
            // Get directory access info
            DirectoryInfo dinfo = new DirectoryInfo(DIRPath_League);
            DirectorySecurity dSecurity = dinfo.GetAccessControl();
            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            // Set the access control
            dinfo.SetAccessControl(dSecurity);
        }
        public static BitmapImage getImageorBlank(string filename_path)
        {
            BitmapImage r = new BitmapImage();

            try
            {
                Uri imgurl = new Uri(filename_path);
                var helmetIMG_source = new BitmapImage(imgurl);
                r = new BitmapImage(imgurl);
            }
            catch (Exception) { }

            return r;
        }

        public static long ZeroifNull(long? l)
        {
            if (l == null)
                return 0;
            else
                return (long)l;
        }
        public static bool isTwoColorDifferent(string s1, string s2)
        {
            int r1 = 0;
            int g1 = 0;
            int b1 = 0;

            int r2 = 0;
            int g2 = 0;
            int b2 = 0;

            int diff = 0;

            bool r = false;

            r1 = Convert.ToInt32(s1.Substring(1, 2), 16);
            g1 = Convert.ToInt32(s1.Substring(3, 2), 16);
            b1 = Convert.ToInt32(s1.Substring(5, 2), 16);

            r2 = Convert.ToInt32(s2.Substring(1, 2), 16);
            g2 = Convert.ToInt32(s2.Substring(3, 2), 16);
            b2 = Convert.ToInt32(s2.Substring(5, 2), 16);

            diff = Math.Abs(r1 - r2) + Math.Abs(g1 - g2) + Math.Abs(b1 - b2);

            if (diff <= app_Constants.MINCOLORDIFF)
                r = false;
            else
                r = true;

            return r;
        }

        
        public static List<string> ListfromListofDelimitted(List<string> s, string delimChar)
        {
            List<string> r = new List<string>();

            foreach (string i in s)
            {
                string[] sa = i.Split(char.Parse(delimChar));
                r.Add(sa[0]);
            }


            return r;
        }
    }

}
