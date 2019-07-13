namespace Gardenation.Domain
{
    public class PlantInfo
    {
        public int Id { get; set; }
        public int LeastManyDaysBetweenWatering { get; set; }
        public bool DailyWaterOptional { get; set; }
        public string ScientificName { get; set; }
        public int DaysToHarvest { get; set; }
    }
}
