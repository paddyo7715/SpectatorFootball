namespace SpectatorFootball
{
    public class StadiumMdl
    {
        public string Stadium_Name { get; set; } = "";
        public string Stadium_Location { get; set; } = "";
        public int Field_Type { get; set; } // 1 grass, 2 artificial
        public string Field_Color { get; set; }
        public string Capacity { get; set; } = "";
        public string Stadium_Img_Path { get; set; } = "";


        public StadiumMdl(string Stadium_Name, string Stadium_Location, int Field_Type, string Field_Color, string Capacity, string Stadium_Img_Path)
        {
            this.Stadium_Img_Path = Stadium_Img_Path;
            this.Stadium_Location = Stadium_Location;
            this.Field_Type = Field_Type;
            this.Field_Color = Field_Color;
            this.Capacity = Capacity;
            this.Stadium_Name = Stadium_Name;
        }
    }
}
