﻿using Csla.State;
using Csla;
using Microsoft.AspNetCore.Mvc;

namespace Dynamo.Ui.Blazor.Server.Controllers
{
    /// <summary>
    /// Gets and puts the current user session data
    /// from the Blazor wasm client components.
    /// </summary>
    /// <param name="applicationContext"></param>
    /// <param name="sessionManager"></param>
    [ApiController]
    [Route("[controller]")]
    public class CslaStateController(ApplicationContext applicationContext, ISessionManager sessionManager) :
        Csla.AspNetCore.Blazor.State.StateController(applicationContext, sessionManager)
    { }
}
