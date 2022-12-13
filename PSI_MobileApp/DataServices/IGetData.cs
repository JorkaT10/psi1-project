using ProfileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_MobileApp.DataServices
{
    public interface IGetData
    {
        Task<List<Profile>> GetAllProfiles();
    }
}
