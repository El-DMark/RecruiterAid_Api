using System.Collections.Generic;

namespace RecruiterAid_Api.Presentation.DTOs
{
    public class ManagerTeamDto
    {
        public UserProfileDto Manager { get; set; } = null!;
        public IEnumerable<UserProfileDto> Agents { get; set; } = new List<UserProfileDto>();

    }
}
