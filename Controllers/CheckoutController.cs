namespace NetCoreDemo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
public class CheckoutController : Controller
{
    private readonly PayOS _payOS;


    public CheckoutController(PayOS payOS)
    {
        _payOS = payOS;

    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        return View("index");
    }
    [HttpGet("/cancel")]
    public IActionResult Cancel()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        return View("cancel");
    }
    [HttpGet("/success")]
    public IActionResult Success()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        return View("success");
    }
    [HttpPost("/create-payment-link")]
    public async Task<IActionResult> Checkout()
    {
        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 2000);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            PaymentData paymentData = new PaymentData(orderCode, 11000, "Thanh toan don hang", items, "http://localhost:3000/cancel", "http://localhost:3000/success");

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            return Redirect(createPayment.checkoutUrl);
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            return Redirect("http://localhost:3000/");
        }
    }
}
