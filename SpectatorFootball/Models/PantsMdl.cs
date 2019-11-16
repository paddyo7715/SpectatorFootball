namespace SpectatorFootball
{
    public class PantsMdl
    {
        public string Pants_Color { get; set; } = "";
        public string Stripe_Color_1 { get; set; } = "";
        public string Stripe_Color_2 { get; set; } = "";
        public string Stripe_Color_3 { get; set; } = "";

        public PantsMdl(string Pants_Color, string Stripe_Color_1, string Stripe_Color_2, string Stripe_Color_3)
        {
            this.Pants_Color = Pants_Color;
            this.Stripe_Color_1 = Stripe_Color_1;
            this.Stripe_Color_2 = Stripe_Color_2;
            this.Stripe_Color_3 = Stripe_Color_3;
        }
    }
}
