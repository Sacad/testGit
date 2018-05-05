using AGLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGLTest.Services
{
    public interface IPetService
    {
        IList<Cat> GetCatsByOwnerGender();
    }
}