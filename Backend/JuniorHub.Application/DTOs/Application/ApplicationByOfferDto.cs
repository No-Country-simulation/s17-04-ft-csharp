﻿using JuniorHub.Application.DTOs.Link;
using JuniorHub.Application.DTOs.Technology;

namespace JuniorHub.Application.DTOs.Application;

public class ApplicationByOfferDto
{
    public int Id { get; set; }
    public int FreelancerId { get; set; }
    public string FreelancerName { get; set; }
    public string FreelancerDescription { get; set; }
    public IEnumerable<TechnologiesDto> Technologies { get; set; }
    public IEnumerable<LinksOffersApplications> Links { get; set; }
    public DateTime ApplicationDate { get; set; }
    public bool Selected { get; set; }
}