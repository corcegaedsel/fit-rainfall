using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.App.Interfaces;

public interface IHttpService
{
    Task<TRet> GetAsync<TRet>(string url) where TRet: class;
}
