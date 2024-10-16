using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServcies.Dtos;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointSecret = "whsec_3fe378e72658b9c9def4f3991bd112b2ed9eb0a0180444bf58927789af643779";

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
        {
           _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));

        [HttpPost   ]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;
                //Handle the event
                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed: ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Failed: ", order.Id);
                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Succeeded: ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentSucessed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Successed: ", order.Id);
                }
                else if (stripeEvent.Type == "payment_intent.created")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Created: ", paymentIntent.Id);
                }
                else
                {
                    Console.WriteLine("Unhandled event type : {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {

                return BadRequest();
            }
        }

    }
}
