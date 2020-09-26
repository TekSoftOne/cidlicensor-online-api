using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OR.CloudStorage;
using OR.Data;
using OR.Data.ViewModels;
using RestSharp;
namespace OR.Web.Apis
{

    public class CreateOrderRequestModel
    {
        public string Token { get; set; }
        public OrderRequestBody OrderRequestBody { get; set; }
    }

    public class OrderRequestBody
    {
        public string action { get; set; }
        public AmountModel amount { get; set; }
        public MerchantAttributes merchantAttributes { get; set; }
    }

    public class AmountModel
    {
        public string currencyCode { get; set; }
        public double value { get; set; }
    }

    public class MerchantAttributes
    {
        public string maskPaymentInfo { get; set; }
        public string redirectUrl { get; set; }
        public bool skipConfirmationPage { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        public PaymentGatewayController()
        {
        }

        [HttpGet("GetToken")]
        public IActionResult GetToken()
        {
            try
            {
                var ngeniousGateway = "https://api-gateway.sandbox.ngenius-payments.com";
                var ngeniousApiKey = "OTNmNGZlZDEtMTc0Mi00ZDhmLWEzZjMtYTE0ZWQ1NDZmYTY1OmJmNDRmNjcxLTczOWMtNDIxMC1hOTgwLWQ4NDU3YzgzYTk0Mg==";
                var gatewayUrl = $"{ngeniousGateway}/identity/auth/access-token";

                var client = new RestClient(gatewayUrl);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Basic {ngeniousApiKey}");
                request.AddHeader("Accept", "application/vnd.ni-identity.v1+json");
                request.AddHeader("Content-Type", "application/vnd.ni-identity.v1+json");
                IRestResponse response = client.Execute(request);
                return new OkObjectResult(response.Content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequestModel orderRequest)
        {
            try
            {
                var ngeniousGateway = "https://api-gateway.sandbox.ngenius-payments.com";
                var outlet = "47294c75-de03-41eb-8a80-0462e0c7c99a";
                var gatewayOrderUrl = $"{ngeniousGateway}/transactions/outlets/{outlet}/orders";
                var client = new RestClient($"{ngeniousGateway}/transactions/outlets/{outlet}/orders");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {orderRequest.Token}");
                request.AddHeader("Content-Type", "application/vnd.ni-payment.v2+json");
                request.AddHeader("Accept", "application/vnd.ni-payment.v2+json");
                request.AddParameter("application/vnd.ni-payment.v2+json", JsonSerializer.Serialize(orderRequest.OrderRequestBody), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return new OkObjectResult(response.Content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
