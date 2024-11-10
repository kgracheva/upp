﻿namespace upp.Dtos.Training
{
    public class FindTrainingsDto
    {
        public DateTime? Date { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; } = "";
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}