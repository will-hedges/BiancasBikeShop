using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BiancasBikeShop.Models;
using Tabloid.Utils;
using Microsoft.Extensions.Hosting;

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
	                        LEFT JOIN [Owner] o ON o.Id = b.OwnerId
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
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"
                        SELECT b.Id, b.Brand, b.Color,
		                        bt.[Name] AS BikeTypeName,
		                        o.[Name] AS OwnerName,
		                        wo.Id AS WorkOrderId, wo.DateInitiated, wo.[Description], wo.DateCompleted
                        FROM Bike b
	                        JOIN BikeType bt ON bt.Id = b.BikeTypeId
	                        LEFT JOIN [Owner] o ON o.Id = b.OwnerId
	                        LEFT JOIN WorkOrder wo ON wo.BikeId = b.Id
                        WHERE b.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Bike bike = null;
                        while (reader.Read())
                        {
                            // this 'bike == null' line is so you don't re-create the bike
                            //      for multiple work orders, just add them to their list
                            if (bike == null)
                            {
                                bike = new Bike()
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
                                    },
                                    WorkOrders = new List<WorkOrder>()
                                };
                            }
                            if (DbUtils.IsNotDbNull(reader, "WorkOrderId"))
                            {
                                WorkOrder workOrder = new WorkOrder()
                                {
                                    Id = DbUtils.GetInt(reader, "WorkOrderId"),
                                    DateInitiated = DbUtils.GetDateTime(reader, "DateInitiated"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    DateCompleted = DbUtils.GetNullableDateTime(
                                        reader,
                                        "DateCompleted"
                                    )
                                };
                                bike.WorkOrders.Add(workOrder);
                            }
                        }
                        return bike;
                    }
                }
            }
        }

        public int GetBikesInShopCount()
        {
            int count = 0;
            // implement code here...
            return count;
        }
    }
}
