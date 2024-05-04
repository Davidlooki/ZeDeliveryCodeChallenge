using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace ZeDeliveryCodeChallenge
{
    [Table("partners")]
    [Index(nameof(document), IsUnique = true)]
    public record Partner(
        string? id,
        string? tradingName,
        string? ownerName,
        string? document,
        MultiPolygon? coverageArea,
        Point? address
    )
    {
        [Key] public string? id { get; private set; } = id;
        public string? tradingName { get; private set; } = tradingName;
        public string? ownerName { get; private set; } = ownerName;
        public string? document { get; private set; } = document;
        public MultiPolygon? coverageArea { get; private set; } = coverageArea;
        public Point? address { get; private set; } = address;
    }
}