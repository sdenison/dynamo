﻿using Microsoft.AspNetCore.Mvc;

namespace Dynamo.Ui.Blazor.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DataPortalController : Csla.Server.Hosts.HttpPortalController
  {
    public DataPortalController(Csla.ApplicationContext applicationContext)
      : base(applicationContext) { }

    [HttpGet]
    public string Get()
    {
      return "Running";
    }

    public override Task PostAsync([FromQuery] string operation)
    {
      var result = base.PostAsync(operation);
      return result;
    }
  }
}