namespace SpectatorFootball
{
    public class HelmetMdl
    {
        public string Helmet_Color { get; set; } = "";
        public string Helmet_Logo_Color { get; set; } = "";
        public string Helmet_Facemask_Color { get; set; } = "";

        public HelmetMdl(string Helmet_Color, string Helmet_Logo_Color, string Helmet_Facemask_Color)
        {
            this.Helmet_Color = Helmet_Color;
            this.Helmet_Logo_Color = Helmet_Logo_Color;
            this.Helmet_Facemask_Color = Helmet_Facemask_Color;
        }
    }
}
