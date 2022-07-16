using FunTranslator2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTranslator2.Services
{
    public interface IFunTranslatorService 
    {
        ResponseDTO GetTranslationResult(string requestUri);
        Task<ResponseDTO> GetTranslationX(string requestUri);
    }
}
