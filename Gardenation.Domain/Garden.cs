using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gardenation.Domain
{
    public class Garden
    {
        // for EF
        public Garden() { }
        public static Garden Factory(string name, DateTime now, Guid? plotId = null)
        {
            return new Garden
            {
                CreatedDate = now,
                GardenName = name,
                PlotId = plotId ?? Guid.Empty
            };
        }

        public Guid Id { get; set; }
        public string GardenName { get; set; }
        public Guid PlotId { get; set; }
        public Plot Plot { get; set; }
        public DateTime LastVisitedDate { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastWateredDate { get; set; }
        public ICollection<Vegetable> Vegetables { get; set; }
        public bool WaterAllPlants()
        {
            if (!Vegetables.Any()) return false;

            return Vegetables.Select(v => { v.Water(); return v; }).Any();
        }

        public WaterUrgency GetMostSevereWaterUrgency(DateTime today)
        {
            if (Vegetables.IsNullOrEmpty()) return WaterUrgency.Invalid;

            return Vegetables.Max(v => v.GetWaterUrgency(today.Date));
        }

        public void AddVegetable(Vegetable v)
        {
            v.Id = v.Id == Guid.Empty ? Guid.NewGuid() : v.Id;
            v.GardenId = this.Id;

            Vegetables.Add(v);
        }
    }

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

    public class PlantInfo
    {
        public int Id { get; set; }
        public int LeastManyDaysBetweenWatering { get; set; }
        public bool DailyWaterOptional { get; set; }
        public string ScientificName { get; set; }
        public int DaysToHarvest { get; set; }
    }

    public class Plot
    {
        public Guid Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Geolocation { get; set; }
        public IQueryable<Garden> Gardens { get; set; }
        public IEnumerable<Point> GetOpenCoordinates()
        {
            throw new NotImplementedException();
        }
    }
}
