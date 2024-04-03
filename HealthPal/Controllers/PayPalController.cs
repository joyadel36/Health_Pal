using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System;
using System.Collections.Generic;

namespace HealthPal.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PayPalController : Controller
    {
        private static readonly string clientId = "AcwsEkZGAiiSuIbaXfkTCfPGRWxJzldHlVl7DwIp5ve15bftEMsjs8MaY7hPrd6CWhN-un0m8FxGk5d7";
        private static readonly string clientSecret = "EPhsZhc7oCcGMwwStD28K3LEJdjr6j7GZQ4nI7XxgzYy0Rho47C1vs2GgNBQiGKcBvTg2DTCtG1R6Hjc";

        public IActionResult Index()
        {
            var apiContext = GetApiContext();
            var payment = Payment(apiContext);
            var redirectUrl = payment.GetApprovalUrl();
            return Redirect(redirectUrl);
        }

        public IActionResult Success(string paymentId, string token, string payerId)
        {
            var apiContext = GetApiContext();
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            return View();
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
        {
            {"mode", "sandbox"}, 
            {"clientId", clientId},
            {"clientSecret", clientSecret}
        };

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            return apiContext;
        }

        private Payment Payment(APIContext apiContext)
        {
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Item Name",
                currency = "USD",
                price = "10",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            var redirectUrls = new RedirectUrls()
            {
                cancel_url = Url.Action("Cancel", "PayPal", null, Request.Scheme),
                return_url = Url.Action("Success", "PayPal", null, Request.Scheme)
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = "10" 
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = "10", // Replace with your item price
                details = details
            };

            var transactionList = new List<Transaction>
            {
                new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = amount,
                    item_list = itemList
                }
            };

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            return payment.Create(apiContext);
        }
    }
}
