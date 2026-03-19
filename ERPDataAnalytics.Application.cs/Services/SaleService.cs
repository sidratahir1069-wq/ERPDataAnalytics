using ERPDataAnalytics.Application.cs.DTO.Sale;
using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.Application.cs.Model;
using ERPDataAnalytics.domain.cs.Entities;
using ERPDataAnalytics.domain.cs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDataAnalytics.Application.cs.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleInterface _repo;

        public SaleService(ISaleInterface repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDataModel<Sale>> CreateSaleAsync(SaleDTO dto)
        {
            try
            {
                var sale = new Sale
                {
                    CompanyId = dto.CompanyId,
                    BranchId = dto.BranchId,
                    InvoiceNumber = dto.InvoiceNumber,
                    CustomerId = dto.CustomerId,
                    SaleDate = dto.SaleDate,
                    DiscountAmount = dto.DiscountAmount,
                    PaidAmount = dto.PaidAmount,
                    SaleItems = new List<SaleItem>()
                };

                decimal totalAmount = 0;

                foreach (var item in dto.Items)
                {
                    var saleItem = new SaleItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Total = item.Quantity * item.UnitPrice
                    };

                    totalAmount += saleItem.Total;
                    sale.SaleItems.Add(saleItem);
                }

                sale.TotalAmount = totalAmount;
                sale.NetAmount = totalAmount - sale.DiscountAmount;

                var result = await _repo.CreateSale(sale);

                return ResponseDataModel<Sale>.SuccessResponse(result, "Sale Created Successfully");
            }
            catch (Exception ex)
            {
                return ResponseDataModel<Sale>.FailureResponse(ex.Message);
            }
        }

        public async Task<ResponseDataModel<List<Sale>>> GetAllAsync()
        {
            var data = await _repo.GetSaleList();
            return ResponseDataModel<List<Sale>>.SuccessResponse(data);
        }

        public async Task<ResponseDataModel<Sale>> GetByIdAsync(int id)
        {
            var data = await _repo.GetSaleById(id);

            if (data == null)
                return ResponseDataModel<Sale>.FailureResponse("Sale not found");

            return ResponseDataModel<Sale>.SuccessResponse(data);
        }

        public async Task<ResponseDataModel<bool>> DeleteAsync(int id)
        {
            var result = await _repo.DeleteSale(id);

            if (!result)
                return ResponseDataModel<bool>.FailureResponse("Sale not found");

            return ResponseDataModel<bool>.SuccessResponse(true, "Deleted Successfully");
        }

        public Task<ResponseDataModel<Sale>> UpdateSale(int id, Sale model)
        {
            throw new NotImplementedException();
        }
    }
}
