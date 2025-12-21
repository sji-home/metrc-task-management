using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IDbService
{
    Task<T> GetAsync<T>(string command, object parms);
    Task<List<T>> GetList<T>(string command, object parms);
    Task<int> EditData(string command, object parms);
}