using JuniorHub.Application.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.DTOs.Offer
{
    public class OffersPagedDto
    {
        public List<OfferDto> Offers { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}
