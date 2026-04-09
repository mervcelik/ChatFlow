using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        Title = "Authorization Violation";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "https://Example.com/probs/business";
    }
}
