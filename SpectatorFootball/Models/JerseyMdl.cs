namespace SpectatorFootball
{
    public class JerseyMdl
    {
        public string Jersey_Color { get; set; } = "";
        public string Sleeve_Color { get; set; } = "";
        public string Shoulder_Stripe_Color { get; set; } = "";
        public string Number_Color { get; set; } = "";
        public string Number_Outline_Color { get; set; } = "";
        public string Sleeve_Stripe1 { get; set; } = "";
        public string Sleeve_Stripe2 { get; set; } = "";
        public string Sleeve_Stripe3 { get; set; } = "";
        public string Sleeve_Stripe4 { get; set; } = "";
        public string Sleeve_Stripe5 { get; set; } = "";
        public string Sleeve_Stripe6 { get; set; } = "";


        public JerseyMdl(string Jersey_Color, string Sleeve_Color, string Shoulder_Stripe_Color, string Number_Color, string Number_Outline_Color, string Sleeve_Stripe1, string Sleeve_Stripe2, string Sleeve_Stripe3, string Sleeve_Stripe4, string Sleeve_Stripe5, string Sleeve_Stripe6)
        {
            this.Jersey_Color = Jersey_Color;
            this.Sleeve_Color = Sleeve_Color;
            this.Shoulder_Stripe_Color = Shoulder_Stripe_Color;
            this.Number_Color = Number_Color;
            this.Number_Outline_Color = Number_Outline_Color;
            this.Sleeve_Stripe1 = Sleeve_Stripe1;
            this.Sleeve_Stripe2 = Sleeve_Stripe2;
            this.Sleeve_Stripe3 = Sleeve_Stripe3;
            this.Sleeve_Stripe4 = Sleeve_Stripe4;
            this.Sleeve_Stripe5 = Sleeve_Stripe5;
            this.Sleeve_Stripe6 = Sleeve_Stripe6;
        }
    }
}
