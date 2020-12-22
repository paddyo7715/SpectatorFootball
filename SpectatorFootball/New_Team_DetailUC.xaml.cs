using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
using log4net;
using System.Windows.Controls;
using SpectatorFootball.Models;
using System.Windows.Data;
using System.Windows.Input;
using SpectatorFootball.Common;

namespace SpectatorFootball
{
    /// <summary>
    /// Interaction logic for New_Team_DetailUC.xaml
    /// </summary>
    public partial class New_Team_DetailUC : UserControl
    {
        private Teams_by_Season binding_team = new Teams_by_Season();
        private Teams_by_Season orig_this_team = null;
        private Boolean bNewLeague;

        private Boolean bnew_team;
        private Boolean bLoadingForm = true;

        public event EventHandler<TeamUpdatedEventArgs> backtoNewLeague;

        // Property Roster As List(Of PlayerMdl) = Nothing
        public Uniform_Image Uniform_Img { get; set; }

        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> Recent_ColorList = new ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();
        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> Standard_ColorList = new ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();

        public bool Event_from_Code { get; set; } = false;
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public New_Team_DetailUC(Teams_by_Season this_team,Boolean bNewLeague)
        {
            // This call is required by the designer.
            InitializeComponent();
            if (this_team != null)
                binding_team = Team_Helper.Clone_Team(this_team);

            this.DataContext = binding_team;

            this.orig_this_team = this_team;
            this.bNewLeague = bNewLeague;

            var all_uniform_colors = new List<string>();

            if (orig_this_team.City == app_Constants.EMPTY_TEAM_SLOT)
                bnew_team = true;
            else
                bnew_team = false;

            if (bnew_team)
            {
                lblTitle.Content = "New Team";
                newt1Add.Content = "Add";
            }
            else
            {
                lblTitle.Content = "Update Team";
                newt1Add.Content = "Update";
                all_uniform_colors = Uniform.getAllColorList(
                    binding_team.Helmet_Color,
                    binding_team.Helmet_Logo_Color,
                    binding_team.Helmet_Facemask_Color,
                    binding_team.Socks_Color,
                    binding_team.Cleats_Color,
                    binding_team.Home_jersey_Color,
                    binding_team.Home_Sleeve_Color,
                    binding_team.Home_Jersey_Number_Color,
                    binding_team.Home_Jersey_Number_Outline_Color,
                    binding_team.Home_Jersey_Shoulder_Stripe,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_1,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_2,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_3,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_4,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_5,
                    binding_team.Home_Jersey_Sleeve_Stripe_Color_6,
                    binding_team.Home_Pants_Color,
                    binding_team.Home_Pants_Stripe_Color_1,
                    binding_team.Home_Pants_Stripe_Color_2,
                    binding_team.Home_Pants_Stripe_Color_3,
                    binding_team.Away_jersey_Color,
                    binding_team.Away_Sleeve_Color,
                    binding_team.Away_Jersey_Number_Color,
                    binding_team.Away_Jersey_Number_Outline_Color,
                    binding_team.Away_Jersey_Shoulder_Stripe,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_1,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_2,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_3,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_4,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_5,
                    binding_team.Away_Jersey_Sleeve_Stripe_Color_6,
                    binding_team.Away_Pants_Color,
                    binding_team.Away_Pants_Stripe_Color_1,
                    binding_team.Away_Pants_Stripe_Color_2,
                    binding_team.Away_Pants_Stripe_Color_3);
            }

            // if we are editing a team then load the uniform colors in the recent colors
            if (all_uniform_colors.Count > 0)
            {
                foreach (var c in all_uniform_colors)
                {
                    System.Windows.Media.Color color_val = CommonUtils.getColorfromHex(c);
                    string color_name = color_val.ToString();
                    string possible_color_name = getColorName(color_name, newtHelmentColor.AvailableColors, newtHelmentColor.StandardColors);
                    if (possible_color_name != null)
                        color_name = possible_color_name;
                    Recent_ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(CommonUtils.getColorfromHex(c), color_name));
                }
            }

            // Create the standard color list and exclude the transparent color, since is causes problems
            foreach (var sc in newtHelmentColor.StandardColors)
            {
                if (sc.Name == "Transparent")
                    continue;
                Standard_ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem(sc.Color, sc.Name));
            }

