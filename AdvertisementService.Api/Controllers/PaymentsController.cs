using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Contracts.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace AdvertisementService.Api.Controllers
{

    [Route("api/[controller]")]
    public class PaymentsController : ApiControllers
    {
        private readonly IAdvertisementUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentsController(IAdvertisementUnitOfWork unitOfWork, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }
            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] Payment payment)
        {
            if (!ModelState.IsValid || id != payment.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingPayment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.PaymentRepository.DeleteAsync(payment);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpPost("request")]
        [Authorize]
        public async Task<IActionResult> RequestPayment([FromBody] RequestPaymentDto dto)
        {
            var usersId = userId;

            // ذخیره در دیتابیس با وضعیت Pending
            var payment = new Payment
            {
                Amount = dto.Amount,
                TestType = dto.TestType,
                Status = PaymentStatus.Pending,
                PaymentMethod = "Zarinpal",
                UserId = userId,
                OrderId = null,
            };
            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.CommitAsync();

            // ارسال درخواست به زرین‌پال
            var merchantId = _config["Zarinpal:MerchantId"];
            var callbackUrl = $"{_config["App:BaseUrl"]}/api/payment/verify?orderId={payment.OrderId}";

            var client = _httpClientFactory.CreateClient();
            var res = await client.PostAsJsonAsync("https://api.zarinpal.com/pg/v4/payment/request.json", new
            {
                merchant_id = merchantId,
                amount = dto.Amount,
                description = $"پرداخت تست {dto.TestType}",
                callback_url = callbackUrl
            });

            var result = await res.Content.ReadFromJsonAsync<ZarinpalRequestResponse>();

            if (result?.Data?.Authority != null)
            {
                payment.TransactionId = result.Data.Authority;
                await _unitOfWork.CommitAsync();
                return Ok(new { paymentUrl = $"https://www.zarinpal.com/pg/StartPay/{result.Data.Authority}" });
            }

            return BadRequest(result?.Errors);
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(Guid orderId, string Authority, string Status)
        {
            var payment = await _unitOfWork.PaymentRepository.GetQueryable().FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null) return NotFound();

            if (Status != "OK")
            {
                payment.Status = PaymentStatus.Failed;
                await _unitOfWork.CommitAsync();
                return Redirect($"{_config["Frontend:BaseUrl"]}/payment-failed");
            }

            var merchantId = _config["Zarinpal:MerchantId"];
            var client = _httpClientFactory.CreateClient();
            var res = await client.PostAsJsonAsync("https://api.zarinpal.com/pg/v4/payment/verify.json", new
            {
                merchant_id = merchantId,
                amount = payment.Amount,
                authority = Authority
            });

            var result = await res.Content.ReadFromJsonAsync<ZarinpalVerifyResponse>();

            if (result?.Data?.Code == 100)
            {
                payment.Status = PaymentStatus.Completed;
                payment.DateModified = DateTime.UtcNow;
                await _unitOfWork.CommitAsync();
                return Redirect($"{_config["Frontend:BaseUrl"]}/test/{payment.TestType}");
            }

            payment.Status = PaymentStatus.Failed;
            await _unitOfWork.CommitAsync();
            return Redirect($"{_config["Frontend:BaseUrl"]}/payment-failed");
        }

        [HttpGet("has-access/{testType}")]
        [Authorize]
        public async Task<IActionResult> HasAccess(string testType)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var hasAccess = await _unitOfWork.PaymentRepository
                .ExistsAsync(p => p.UserId == userId && p.TestType == testType && p.Status == PaymentStatus.Completed);

            return Ok(new { hasAccess });
        }
    }

}
