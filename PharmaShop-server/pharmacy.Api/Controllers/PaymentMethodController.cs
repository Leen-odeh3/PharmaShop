using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        public PaymentMethodController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod([FromBody] PaymentMethodRequestDto paymentMethodRequestDto)
        {
            if (paymentMethodRequestDto == null)
            {
                return _responseHandler.BadRequest("Invalid payment method data.");
            }

            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodRequestDto);
            var paymentMethodResponseDto = await _unitOfWork.PaymentMethodRepository.CreateAsync(paymentMethod);
            _unitOfWork.Complete();
            return _responseHandler.Created(paymentMethodResponseDto, "Payment method created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(int id, [FromBody] PaymentMethodRequestDto paymentMethodRequestDto)
        {
            if (paymentMethodRequestDto == null)
            {
                return _responseHandler.BadRequest("Invalid payment method data.");
            }

            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodRequestDto);
           // paymentMethod.PaymentMethodId = id;

            var updatedPaymentMethod = await _unitOfWork.PaymentMethodRepository.UpdateAsync(id, paymentMethod);
            _unitOfWork.Complete();

            if (updatedPaymentMethod == null)
            {
                return _responseHandler.NotFound("Payment method not found.");
            }

            return _responseHandler.Success(updatedPaymentMethod, "Payment method updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var result = await _unitOfWork.PaymentMethodRepository.DeleteAsync(id);
            _unitOfWork.Complete();

            return _responseHandler.Success(result, "Payment method deleted successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var paymentMethod = await _unitOfWork.PaymentMethodRepository.GetByID(id);
            if (paymentMethod == null)
            {
                return _responseHandler.NotFound("Payment method not found.");
            }

            return _responseHandler.Success(paymentMethod, "Payment method retrieved successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            var paymentMethods = await _unitOfWork.PaymentMethodRepository.GetAllAsync();
            return _responseHandler.Success(paymentMethods, "Payment methods retrieved successfully.");
        }
    }
}