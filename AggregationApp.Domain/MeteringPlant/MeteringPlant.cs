﻿namespace AggregationApp.Domain.MeteringPlant
{
    public class MeteringPlant
    {
        public string Network { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string PPlus { get; set; }
        public string PMinus { get; set; }
        public DateTime Date { get; set; }
    }
}