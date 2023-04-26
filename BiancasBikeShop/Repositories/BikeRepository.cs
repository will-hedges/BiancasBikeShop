using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BiancasBikeShop.Models;
using Tabloid.Utils;

namespace BiancasBikeShop.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(
                    "server=localhost\\SQLExpress;database=BiancasBikeShop;integrated security=true;TrustServerCertificate=true"
                );
            }
        }

        public List<Bike> GetAllBikes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"
                        SELECT b.Id, b.Brand, b.Color,
		                        bt.[Name] AS BikeTypeName,
		                        o.[Name] AS OwnerName
                        FROM Bike b
	                        JOIN BikeType bt ON bt.Id = b.BikeTypeId
	                        LEFT JOIN Owner o ON o.Id = b.OwnerId
                        ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        var bikes = new List<Bike>();
                        while (reader.Read())
                        {
                            Bike bike = new Bike()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Brand = DbUtils.GetString(reader, "Brand"),
                                Color = DbUtils.GetString(reader, "Color"),
                                BikeType = new BikeType()
                                {
                                    Name = DbUtils.GetString(reader, "BikeTypeName")
                                },
                                Owner = new Owner()
                                {
                                    Name = DbUtils.GetString(reader, "OwnerName")
                                }
                            };
                            bikes.Add(bike);
                        }
                        return bikes;
                    }
                }
            }
        }

        public Bike GetBikeById(int id)
        {
            Bike bike = null;
            //implement code here...
            return bike;
        }

        public int GetBikesInShopCount()
        {
            int count = 0;
            // implement code here...
            return count;
        }
    }
}
