using System.Net;

namespace Metrc.TaskManagement.Application.DTOs.Authentication;

public record TokenResponse(
    string AccessToken,
    long Expiration
);
