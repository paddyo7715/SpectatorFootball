﻿using SpectatorFootball.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for Game_Options_Dialog.xaml
    /// </summary>
    public partial class Game_Options_Dialog : Window
    {
        private string ball_Color1;
        private string ball_Color2;
        public Game_Options_Dialog(string ball_Colors, string ThreeDee)
        {
            InitializeComponent();

            //Get the screen resolution of the primary monitor
            Tuple<int, int> Screen_Res = sysInfor.getScreenResolution();
            this.Width = Screen_Res.Item1 * .35;
            this.Height = Screen_Res.Item2 * .3;

            string[] m = ball_Colors.Split('|');
            ball_Color1 = m[0];
            ball_Color2 = m[1];
            int selectedIndex = 0;

            bool ThreeDee_ball;
            try
            {
                ThreeDee_ball = Convert.ToBoolean(ThreeDee);
            }
            catch (Exception e)
            {
                ThreeDee_ball = true;
            }

            var cboItems = cboBallColor.Items;
            int i = 0;
            foreach (ComboBoxItem cb in cboItems)
            {
                string[] mm = getColorsFromCBItem(cb).Split('|');
                string color1 = mm[0];
                string color2 = mm[1];

                if (color1.ToUpper() == ball_Color1.ToUpper() &&
                    color2.ToUpper() == ball_Color2.ToUpper())
                {
                    selectedIndex = i;
                    break;
                }
                i++;
            }

            cboBallColor.SelectedIndex = selectedIndex;
            if (ThreeDee_ball)
                opt3d.IsChecked = true;
            else
                opt2d.IsChecked = true;

        }

        private string getColorsFromCBItem(ComboBoxItem cb)
        {
            StackPanel sp = (StackPanel)cb.Content;
            Ellipse e = (Ellipse)sp.Children[0];
            var gradb = (LinearGradientBrush)e.Fill;
            string color1 = CommonUtils.getHexfromColor(gradb.GradientStops[0].Color);
            string color2 = CommonUtils.getHexfromColor(gradb.GradientStops[1].Color);
            return color1 + "|" + color2;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cb = (ComboBoxItem)cboBallColor.SelectedItem;
            string color_string = getColorsFromCBItem(cb);

            bool threeDee = false;

            if (opt3d.IsChecked == true)
                threeDee = true;

            League_Services ls = new League_Services();
            ls.Create_Options_File(color_string, threeDee);
            this.Close();
        }
    }
}
