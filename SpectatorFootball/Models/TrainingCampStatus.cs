﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    class TrainingCampStatus
    {

        public string helmet_filename { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public string Team_Name { get; set; }
        public int Status { get; set; } //1 Not Started, 2 In Progress, 3 Completed
        public long Season_ID { get; set; }
        public long? Franchise_ID { get; set; }

    }
}
