﻿namespace PlaceRentalApp.API.Models
{
    public class CreateBookInputModel
    {
        public int IdUser { get; set; }
        public int IdPlace { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
    }
}