            // Set the transparent less coloritem array to the stand color for each color picker
            newtHelmentColor.StandardColors = Standard_ColorList;
            newtHelmentLogoColor.StandardColors = Standard_ColorList;
            newtFacemaskColor.StandardColors = Standard_ColorList;
            newtSockColor.StandardColors = Standard_ColorList;
            newtCleatsColor.StandardColors = Standard_ColorList;

            newtHomeJerseyColor.StandardColors = Standard_ColorList;
            newtHomeSleeveColor.StandardColors = Standard_ColorList;

            newtHomeJerseyNumberColor.StandardColors = Standard_ColorList;
            newtHomeNumberOutlineColor.StandardColors = Standard_ColorList;

            newtHomeShoulderStripeColor.StandardColors = Standard_ColorList;

            newtHomeJerseySleeve1Color.StandardColors = Standard_ColorList;
            newtHomeJerseySleeve2Color.StandardColors = Standard_ColorList;
            newtHomeJerseySleeve3Color.StandardColors = Standard_ColorList;
            newtHomeJerseySleeve4Color.StandardColors = Standard_ColorList;
            newtHomeJerseySleeve5Color.StandardColors = Standard_ColorList;
            newtHomeJerseySleeve6Color.StandardColors = Standard_ColorList;

            newtHomePantsColor.StandardColors = Standard_ColorList;
            newtHomePantsStripe1Color.StandardColors = Standard_ColorList;
            newtHomePantsStripe2Color.StandardColors = Standard_ColorList;
            newtHomePantsStripe3Color.StandardColors = Standard_ColorList;

            newtAwayJerseyColor.StandardColors = Standard_ColorList;
            newtAwaySleeveColor.StandardColors = Standard_ColorList;

            newtAwayJerseyNumberColor.StandardColors = Standard_ColorList;
            newtAwayNumberOutlineColor.StandardColors = Standard_ColorList;

            newtAwayShoulderStripeColor.StandardColors = Standard_ColorList;

            newtAwayJerseySleeve1Color.StandardColors = Standard_ColorList;
            newtAwayJerseySleeve2Color.StandardColors = Standard_ColorList;
            newtAwayJerseySleeve3Color.StandardColors = Standard_ColorList;
            newtAwayJerseySleeve4Color.StandardColors = Standard_ColorList;
            newtAwayJerseySleeve5Color.StandardColors = Standard_ColorList;
            newtAwayJerseySleeve6Color.StandardColors = Standard_ColorList;

            newtAwayPantsColor.StandardColors = Standard_ColorList;
            newtAwayPantsStripe1Color.StandardColors = Standard_ColorList;
            newtAwayPantsStripe2Color.StandardColors = Standard_ColorList;
            newtAwayPantsStripe3Color.StandardColors = Standard_ColorList;

            // For some reason, I couldn't set the recentcolors in xaml
            newtHelmentColor.RecentColors = Recent_ColorList;
            newtHelmentLogoColor.RecentColors = Recent_ColorList;
            newtFacemaskColor.RecentColors = Recent_ColorList;
            newtSockColor.RecentColors = Recent_ColorList;
            newtCleatsColor.RecentColors = Recent_ColorList;

            newtHomeJerseyColor.RecentColors = Recent_ColorList;
            newtHomeSleeveColor.RecentColors = Recent_ColorList;

            newtHomeJerseyNumberColor.RecentColors = Recent_ColorList;
            newtHomeNumberOutlineColor.RecentColors = Recent_ColorList;

            newtHomeShoulderStripeColor.RecentColors = Recent_ColorList;

            newtHomeJerseySleeve1Color.RecentColors = Recent_ColorList;
            newtHomeJerseySleeve2Color.RecentColors = Recent_ColorList;
            newtHomeJerseySleeve3Color.RecentColors = Recent_ColorList;
            newtHomeJerseySleeve4Color.RecentColors = Recent_ColorList;
            newtHomeJerseySleeve5Color.RecentColors = Recent_ColorList;
            newtHomeJerseySleeve6Color.RecentColors = Recent_ColorList;

