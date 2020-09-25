using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OR.CloudStorage;
using OR.Data;
using OR.Data.ViewModels;
using RestSharp;
namespace OR.Web.Apis
{
    public class TokenRequestModel
    {
        public string GatewayUrl { get; set; }
        public string ApiKey { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        public PaymentGatewayController()
        {
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] TokenRequestModel gatewayRequest)
        {
            var client = new RestClient(gatewayRequest.GatewayUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic {gatewayRequest.ApiKey}");
            request.AddHeader("Accept", "application/vnd.ni-identity.v1+json");
            request.AddHeader("Content-Type", "application/vnd.ni-identity.v1+json");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return new OkObjectResult(response.Content);
        }
    }
}
