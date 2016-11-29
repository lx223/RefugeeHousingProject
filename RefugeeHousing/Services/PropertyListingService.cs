using System.Collections.Generic;
using System.Data;
using System.Linq;
using RefugeeHousing.Models;
﻿using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Services
{
    public interface IPropertyListingService
    {
        void AddListingToDatabase(ListingViewModel listingViewModel, string currentUserId);
        ListingDetailsViewModel GetListing(int id);
        ListingViewModel GetListingViewModel(int id);
        IEnumerable<ListingDetailsViewModel> GetListings();
        IEnumerable<ListingDetailsViewModel> GetListings(string ownerId);
        void DeleteListing(int id);
    }

    public class PropertyListingService : IPropertyListingService
    {
        private readonly ILocationRepository locationRepository;
        private readonly IUserIdentityService userIdentityService;

        public PropertyListingService(ILocationRepository locationRepository, IUserIdentityService userIdentityService)
        {
            this.locationRepository = locationRepository;
            this.userIdentityService = userIdentityService;
        }

        public void AddListingToDatabase(ListingViewModel listingViewModel, string currentUserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var locationId = listingViewModel.PlaceId;
                var location = locationRepository.GetOrCreateLocation(db, locationId);
                var currentUser = userIdentityService.GetUser(db, currentUserId);

                var listing = new Listing
                {
                    Appliances = listingViewModel.Appliances,
                    Elevator = listingViewModel.Elevator,
                    Furnished = listingViewModel.Furnished,
                    Price = listingViewModel.Price,
                    LanguagesSpoken = listingViewModel.LanguagesSpoken,
                    Location = location,
                    LocationId = locationId,
                    NumberOfBedrooms = listingViewModel.NumberOfBedrooms,
                    Owner = currentUser,
                    OwnerId = currentUserId
                };

                db.Listings.Add(listing);
                db.SaveChanges();
            }
        }

        public ListingDetailsViewModel GetListing(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var requestedListing = db.Listings.Find(id);
                if (requestedListing == null)
                {
                    return null;
                }
                var location = db.Locations.Find(requestedListing.LocationId);
                var listingDetailsViewModel = new ListingDetailsViewModel
                {
                    Appliances = requestedListing.Appliances,
                    Elevator = requestedListing.Elevator,
                    Furnished = requestedListing.Furnished,
                    Id = requestedListing.Id,
                    LanguagesSpoken = requestedListing.LanguagesSpoken,
                    Location = location,
                    NumberOfBedrooms = requestedListing.NumberOfBedrooms,
                    Price = requestedListing.Price,
                    OwnerId = requestedListing.OwnerId
                };

                return listingDetailsViewModel;
            }
        }

        public ListingViewModel GetListingViewModel(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var listing = db.Listings.Find(id);
                if (listing == null)
                {
                    return null;
                }
                var location = db.Locations.Find(listing.LocationId);
                var listingViewModel = new ListingViewModel
                {
                    Appliances = listing.Appliances,
                    Elevator = listing.Elevator,
                    Furnished = listing.Furnished,
                    LanguagesSpoken = listing.LanguagesSpoken,
                    Location = location,
                    NumberOfBedrooms = listing.NumberOfBedrooms,
                    PlaceId = listing.LocationId,
                    Price = listing.Price
                };

                return listingViewModel;
            }
        }

        public IEnumerable<ListingDetailsViewModel> GetListings()
        {
            using (var db = new ApplicationDbContext())
            {
                var listings = db.Listings.ToList();
                return ConvertListingToListingDetailsViewModel(listings, db);
            }
        }
   
        public IEnumerable<ListingDetailsViewModel> GetListings(string ownerId)
        {
            using (var db = new ApplicationDbContext())
            {
                var listings = db.Listings.Where(l => l.OwnerId == ownerId).ToList();
                return ConvertListingToListingDetailsViewModel(listings, db);
            }
        }

        public void DeleteListing(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var listing = db.Listings.Find(id);
                db.Listings.Remove(listing);
                db.SaveChanges();
            }
        }

        private static IEnumerable<ListingDetailsViewModel> ConvertListingToListingDetailsViewModel(List<Listing> listings, ApplicationDbContext db)
        {
            return (from listing in listings
                let location = db.Locations.Find(listing.LocationId)
                select new ListingDetailsViewModel
                {
                    Appliances = listing.Appliances,
                    Elevator = listing.Elevator,
                    Furnished = listing.Furnished,
                    Id = listing.Id,
                    LanguagesSpoken = listing.LanguagesSpoken,
                    Location = location,
                    NumberOfBedrooms = listing.NumberOfBedrooms,
                    Price = listing.Price,
                    OwnerId = listing.OwnerId
                }).ToList();
        }
    }
}