using Microsoft.AspNetCore.Mvc;

namespace ZeDeliveryCodeChallenge
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController(IPartnerRepository partnerRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            try { return Ok(await partnerRepository.GetPartners()); }
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

                if (checkPartnerId != null)
                {
                    ModelState.AddModelError("id", "Partner id already in use.");

                    return BadRequest(ModelState);
                }

                var checkPartnerDocument = await partnerRepository.GetPartnerByDocument(partner.document);

                if (checkPartnerDocument != null)
                {
                    ModelState.AddModelError("document", "Partner document already in use.");

                    return BadRequest(ModelState);
                }

                var createdPartner = await partnerRepository.CreatePartner(partner);

                return CreatedAtAction(nameof(GetPartnerById), new
                { id = createdPartner.id }, createdPartner);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new partner record");
            }
        }
    }
}