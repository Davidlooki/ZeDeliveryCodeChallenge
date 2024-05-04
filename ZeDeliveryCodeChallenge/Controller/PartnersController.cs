using Microsoft.AspNetCore.Mvc;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace ZeDeliveryCodeChallenge
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController(IPartnerRepository partnerRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            try
            {
                var partners = await partnerRepository.GetPartners();
                var geoJSONFeatureCollection = ConvertToGeoJSON(partners);

                return Ok(partners);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartnerById(string id)
        {
            try
            {
                var result = await partnerRepository.GetPartnerById(id);

                return result is null ? NotFound() : Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartner(Partner? partner)
        {
            try
            {
                if (partner?.id is null || partner.document is null) return BadRequest();

                var checkPartnerId = await partnerRepository.GetPartnerById(partner.id);

                if (checkPartnerId is not null)
                {
                    ModelState.AddModelError("id", "Partner id already in use.");

                    return BadRequest(ModelState);
                }

                var checkPartnerDocument = await partnerRepository.GetPartnerByDocument(partner.document);

                if (checkPartnerDocument is not null)
                {
                    ModelState.AddModelError("document", "Partner document already in use.");

                    return BadRequest(ModelState);
                }

                var createdPartner = await partnerRepository.CreatePartner(partner);
                var geoJSONFeatureCollection = ConvertToGeoJSON(new List<Partner>
                { createdPartner });

                return Ok(geoJSONFeatureCollection);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new partner record");
            }
        }

        private static GeoJSON.Net.Geometry.MultiPolygon ConvertGeometryToGeoJSON(
            NetTopologySuite.Geometries.MultiPolygon ntsMultiPolygon
        )
        {
            var geoJSONMultiPolygon = new GeoJSON.Net.Geometry.MultiPolygon(
                ntsMultiPolygon.Geometries.Select(ntsPolygon =>
                    new Polygon(
                        new List<LineString>
                        { new LineString(
                              ntsPolygon.Coordinates.Select(ntsCoordinate =>
                                  new Position(ntsCoordinate.Y, ntsCoordinate.X)).ToList()
                          ) }
                    )).ToList()
            );

            return geoJSONMultiPolygon;
        }

        private static GeoJSON.Net.Feature.FeatureCollection ConvertToGeoJSON(IEnumerable<Partner> partners)
        {
            var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();

            foreach (var partner in partners)
            {
                var properties = new Dictionary<string, object>
                { { "id", partner.id },
                  { "tradingName", partner.tradingName },
                  { "ownerName", partner.ownerName },
                  { "document", partner.document },
                  { "coverageArea", partner.coverageArea },
                  { "address", partner.address } };

                var geometry = ConvertGeometryToGeoJSON(partner.coverageArea);

                var feature = new Feature(geometry, properties);
                featureCollection.Features.Add(feature);
            }

            return featureCollection;
        }
    }
}