using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeoJSON.Net.Geometry;
using Microsoft.EntityFrameworkCore;

namespace ZeDeliveryCodeChallenge
{
    [Table("partners")]
    [Index(nameof(document), IsUnique = true)]
    public class Partner
    {
        [Key] public string? id { get; private set; }
        public string? tradingName { get; private set; }
        public string? ownerName { get; private set; }
        public string? document { get; private set; }
        public MultiPolygon? coverageArea { get; private set; }
        public Point? address { get; private set; }

        public Partner(
            string? id,
            string? tradingName,
            string? ownerName,
            string? document,
            MultiPolygon? coverageArea,
            Point? address
        )
        {
            this.id = id;
            this.tradingName = tradingName;
            this.ownerName = ownerName;
            this.document = document;
            this.coverageArea = coverageArea;
            this.address = address;
        }
    }
}