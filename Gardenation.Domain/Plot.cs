using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Gardenation.Domain
{
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
