using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.Controllers.ViewModelData
{
    public class BaseViewModel
    {
        public AuthenticationData AuthenticationData { get; set; }

        public AuthorizationData AuthorizationData { get; set; }

    }
}