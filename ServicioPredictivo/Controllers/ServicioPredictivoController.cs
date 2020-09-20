using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioPredictivo.Controllers
{
    public class ServicioPredictivoController : ApiController
    {
        // GET api/ServicioPredictivo
        public IEnumerable<Models.CargaTemporal> Get()
        {

            IEnumerable<Models.CargaTemporal> lst;

            using (Models.DBPREDICTIVOEntities db = new Models.DBPREDICTIVOEntities())
            {
                lst = db.CargaTemporal.ToList();
            }
            return lst;
        }
    }
}
