using System;
using System.Drawing;

namespace Gardenation.Domain
{
    public class Vegetable
    {
        public Guid Id { get; set; }
        public Guid GardenId { get; set; }
        public int PlantInfoId { get; set; }
        public Point Coordinate { get; set; }
        public PlantInfo PlantInfo { get; set; }
        public VegetableType Type { get; set; }
        public DateTime PlantedDate { get; set; }
        public DateTime LastWateredDate { get; set; }
        public DateTime LastHarvestedDate { get; set; }
        public DateTime ShouldHarvestDate { get; set; }
        public int DaysCanGoWithoutWater { get; set; }

        public WaterUrgency GetWaterUrgency(DateTime now)
        {
            var needsWateredOnDate = LastWateredDate.AddDays(PlantInfo.LeastManyDaysBetweenWatering).Date;
            var needsWateredInDays = (now.Date - needsWateredOnDate).Days;

            if (needsWateredInDays < 0)
            {
                return PlantInfo.DailyWaterOptional ?
                    WaterUrgency.WaterOptional :
                    WaterUrgency.DoNotWater;
            }
            else if (needsWateredInDays == 0) return WaterUrgency.WaterToday;
            else if (needsWateredInDays <= 2) return WaterUrgency.WaterOverdue;
            else return WaterUrgency.WaterCritial;
        }

        public void Water(DateTime? date = null)
        {
            LastWateredDate = date == null ? DateTime.Now : (DateTime)date;
        }
    }
}
