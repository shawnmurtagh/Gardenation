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
}
