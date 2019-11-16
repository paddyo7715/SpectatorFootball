namespace SpectatorFootball
{
    public class UniformMdl
    {
        public HelmetMdl Helmet { get; set; }
        public JerseyMdl Home_Jersey { get; set; }
        public JerseyMdl Away_Jersey { get; set; }
        public PantsMdl Home_Pants { get; set; }
        public PantsMdl Away_Pants { get; set; }
        public FootwearMdl Footwear { get; set; }

        public UniformMdl(HelmetMdl Helmet, JerseyMdl Home_Jersey, JerseyMdl Away_Jersey, PantsMdl Home_Pants, PantsMdl Away_Pants, FootwearMdl Footwear)
        {
            this.Helmet = Helmet;
            this.Home_Jersey = Home_Jersey;
            this.Away_Jersey = Away_Jersey;
            this.Home_Pants = Home_Pants;
            this.Away_Pants = Away_Pants;
            this.Footwear = Footwear;
        }
    }
}
