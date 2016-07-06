using ConsoleApplicationOWIN.IoC;
using ConsoleApplicationOWIN.Model;
using ConsoleApplicationOWIN.Providers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ValueProviders;

namespace ConsoleApplicationOWIN.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/v3/persons")]
    [Authorize]
    public class PersonsController : ApiController
    {
        private readonly IPersonsRepository _pq;
        //public PersonsController()
        //{

        //}
        /// <summary>
        /// 
        /// </summary>
        public PersonsController(IPersonsRepository pq)
        {
            this._pq = pq;
        }

        /// <summary>
        /// Retorna una llista de personas
        /// </summary>
        /// <returns>Person[]</returns>
        [Route("", Name = "ListPersons")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(Person[]))]
        public IHttpActionResult ListPersons([ValueProvider(typeof(HeaderValueProviderFactory))] string UserAgent,[FromUri]Filter  filter)
        {
            try
            {
                //!? throw new InvalidOperationException("desastre!");
                return Ok(_pq.GetAll());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}", Name = "PersonById")]
        [HttpGet]
        [ResponseType(typeof(Person))]
        public HttpResponseMessage PersonById(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        return this.Request.CreateResponse(new Person() { Id = 1, Name = "Person 1 Self OWIN" });
                    case 2:
                        return this.Request.CreateResponse(new Person() { Id = 2, Name = "Person 2 Self OWIN" });
                    default:
                        return this.Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Route("", Name = "AddPerson")]
        [HttpPost]
        public HttpResponseMessage AddPerson([FromBody]Person person)
        {
            try
            {
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        [Route("{personId}", Name = "UpdatePerson")]
        [HttpPut]
        public HttpResponseMessage UpdatePerson(int personId, [FromBody]Person person)
        {
            try
            {
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [Route("{personId}", Name = "DeletePerson")]
        [HttpDelete]
        public HttpResponseMessage DeletePerson(int personId)
        {
            try
            {
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }

    public class Filter
    {
        public int Id { get; set; }
        public string Search { get; set; }
    }
}
