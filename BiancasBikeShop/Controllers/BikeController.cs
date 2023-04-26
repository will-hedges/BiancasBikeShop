﻿using BiancasBikeShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiancasBikeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private IBikeRepository _bikeRepo;

        public BikeController(IBikeRepository bikeRepo)
        {
            _bikeRepo = bikeRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bikeRepo.GetAllBikes());
        }

        //
        // public IActionResult Get(int id)
        // {
        //     //add implementation here...
        //     return Ok();
        // }

        //
        // public IActionResult GetBikesInShopCount()
        // {
        //     //add implementation here...

        //     return Ok();
        // }
    }
}
