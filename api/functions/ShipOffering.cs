using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using api.entities;

namespace api.functions 
{
    public class ShipOffering
    {
        public ShipOffering(SwsTravelContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ShipOffering");
        }

        [Function("PostShipOffering")]
        public HttpResponseData Post(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ship-offering")] HttpRequestData request)
        {  
            _logger.LogInformation("PostShipOffering");

            string body = string.Empty;
            using (var reader = new StreamReader(request.Body))
            {
                body = reader.ReadToEnd();
            }
            
            var offering = JsonConvert.DeserializeObject<models.ShipOffering>(body);
            var tp = new TravelProduct() { ProductName = offering.Name };
            var pto = new PassengerTransportationOffering() 
            { 
                FacilityIdgoingTo = offering.Destination.ID, 
                FacilityIdoriginatingFrom = offering.Origin.ID,
                Product = tp 
            };
            var so = new entities.ShipOffering() { Product = pto };
            _context.Add(so);
            _context.SaveChanges();

            var response = request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Access-Control-Expose-Headers", "Location");
            response.Headers.Add("Location", $"{new Uri(request.Url, $"{so.ProductId}")}");

            return response;    
        }

        [Function("GetShipOffering")]
        public HttpResponseData Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ship-offering/{id:int?}")] HttpRequestData request,
            int? id)
        {
            _logger.LogInformation("GetShipOffering");
 
            dynamic offerings = id.HasValue ? GetOne(id.Value) : GetMany();
            if (offerings != null)
            {
                var response = request.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");
                response.WriteString((string)JsonConvert.SerializeObject(offerings));
                return response;
            }
            else
            {
                var response = request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
         }

        [Function("PutShipOffering")]
        public HttpResponseData Put(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ship-offering/{id:int}")] HttpRequestData request,
            int id)
        {
            var offering = _context.ShipOfferings
                                   .Where(so => so.ProductId== id)
                                   .Include("Product.Product")
                                   .FirstOrDefault();
            if (offering != null)
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(request.Body))
                {
                    body = reader.ReadToEnd();
                }

                var updated = JsonConvert.DeserializeObject<models.ShipOffering>(body);
                offering.Product.FacilityIdgoingTo = updated.Destination.ID;
                offering.Product.FacilityIdoriginatingFrom = updated.Origin.ID;
                offering.Product.Product.ProductName = updated.Name;
                _context.SaveChanges();

                var response = request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            }
            else
            {
                var response = request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }

        [Function("DeleteShipOffering")]
        public HttpResponseData Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "ship-offering/{id:int}")] HttpRequestData request,
            int id)
        {
            var offering = _context.ShipOfferings
                                   .Where(so => so.ProductId == id)
                                   .Include("Product.Product")
                                   .FirstOrDefault();

            if (offering != null)
            {

                _context.ShipOfferings.Remove(offering);
                _context.PassengerTransportationOfferings.Remove(offering.Product);
                _context.TravelProducts.Remove(offering.Product.Product);
                _context.SaveChanges();

                var response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            else
            {
                var response = request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }

        private models.ShipOffering GetOne(int id)
        {
            var offering = _context.ShipOfferings
                .Where(so => so.ProductId == id)
                .OrderBy(so => so.ProductId)
                .Select(so => new models.ShipOffering()
                {
                    ID = so.Product.Product.ProductId,
                    Name = so.Product.Product.ProductName,
                    Origin = new models.Facility()
                    {
                        ID = so.Product.FacilityIdoriginatingFromNavigation.FacilityId,
                        Name = so.Product.FacilityIdoriginatingFromNavigation.FacilityName
                    },                    
                    Destination = new models.Facility()
                    {
                        ID = so.Product.FacilityIdgoingToNavigation.FacilityId,
                        Name = so.Product.FacilityIdgoingToNavigation.FacilityName
                    }
                })
                .FirstOrDefault();

            return offering;
        }

        private IEnumerable<models.ShipOffering> GetMany()
        {
            return _context.ShipOfferings
                .OrderBy(so => so.ProductId)
                .Select(so => new models.ShipOffering() 
                {
                    ID = so.Product.Product.ProductId,
                    Name = so.Product.Product.ProductName,
                    Origin = new models.Facility()
                    {
                        ID = so.Product.FacilityIdoriginatingFromNavigation.FacilityId,
                        Name = so.Product.FacilityIdoriginatingFromNavigation.FacilityName
                    },                    
                    Destination = new models.Facility()
                    {
                        ID = so.Product.FacilityIdgoingToNavigation.FacilityId,
                        Name = so.Product.FacilityIdgoingToNavigation.FacilityName
                    }
                });
        }

        private readonly SwsTravelContext _context;
        private readonly ILogger _logger;
    }
}