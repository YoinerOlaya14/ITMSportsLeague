namespace SportsLeague.API.DTOs.Request
{
    public class CreateMatchLineupDto
    {
        public int PlayerID { get; set; }
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;
    }
}
