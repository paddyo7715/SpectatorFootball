using System;
using System.Windows;
using System.Diagnostics;
using System.IO;
using log4net;
using SpectatorFootball.Versioning;

namespace SpectatorFootball
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            string DIRPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), App_Constants.GAME_DOC_FOLDER);

            string Log_folder = App_Constants.LOG_FOLDER;
            string Drive_letter = DIRPath.Split(':')[0];

            if (AlreadyRunning())
            {
                logger.Error("Application already running.  Can not run more than one instance.");
                MessageBox.Show("Application already running.  Can not run more than one instance.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }

            // Check that the disk drive that holds the team databases and loggs has enough disk space\
            DriveInfo drive = new DriveInfo(Drive_letter);
            long free_disk_space = (drive.AvailableFreeSpace / 1024) / 1024;
            if (free_disk_space < App_Constants.MIN_FREE_DISK_SPACE)
            {
                logger.Error("Only " + free_disk_space.ToString() + " free disk space available.  Ending program");
                MessageBox.Show("Insufficient free space available.  Only " + free_disk_space.ToString() + " free disk space available.  Ending program");
                Environment.Exit(0);
            }

            // If it doesn't exist then create the Game folder/log folder under my documents.  This
            // should only be necessary the first time that the game is run
            if (!Directory.Exists(DIRPath))
            {
                Directory.CreateDirectory(DIRPath);
                Directory.CreateDirectory(DIRPath + Path.DirectorySeparatorChar + Log_folder);
            }

            // Set the folder for the logs
            GlobalContext.Properties["logFolder"] = DIRPath + Path.DirectorySeparatorChar + Log_folder;
//            GlobalContext.Properties["logFolder"] = ".";

            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log4Net.Config.xml"));

            logger.Info("=================== Start Program ========================");
            logger.Info("Application Version: " + App_Version.APP_VERSION);
            logger.Info("Hostname: " + Environment.MachineName);
            logger.Info("Operating System: " + Environment.OSVersion);
            logger.Info(".NET version: " + Environment.Version.ToString());

            MainWindow win = new MainWindow();
            MainWindow = win;
            win.Show();
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            logger.Info("=================== End Program =========================");
        }

        // Return True if another instance
        // of this program is already running.
        private bool AlreadyRunning()
        {
            // Get our process name.
            Process my_proc = Process.GetCurrentProcess();
            string my_name = my_proc.ProcessName;

            // Get information about processes with this name.
            Process[] procs = Process.GetProcessesByName(my_name);

            // If there is only one, it's us.
            if (procs.Length == 1)
                return false;

            // If there is more than one process,
            // see if one has a StartTime before ours.
            int i;
            for (i = 0; i <= procs.Length - 1; i++)
            {
                if (procs[i].StartTime < my_proc.StartTime)
                    return true;
            }

            // If we get here, we were first.
            return false;
        }


    }
}