            newtHomePantsColor.RecentColors = Recent_ColorList;
            newtHomePantsStripe1Color.RecentColors = Recent_ColorList;
            newtHomePantsStripe2Color.RecentColors = Recent_ColorList;
            newtHomePantsStripe3Color.RecentColors = Recent_ColorList;

            newtAwayJerseyColor.RecentColors = Recent_ColorList;
            newtAwaySleeveColor.RecentColors = Recent_ColorList;

            newtAwayJerseyNumberColor.RecentColors = Recent_ColorList;
            newtAwayNumberOutlineColor.RecentColors = Recent_ColorList;

            newtAwayShoulderStripeColor.RecentColors = Recent_ColorList;

            newtAwayJerseySleeve1Color.RecentColors = Recent_ColorList;
            newtAwayJerseySleeve2Color.RecentColors = Recent_ColorList;
            newtAwayJerseySleeve3Color.RecentColors = Recent_ColorList;
            newtAwayJerseySleeve4Color.RecentColors = Recent_ColorList;
            newtAwayJerseySleeve5Color.RecentColors = Recent_ColorList;
            newtAwayJerseySleeve6Color.RecentColors = Recent_ColorList;

            newtAwayPantsColor.RecentColors = Recent_ColorList;
            newtAwayPantsStripe1Color.RecentColors = Recent_ColorList;
            newtAwayPantsStripe2Color.RecentColors = Recent_ColorList;
            newtAwayPantsStripe3Color.RecentColors = Recent_ColorList;

        }
        private void New_Team_detail_Loaded(Object sender, EventArgs e)
        {

            setInitialUniform();
            bLoadingForm = false;
            Mouse.OverrideCursor = null;

        }
        private void setInitialUniform()
        {

            Uniform_Img = new Uniform_Image(CommonUtils.getAppPath() + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "blankUniform.png");

            // If you are editing a team then there is no need to set the images to grey
            if (bnew_team)
            {
                Uniform_Img.Flip_All_Colors(true, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR);
                Uniform_Img.Flip_All_Colors(false, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR, app_Constants.STOCK_GREY_COLOR);
            }
            else
            {
                Color mc = default(Color);
                System.Drawing.Color helmetColor = default(System.Drawing.Color);
                System.Drawing.Color helmetLogoColor = default(System.Drawing.Color);
                System.Drawing.Color helmetFacemaskColor = default(System.Drawing.Color);
                System.Drawing.Color SocksColor = default(System.Drawing.Color);
                System.Drawing.Color CleatsColor = default(System.Drawing.Color);

                System.Drawing.Color HomeJerseyColor = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseySleeveColor = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyShoulderLoopColor = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyNumberColor = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyNumberOutlineColor = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_1 = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_2 = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_3 = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_4 = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_5 = default(System.Drawing.Color);
                System.Drawing.Color HomeJerseyStripe_6 = default(System.Drawing.Color);
                System.Drawing.Color HomePantsColor = default(System.Drawing.Color);
                System.Drawing.Color HomePants_Stripe_1 = default(System.Drawing.Color);
                System.Drawing.Color HomePants_Stripe_2 = default(System.Drawing.Color);
                System.Drawing.Color HomePants_Stripe_3 = default(System.Drawing.Color);

                System.Drawing.Color AwayJerseyColor = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseySleeveColor = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyShoulderLoopColor = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyNumberColor = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyNumberOutlineColor = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_1 = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_2 = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_3 = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_4 = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_5 = default(System.Drawing.Color);
                System.Drawing.Color AwayJerseyStripe_6 = default(System.Drawing.Color);
                System.Drawing.Color AwayPantsColor = default(System.Drawing.Color);
                System.Drawing.Color AwayPants_Stripe_1 = default(System.Drawing.Color);
                System.Drawing.Color AwayPants_Stripe_2 = default(System.Drawing.Color);
                System.Drawing.Color AwayPants_Stripe_3 = default(System.Drawing.Color);

                mc = new SolidColorBrush((Color)newtHelmentColor.SelectedColor).Color;
                helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color;
                helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color;
                helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtSockColor.SelectedColor).Color;
                SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color;
                CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseyColor.SelectedColor).Color;
                HomeJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeSleeveColor.SelectedColor).Color;
                HomeJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeShoulderStripeColor.SelectedColor).Color;
                HomeJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseyNumberColor.SelectedColor).Color;
                HomeJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeNumberOutlineColor.SelectedColor).Color;
                HomeJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve1Color.SelectedColor).Color;
                HomeJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve2Color.SelectedColor).Color;
                HomeJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve3Color.SelectedColor).Color;
                HomeJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve4Color.SelectedColor).Color;
                HomeJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve5Color.SelectedColor).Color;
                HomeJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomeJerseySleeve6Color.SelectedColor).Color;
                HomeJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomePantsColor.SelectedColor).Color;
                HomePantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomePantsStripe1Color.SelectedColor).Color;
                HomePants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomePantsStripe2Color.SelectedColor).Color;
                HomePants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtHomePantsStripe3Color.SelectedColor).Color;
                HomePants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                Uniform_Img.Flip_All_Colors(true, helmetColor, helmetFacemaskColor, helmetLogoColor, HomeJerseyColor, HomeJerseyNumberColor, HomeJerseyNumberOutlineColor, HomeJerseySleeveColor, HomeJerseyShoulderLoopColor, HomeJerseyStripe_1, HomeJerseyStripe_2, HomeJerseyStripe_3, HomeJerseyStripe_4, HomeJerseyStripe_5, HomeJerseyStripe_6, HomePantsColor, HomePants_Stripe_1, HomePants_Stripe_2, HomePants_Stripe_3, SocksColor, CleatsColor);

                mc = new SolidColorBrush((Color)newtAwayJerseyColor.SelectedColor).Color;
                AwayJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwaySleeveColor.SelectedColor).Color;
                AwayJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayShoulderStripeColor.SelectedColor).Color;
                AwayJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseyNumberColor.SelectedColor).Color;
                AwayJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayNumberOutlineColor.SelectedColor).Color;
                AwayJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve1Color.SelectedColor).Color;
                AwayJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve2Color.SelectedColor).Color;
                AwayJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve3Color.SelectedColor).Color;
                AwayJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve4Color.SelectedColor).Color;
                AwayJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve5Color.SelectedColor).Color;
                AwayJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayJerseySleeve6Color.SelectedColor).Color;
                AwayJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayPantsColor.SelectedColor).Color;
                AwayPantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayPantsStripe1Color.SelectedColor).Color;
                AwayPants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayPantsStripe2Color.SelectedColor).Color;
                AwayPants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                mc = new SolidColorBrush((Color)newtAwayPantsStripe3Color.SelectedColor).Color;
                AwayPants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);

                Uniform_Img.Flip_All_Colors(false, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor, AwayJerseyNumberColor, AwayJerseyNumberOutlineColor, AwayJerseySleeveColor, AwayJerseyShoulderLoopColor, AwayJerseyStripe_1, AwayJerseyStripe_2, AwayJerseyStripe_3, AwayJerseyStripe_4, AwayJerseyStripe_5, AwayJerseyStripe_6, AwayPantsColor, AwayPants_Stripe_1, AwayPants_Stripe_2, AwayPants_Stripe_3, SocksColor, CleatsColor);

            }

            newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image();
            newtAwayUniform.Source = Uniform_Img.GetAwayUniform_Image();


        }

        private string getColorName(string c, ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> availableColors, ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> standardColors)
        {
            string r = null;
            bool bfound = false;

            foreach (var a in availableColors)
            {
                if (a.Color.ToString() == c)
                {
                    r = a.Name;
                    bfound = true;
                    break;
                }
            }

            if (!bfound)
            {
                foreach (var s in standardColors)
                {
                    if (s.Color.ToString() == c)
                    {
                        r = s.Name;
                        bfound = true;
                        break;
                    }
                }
            }

            return r;
        }

        private void newtHelmentLogoColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtFacemaskColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtSockColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtCleatsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtHelmentColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtHomeJerseyColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtHomeSleeveColor.SelectedColor = newtHomeJerseyColor.SelectedColor;
            newtHomeShoulderStripeColor.SelectedColor = newtHomeJerseyColor.SelectedColor;
            newtHomeJerseySleeve1Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve2Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve3Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve4Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve5Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve6Color.SelectedColor = newtHomeSleeveColor.SelectedColor;

            setHomeUniform();

            Event_from_Code = false;
        }
        private void newtHomeShoulderStripeColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeNumberOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseyNumberColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }
        private void newtHomeSleeveColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtHomeJerseySleeve1Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve2Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve3Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve4Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve5Color.SelectedColor = newtHomeSleeveColor.SelectedColor;
            newtHomeJerseySleeve6Color.SelectedColor = newtHomeSleeveColor.SelectedColor;

            setHomeUniform();

            Event_from_Code = false;
        }
        private void newtHomeJerseySleeve1Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve4Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve5Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve6Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }
        private void newtHomePantsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtHomePantsStripe1Color.SelectedColor = newtHomePantsColor.SelectedColor;
            newtHomePantsStripe2Color.SelectedColor = newtHomePantsColor.SelectedColor;
            newtHomePantsStripe3Color.SelectedColor = newtHomePantsColor.SelectedColor;

            setHomeUniform();

            Event_from_Code = false;
        }
        private void newtHomePantsStripe1Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }
        private void newtHomePantsStripe2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }
        private void newtHomePantsStripe3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setHomeUniform();
        }
        private void newtAwayJerseyColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtAwaySleeveColor.SelectedColor = newtAwayJerseyColor.SelectedColor;
            newtAwayShoulderStripeColor.SelectedColor = newtAwayJerseyColor.SelectedColor;
            newtAwayJerseySleeve1Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve2Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve3Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve4Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve5Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve6Color.SelectedColor = newtAwaySleeveColor.SelectedColor;

            setAwayUniform();
            Event_from_Code = false;
        }
        private void newtAwayShoulderStripeColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayNumberOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseyNumberColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }
        private void newtAwaySleeveColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtAwayJerseySleeve1Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve2Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve3Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve4Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve5Color.SelectedColor = newtAwaySleeveColor.SelectedColor;
            newtAwayJerseySleeve6Color.SelectedColor = newtAwaySleeveColor.SelectedColor;

            setAwayUniform();
            Event_from_Code = false;
        }
        private void newtAwayJerseySleeve1Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve4Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve5Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve6Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            Event_from_Code = true;
            newtAwayPantsStripe1Color.SelectedColor = newtAwayPantsColor.SelectedColor;
            newtAwayPantsStripe2Color.SelectedColor = newtAwayPantsColor.SelectedColor;
            newtAwayPantsStripe3Color.SelectedColor = newtAwayPantsColor.SelectedColor;

            setAwayUniform();
            Event_from_Code = false;
        }
        private void newtAwayPantsStripe1Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsStripe2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsStripe3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code || bLoadingForm)
                return;

            setAwayUniform();
        }
        private void newt1Cancel_Click(object sender, RoutedEventArgs e)
        {

            if (bNewLeague)
                backtoNewLeague?.Invoke(this, new TeamUpdatedEventArgs(false));

        }

        private void newtbtnHelmetImgPath_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new OpenFileDialog();
            string init_folder = CommonUtils.getAppPath();
            init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Helmets";

            OpenFileDialog.InitialDirectory = init_folder;
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp";

            if (OpenFileDialog.ShowDialog() == true)
            {
                string filepath = OpenFileDialog.FileName;
                binding_team.Helmet_img_path = filepath;
                newtHelmetImgPath.Text = filepath;
            }
        }
        private void newtbtnStadiumPath_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new OpenFileDialog();
            string init_folder = CommonUtils.getAppPath();
            init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Stadiums";

            OpenFileDialog.InitialDirectory = init_folder;
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp";

            if (OpenFileDialog.ShowDialog() == true)
            {
                string filepath = OpenFileDialog.FileName;
                binding_team.Stadium_Img_Path = filepath;
                      newtStadiumPath.Text = filepath;
            }
        }
        
        private void newt1Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                logger.Debug("Just before team validation");
                Validate();
                logger.Debug("Stock Team validated");

                if (bNewLeague)
                {
                    Team_Helper.CopyTeamValues(binding_team, this.orig_this_team);
                    backtoNewLeague?.Invoke(this, new TeamUpdatedEventArgs(true));
                }
                else
                {
                    //Save updated team to database.
                }

 
            }
            catch (Exception ex)
            {
                logger.Error("Error saving team");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void setHomeUniform()
        {
            Color mc = default(Color);
            System.Drawing.Color helmetColor = default(System.Drawing.Color);
            System.Drawing.Color helmetLogoColor = default(System.Drawing.Color);
            System.Drawing.Color helmetFacemaskColor = default(System.Drawing.Color);
            System.Drawing.Color SocksColor = default(System.Drawing.Color);
            System.Drawing.Color CleatsColor = default(System.Drawing.Color);

            System.Drawing.Color HomeJerseyColor = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseySleeveColor = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyShoulderLoopColor = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyNumberColor = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyNumberOutlineColor = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_1 = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_2 = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_3 = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_4 = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_5 = default(System.Drawing.Color);
            System.Drawing.Color HomeJerseyStripe_6 = default(System.Drawing.Color);
            System.Drawing.Color HomePantsColor = default(System.Drawing.Color);
            System.Drawing.Color HomePants_Stripe_1 = default(System.Drawing.Color);
            System.Drawing.Color HomePants_Stripe_2 = default(System.Drawing.Color);
            System.Drawing.Color HomePants_Stripe_3 = default(System.Drawing.Color);

            if (newtHelmentColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentColor.SelectedColor).Color;
                helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHelmentLogoColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color;
                helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetLogoColor = app_Constants.STOCK_GREY_COLOR;

            if (newtFacemaskColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color;
                helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetFacemaskColor = app_Constants.STOCK_GREY_COLOR;

            if (newtSockColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtSockColor.SelectedColor).Color;
                SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                SocksColor = app_Constants.STOCK_GREY_COLOR;

            if (newtCleatsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color;
                CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                CleatsColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseyColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseyColor.SelectedColor).Color;
                HomeJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeSleeveColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeSleeveColor.SelectedColor).Color;
                HomeJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseySleeveColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeShoulderStripeColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeShoulderStripeColor.SelectedColor).Color;
                HomeJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyShoulderLoopColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseyNumberColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseyNumberColor.SelectedColor).Color;
                HomeJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyNumberColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeNumberOutlineColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeNumberOutlineColor.SelectedColor).Color;
                HomeJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyNumberOutlineColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve1Color.SelectedColor).Color;
                HomeJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_1 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve2Color.SelectedColor).Color;
                HomeJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_2 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve3Color.SelectedColor).Color;
                HomeJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_3 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve4Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve4Color.SelectedColor).Color;
                HomeJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_4 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve5Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve5Color.SelectedColor).Color;
                HomeJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_5 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve6Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve6Color.SelectedColor).Color;
                HomeJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_6 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsColor.SelectedColor).Color;
                HomePantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePantsColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe1Color.SelectedColor).Color;
                HomePants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_1 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe2Color.SelectedColor).Color;
                HomePants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_2 = app_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe3Color.SelectedColor).Color;
                HomePants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_3 = app_Constants.STOCK_GREY_COLOR;

            Uniform_Img.Flip_All_Colors(true, helmetColor, helmetFacemaskColor, helmetLogoColor, HomeJerseyColor, HomeJerseyNumberColor, HomeJerseyNumberOutlineColor, HomeJerseySleeveColor, HomeJerseyShoulderLoopColor, HomeJerseyStripe_1, HomeJerseyStripe_2, HomeJerseyStripe_3, HomeJerseyStripe_4, HomeJerseyStripe_5, HomeJerseyStripe_6, HomePantsColor, HomePants_Stripe_1, HomePants_Stripe_2, HomePants_Stripe_3, SocksColor, CleatsColor);

            newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image();
        }

        public void setAwayUniform()
        {
            Color mc = default(Color);
            System.Drawing.Color helmetColor = default(System.Drawing.Color);
            System.Drawing.Color helmetLogoColor = default(System.Drawing.Color);
            System.Drawing.Color helmetFacemaskColor = default(System.Drawing.Color);
            System.Drawing.Color SocksColor = default(System.Drawing.Color);
            System.Drawing.Color CleatsColor = default(System.Drawing.Color);

            System.Drawing.Color AwayJerseyColor = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseySleeveColor = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyShoulderLoopColor = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyNumberColor = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyNumberOutlineColor = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_1 = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_2 = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_3 = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_4 = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_5 = default(System.Drawing.Color);
            System.Drawing.Color AwayJerseyStripe_6 = default(System.Drawing.Color);
            System.Drawing.Color AwayPantsColor = default(System.Drawing.Color);
            System.Drawing.Color AwayPants_Stripe_1 = default(System.Drawing.Color);
            System.Drawing.Color AwayPants_Stripe_2 = default(System.Drawing.Color);
            System.Drawing.Color AwayPants_Stripe_3 = default(System.Drawing.Color);

            if (newtHelmentColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentColor.SelectedColor).Color;
                helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetColor = app_Constants.STOCK_GREY_COLOR;

            if (newtHelmentLogoColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color;
                helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetLogoColor = app_Constants.STOCK_GREY_COLOR;

            if (newtFacemaskColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color;
                helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetFacemaskColor = app_Constants.STOCK_GREY_COLOR;

            if (newtSockColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtSockColor.SelectedColor).Color;
                SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                SocksColor = app_Constants.STOCK_GREY_COLOR;

            if (newtCleatsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color;
                CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                CleatsColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseyColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseyColor.SelectedColor).Color;
                AwayJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwaySleeveColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwaySleeveColor.SelectedColor).Color;
                AwayJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseySleeveColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayShoulderStripeColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayShoulderStripeColor.SelectedColor).Color;
                AwayJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyShoulderLoopColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseyNumberColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseyNumberColor.SelectedColor).Color;
                AwayJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyNumberColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayNumberOutlineColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayNumberOutlineColor.SelectedColor).Color;
                AwayJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyNumberOutlineColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve1Color.SelectedColor).Color;
                AwayJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_1 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve2Color.SelectedColor).Color;
                AwayJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_2 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve3Color.SelectedColor).Color;
                AwayJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_3 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve4Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve4Color.SelectedColor).Color;
                AwayJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_4 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve5Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve5Color.SelectedColor).Color;
                AwayJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_5 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve6Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve6Color.SelectedColor).Color;
                AwayJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_6 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsColor.SelectedColor).Color;
                AwayPantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPantsColor = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe1Color.SelectedColor).Color;
                AwayPants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_1 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe2Color.SelectedColor).Color;
                AwayPants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_2 = app_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe3Color.SelectedColor).Color;
                AwayPants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_3 = app_Constants.STOCK_GREY_COLOR;

            Uniform_Img.Flip_All_Colors(false, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor, AwayJerseyNumberColor, AwayJerseyNumberOutlineColor, AwayJerseySleeveColor, AwayJerseyShoulderLoopColor, AwayJerseyStripe_1, AwayJerseyStripe_2, AwayJerseyStripe_3, AwayJerseyStripe_4, AwayJerseyStripe_5, AwayJerseyStripe_6, AwayPantsColor, AwayPants_Stripe_1, AwayPants_Stripe_2, AwayPants_Stripe_3, SocksColor, CleatsColor);

            newtAwayUniform.Source = Uniform_Img.GetAwayUniform_Image();
        }

        private void Validate()
        {

            if (CommonUtils.isBlank(binding_team.City_Abr))
                throw new Exception("City Abbriviation must have a value");

            if (!CommonUtils.isAlpha(binding_team.City_Abr, false))
                throw new Exception("Invalid character in City Abbriviation!");

            if (CommonUtils.isBlank(binding_team.City))
                throw new Exception("City must have a value");

            if (!CommonUtils.isAlpha(binding_team.City, true))
                throw new Exception("Invalid character in City!");

            if (binding_team.City == app_Constants.EMPTY_TEAM_SLOT)
                throw new Exception("City can not have a value of " + app_Constants.EMPTY_TEAM_SLOT);

            if (CommonUtils.isBlank(binding_team.Nickname))
                throw new Exception("Nickname must have a value");

            if (!CommonUtils.isAlphaNumeric(binding_team.Nickname, true))
                throw new Exception("Invalid character in Nickname!");

            if (CommonUtils.isBlank(binding_team.Stadium_Name))
                throw new Exception("Stadium Name must have a value");

            if (!CommonUtils.isAlphaNumeric(binding_team.Stadium_Name, true))
                throw new Exception("Invalid character in Stadium!");

            if (CommonUtils.isBlank(binding_team.Stadium_Location))
                throw new Exception("Stadium Location must have a value");

            if (!Validator.isValidateColorString(binding_team.Stadium_Field_Color))
                throw new Exception("Field color of team stadium must be supplied");

            if (CommonUtils.isBlank(binding_team.Stadium_Capacity))
                throw new Exception("Stadium Capacity must be supplied and numeric");

            if (CommonUtils.isBlank(binding_team.Stadium_Img_Path))
                throw new Exception("Image of team stadium must be supplied");

            if (!Validator.isValidateColorString(binding_team.Helmet_Color))
                throw new Exception("A helmet color must be selected");

            if (!Validator.isValidateColorString(binding_team.Helmet_Logo_Color))
                throw new Exception("A helmet Logo color must be selected");

            if (!Validator.isValidateColorString(binding_team.Helmet_Facemask_Color))
                throw new Exception("A helmet facemask must be selected");

            if (CommonUtils.isBlank(binding_team.Helmet_img_path))
                throw new Exception("Helmet image path must have a value");

            if (!Validator.isValidateColorString(binding_team.Socks_Color))
                throw new Exception("Sock color must have a value");

            if (!Validator.isValidateColorString(binding_team.Cleats_Color))
                throw new Exception("Cleat color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_jersey_Color))
                throw new Exception("Home jersey color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Sleeve_Color))
                throw new Exception("Home sleeve color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Shoulder_Stripe))
                throw new Exception("Home shoulder loop color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Number_Color))
                throw new Exception("Home jersey number color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Number_Outline_Color))
                throw new Exception("Home jersey outline number color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_1))
                throw new Exception("Home jersey sleeve string 1 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_2))
                throw new Exception("Home jersey sleeve string 2 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_3))
                throw new Exception("Home jersey sleeve string 3 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_4))
                throw new Exception("Home jersey sleeve string 4 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_5))
                throw new Exception("Home jersey sleeve string 5 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Jersey_Sleeve_Stripe_Color_6))
                throw new Exception("Home jersey sleeve string 6 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Pants_Color))
                throw new Exception("Home pants color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Pants_Stripe_Color_1))
                throw new Exception("Home pants stripe 1 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Pants_Stripe_Color_2))
                throw new Exception("Home pants stripe 2 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Home_Pants_Stripe_Color_3))
                throw new Exception("Home pants stripe 3 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_jersey_Color))
                throw new Exception("Away jersey color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Sleeve_Color))
                throw new Exception("Away sleeve color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Shoulder_Stripe))
                throw new Exception("Away shoulder loop color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Number_Color))
                throw new Exception("Away jersey number color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Number_Outline_Color))
                throw new Exception("Away jersey outline number color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_1))
                throw new Exception("Away jersey sleeve string 1 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_2))
                throw new Exception("Away jersey sleeve string 2 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_3))
                throw new Exception("Away jersey sleeve string 3 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_4))
                throw new Exception("Away jersey sleeve string 4 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_5))
                throw new Exception("Away jersey sleeve string 5 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Jersey_Sleeve_Stripe_Color_6))
                throw new Exception("Away jersey sleeve string 6 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Pants_Color))
                throw new Exception("Away pants color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Pants_Stripe_Color_1))
                throw new Exception("Away pants stripe 1 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Pants_Stripe_Color_2))
                throw new Exception("Away pants stripe 2 color must have a value");

            if (!Validator.isValidateColorString(binding_team.Away_Pants_Stripe_Color_3))
                throw new Exception("Away pants stripe 3 color must have a value");

            // if this is not a new league than check that the team is not a duplicate
            if (!bnew_team)
            {
//Pass in the team structure and test that the team is unique excluding the original index of the team.
            }



        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

                        var hlp_form = new Help_NewTeam();
                        hlp_form.Top = (SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2;
                        hlp_form.Left = (SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2;
                        hlp_form.ShowDialog();


        }


    }
}
