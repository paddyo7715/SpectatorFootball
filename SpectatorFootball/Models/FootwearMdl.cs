namespace SpectatorFootball
{
    public class FootwearMdl
    {
        public string Socks_Color { get; set; } = "";
        public string Cleats_Color { get; set; } = "";

        public FootwearMdl(string Socks_Color, string Cleats_Color)
        {
            this.Socks_Color = Socks_Color;
            this.Cleats_Color = Cleats_Color;
        }
    }
}
