using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace Web.Appliaction.Values
{
    using System;
    using Domain;
    using Domain.ValuesProvider;
    using Microsoft.Extensions.OptionsModel;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ValuesControllerConfiguration _configuration;
        private readonly IValuesProvider _valuesProvider;
        

        public ValuesController(IValuesProvider valuesProvider, IOptions<ValuesControllerConfiguration> optionsAccessor)
        {
            if(valuesProvider == null)
                throw new ArgumentException("valuesProvider");
            if (optionsAccessor?.Value == null)
                throw new ArgumentException("optionsAccessor");

            _valuesProvider = valuesProvider;
            _configuration = optionsAccessor.Value;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {_configuration.Value1, _configuration.Value2};
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id > 1)
                throw new ArgumentException("id");

            return _valuesProvider.GetValue(_configuration) + id;
        }
    }
}