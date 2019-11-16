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

namespace SpectatorFootball 
{
    public partial class NewTeamUC : UserControl
    {
        public enum form_func
        {
            New_Team,
            Stock_Team_New,
            Stock_Team_Edit,
            Update_Team
        }
        // pw is the parent window mainwindow
        private TeamMdl this_team = null;

        public event EventHandler<TeamUpdatedEventArgs> backtoNewLeague;
        public event EventHandler backtoStockTeams;

        // Property Roster As List(Of PlayerMdl) = Nothing
        public Uniform_Image Uniform_Img { get; set; }

        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> Recent_ColorList = new ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();
        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> Standard_ColorList = new ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();

        private string original_city = null;
        private string original_nickname = null;

        public form_func Form_Function { get; set; } = default(form_func);

        public bool Event_from_Code { get; set; } = false;
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public NewTeamUC(TeamMdl this_team, string func)
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            this.this_team = this_team;

            var all_uniform_colors = new List<string>();

            switch (func)
            {
                case "New_League":
                    {
                        Form_Function = form_func.New_Team;
                        lblTitle.Content = "New Team";
                        newt1Add.Content = "Add";
                        break;
                    }

                case "New_Stock_Team":
                    {
                        Form_Function = form_func.Stock_Team_New;
                        lblTitle.Content = "New Stock Team";
                        newt1Add.Content = "Add";
                        break;
                    }

                case "Update_Stock_Team":
                    {
                        original_city = this_team.City;
                        original_nickname = this_team.Nickname;

                        Form_Function = form_func.Stock_Team_Edit;
                        lblTitle.Content = "Update Stock Team";
                        newt1Add.Content = "Save";
                        all_uniform_colors = Uniform.getAllColorList(this_team.Uniform);
                        break;
                    }
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
                if (sc.Name  == "Transparent")
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

        public void setBaseUniform()
        {
            Uniform_Img = new Uniform_Image(CommonUtils.getAppPath() + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "blankUniform.png");

            // If you are editing a team then there is no need to set the images to grey
            if (Form_Function == (int)form_func.New_Team || (int)Form_Function == (int)form_func.Stock_Team_New)
            {
                Uniform_Img.Flip_All_Colors(true, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR);

                Uniform_Img.Flip_All_Colors(false, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR);
            }
            newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image();
            newtAwayUniform.Source = Uniform_Img.GetAwayUniform_Image();
        }
        public void setfields()
        {
            var colorConverter = new ColorConverter();
            var bc = new BrushConverter();

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

            string stadium_img_path = "";
            string helmet_img_path = "";

            logger.Debug("Basic team info Controls set");

            newtCityAbb.Text = this_team.City_Abr;
            newtCity.Text = this_team.City;
            newtNickname.Text = this_team.Nickname;
            newtHelmetImgPath.Text = helmet_img_path;

            if (this_team.Stadium != null)
            {
                if (this_team.Stadium.Stadium_Img_Path  == Path.GetFileName(this_team.Stadium.Stadium_Img_Path))
                {
                    string init_folder = CommonUtils.getAppPath();
                    init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Stadiums";
                    stadium_img_path = init_folder + Path.DirectorySeparatorChar + Path.GetFileName(this_team.Stadium.Stadium_Img_Path);
                }
                else
                    stadium_img_path = this_team.Stadium.Stadium_Img_Path;

                logger.Debug("Setting stadium image control " + stadium_img_path);

                newtStadium.Text = this_team.Stadium.Stadium_Name;
                newtStadiumLocation.Text = this_team.Stadium.Stadium_Location;
                newtStadiumPath.Text = stadium_img_path;
                Stadium_image.Source = new BitmapImage(new Uri(stadium_img_path));
                newtStadiumCapacity.Text = this_team.Stadium.Capacity;
                newl1FieldType.SelectedIndex = this_team.Stadium.Field_Type - 1;
                newl1FieldColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Stadium.Field_Color);
            }

            if (this_team.Helmet_img_path != null && this_team.Helmet_img_path.Length > 0)
            {
                if (this_team.Helmet_img_path == Path.GetFileName(this_team.Helmet_img_path) )
                {
                    string init_folder = CommonUtils.getAppPath();
                    init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Helmets";
                    helmet_img_path = init_folder + Path.DirectorySeparatorChar + Path.GetFileName(this_team.Helmet_img_path);
                }
                else
                    helmet_img_path = this_team.Helmet_img_path;

                logger.Debug("Setting helmet image control " + helmet_img_path);

                newtHelmetImgPath.Text = helmet_img_path;
                Helmet_image.Source = new BitmapImage(new Uri(helmet_img_path));
            }

            if (this_team.Uniform != null)
            {
                logger.Debug("Setting uniform image controls");

                newtHelmentColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Color);
                newtHelmentLogoColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Logo_Color);
                newtFacemaskColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Facemask_Color);

                newtSockColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Footwear.Socks_Color);
                newtCleatsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Footwear.Cleats_Color);

                newtHomeJerseyColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Jersey_Color);
                newtHomeSleeveColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Color);
                newtHomeShoulderStripeColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Shoulder_Stripe_Color);
                newtHomeJerseyNumberColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Number_Color);
                newtHomeNumberOutlineColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Number_Outline_Color);
                newtHomeJerseySleeve1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe1);
                newtHomeJerseySleeve2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe2);
                newtHomeJerseySleeve3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe3);
                newtHomeJerseySleeve4Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe4);
                newtHomeJerseySleeve5Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe5);
                newtHomeJerseySleeve6Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe6);

                newtHomePantsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Pants_Color);
                newtHomePantsStripe1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_1);
                newtHomePantsStripe2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_2);
                newtHomePantsStripe3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_3);

                newtAwayJerseyColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Jersey_Color);
                newtAwaySleeveColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Color);
                newtAwayShoulderStripeColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Shoulder_Stripe_Color);
                newtAwayJerseyNumberColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Number_Color);
                newtAwayNumberOutlineColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Number_Outline_Color);
                newtAwayJerseySleeve1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe1);
                newtAwayJerseySleeve2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe2);
                newtAwayJerseySleeve3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe3);
                newtAwayJerseySleeve4Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe4);
                newtAwayJerseySleeve5Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe5);
                newtAwayJerseySleeve6Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe6);

                newtAwayPantsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Pants_Color);
                newtAwayPantsStripe1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_1);
                newtAwayPantsStripe2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_2);
                newtAwayPantsStripe3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_3);

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

                newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image();

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

                Uniform_Img.Flip_All_Colors(true, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor, AwayJerseyNumberColor, AwayJerseyNumberOutlineColor, AwayJerseySleeveColor, AwayJerseyShoulderLoopColor, AwayJerseyStripe_1, AwayJerseyStripe_2, AwayJerseyStripe_3, AwayJerseyStripe_4, AwayJerseyStripe_5, AwayJerseyStripe_6, AwayPantsColor, AwayPants_Stripe_1, AwayPants_Stripe_2, AwayPants_Stripe_3, SocksColor, CleatsColor);

                newtAwayUniform.Source = Uniform_Img.GetAwayUniform_Image();
            }
        }
        private void newtHelmentLogoColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtFacemaskColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtSockColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtCleatsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtHelmentColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
            setAwayUniform();
        }
        private void newtHomeJerseyColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeNumberOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseyNumberColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }
        private void newtHomeSleeveColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve4Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve5Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }

        private void newtHomeJerseySleeve6Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }
        private void newtHomePantsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setHomeUniform();
        }
        private void newtHomePantsStripe2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }
        private void newtHomePantsStripe3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setHomeUniform();
        }
        private void newtAwayJerseyColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayNumberOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseyNumberColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }
        private void newtAwaySleeveColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve4Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve5Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }

        private void newtAwayJerseySleeve6Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
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
            if (Event_from_Code)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsStripe2Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }
        private void newtAwayPantsStripe3Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Event_from_Code)
                return;

            setAwayUniform();
        }
        private void newt1Cancel_Click(object sender, RoutedEventArgs e)
        {
            switch (Form_Function)
            {
                case form_func.New_Team:
                    {
                        backtoNewLeague?.Invoke(this, new TeamUpdatedEventArgs(false));
                        break;
                    }

                case form_func.Stock_Team_New:
                    {
                        backtoStockTeams?.Invoke(this, new EventArgs());
                        break;
                    }

                case form_func.Stock_Team_Edit:
                    {
                        backtoStockTeams?.Invoke(this, new EventArgs());
                        break;
                    }
            }
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
                newtHelmetImgPath.Text = filepath;
                Helmet_image.Source = new BitmapImage(new Uri(filepath));
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
                newtStadiumPath.Text = filepath;
                Stadium_image.Source = new BitmapImage(new Uri(filepath));
            }
        }


        private void newt1Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StadiumMdl stadium = null;
                FootwearMdl Footwear = null;
                HelmetMdl Helmet = null;
                JerseyMdl Home_Jersey = null;
                PantsMdl Home_Pants = null;
                JerseyMdl Away_Jersey = null;
                PantsMdl Away_Pants = null;
                UniformMdl Uniform = null;
                string Stadium_Field_Color = null;
                string socks_color = null;
                string cleats_color = null;
                string helmet_color = null;
                string helmet_logo_color = null;
                string helmet_facemask_color = null;
                string Home_Jersey_Color = null;
                string Home_Sleeve_Color = null;
                string Home_Shoulder_Stripe_Color = null;
                string Home_Jersey_Number_Color = null;
                string Home_Jersey_Outline_Number_Color = null;
                string Home_Jersey_Sleeve_1_Color = null;
                string Home_Jersey_Sleeve_2_Color = null;
                string Home_Jersey_Sleeve_3_Color = null;
                string Home_Jersey_Sleeve_4_Color = null;
                string Home_Jersey_Sleeve_5_Color = null;
                string Home_Jersey_Sleeve_6_Color = null;
                string Home_Pants_Color = null;
                string Home_Pants_Stripe_1_Color = null;
                string Home_Pants_Stripe_2_Color = null;
                string Home_Pants_Stripe_3_Color = null;

                string Away_Jersey_Color = null;
                string Away_Sleeve_Color = null;
                string Away_Shoulder_Stripe_Color = null;
                string Away_Jersey_Number_Color = null;
                string Away_Jersey_Outline_Number_Color = null;
                string Away_Jersey_Sleeve_1_Color = null;
                string Away_Jersey_Sleeve_2_Color = null;
                string Away_Jersey_Sleeve_3_Color = null;
                string Away_Jersey_Sleeve_4_Color = null;
                string Away_Jersey_Sleeve_5_Color = null;
                string Away_Jersey_Sleeve_6_Color = null;
                string Away_Pants_Color = null;
                string Away_Pants_Stripe_1_Color = null;
                string Away_Pants_Stripe_2_Color = null;
                string Away_Pants_Stripe_3_Color = null;

                string City_Abr = newtCityAbb.Text.Trim();
                string City = newtCity.Text.Trim();
                string Nickname = newtNickname.Text.Trim();

                logger.Debug("Just before team validation");
                Validate();
                logger.Debug("Team validated");

                socks_color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtSockColor.SelectedColor).Color);
                cleats_color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color);

                helmet_color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHelmentColor.SelectedColor).Color);
                helmet_logo_color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color);
                helmet_facemask_color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color);

                Home_Jersey_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseyColor.SelectedColor).Color);
                Home_Sleeve_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeSleeveColor.SelectedColor).Color);
                Home_Shoulder_Stripe_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeShoulderStripeColor.SelectedColor).Color);
                Home_Jersey_Number_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseyNumberColor.SelectedColor).Color);
                Home_Jersey_Outline_Number_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeNumberOutlineColor.SelectedColor).Color);
                Home_Jersey_Sleeve_1_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve1Color.SelectedColor).Color);
                Home_Jersey_Sleeve_2_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve2Color.SelectedColor).Color);
                Home_Jersey_Sleeve_3_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve3Color.SelectedColor).Color);
                Home_Jersey_Sleeve_4_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve4Color.SelectedColor).Color);
                Home_Jersey_Sleeve_5_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve5Color.SelectedColor).Color);
                Home_Jersey_Sleeve_6_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomeJerseySleeve6Color.SelectedColor).Color);
                Home_Pants_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomePantsColor.SelectedColor).Color);
                Home_Pants_Stripe_1_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomePantsStripe1Color.SelectedColor).Color);
                Home_Pants_Stripe_2_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomePantsStripe2Color.SelectedColor).Color);
                Home_Pants_Stripe_3_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtHomePantsStripe3Color.SelectedColor).Color);

                Away_Jersey_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseyColor.SelectedColor).Color);
                Away_Sleeve_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwaySleeveColor.SelectedColor).Color);
                Away_Shoulder_Stripe_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayShoulderStripeColor.SelectedColor).Color);
                Away_Jersey_Number_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseyNumberColor.SelectedColor).Color);
                Away_Jersey_Outline_Number_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayNumberOutlineColor.SelectedColor).Color);
                Away_Jersey_Sleeve_1_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve1Color.SelectedColor).Color);
                Away_Jersey_Sleeve_2_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve2Color.SelectedColor).Color);
                Away_Jersey_Sleeve_3_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve3Color.SelectedColor).Color);
                Away_Jersey_Sleeve_4_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve4Color.SelectedColor).Color);
                Away_Jersey_Sleeve_5_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve5Color.SelectedColor).Color);
                Away_Jersey_Sleeve_6_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayJerseySleeve6Color.SelectedColor).Color);
                Away_Pants_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayPantsColor.SelectedColor).Color);
                Away_Pants_Stripe_1_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayPantsStripe1Color.SelectedColor).Color);
                Away_Pants_Stripe_2_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayPantsStripe2Color.SelectedColor).Color);
                Away_Pants_Stripe_3_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newtAwayPantsStripe3Color.SelectedColor).Color);


                var Field_Type_Int = default(short);
                if (newl1FieldType.Text == "Grass")
                    Field_Type_Int = Convert.ToInt16(1);
                else if (newl1FieldType.Text == "Artificial")
                    Field_Type_Int = Convert.ToInt16(2);

                Stadium_Field_Color = CommonUtils.getHexfromColor(new SolidColorBrush((Color)newl1FieldColor.SelectedColor).Color);

                stadium = new StadiumMdl(newtStadium.Text.Trim(), newtStadiumLocation.Text.Trim(), (int)Field_Type_Int, Stadium_Field_Color, newtStadiumCapacity.Text.Trim(), newtStadiumPath.Text.Trim());
                Footwear = new FootwearMdl(socks_color, cleats_color);
                Helmet = new HelmetMdl(helmet_color, helmet_logo_color, helmet_facemask_color);
                Home_Jersey = new JerseyMdl(Home_Jersey_Color, Home_Sleeve_Color, Home_Shoulder_Stripe_Color, Home_Jersey_Number_Color, Home_Jersey_Outline_Number_Color, Home_Jersey_Sleeve_1_Color, Home_Jersey_Sleeve_2_Color, Home_Jersey_Sleeve_3_Color, Home_Jersey_Sleeve_4_Color, Home_Jersey_Sleeve_5_Color, Home_Jersey_Sleeve_6_Color);

                Home_Pants = new PantsMdl(Home_Pants_Color, Home_Pants_Stripe_1_Color, Home_Pants_Stripe_2_Color, Home_Pants_Stripe_3_Color);

                Away_Jersey = new JerseyMdl(Away_Jersey_Color, Away_Sleeve_Color, Away_Shoulder_Stripe_Color, Away_Jersey_Number_Color, Away_Jersey_Outline_Number_Color, Away_Jersey_Sleeve_1_Color, Away_Jersey_Sleeve_2_Color, Away_Jersey_Sleeve_3_Color, Away_Jersey_Sleeve_4_Color, Away_Jersey_Sleeve_5_Color, Away_Jersey_Sleeve_6_Color);

                Away_Pants = new PantsMdl(Away_Pants_Color, Away_Pants_Stripe_1_Color, Away_Pants_Stripe_2_Color, Away_Pants_Stripe_3_Color);
                Uniform = new UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear);

                this_team.setFields("C", City_Abr, City, Nickname, stadium, Uniform, newtHelmetImgPath.Text.Trim());

                switch (Form_Function)
                {
                    case form_func.New_Team:
                        {
                            backtoNewLeague?.Invoke(this, new TeamUpdatedEventArgs(true));
                            break;
                        }

                    case form_func.Stock_Team_New:
                        {
                            var sts = new StockTeams_Services();
                            sts.AddStockTeam(this_team);
                            backtoStockTeams?.Invoke(this, new EventArgs());
                            break;
                        }

                    case form_func.Stock_Team_Edit:
                        {
                            var sts = new StockTeams_Services();
                            sts.UpdateStockTeam(this_team);
                            backtoStockTeams?.Invoke(this, new EventArgs());
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error saving team error");
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
                helmetColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHelmentLogoColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color;
                helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetLogoColor = App_Constants.STOCK_GREY_COLOR;

            if (newtFacemaskColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color;
                helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetFacemaskColor = App_Constants.STOCK_GREY_COLOR;

            if (newtSockColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtSockColor.SelectedColor).Color;
                SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                SocksColor = App_Constants.STOCK_GREY_COLOR;

            if (newtCleatsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color;
                CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                CleatsColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseyColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseyColor.SelectedColor).Color;
                HomeJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeSleeveColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeSleeveColor.SelectedColor).Color;
                HomeJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseySleeveColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeShoulderStripeColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeShoulderStripeColor.SelectedColor).Color;
                HomeJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyShoulderLoopColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseyNumberColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseyNumberColor.SelectedColor).Color;
                HomeJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyNumberColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeNumberOutlineColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeNumberOutlineColor.SelectedColor).Color;
                HomeJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyNumberOutlineColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve1Color.SelectedColor).Color;
                HomeJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_1 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve2Color.SelectedColor).Color;
                HomeJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_2 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve3Color.SelectedColor).Color;
                HomeJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_3 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve4Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve4Color.SelectedColor).Color;
                HomeJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_4 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve5Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve5Color.SelectedColor).Color;
                HomeJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_5 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomeJerseySleeve6Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomeJerseySleeve6Color.SelectedColor).Color;
                HomeJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomeJerseyStripe_6 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsColor.SelectedColor).Color;
                HomePantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePantsColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe1Color.SelectedColor).Color;
                HomePants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_1 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe2Color.SelectedColor).Color;
                HomePants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_2 = App_Constants.STOCK_GREY_COLOR;

            if (newtHomePantsStripe3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHomePantsStripe3Color.SelectedColor).Color;
                HomePants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                HomePants_Stripe_3 = App_Constants.STOCK_GREY_COLOR;

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
                helmetColor = App_Constants.STOCK_GREY_COLOR;

            if (newtHelmentLogoColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtHelmentLogoColor.SelectedColor).Color;
                helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetLogoColor = App_Constants.STOCK_GREY_COLOR;

            if (newtFacemaskColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtFacemaskColor.SelectedColor).Color;
                helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                helmetFacemaskColor = App_Constants.STOCK_GREY_COLOR;

            if (newtSockColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtSockColor.SelectedColor).Color;
                SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                SocksColor = App_Constants.STOCK_GREY_COLOR;

            if (newtCleatsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtCleatsColor.SelectedColor).Color;
                CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                CleatsColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseyColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseyColor.SelectedColor).Color;
                AwayJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwaySleeveColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwaySleeveColor.SelectedColor).Color;
                AwayJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseySleeveColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayShoulderStripeColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayShoulderStripeColor.SelectedColor).Color;
                AwayJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyShoulderLoopColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseyNumberColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseyNumberColor.SelectedColor).Color;
                AwayJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyNumberColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayNumberOutlineColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayNumberOutlineColor.SelectedColor).Color;
                AwayJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyNumberOutlineColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve1Color.SelectedColor).Color;
                AwayJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_1 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve2Color.SelectedColor).Color;
                AwayJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_2 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve3Color.SelectedColor).Color;
                AwayJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_3 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve4Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve4Color.SelectedColor).Color;
                AwayJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_4 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve5Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve5Color.SelectedColor).Color;
                AwayJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_5 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayJerseySleeve6Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayJerseySleeve6Color.SelectedColor).Color;
                AwayJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayJerseyStripe_6 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsColor.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsColor.SelectedColor).Color;
                AwayPantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPantsColor = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe1Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe1Color.SelectedColor).Color;
                AwayPants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_1 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe2Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe2Color.SelectedColor).Color;
                AwayPants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_2 = App_Constants.STOCK_GREY_COLOR;

            if (newtAwayPantsStripe3Color.SelectedColor != null)
            {
                mc = new SolidColorBrush((Color)newtAwayPantsStripe3Color.SelectedColor).Color;
                AwayPants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B);
            }
            else
                AwayPants_Stripe_3 = App_Constants.STOCK_GREY_COLOR;

            Uniform_Img.Flip_All_Colors(false, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor, AwayJerseyNumberColor, AwayJerseyNumberOutlineColor, AwayJerseySleeveColor, AwayJerseyShoulderLoopColor, AwayJerseyStripe_1, AwayJerseyStripe_2, AwayJerseyStripe_3, AwayJerseyStripe_4, AwayJerseyStripe_5, AwayJerseyStripe_6, AwayPantsColor, AwayPants_Stripe_1, AwayPants_Stripe_2, AwayPants_Stripe_3, SocksColor, CleatsColor);

            newtAwayUniform.Source = Uniform_Img.GetAwayUniform_Image();
        }

        private void Validate()
        {
            if (CommonUtils.isBlank(newtCityAbb.Text))
                throw new Exception("City Abbriviation must have a value");

            if (!CommonUtils.isAlpha(newtCityAbb.Text, false))
                throw new Exception("Invalid character in City Abbriviation!");

            if (CommonUtils.isBlank(newtCity.Text))
                throw new Exception("City must have a value");

            if (!CommonUtils.isAlpha(newtCity.Text, true))
                throw new Exception("Invalid character in City!");

            if (CommonUtils.isBlank(newtNickname.Text))
                throw new Exception("Nickname must have a value");

            if (!CommonUtils.isAlpha(newtNickname.Text, true))
                throw new Exception("Invalid character in Nickname!");

            if (CommonUtils.isBlank(newtStadium.Text))
                throw new Exception("Stadium Name must have a value");

            if (!CommonUtils.isAlpha(newtStadium.Text, true))
                throw new Exception("Invalid character in Stadium!");

            if (CommonUtils.isBlank(newtStadiumLocation.Text))
                throw new Exception("Stadium Location must have a value");

            if (newl1FieldColor.SelectedColor == null)
                throw new Exception("Field color of team stadium must be supplied");

            if (CommonUtils.isBlank(newtStadiumCapacity.Text))
                throw new Exception("Stadium Capacity must be supplied and numeric");

            if (CommonUtils.isBlank(newtStadiumPath.Text))
                throw new Exception("Image of team stadium must be supplied");

            if (newtHelmentColor.SelectedColor == null)
                throw new Exception("A helmet color must be selected");

            if (newtHelmentLogoColor.SelectedColor == null)
                throw new Exception("A helmet Logo color must be selected");

            if (newtFacemaskColor.SelectedColor == null)
                throw new Exception("A helmet facemask must be selected");

            if (CommonUtils.isBlank(newtHelmetImgPath.Text))
                throw new Exception("Helmet image path must have a value");

            if (newtSockColor.SelectedColor == null)
                throw new Exception("Sock color must have a value");

            if (newtCleatsColor.SelectedColor == null)
                throw new Exception("Cleat color must have a value");

            if (newtHomeJerseyColor.SelectedColor == null)
                throw new Exception("Home jersey color must have a value");

            if (newtHomeSleeveColor.SelectedColor == null)
                throw new Exception("Home sleeve color must have a value");

            if (newtHomeShoulderStripeColor.SelectedColor == null)
                throw new Exception("Home shoulder loop color must have a value");

            if (newtHomeJerseyNumberColor.SelectedColor == null)
                throw new Exception("Home jersey number color must have a value");

            if (newtHomeNumberOutlineColor.SelectedColor == null)
                throw new Exception("Home jersey outline number color must have a value");

            if (newtHomeJerseySleeve1Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 1 color must have a value");

            if (newtHomeJerseySleeve2Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 2 color must have a value");

            if (newtHomeJerseySleeve3Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 3 color must have a value");

            if (newtHomeJerseySleeve4Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 4 color must have a value");

            if (newtHomeJerseySleeve5Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 5 color must have a value");

            if (newtHomeJerseySleeve6Color.SelectedColor == null)
                throw new Exception("Home jersey sleeve string 6 color must have a value");

            if (newtHomePantsColor.SelectedColor == null)
                throw new Exception("Home pants color must have a value");

            if (newtHomePantsStripe1Color.SelectedColor == null)
                throw new Exception("Home pants stripe 1 color must have a value");

            if (newtHomePantsStripe2Color.SelectedColor == null)
                throw new Exception("Home pants stripe 2 color must have a value");

            if (newtHomePantsStripe3Color.SelectedColor == null)
                throw new Exception("Home pants stripe 3 color must have a value");

            if (newtAwayJerseyColor.SelectedColor == null)
                throw new Exception("Away jersey color must have a value");

            if (newtAwaySleeveColor.SelectedColor == null)
                throw new Exception("Away sleeve color must have a value");

            if (newtAwayShoulderStripeColor.SelectedColor == null)
                throw new Exception("Away shoulder loop color must have a value");

            if (newtAwayJerseyNumberColor.SelectedColor == null)
                throw new Exception("Away jersey number color must have a value");

            if (newtAwayNumberOutlineColor.SelectedColor == null)
                throw new Exception("Away jersey outline number color must have a value");

            if (newtAwayJerseySleeve1Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 1 color must have a value");

            if (newtAwayJerseySleeve2Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 2 color must have a value");

            if (newtAwayJerseySleeve3Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 3 color must have a value");

            if (newtAwayJerseySleeve4Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 4 color must have a value");

            if (newtAwayJerseySleeve5Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 5 color must have a value");

            if (newtAwayJerseySleeve6Color.SelectedColor == null)
                throw new Exception("Away jersey sleeve string 6 color must have a value");

            if (newtAwayPantsColor.SelectedColor == null)
                throw new Exception("Away pants color must have a value");

            if (newtAwayPantsStripe1Color.SelectedColor == null)
                throw new Exception("Away pants stripe 1 color must have a value");

            if (newtAwayPantsStripe2Color.SelectedColor == null)
                throw new Exception("Away pants stripe 2 color must have a value");

            if (newtAwayPantsStripe3Color.SelectedColor == null)
                throw new Exception("Away pants stripe 3 color must have a value");

            // Only if stock team check for duplicate team here
            if (Form_Function == form_func.Stock_Team_New)
            {
                var st_services = new StockTeams_Services();
                if (st_services.DoesTeamAlreadyExist(newtCity.Text.Trim(), newtNickname.Text.Trim()))
                    throw new Exception("This Stock Team Already Exists!  No Duplicate Stock Teams Allowed!");
            }
            else if ((int)Form_Function == (int)form_func.Stock_Team_Edit)
            {
                var st_services = new StockTeams_Services();
                if (st_services.DoesTeamAlreadyExist_ID(newtCity.Text.Trim(), newtNickname.Text.Trim(), original_city, original_nickname))
                    throw new Exception("This Stock Team Already Exists!  No Duplicate Stock Teams Allowed!");
            }
        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            switch (Form_Function)
            {
                case form_func.New_Team:
                    {
                        var hlp_form = new Help_NewTeam();
                        hlp_form.Top = (SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2;
                        hlp_form.Left = (SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2;
                        hlp_form.ShowDialog();
                        break;
                    }
            }
        }
    }
}
