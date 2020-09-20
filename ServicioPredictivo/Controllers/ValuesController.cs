using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioPredictivo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Models.CargaTemporal> Get()
        {

            IEnumerable<Models.CargaTemporal> lst;

            using (Models.DBPREDICTIVOEntities db=new Models.DBPREDICTIVOEntities())
            {
                lst = db.CargaTemporal.ToList();
            }
            return lst;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
