using System;
using System.Collections.Generic;
using _50Pixels.Models;
using Microsoft.AspNetCore.Identity;

namespace _50Pixels.Data
{
    public class DbSeeder : IDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public DbSeeder(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        public void Seed()
        {
            if (this._context.Database.EnsureCreated())
            {
                SeedUser();
                SeedPhotos();
            }
        }

        private void SeedUser()
        {
            var email = "fiftypixels@fiftypixels.com";
            var password = "fiftypixels";

            var defaultUser = this._userManager.FindByNameAsync(email).GetAwaiter().GetResult();

            if (defaultUser == null)
            {
                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    Firstname = "Fifty",
                    Lastname = "Pixels",
                    PhotoPath = "no-photo.png",
                    RegisteredDate = DateTime.Now
                };

                this._userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            }

        }

        private void SeedPhotos()
        {
            var uploaderId = this._userManager.FindByEmailAsync("fiftypixels@fiftypixels.com").GetAwaiter().GetResult().Id;

            var photos = new List<Photo>
            {
                new Photo
                {
                    Title = "Beautiful Green Falls",
                    Path = "photo1.jpg",
                    Views = 34,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Bridge to Terabithia",
                    Path = "photo2.jpg",
                    Views = 36,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Baywatch",
                    Path = "photo3.jpg",
                    Views = 37,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "A Perfect Bridge in the forest",
                    Path = "photo4.jpg",
                    Views = 35,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Incredible Falls",
                    Path = "photo5.jpg",
                    Views = 78,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Lake at the heart of the forest",
                    Path = "photo6.jpg",
                    Views = 65,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "First time surfing",
                    Path = "photo7.jpg",
                    Views = 45,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Nothern Lights",
                    Path = "photo8.jpg",
                    Views = 87,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Lanterns",
                    Path = "photo9.jpg",
                    Views = 54,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Gothic Girl",
                    Path = "photo10.jpg",
                    Views = 12,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Koi Fish in the pond",
                    Path = "photo11.jpg",
                    Views = 43,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Camping under the starrynight",
                    Path = "photo12.jpg",
                    Views = 67,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Lesbians in the shore",
                    Path = "photo13.jpg",
                    Views = 74,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "The Rock",
                    Path = "photo14.jpg",
                    Views = 76,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "The CEO of Mozilla Firefox",
                    Path = "photo15.jpg",
                    Views = 87,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Massive Falls",
                    Path = "photo16.jpg",
                    Views = 89,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Pets of Linus Torvalds",
                    Path = "photo17.jpg",
                    Views = 98,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Sunset",
                    Path = "photo18.jpg",
                    Views = 120,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "The Web",
                    Path = "photo19.jpg",
                    Views = 123,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Kaartehan at the Shore",
                    Path = "photo20.jpg",
                    Views = 231,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Starrynight at the shore city",
                    Path = "photo21.jpg",
                    Views = 124,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "The Red Forest",
                    Path = "photo22.jpg",
                    Views = 234,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "You and I are like Diamonds in the sky",
                    Path = "photo23.jpg",
                    Views = 431,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Starry Night Yeah",
                    Path = "photo24.jpg",
                    Views = 458,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Pea of my Cock",
                    Path = "photo25.jpg",
                    Views = 543,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Antarticas Fishing boat",
                    Path = "photo26.jpg",
                    Views = 2,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Polar Bear",
                    Path = "photo27.jpg",
                    Views = 3,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Red Elephants",
                    Path = "photo28.jpg",
                    Views = 452,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Fog in the mountains",
                    Path = "photo29.jpg",
                    Views = 531,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Drone shot mother, father",
                    Path = "photo30.jpg",
                    Views = 345,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Stamina mina eh eh it's time for africa",
                    Path = "photo31.jpg",
                    Views = 567,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Date of two camels",
                    Path = "photo32.jpg",
                    Views = 98,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Chinese Bridge",
                    Path = "photo33.jpg",
                    Views = 567,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Cum ping",
                    Path = "photo34.jpg",
                    Views = 657,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Lakeshore Trees",
                    Path = "photo35.jpg",
                    Views = 576,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Aurora of my life",
                    Path = "photo36.jpg",
                    Views = 531,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Beautiful Chinese Girl",
                    Path = "photo37.jpg",
                    Views = 678,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "How to make a Laing part 1",
                    Path = "photo38.jpg",
                    Views = 988,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "How to make a Laing part 2",
                    Path = "photo17.jpg",
                    Views = 98,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                },
                new Photo
                {
                    Title = "Paradise",
                    Path = "photo40.jpg",
                    Views = 968,
                    DateUploaded = DateTime.Now,
                    UploaderId = uploaderId
                }
            };

            this._context.AddRange(photos);
            this._context.SaveChanges();
        }
    }
}