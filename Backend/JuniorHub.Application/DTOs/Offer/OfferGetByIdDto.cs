using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.DTOs.Offer
{
    public class OfferGetByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string FullNameAuthor { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public EstimatedTime EstimatedTime { get; set; }
        public State State { get; set; }
        public Difficult Difficult { get; set; }
        public decimal Price { get; set; }
        public List<TechnologiesDto> Technologies { get; set; }
    }
}
