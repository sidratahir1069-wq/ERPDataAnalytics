using ERPDataAnalytics.Application.cs.DTO.Sale;
using ERPDataAnalytics.Application.cs.Model;
using ERPDataAnalytics.domain.cs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDataAnalytics.Application.cs.Interface
{
    public interface ISaleService
    {
        Task<ResponseDataModel<Sale>> CreateSaleAsync(SaleDTO dto);
        Task<ResponseDataModel<List<Sale>>> GetAllAsync();
        Task<ResponseDataModel<Sale>> GetByIdAsync(int id);
        Task<ResponseDataModel<bool>> DeleteAsync(int id);

        Task<ResponseDataModel<Sale>> UpdateSale(int id, Sale model);


    }

}
